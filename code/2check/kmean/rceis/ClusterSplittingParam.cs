using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace RCEIS
{
	[Serializable]
	public class ClusterSplittingParam
	{
		private long id_region;		
		private long id_form;
		private long id_column;

		public double min_value;
		public double max_value;

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

		public long ID_Column
		{
			get {return id_column;}
			set {id_column = value;}
		}

		public double Weight
		{
			get {return max_value-min_value;}
		}
	}

	[Serializable]
	public class ClusterSplittingParamCollection  : System.Collections.CollectionBase
	{
		public virtual void Add (ClusterSplittingParam csp)
		{
			this.List.Add(csp);
		}

		public virtual ClusterSplittingParam this[int Index] 
		{ 
			get 
			{ 
				return (ClusterSplittingParam)this.List[Index];            
			}        
		} 


		public ClusterSplittingParam Find(long id_region, long id_form, long id_column)
		{
			foreach(ClusterSplittingParam csp in this)
			{
				if ( (csp.ID_Column==id_column) && (csp.ID_Form==id_form) && (csp.ID_Region==id_region))
				{
					return csp;
				}
			}

			return null;
		}

		public void Load(SqlConnection conn, long id_region, long id_form)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getClusterSplittingParamList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			cmd.Parameters.Add("@id_region", SqlDbType.BigInt);
			cmd.Parameters.Add("@id_form", SqlDbType.BigInt);


			cmd.Parameters["@id_form"].Value = id_form;
			cmd.Parameters["@id_region"].Value = id_region;

			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				ClusterSplittingParam csp = new ClusterSplittingParam();
				
				csp.ID_Region	= id_region;
				csp.ID_Form		= id_form;	
			
				csp.ID_Column   = dr.GetInt64(0);
				csp.min_value	= dr.GetDouble(1);
				csp.max_value	= dr.GetDouble(2);				

				Add( csp );
			}
			
			dr.Close();
		}
	}	
}
