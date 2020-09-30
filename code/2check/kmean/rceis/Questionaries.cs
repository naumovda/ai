using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace RCEIS
{
	/// <summary>
	/// </summary>
	[Serializable]
	public class Questionarie
	{
		private long id;		
		private string name;
		private string comment;

		public long MinID;
		public long MaxID;
		public long CountID;

		public TableCollection tableCollection;
		public EditRuleCollection editRuleCollection;

		public Questionarie()
		{
			tableCollection = new TableCollection();
			editRuleCollection = new EditRuleCollection();
		}

		public long ID
		{
			get {return id;}
			set {id = value;}
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

		public override string ToString()
		{
			return Name + " : " + Comment;
		}

		public void LoadTableCollection(SqlConnection conn, DomainCollection dc)
		{
			tableCollection.Load(conn, ID, dc);
		}

		public void LoadEditRuleCollection(SqlConnection conn)
		{
			editRuleCollection.Load(conn, ID, GetColumnCollection());
		}

		public ColumnCollection GetColumnCollection()
		{
			ColumnCollection cc = new ColumnCollection();

			foreach(Table table in tableCollection)
			{
				foreach(Column column in table.columnCollection)
				{
                    cc.Add(column);
				}
			}

			int n = cc.Count;

			Column [] ca = new Column[n]; 

			foreach(Column column in cc)
			{
				ca[column.Number-1] = column;
			}

			cc.Clear();
			for(int i=0; i<n; i++)
				cc.Add(ca[i]);

			return cc;
		}

		public ColumnCollection GetColumnCollection(DomainType dt)
		{
			ColumnCollection cc = new ColumnCollection();

			foreach(Table table in tableCollection)
			{
				foreach(Column column in table.columnCollection)
				{
					if (column.Domain.DomainType == dt)
						cc.Add(column);
				}
			}

			return cc;
		}
	}

	/// <summary>
	/// A collection of Cluster objects or Clusters
	/// </summary>
	[Serializable]
	public class QuestionarieCollection  : System.Collections.CollectionBase
	{
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="region"></param>
		public virtual void Add (Questionarie qst)
		{
			this.List.Add(qst);
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual Questionarie this[int Index] 
		{ 
			get 
			{ 
				return (Questionarie)this.List[Index];            
			}        
		} 

		public Questionarie FindByID(long id)
		{
			foreach(Questionarie qst in this)
			{
				if (qst.ID == id)
					return qst;
			}			
			return null;
		}

		public void Load(SqlConnection conn, DomainCollection dc)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getFormList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				Questionarie qst = new Questionarie();
				
				qst.ID	= dr.GetInt64(0);
				qst.Name	= dr.GetString(1);
				qst.Comment= dr.GetString(2);

				Add( qst );
			}
			dr.Close();

			foreach(Questionarie qst in this)
			{
				qst.LoadTableCollection(conn, dc);
				qst.LoadEditRuleCollection(conn);
			}

			foreach(Questionarie qst in this)
			{
				if (qst.tableCollection.Count == 0)
					continue;

				cmd = new SqlCommand("sp_getMinMaxId", conn);
			
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add("@name", SqlDbType.VarChar, 255);

				cmd.Parameters["@name"].Value = qst.tableCollection[0].Name;
			
				dr = cmd.ExecuteReader();
			
				while( dr.Read() )
				{					
					qst.MinID	= (long)dr.GetInt32(0);
					qst.MaxID	= (long)dr.GetInt32(1);
					qst.CountID = (long)dr.GetInt32(2);
				}
				dr.Close();
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
