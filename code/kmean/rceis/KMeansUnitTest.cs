using System;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace RCEIS.KMeans
{

	/// <summary>
	/// KMeansUnitTest is used to test the KMeans implementation
	/// </summary>
	class KMeansUnitTest
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		//[STAThread]
		//static void Main(string[] args)
		//{
		//	KMeansUnitTest.ClusterDataTableTestCase();

			//KMeansUnitTest.ConvertDataTableToArrayTestCase();

			//KMeansUnitTest.ClusterDataSetTestCase();	

			//KMeansUnitTest.ClusterMean();
		//}
	

		/// <summary>
		/// Test the Euclidean Distance calculation between two data points
		/// </summary>
		public static void EuclideanDistanceTest()
		{
			double [] John = new double[] {20, 170, 80};

			double [] Henry = new double[] {30, 160, 120};

			double distance = KMeans.EuclideanDistance(John, Henry);

			System.Diagnostics.Debug.WriteLine(distance);
		}
		
		/// <summary>
		/// Test the Manhattan Distance calculation between two data points
		/// </summary>
		public static void ManhattanDistanceTest()
		{
			double [] John = new double[] {20, 170, 80};

			double [] Henry = new double[] {30, 160, 120};

			double distance = KMeans.ManhattanDistance(John, Henry);

			System.Diagnostics.Debug.WriteLine(distance);
		}
		
		/// <summary>
		/// Test the Cluster Mean calculation between two data points
		/// </summary>
		public static void ClusterMean()
		{
			double [] John = new double[] {20, 170, 80};

			double [] Henry = new double[] {30, 160, 120};
	
			double [,] cluster = {{20,170,80}, {30,160,120}};

			double [] centroid = KMeans.ClusterMean(cluster);
			
			//((20+30)/2), ((170+160)/2), ((80+120)/2)
			System.Diagnostics.Debug.WriteLine(centroid.ToString());
		}
		
		/// <summary>
		/// Test the clustering of data in an Array
		/// </summary>
		public static void ClusterDataSetTestCase()
		{

			double [] a = new double[] {20, 170, 80};
			
			double [] b = new double[] {40, 190, 100};

			double [] c = new double[] {60, 210, 130};

			double [] d = new double[] {70, 240, 160};

			double [] e = new double[] {90, 240, 200};

			double [] f = new double[] {130, 270, 250};

			double [] g = new double[] {170, 740, 320};
			
			double [] h = new double[] {220, 680, 180};

			double [] i = new double[] {260, 480, 2000};

			double [] j = new double[] {100, 2720, 2250};

			double [] k = new double[] {470, 300, 720};


			ClusterCollection clusters;

			double [,] data = {{20, 170, 80},{40, 190, 100},{60, 210, 130},{70, 240, 160},{90, 240, 200},{130, 270, 250},{170, 740, 320},{220, 680, 180},{260, 480, 2000},{100, 2720, 2250},{470, 300, 720}};
			
			clusters = KMeans.ClusterDataSet (4,data);
			
			//This line has been commented out. Uncomment it to serialize your object(s)
			KMeans.Serialize(clusters, @"kmeansclusters.xml");
		}

		public static void ClusterDataReaderTestCase(SqlDataReader dr, int rowCount)
		{
			ClusterCollection clusters;
			double [,] data = KMeans.ConvertDataReaderToArray(dr, rowCount);
			double []  maxvalues = KMeans.NormalizeData(ref data);
			clusters = KMeans.ClusterDataSet(4, data);
			KMeans.Serialize(clusters, @"kmeansclusters.xml");

			FileInfo f = new FileInfo("clusters.txt");
			StreamWriter writer = f.CreateText();

			foreach(Cluster c in clusters)
			{
				writer.Write("{0} ",c.Count);
			}

			writer.Close();
		}
		
		/// <summary>
		/// Test the conversion of a 2-dimensional Array to a DataTable
		/// </summary>
		public static void ConvertDataTableToArrayTestCase()
		{
			DataTable table = new DataTable();

			DataRow row = null;

			DataColumn lengthColumn = new DataColumn("Length",System.Type.GetType("System.Double"));

			DataColumn widthColumn = new DataColumn("Width",System.Type.GetType("System.Double"));

			DataColumn heightColumn = new DataColumn("Height",System.Type.GetType("System.Double"));


			table.Columns.Add(lengthColumn);

			table.Columns.Add(widthColumn);

			table.Columns.Add(heightColumn);


			row = table.NewRow();

			//add first row to the table
			row["Length"] = 20.0;

			row["Width"] = 170.0;

			row["Height"] = 80.0;

			table.Rows.Add(row);


			row = table.NewRow();

			//add second row to the table
			row["Length"] = 40.0;

			row["Width"] = 190.0;

			row["Height"] = 100.0;

			table.Rows.Add(row);

			row = table.NewRow();


			//add third row to the table
			row["Length"] = 60.0;

			row["Width"] = 210.0;

			row["Height"] = 130.0;

			table.Rows.Add(row);


			//convert the dataset to an array
			KMeans.ConvertDataTableToArray(table); 
		}
		
		/// <summary>
		/// Test the clustering of data in an DataTable
		/// </summary>
		public static void ClusterDataTableTestCase()
		{

			DataTable table = new DataTable();

			DataRow row = null;

			DataColumn lengthColumn = new DataColumn("Length",System.Type.GetType("System.Double"));

			DataColumn widthColumn = new DataColumn("Width",System.Type.GetType("System.Double"));

			DataColumn heightColumn = new DataColumn("Height",System.Type.GetType("System.Double"));


			table.Columns.Add(lengthColumn);

			table.Columns.Add(widthColumn);

			table.Columns.Add(heightColumn);


			row = table.NewRow();

			//add first row to the table
			row["Length"] = 20.0;

			row["Width"] = 170.0;

			row["Height"] = 80.0;

			table.Rows.Add(row);


			row = table.NewRow();


			//add second row to the table
			row = table.NewRow();

			row["Length"] = 40.0;

			row["Width"] = 190.0;

			row["Height"] = 100.0;

			table.Rows.Add(row);


			//add third row to the table
			row = table.NewRow();

			row["Length"] = 60.0;

			row["Width"] = 210.0;

			row["Height"] = 130.0;

			table.Rows.Add(row);


			KMeans.ClusterDataSet(2,KMeans.ConvertDataTableToArray(table)); 

		}
		
	}


}
