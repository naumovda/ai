using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace RCEIS
{
	public enum TableTypes
	{
		ParentTable = 1,
        ChildTable	= 2
	};

	/// <summary>
	/// </summary>
	[Serializable]
	public class Table
	{
		private long id;
		private long id_form;
		private string name;
		private string comment;
		private string id_object_name;
		private string id_form_name;
		private TableTypes type;
        
		public ColumnCollection columnCollection;

		public Table()
		{
			columnCollection = new ColumnCollection();
		}

		public long ID
		{
			get {return id;}
			set {id = value;}
		}

		public long ID_Form
		{
			get {return id_form;}
			set {id_form = value;}
		}

		public string Name
		{
			get {return name;}
			set {name = value;}
		}

		public string Comment
		{
			get {return comment;}
			set {comment = value;}
		}

		public string ID_ObjectName
		{
			get {return id_object_name;}
			set {id_object_name = value;}
		}

		public string ID_FormName
		{
			get {return id_form_name;}
			set {id_form_name = value;}
		}

		public TableTypes Type
		{
			get {return type;}
			set {type = value;}
		}
		
		public void LoadColumnCollection(SqlConnection conn, DomainCollection dc)
		{
			columnCollection.Load(conn, ID, dc, this);
		}

		public override string ToString()
		{
			return Comment + "(" + Name + ")";
		}
	}

	/// <summary>
	/// A collection of Cluster objects or Clusters
	/// </summary>
	[Serializable]
	public class TableCollection  : System.Collections.CollectionBase
	{
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="region"></param>
		public virtual void Add (Table table)
		{
			this.List.Add(table);
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual Table this[int Index] 
		{ 
			get 
			{ 
				return (Table)this.List[Index];            
			}        
		} 

		public void Load(SqlConnection conn, long id_form, DomainCollection dc)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getTableList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@id_form", SqlDbType.BigInt);

			cmd.Parameters["@id_form"].Value = id_form;
			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				Table table = new Table();
				
				table.ID			= dr.GetInt64(0);
				table.ID_Form		= dr.GetInt64(1);
				table.Name			= dr.GetString(2);
				table.Comment		= dr.GetString(3);
				table.ID_ObjectName = dr.GetString(4);
				table.ID_FormName	= dr.GetString(5);

				if (dr.GetByte(6)==1)
					table.Type = TableTypes.ParentTable;
				else
					table.Type = TableTypes.ChildTable;
				
				Add( table );
			}
			dr.Close();

			foreach(Table table in this)
			{
				table.LoadColumnCollection(conn, dc);
			}
		}

		/*public void Insert(SqlConnection conn, Region region)
		{
			SqlCommand cmd = new SqlCommand("sp_addRegion", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			cmd.Parameters.Add("@name", SqlDbType.VarChar);
			cmd.Parameters.Add("@okato", SqlDbType.VarChar);

			cmd.Parameters["@name"].Value = region.Name;
			cmd.Parameters["@okato"].Value = region.OKATO;
			
			try
			{
				cmd.ExecuteNonQuery();				
			}
			catch(System.Data.SqlClient.SqlException ex)
			{	
				MessageBox.Show(ex.Message);
			}
			catch(System.Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void Update(SqlConnection conn, Region region)
		{
			SqlCommand cmd = new SqlCommand("sp_updateRegion", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			cmd.Parameters.Add("@name", SqlDbType.VarChar);
			cmd.Parameters.Add("@okato", SqlDbType.VarChar);

			cmd.Parameters["@name"].Value = region.Name;
			cmd.Parameters["@okato"].Value = region.OKATO;
			
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch(System.Data.SqlClient.SqlException ex)
			{	
				MessageBox.Show(ex.Message);
			}
			catch(System.Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		public void Delete(SqlConnection conn, Region region)
		{
			SqlCommand cmd = new SqlCommand("sp_deleteRegion", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			cmd.Parameters.Add("@okato", SqlDbType.VarChar);

			cmd.Parameters["@okato"].Value = region.OKATO;
			
			try
			{
				cmd.ExecuteNonQuery();
			}
			catch(System.Data.SqlClient.SqlException ex)
			{	
				MessageBox.Show(ex.Message);
			}
			catch(System.Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}*/
	}
	
}
