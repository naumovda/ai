using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace RCEIS
{
	[Serializable]
	public class EditRuleParam
	{
		private long id;
		private long id_editrule;
		private long id_column;
		private long id_domainvalue;
		private double coefficient;
        
		public Column Column;
		public DomainValue DomainValue;

		public long ID
		{
			get {return id;}
			set {id = value;}
		}

		public long ID_EditRule
		{
			get {return id_editrule;}
			set {id_editrule = value;}
		}

		public long ID_Column
		{
			get {return id_column;}
			set {id_column = value;}
		}

		public long ID_DomainValue
		{
			get {return id_domainvalue;}
			set {id_domainvalue = value;}
		}

		public double Coefficient
		{
			get {return coefficient;}
			set {coefficient = value;}
		}

		public override string ToString()
		{
			return Coefficient.ToString();
		}
	}

	[Serializable]
	public class EditRuleParamCollection  : System.Collections.CollectionBase
	{
		
		public virtual void Add (EditRuleParam erp)
		{
			this.List.Add(erp);
		}
		
		public virtual EditRuleParam this[int Index] 
		{ 
			get 
			{ 
				return (EditRuleParam)this.List[Index];            
			}        
		} 

		public bool IncludeExplicitly(Column column, DomainValue dv)
		{
			foreach(EditRuleParam erp in this)
			{
				if ((erp.ID_DomainValue == dv.ID)&&(erp.Column.ID == column.ID))
				{
					if (erp.Coefficient != 0)
					{
						return true;
					}
					else
					{
						return false;
					}
				}
			}

			return false;
		}

		public bool IncludeExplicitly(Column column)
		{
			if (column.Domain.DomainType == DomainType.QuantitiveDomain)
			{			
				foreach(EditRuleParam erp in this)
				{
					if (erp.Column == column)
					{
						if (erp.Coefficient != 0)
						{
							return true;
						}
					}
				}

				return false;
			}
			else
			{		
				//≈сли DomainValue = null, то перед нами €вно численное правило,
				//а в них качественный показатель не входит
				int i = 0;

				foreach(EditRuleParam erp in this)
				{
					if (erp.DomainValue == null)
						return false;
					if (erp.Column == column)
					{
						if (erp.Coefficient != 0)
						{
							i++;
						}
					}
				}

				return (i != column.Domain.valueCollection.Count);
			}
		}

		public double GetCoeeficient(Column column, DomainValue dv)
		{
			if (column.Domain.DomainType == DomainType.QuantitiveDomain)
			{
				foreach(EditRuleParam erp in this)
				{
					if (erp.Column.ID == column.ID)
						return erp.Coefficient;
				}
			}
			else
			{			
				foreach(EditRuleParam erp in this)
				{
					if ((erp.ID_DomainValue == dv.ID)&&(erp.Column.ID == column.ID))
						return erp.Coefficient;
				}
			}

			return 0.0;
		}

		public void Load(SqlConnection conn, long id_editrule, ColumnCollection cc)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getEditRuleParamList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@id_editrule", SqlDbType.BigInt);

			cmd.Parameters["@id_editrule"].Value = id_editrule;
			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				EditRuleParam erp = new EditRuleParam();
				
				erp.ID				= dr.GetInt64(0);
				erp.ID_EditRule		= dr.GetInt64(1);
				erp.ID_Column		= dr.GetInt64(2);
				erp.ID_DomainValue	= dr.GetInt64(3);
				erp.Coefficient		= dr.GetDouble(4);

				erp.Column			= cc.FindByID(erp.ID_Column);
				erp.DomainValue		= erp.Column.Domain.valueCollection.FindByID(erp.ID_DomainValue);
		
				Add( erp );
			}
			dr.Close();

		}

	}
	
}
