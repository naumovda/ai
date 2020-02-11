using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace RCEIS
{
	[Serializable]
	public class ClusterParam
	{
		private long id_cluster;		
		private long id_column;	

		private double min_value;
		private double max_value;
		private double mean;
		private double stddev;

		public Column  column;

		public long ID_Cluster
		{
			get {return id_cluster;}
			set {id_cluster = value;}
		}

		public long ID_Column
		{
			get {return id_column;}
			set {id_column = value;}
		}

		public double MinValue
		{
			get {return min_value;}
			set {min_value = value;}
		}

		public double MaxValue
		{
			get {return max_value;}
			set {max_value = value;}
		}

		public double Mean
		{
			get {return mean;}
			set {mean = value;}
		}

		public double StdDev
		{
			get {return stddev;}
			set {stddev = value;}
		}

		public Column Column
		{
			get {return column;}
			set {column = value;}
		}

	}

	[Serializable]
	public class ClusterParamCollection  : System.Collections.CollectionBase
	{
		public virtual void Add (ClusterParam cp)
		{
			this.List.Add(cp);
		}

		public virtual ClusterParam this[int Index] 
		{ 
			get 
			{ 
				return (ClusterParam)this.List[Index];            
			}        
		} 


		public void Load(SqlConnection conn, long id_cluster, Questionarie questionarie)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getClusterParamList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@id_cluster", SqlDbType.BigInt);

			cmd.Parameters["@id_cluster"].Value = id_cluster;
			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				ClusterParam cp = new ClusterParam();
				
				cp.ID_Cluster	= dr.GetInt64(0);
				cp.ID_Column	= dr.GetInt64(1);
				cp.Mean			= dr.GetDouble(2);
				cp.StdDev		= dr.GetDouble(3);
				cp.MinValue		= dr.GetDouble(4);
				cp.MaxValue		= dr.GetDouble(5);

				ColumnCollection cc = questionarie.GetColumnCollection();
				cp.Column		= cc.FindByID(cp.ID_Column);

				Add( cp );
			}
			dr.Close();

		}
	}	
}
