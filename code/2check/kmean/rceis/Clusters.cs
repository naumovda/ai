using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RCEIS
{
	[Serializable]
	public class Cluster  : System.Collections.CollectionBase
	{
		private long id_cluster;		
		private long id_region;		
		private long id_form;		
		private long number;
		private string name;
		private long record_count;
		private long donor_count;

		public Questionarie	Questionarie;

		public ClusterParamCollection clusterParamCollection;

		public Cluster()
		{
			clusterParamCollection = new ClusterParamCollection();
		}

		public long ID
		{
			get {return id_cluster;}
			set {id_cluster = value;}
		}

		public long ID_Region
		{
			get {return id_region;}
			set {id_region = value;}
		}

		public long ID_Form
		{
			get {return id_form;}
			set {id_form = value;}
		}
		
		public long Number
		{
			get {return number;}
			set {number = value;}
		}

		public string Name
		{
			get {return name;}
			set {name = value;}
		}

		public long RecordCount
		{
			get {return record_count;}
			set {record_count = value;}
		}
		
		public long DonorCount
		{
			get {return donor_count;}
			set {donor_count = value;}
		}

		public override string ToString()
		{
			return "Кластер №" + Number.ToString("D2") + " (" + Name+")";
		}

		public void LoadParamCollection(SqlConnection conn)
		{
			clusterParamCollection.Load(conn, ID, this.Questionarie);
		}

	}

	[Serializable]
	public class ClusterCollection  : System.Collections.CollectionBase
	{
		public virtual void Add (Cluster cluster)
		{
			this.List.Add(cluster);
		}
		
		public virtual Cluster this[int Index] 
		{ 
			get 
			{ 
				return (Cluster)this.List[Index];            
			}        
		} 

		public void Load(SqlConnection conn, long id_region, long id_form, Questionarie questionarie)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getClusterList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@id_region", SqlDbType.BigInt);
			cmd.Parameters.Add("@id_form", SqlDbType.BigInt);

			cmd.Parameters["@id_form"].Value = id_form;
			cmd.Parameters["@id_region"].Value = id_region;

			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				Cluster cluster = new Cluster();
				
				cluster.ID			= dr.GetInt64(0);
				cluster.ID_Region	= dr.GetInt64(1);
				cluster.ID_Form		= dr.GetInt64(2);
				cluster.Number		= dr.GetInt64(3);
				cluster.Name		= dr.GetString(4);
				cluster.RecordCount = dr.GetInt64(5); 
				cluster.DonorCount = dr.GetInt64(6); 

				cluster.Questionarie=questionarie;

				Add( cluster );
			}
			
			dr.Close();

			foreach(Cluster cluster in this)
			{
                cluster.LoadParamCollection(conn);
			}

		}

	}	
}
