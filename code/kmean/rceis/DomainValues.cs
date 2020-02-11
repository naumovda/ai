using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace RCEIS
{
	[Serializable]
	public class DomainValue
	{
		private long id;
		private long id_domain;
		private long domain_value;
		private string name;
        
		public long ID
		{
			get {return id;}
			set {id = value;}
		}

		public long ID_Domain
		{
			get {return id_domain;}
			set {id_domain = value;}
		}

		public string Name
		{
			get {return name;}
			set {name = value;}
		}

		public long	Value
		{
			get {return domain_value;}
			set {domain_value = value;}
		}
		
		public override string ToString()
		{
			return "(" + Value.ToString() + ")" + Name ;
		}
	}

	[Serializable]
	public class DomainValueCollection  : System.Collections.CollectionBase
	{
		public virtual void Add (DomainValue domain)
		{
			this.List.Add(domain);
		}
		
		public virtual DomainValue this[int Index] 
		{ 
			get 
			{ 
				return (DomainValue)this.List[Index];            
			}        
		} 

		public DomainValue FindByID(long id)
		{
			foreach(DomainValue dv in this)
			{
				if (dv.ID == id)
					return dv;
			}			
			return null;
		}

		public DomainValue FindByValue(long val)
		{
			foreach(DomainValue dv in this)
			{
				if (dv.Value == val)
					return dv;
			}			
			return null;
		}

		public int FindIndexByValue(long val)
		{
			int i=0;
			foreach(DomainValue dv in this)
			{
				if (dv.Value == val)
					return i;
				i++;
			}			
			return 0;
		}

		public void Load(SqlConnection conn, long id_domain)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getDomainValueList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@id_domain", SqlDbType.BigInt);

			cmd.Parameters["@id_domain"].Value = id_domain;
			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				DomainValue domain_value = new DomainValue();
				
				domain_value.ID			= dr.GetInt64(0);
				domain_value.ID_Domain	= dr.GetInt64(1);
				domain_value.Value		= dr.GetInt64(2);
				domain_value.Name			= dr.GetString(3);
	
				Add( domain_value );
			}
			dr.Close();

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
