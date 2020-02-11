using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace RCEIS
{
	public enum RuleTypes
	{
		QuantativeStrict = 1,
		QuantativeUnstrict = 2,
		Qualitative = 3
	};

	[Serializable]
	public class EditRule
	{
		private long id;		
		private long number;
		private RuleTypes type;
        
		public EditRuleParamCollection editRuleParamCollection;

		public EditRule()
		{
			editRuleParamCollection = new EditRuleParamCollection();
		}

		public long ID
		{
			get {return id;}
			set {id = value;}
		}

		public long Number
		{
			get {return number;}
			set {number = value;}
		}

		public RuleTypes RuleType
		{
			get	{return type;}
			set {type = value;}
		}

		public override string ToString()
		{
			return Number.ToString();
		}

		public string ToDescriptionString(Questionarie qst)
		{
			string s = "";

			if (RuleType == RuleTypes.Qualitative)
			{
				Edits.QLT_EditRule qlt_er = GetQLT(qst);

                bool [][] v = qlt_er.Values;

				int c_count = v.GetUpperBound(0)+1;
				
				ColumnCollection cc = qst.GetColumnCollection(DomainType.QualitativeDomain);

				for(int i=0; i<c_count; i++)
				{
					Column column = cc[i];

					int p_count = v[i].GetUpperBound(0)+1;

					s += "(";

					bool first = true;

					for(int j=0; j<p_count; j++)
					{
						if (v[i][j])
						{
							if (!first)
								s += " ИЛИ ";
							s += " {" + column.ShortName + "}= '" + column.Domain.valueCollection[j].Name+ "' "; 
							/*s += " {" + column.UniqueCode.ToString() + "}= '" + column.Domain.valueCollection[j].Name+ "' "; */

							first = false;
						}						
					}

					s += ")";

					if (i + 1 != c_count)
						s += " И ";
				}
			}
			else
			{
				EditRuleParam erp;
				for(int i=0; i<editRuleParamCollection.Count; i++)
				{
					erp = editRuleParamCollection[i];

					s += ((erp.Coefficient<0)?"(":"") + erp.Coefficient.ToString() + ((erp.Coefficient<0)?")*":"*")  + "{"+ erp.Column.UniqueCode.ToString() + "}";

					if (i + 1 != editRuleParamCollection.Count)
						s += " + ";
				}
				
				if (RuleType == RuleTypes.QuantativeStrict)
					s += " > 0";
				else
					s += " >= 0";				
			}

			return "Правило №" + Number.ToString() + ": " + s;
		}

		public Edits.QLT_EditRule GetQLT(Questionarie qst)
		{
			if (type != RuleTypes.Qualitative) return null;

			Edits.QLT_EditRule qlt_er = new Edits.QLT_EditRule();
			
			int count = qst.GetColumnCollection(DomainType.QualitativeDomain).Count;
			
			int [] size = new int[count];

			for(int i=0; i<count; i++)
			{			
				size[i] = editRuleParamCollection[i].Column.Domain.valueCollection.Count;				
			}

			qlt_er.Initialize(size);

			for(int i=0; i<editRuleParamCollection.Count; i++)
			{			
				EditRuleParam erp = editRuleParamCollection[i];				
				
				int idx_v = erp.Column.Domain.valueCollection.FindIndexByValue(erp.DomainValue.Value);
				int idx_c = (int)qst.GetColumnCollection(DomainType.QualitativeDomain).GetIndexByID(erp.Column.ID);

				qlt_er.SetCell(idx_c,idx_v,true);
			}

			return qlt_er;
		}

		public Edits.QNT_EditRule GetQNT(Questionarie qst)
		{
			if (type == RuleTypes.Qualitative) return null;

			Edits.QNT_EditRule qnt_er = new Edits.QNT_EditRule();
			
			int count= qst.GetColumnCollection().GetCount(DomainType.QuantitiveDomain);
			
			qnt_er.Initialize(count);
			
			if (type == RuleTypes.QuantativeUnstrict)
				qnt_er.Flag = 0;
			else
				qnt_er.Flag = 1;

			foreach(EditRuleParam erp in editRuleParamCollection)
			{	
				double v = erp.Coefficient;
				int idx_c = (int)qst.GetColumnCollection(DomainType.QuantitiveDomain).GetIndexByID(erp.Column.ID);

				qnt_er.SetCell(idx_c, v);
			}

			return qnt_er;
		}

		public void LoadParamCollection(SqlConnection conn, ColumnCollection cc)
		{
			editRuleParamCollection.Load(conn, ID, cc);
		}

		public bool IncludeExplicitly(Column column)
		{
			return editRuleParamCollection.IncludeExplicitly(column);;
		}

		public bool IncludeExplicitly(Column column, DomainValue dv)
		{
			return editRuleParamCollection.IncludeExplicitly(column, dv);
		}

		public double GetCoeeficient(Column column, DomainValue dv)
		{
			return editRuleParamCollection.GetCoeeficient(column, dv);
		}
	}

	[Serializable]
	public class EditRuleCollection  : System.Collections.CollectionBase
	{
		public virtual void Add (EditRule er)
		{
			this.List.Add(er);
		}
		
		public virtual EditRule this[int Index] 
		{ 
			get 
			{ 
				return (EditRule)this.List[Index];            
			}        
		} 

		public EditRule FindByID(long id)
		{
			foreach(EditRule er in this)
			{
				if (er.ID == id)
					return er;
			}			
			return null;
		}

		public long GetCountByType(RuleTypes rt)
		{
			long i = 0;

			foreach(EditRule er in this)
			{
				if (er.RuleType == rt)
					i++;
			}			
			
			return i;
		}

		public long GetCountQNT()
		{
			return GetCountByType(RuleTypes.QuantativeStrict) +
				   GetCountByType(RuleTypes.QuantativeUnstrict);

		}
		
		public long GetCountQLT()
		{
			return GetCountByType(RuleTypes.Qualitative);
		}

		public Edits.QLT_EditRulesCollection GetQLT_EditRuleCollection(Questionarie qst)
		{
			Edits.QLT_EditRulesCollection qlt_erc = new Edits.QLT_EditRulesCollection();

			foreach(EditRule er in this)
			{
				if (er.RuleType == RuleTypes.Qualitative)
					qlt_erc.Add(er.GetQLT(qst));
			}

			return qlt_erc;
		}
		
		public Edits.QNT_EditRulesCollection GetQNT_EditRuleCollection(Questionarie qst)
		{
			Edits.QNT_EditRulesCollection qlt_erc = new Edits.QNT_EditRulesCollection();

			foreach(EditRule er in this)
			{
				if (er.RuleType != RuleTypes.Qualitative)
					qlt_erc.Add(er.GetQNT(qst));
			}

			return qlt_erc;
		}

		public void Load(SqlConnection conn, long id_form, ColumnCollection cc)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getEditRuleList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			cmd.Parameters.Add("@id_form", SqlDbType.BigInt);

			cmd.Parameters["@id_form"].Value = id_form;

			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				EditRule er = new EditRule();
				
				er.ID		= dr.GetInt64(0);
				er.Number	= dr.GetInt64(2);

				switch (dr.GetInt64(3))
				{
					case 1:
						er.RuleType = RuleTypes.Qualitative; break;
					case 2:
						er.RuleType = RuleTypes.QuantativeUnstrict; break;
					case 3:
						er.RuleType = RuleTypes.QuantativeStrict; break;
				}
				Add( er );
			}
			dr.Close();

			foreach(EditRule er in this)
			{
				er.LoadParamCollection(conn, cc);
			}
		}
		
	}
	
}
