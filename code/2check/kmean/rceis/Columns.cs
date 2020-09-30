using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace RCEIS
{
	[Serializable]
	public class Column
	{
		private long id;
		private long id_table;
		private string name;
		private long number;
		private long code;
		private string title;
		private bool process;
		private string shortname;
		private long uniquecode;
		private double weight;

		private Domain domain;

		public Table table;
       
		public ImputeRuleCollection imputeRuleCollection;

		public Column()
		{
			imputeRuleCollection = new ImputeRuleCollection();
		}

		public long ID
		{
			get {return id;}
			set {id = value;}
		}

		public long ID_Table
		{
			get {return id_table;}
			set {id_table = value;}
		}

		public string Name
		{
			get {return name;}
			set {name = value;}
		}

		public long Number
		{
			get {return number;}
			set {number = value;}
		}

		public long Code
		{
			get {return code;}
			set {code = value;}
		}

		public string Title
		{
			get {return title.Trim();}
			set {title = value;}
		}

		public bool Process
		{
			get {return process;}
			set {process = value;}
		}

		public string ShortName
		{
			get {return shortname.Trim();}
			set {shortname = value;}
		}

		public long UniqueCode
		{
			get {return uniquecode;}
			set {uniquecode = value;}
		}

		public Domain Domain
		{
			get {return domain;}
			set {domain = value;}
		}

		public double Weight
		{
			get {return weight;}
			set {weight = value;}
		}

		public override string ToString()
		{
			if (UniqueCode == 0)
				return ShortName;
            else
				return "(" + UniqueCode.ToString() + ") " + ShortName;
		}

		public void LoadImputeRuleCollection(SqlConnection conn)
		{
			imputeRuleCollection.Load(conn, ID, this);
		}

		public void GenerateImputeRules(SqlConnection conn)
		{
			if (domain.DomainType == DomainType.QualitativeDomain)
			{
				imputeRuleCollection.DeleteAll(conn);

				ImputeRule qlt_ir = new ImputeRule();

				qlt_ir.Column		= this;
				qlt_ir.ID_Column	= this.ID;
				qlt_ir.Number		= 1;
				qlt_ir.ImputeType   = ImputeTypes.Logical;

				imputeRuleCollection.Insert(conn, qlt_ir);
			}
			else
			{
				foreach(ImputeRule ir in imputeRuleCollection)
				{
					if (ir.ImputeType != ImputeTypes.Statistical)
					{
						imputeRuleCollection.Delete(conn, ir);
					}
				}
				LoadImputeRuleCollection(conn);

				ImputeRule qlt_ir = new ImputeRule();

				qlt_ir.Column		= this;
				qlt_ir.ID_Column	= this.ID;
				qlt_ir.Number		= 1;
				qlt_ir.ImputeType   = ImputeTypes.Balance;

				imputeRuleCollection.Insert(conn, qlt_ir);

				qlt_ir = new ImputeRule();

				qlt_ir.Column		= this;
				qlt_ir.ID_Column	= this.ID;
				qlt_ir.Number		= 2;
				qlt_ir.ImputeType   = ImputeTypes.ZeroValue;

				imputeRuleCollection.Insert(conn, qlt_ir);

				qlt_ir = new ImputeRule();

				qlt_ir.Column		= this;
				qlt_ir.ID_Column	= this.ID;
				qlt_ir.Number		= 3;
				qlt_ir.ImputeType   = ImputeTypes.HotDeck;

				imputeRuleCollection.Insert(conn, qlt_ir);
			}
		}

	}

	/// <summary>
	/// A collection of Cluster objects or Clusters
	/// </summary>
	[Serializable]
	public class ColumnCollection  : System.Collections.CollectionBase
	{
		public virtual void Add (Column column)
		{
			this.List.Add(column);
		}
		
		public virtual Column this[int Index] 
		{ 
			get 
			{ 
				return (Column)this.List[Index];            
			}        
		} 

		public Column FindByID(long id)
		{
			foreach(Column column in this)
			{
				if (column.ID == id)
					return column;
			}			
			
			return null;
		}

		public Column FindByNumber(long number)
		{
			foreach(Column column in this)
			{
				if (column.Number == number)
					return column;
			}			
			
			return null;
		}

		public long GetIndexByID(long id)
		{
			long i = 0;
			
			foreach(Column column in this)
			{
				if (column.ID == id)
					return i;
				i ++;
			}			
			
			return -1;
		}

		public int GetIndexByNumber(long number)
		{
			int i = 0;
			
			foreach(Column column in this)
			{
				if (column.Number == number)
					return i;
				i ++;
			}			
			
			return -1;
		}

		public int GetCount(DomainType dt)
		{
			int i = 0;
			
			foreach(Column column in this)
			{
				if (column.Domain.DomainType == dt)
					i ++;
			}			
			
			return i;
		}

		public ColumnCollection GetColumnCollection(DomainType dt)
		{
			ColumnCollection cc = new ColumnCollection();

			foreach(Column column in this)
			{
				if (column.Domain.DomainType == dt)
					cc.Add(column);
			}	

			return cc;
		}

		public void Load(SqlConnection conn, long id_table, DomainCollection dc, Table table)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getColumnList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@id_table", SqlDbType.BigInt);

			cmd.Parameters["@id_table"].Value = id_table;
			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				Column column = new Column();
				
				column.ID			= dr.GetInt64(0);
				column.ID_Table		= dr.GetInt64(1);
				column.Number		= dr.GetInt64(2);
				column.Name			= dr.GetString(3);
				column.Code			= dr.GetInt64(4);
				column.Title		= dr.GetString(5);
				column.Process		= dr.GetBoolean(6);
				column.ShortName	= dr.GetString(7);
				column.UniqueCode	= dr.GetInt64(8);
				column.Weight		= dr.GetDouble(10);

				column.table		= table;

				if(!dr.IsDBNull(9))
				{
					long id_domain		= dr.GetInt64(9);
					column.Domain		= dc.FindByID(id_domain);
				}

				Add( column );
			}

			dr.Close();

			foreach(Column column in this)
			{
				column.LoadImputeRuleCollection(conn);
			}
		}

		public void Load(SqlConnection conn, long id_region, long id_form, Questionarie questionarie)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getColumnSplittingList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@id_region", SqlDbType.BigInt);
			cmd.Parameters.Add("@id_form", SqlDbType.BigInt);

			cmd.Parameters["@id_region"].Value = id_region;
			cmd.Parameters["@id_form"].Value = id_form;
			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				Column column = new Column();
				
				column.ID			= dr.GetInt64(0);
				column.ID_Table		= dr.GetInt64(1);
				column.Number		= dr.GetInt64(2);
				column.Name			= dr.GetString(3);
				column.Code			= dr.GetInt64(4);
				column.Title		= dr.GetString(5);
				column.Process		= dr.GetBoolean(6);
				column.ShortName	= dr.GetString(7);
				column.UniqueCode	= dr.GetInt64(8);

				Add( column );
			}
			
			dr.Close();

			foreach(Column column in this)
			{
				column.LoadImputeRuleCollection(conn);
			}
		}
	}
	
}
