using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace RCEIS
{
	[Serializable]
	public class ClusterSplitting
	{
		private long id_region;		
		private long id_form;	

		public Questionarie questionarie;

		public ClusterCollection clusterCollection;
		public ColumnCollection  columnCollection;

		public ClusterSplittingParamCollection paramCollection;
	
		public ClusterSplitting()
		{
			clusterCollection = new ClusterCollection();
			columnCollection  = new ColumnCollection();
			paramCollection = new ClusterSplittingParamCollection();
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

		public double GetWeight(Column column)
		{
			return paramCollection.Find(id_region, id_form, column.ID).Weight;
		}

		public void LoadClusterCollection(SqlConnection conn)
		{
			clusterCollection.Load(conn, ID_Region, ID_Form, questionarie);
		}

		public void LoadColumnCollection(SqlConnection conn)
		{
			columnCollection.Load(conn, ID_Region, ID_Form, questionarie);
		}

		public void LoadParamCollection(SqlConnection conn)
		{
			paramCollection.Load(conn, ID_Region, ID_Form);
		}
	}

	[Serializable]
	public class ClusterSplittingCollection  : System.Collections.CollectionBase
	{
		public virtual void Add (ClusterSplitting cs)
		{
			this.List.Add(cs);
		}

		public virtual ClusterSplitting this[int Index] 
		{ 
			get 
			{ 
				return (ClusterSplitting)this.List[Index];            
			}        
		} 

		public ClusterSplitting Find(long id_region, long id_form)
		{
			foreach(ClusterSplitting cs in this)
			{
				if ( (cs.ID_Form == id_form) && (cs.ID_Region == id_region) )
				{
					return cs;
				}
			}
			
			return null;
		}

		public void Load(SqlConnection conn, QuestionarieCollection qc)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getClusterSplittingList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				ClusterSplitting cs = new ClusterSplitting();
				
				cs.ID_Form	= dr.GetInt64(0);
				cs.ID_Region= dr.GetInt64(1);				

				cs.questionarie = qc.FindByID(cs.ID_Form);

				Add( cs );
			}
			dr.Close();

			foreach(ClusterSplitting cs in this)
			{
				cs.LoadClusterCollection(conn);
				cs.LoadColumnCollection(conn);
				cs.LoadParamCollection(conn);
			}
		}
	}	
}
