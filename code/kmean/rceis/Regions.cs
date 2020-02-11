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
	public class Region
	{
		private long id;		
		private string name;
		private string okato;

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

		public string OKATO
		{
			get {return okato;}
			set {okato = value;}
		}

		public override string ToString()
		{
			return okato + " : " + name;
		}
	}

	/// <summary>
	/// A collection of Cluster objects or Clusters
	/// </summary>
	[Serializable]
	public class RegionCollection  : System.Collections.CollectionBase
	{
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="region"></param>
		public virtual void Add (Region region)
		{
			this.List.Add(region);
		}
		
		/// <summary>
		/// 
		/// </summary>
		public virtual Region this[int Index] 
		{ 
			get 
			{ 
				return (Region)this.List[Index];            
			}        
		} 

		public Region FindByID(long id)
		{
			foreach(Region region in this)
			{
				if (region.ID == id)
					return region;
			}			
			
			return null;
		}

		public Region FindByOKATO(string OKATO)
		{
			foreach(Region region in this)
			{
				if (region.OKATO == OKATO)
					return region;
			}			
			
			return null;
		}

		public void Load(SqlConnection conn)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getRegionList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				Region region = new Region();
				
				region.ID	= dr.GetInt64(0);
				region.Name	= dr.GetString(1);
				region.OKATO= dr.GetString(2);

				Add( region );
			}
			dr.Close();
		}

		public void Insert(SqlConnection conn, Region region)
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
		}
	}
	
}
