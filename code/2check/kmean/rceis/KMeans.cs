using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace RCEIS.KMeans
{

	public class KMeans
	{
		public KMeans()
		{
		}
		
		public static double EuclideanDistance(double [] X, double []Y)
		{
			if (Y == null) 
				Y = new double[X.Length];

			int count = 0;

			double distance = 0.0;

			double sum = 0.0;


			if(X.GetUpperBound(0) != Y.GetUpperBound(0))
			{
				throw new System.ArgumentException("the number of elements in X must match the number of elements in Y");
			}
			else
			{
				count = X.Length;
			}

			for (int i = 0; i < count; i++)
			{
				sum = sum + Math.Pow(Math.Abs(X[i] - Y[i]),2);
			}

			distance = Math.Sqrt(sum);

			return distance;
		}

		public static double EuclideanDistance(double [,] data, int i, int j)
		{
			int count = data.GetUpperBound(0);

			double distance = 0.0;

			double sum = 0.0;

			for (int k = 0; k < count; k++)
			{
				sum = sum + Math.Pow(Math.Abs(data[i,k] - data[j,k]),2);
			}

			distance = Math.Sqrt(sum);

			return distance;
		}
		
		public static double ManhattanDistance(double [] X, double []Y)
		{
			int count = 0;

			double distance = 0.0;

			double sum = 0.0;


			if(X.GetUpperBound(0) != Y.GetUpperBound(0))
			{
				throw new System.ArgumentException("the number of elements in X must match the number of elements in Y");
			}
			else
			{
				count = X.Length;
			}

			for (int i = 0; i < count; i++)
			{
				sum = sum + Math.Abs(X[i] - Y[i]);
			}

			distance = sum;

			return distance;
		}

		public static double [] ClusterMean(double [,] cluster)
		{
			int rowCount = 0;

			int fieldCount = 0;

			double [,] dataSum;

			double [] centroid;


			rowCount = cluster.GetUpperBound(0) + 1;

			fieldCount = cluster.GetUpperBound(1) + 1;

			dataSum = new double[1,fieldCount];

			centroid = new double[fieldCount];

			//((20+30)/2), ((170+160)/2), ((80+120)/2)
			for( int j = 0; j < fieldCount; j++)
			{
				for (int i = 0; i < rowCount; i++)
				{
					dataSum[0,j] = dataSum[0,j] + cluster[i,j];	
				}

				centroid[j] = (dataSum[0,j] / rowCount);
			}
			
			return centroid;
		}

		
		public static ClusterCollection ClusterDataSet(int clusterCount, double [,] data)
		{
			//bool stableClusterFormation = false;

			int clusterNumber = 0;

			int rowCount = data.GetUpperBound(0) + 1;

			int fieldCount = data.GetUpperBound(1) + 1;

			int stableClustersCount = 0;

			int iterationCount = 0;

			double [] dataPoint;

			Random random = new Random();

			Cluster cluster = null;

			ClusterCollection clusters = new ClusterCollection();

			System.Collections.ArrayList clusterNumbers = new System.Collections.ArrayList(clusterCount);

			
			while(clusterNumbers.Count < clusterCount)
			{
				clusterNumber = random.Next(0, rowCount-1);

				if (!clusterNumbers.Contains(clusterNumber)) 
				{
					
					cluster = new Cluster();

					clusterNumbers.Add(clusterNumber);
					
					dataPoint = new double[fieldCount];

					for (int field = 0; field < fieldCount; field++)
					{
						dataPoint.SetValue((data[clusterNumber, field]),field);	
					}

					cluster.Add(dataPoint);

					clusters.Add(cluster);
				}
			}
			
			
			while (stableClustersCount != clusters.Count)
			{
				stableClustersCount = 0;

				ClusterCollection newClusters = KMeans.ClusterDataSet(clusters, data);

				for (int clusterIndex = 0; clusterIndex < clusters.Count; clusterIndex++)
				{
					try
					{
						if ((KMeans.EuclideanDistance(newClusters[clusterIndex].ClusterMean, clusters[clusterIndex].ClusterMean))==0)
						{
							stableClustersCount++;						
						}			
					}
					catch(Exception ex)
							 {
							 }
				}

				iterationCount++;

				clusters = newClusters;
			}

			return clusters;

		}
		

		public static ClusterCollection ClusterDataSet(ClusterCollection clusters, double [,] data)
		{
			double [] dataPoint;

			double [] clusterMean;

			double firstClusterDistance = 0.0;

			double secondClusterDistance = 0.0;

			int rowCount = data.GetUpperBound(0) + 1;

			int fieldCount = data.GetUpperBound(1) + 1;

			int position = 0;


			// create a new collection of clusters
			ClusterCollection newClusters = new ClusterCollection();

			for(int count = 0; count < clusters.Count; count++)
			{
				Cluster newCluster = new Cluster();

				newClusters.Add(newCluster);
			}


			if(clusters.Count <= 0)
			{
				throw new SystemException("Cluster Count Cannot Be Zero!");
			}


			//((20+30)/2), ((170+160)/2), ((80+120)/2)
			for( int row = 0; row < rowCount; row++)
			{
				dataPoint = new double[fieldCount];

				for (int field = 0; field < fieldCount; field++)
				{
					dataPoint.SetValue((data[row,field]),field);	
				}

				for(int cluster = 0; cluster < clusters.Count; cluster++)
				{
					clusterMean = clusters[cluster].ClusterMean;

					if(cluster == 0)
					{
						firstClusterDistance = KMeans.EuclideanDistance(dataPoint, clusterMean);
	
						position = cluster;
					}
					else
					{
						secondClusterDistance = KMeans.EuclideanDistance(dataPoint, clusterMean);

						if (firstClusterDistance > secondClusterDistance)
						{
							firstClusterDistance = secondClusterDistance;

							position = cluster;
						}							
					}
				}
				
				newClusters[position].Add(dataPoint);				 
			}

			return newClusters;
		}
		

		#region "Remarks" - This section has been commented out. Uncomment it to serialize your objects
		
		//		/// <summary>
		//		/// Saves or Serializes a Cluster Collection To an Xml file
		//		/// </summary>
		//		/// <param name="myObject">A serializable object to be persisted to an Xml file</param>
		//		/// <param name="writeToXmlPath">The location of the Xml file tha will contain serialized data</param>
		//		/// <returns>True if the serialization is successful otherwise false</returns>
				public static bool Serialize(System.Object myObject, string writeToXmlPath)
				{
					bool state = true;
					System.Runtime.Serialization.IFormatter formatter = null;
					System.IO.Stream stream = null;
					try
					{
						formatter = new System.Runtime.Serialization.Formatters.Soap.SoapFormatter();
						
						stream = new System.IO.FileStream(writeToXmlPath, FileMode.Create, FileAccess.Write, FileShare.None);
						formatter.Serialize(stream, myObject);
					}
					catch(System.Exception ex)
					{
						state = false;
						System.Diagnostics.Debug.WriteLine(ex.ToString());
					}
					finally
					{
						stream.Close();
						formatter = null;
						stream = null;
					}
					return state;
				}

		#endregion - This section has been commented out. Uncomment it to serialize your objects
		

		public static double [,] ConvertDataTableToArray(DataTable table)
		{
			int rowCount = table.Rows.Count;

			int fieldCount = table.Columns.Count;
			
			double [,] dataPoints;

			double fieldValue = 0.0;

			DataRow row;
			

			dataPoints = new double[rowCount, fieldCount];

			for(int rowPosition = 0; rowPosition < rowCount; rowPosition++)
			{
				row = table.Rows[rowPosition];

				for (int fieldPosition = 0; fieldPosition < fieldCount; fieldPosition++)
				{
					try
					{
						if (row.IsNull(fieldPosition))
							fieldValue = -1;
						else
							fieldValue = double.Parse(row[fieldPosition].ToString());
					}
					catch(System.Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.ToString());

						throw new InvalidCastException("Invalid row at " + rowPosition.ToString() + " and field " + fieldPosition.ToString(),ex);
					}

					dataPoints[rowPosition, fieldPosition] = fieldValue;	
				}					
			}

			return dataPoints;
		}


		public static double [,] ConvertDataReaderToArray(SqlDataReader dr, int rowCount)
		{
			int fieldCount = dr.FieldCount;
			
			double [,] dataPoints;

			dataPoints = new double[rowCount, fieldCount];

			int rowPosition = 0;

			while( dr.Read() )
			{
				if (rowPosition + 1 > rowCount) break;
				for (int fieldPosition = 0; fieldPosition < fieldCount; fieldPosition++)
				{
					dataPoints[rowPosition, fieldPosition] = dr.GetDouble(fieldPosition);
				}
				rowPosition++;
			}

			return dataPoints;
		}

		public static double [] NormalizeData(ref double [,] data)
		{
			int rowCount = data.GetUpperBound(0) + 1;

			int fieldCount = data.GetUpperBound(1) + 1;

			double [] fieldMax = new double[fieldCount];

			for(int j=0; j<fieldCount; j++)
			{
				for (int i=0; i<rowCount; i++)
				{
					if (fieldMax[j] < data[i,j])
						fieldMax[j] = data[i,j];
				}
			}
			

			for(int j=0; j<fieldCount; j++)
			{
				if (fieldMax[j] == 0) continue;
				for (int i=0; i<rowCount; i++)
				{
					data[i,j] /= fieldMax[j];					
				}
			}

			return fieldMax;
		}

	}
	
	/// <summary>
	/// A class containing a group of data with similar characteristics (cluster)
	/// </summary>
	[Serializable]
	public class Cluster  : System.Collections.CollectionBase
	{
		private double [] _clusterSum;		
		/// <summary>
		/// The sum of all the data in the cluster
		/// </summary>
		public double [] ClusterSum
		{
			get
			{
				return this._clusterSum;
			}
		}

		private double [] _clusterMean;
		/// <summary>
		/// The mean of all the data in the cluster
		/// </summary>
		public double [] ClusterMean
		{
			get
			{
				if (this.Count != 0)
				{
					for (int count = 0; count < this[0].Length; count++)
					{
						this._clusterMean[count] = (this._clusterSum[count] / this.List.Count);
					}
				}
				
				return this._clusterMean;
			}
		}

		/// <summary>
		/// Adds a single dimension array data to the cluster
		/// </summary>
		/// <param name="data">A 1-dimensional array containing data that will be added to the cluster</param>
		public virtual void Add (double [] data)
		{
			this.List.Add(data);
			
			if (this.List.Count == 1)
			{
				this._clusterSum = new double[data.Length];

				this._clusterMean = new double[data.Length];
			}
		
			for (int count = 0; count < data.Length; count++)
			{
				this._clusterSum[count] = this._clusterSum[count] + data[count];
			}
		}
		
		/// <summary>
		/// Returns the one dimensional array data located at the index
		/// </summary>
		public virtual double [] this[int Index] 
		{ 
			get 
			{ 
				return (double[])this.List[Index];            
			}        
		} 
	}

	/// <summary>
	/// A collection of Cluster objects or Clusters
	/// </summary>
	[Serializable]
	public class ClusterCollection  : System.Collections.CollectionBase
	{
		
		/// <summary>
		/// Adds a Cluster to the collection of Clusters
		/// </summary>
		/// <param name="cluster">A Cluster to be added to the collection of clusters</param>
		public virtual void Add (Cluster cluster)
		{
			this.List.Add(cluster);
		}
		
		/// <summary>
		/// Returns the Cluster at this index
		/// </summary>
		public virtual Cluster this[int Index] 
		{ 
			get 
			{ 
				//return the Neuron at IList[Index] 
				return (Cluster)this.List[Index];            
			}        
		} 
	}
	

}
