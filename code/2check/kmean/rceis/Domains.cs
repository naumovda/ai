using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace RCEIS
{
	public enum DomainType 
	{
		QuantitiveDomain = 1,
		QualitativeDomain = 2
	};

	[Serializable]
	public class Domain
	{
		private long id;		
		private string comment;
		private DomainType type;		
		private double minvalue;
		private double maxvalue;

		public DomainValueCollection valueCollection;

		public Domain()
		{
			valueCollection = new DomainValueCollection();
		}

		public long ID
		{
			get {return id;}
			set {id = value;}
		}

		public string Comment
		{
			get {return comment;}
			set {comment = value;}
		}

		public DomainType DomainType
		{
			get {return type;}
			set {type = value;}
		}

		public double MinValue
		{
			get {return minvalue;}
			set {minvalue = value;}
		}

		public double MaxValue
		{
			get {return maxvalue;}
			set {maxvalue = value;}
		}

		public override string ToString()
		{
			return Comment;
		}

		public void LoadValueCollection(SqlConnection conn)
		{
			valueCollection.Load(conn, ID);
		}

		public DomainValue FindByValue(long val)
		{
			return valueCollection.FindByValue(val);
		}
	}

	[Serializable]
	public class DomainCollection  : System.Collections.CollectionBase
	{

		public virtual void Add (Domain domain)
		{
			this.List.Add(domain);
		}
		
		public virtual Domain this[int Index] 
		{ 
			get 
			{ 
				return (Domain)this.List[Index];            
			}        
		} 

		public Domain FindByID(long id)
		{
			foreach(Domain domain in this)
			{
				if (domain.ID == id)
					return domain;
			}			
			return null;
		}

		public void Load(SqlConnection conn)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getDomainList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				Domain domain = new Domain();
				
				domain.ID			= dr.GetInt64(0);
				domain.Comment		= dr.GetString(1);
				
				if (dr.GetInt64(2)==1)
				{
					domain.DomainType	= DomainType.QuantitiveDomain;
				}
				else
				{
					domain.DomainType	= DomainType.QualitativeDomain;
				}
				
				if (domain.DomainType == DomainType.QuantitiveDomain)
				{				
					domain.MinValue		= dr.GetDouble(3);
					domain.MaxValue		= dr.GetDouble(4);
				}
				Add( domain );
			}
			dr.Close();

			foreach(Domain domain in this)
			{
				domain.LoadValueCollection(conn);
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
