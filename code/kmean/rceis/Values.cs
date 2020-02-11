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
	public class Value
	{
		private long id;	
	
		private string okato;

		private object [] data;

		public Value(Questionarie qst)
		{
			long count = qst.GetColumnCollection().Count;

			data = new object[count];
		}	
	

		public long ID
		{
			get {return id;}
			set {id = value;}
		}

		public string OKATO
		{
			get {return okato;}
			set {okato = value.Substring(1,2);}
		}

		public object this[int Index] 
		{ 
			get 
			{ 
				return data[Index];            
			}        
			set 
			{
				data[Index] = value;
			}
		} 

		public bool [][] GetQLT(Questionarie qst)
		{
			int qlt_count = 0;

			ColumnCollection cc = qst.GetColumnCollection();

			for(int i=0; i<=data.GetUpperBound(0); i++)
			{
				if (cc[i].Domain.DomainType == DomainType.QualitativeDomain)	
				{
					qlt_count++;
				}
			}

			bool [][] qlt_data = new bool[qlt_count][];

			int k = 0;

			for(int i=0; i<=data.GetUpperBound(0); i++)
			{
				if (cc[i].Domain.DomainType == DomainType.QualitativeDomain)	
				{
					qlt_data[k] = new bool[cc[i].Domain.valueCollection.Count];
					int idx = cc[i].Domain.valueCollection.FindIndexByValue((long)(double)(data[i]));
					qlt_data[k][idx] = true;
					k++;
				}
			}

			return qlt_data;
		}

		public double [] GetQNT(Questionarie qst)
		{
			int qnt_count = 0;

			for(int i=0; i<=data.GetUpperBound(0); i++)
			{
				if (qst.GetColumnCollection()[i].Domain.DomainType == DomainType.QuantitiveDomain)	
				{
					qnt_count++;
				}
			}

			double [] qnt_data = new double[qnt_count];

			int k = 0;

			for(int i=0; i<=data.GetUpperBound(0); i++)
			{
				if (qst.GetColumnCollection()[i].Domain.DomainType == DomainType.QuantitiveDomain)	
				{
					qnt_data[k] = (double)(data[i]);
					k++;
				}
			}

			return qnt_data;
		}
	
		public string ToString()
		{
			return id.ToString();
		}

		public string ToString(Questionarie qst, int index)
		{
			Column column = qst.GetColumnCollection()[index];

			if (data[index] == null)
			{
				return "NULL";
			}

			if (column.Domain.DomainType == DomainType.QuantitiveDomain)
			{
				return data[index].ToString();
			}
			else
			{
				double val = (double)data[index];
				DomainValue domain_value = column.Domain.FindByValue(Int32.Parse(val.ToString()));
				return domain_value.Name;
			}
		}

		public void Load(Questionarie qst, long id_value, string OKATO, ref double[,] data)
		{
			for(int i=0; i<data.GetUpperBound(0); i++)
			{
				if ((long)data[i, 0] == id_value)
				{
					Load(qst, id_value, OKATO, ref data, i);
				}
			}
		}

		public void Load(Questionarie qst, long id_value, string OKATO, ref double[,] data, int index)
		{
			this.id = id_value;

			this.okato = OKATO.Substring(0,2);

			int i = 0;

			foreach(Column column in qst.GetColumnCollection())
			{
				this.data[column.Number-1] = (object)data[ index, i + 2 ];
				i++;
			}
		}

		private void Load(SqlConnection conn, long id, Questionarie qst, int index)
		{
            Column column = qst.GetColumnCollection()[index];

			this.id = id;

			if (column.Code != -1)
			{
				SqlCommand cmd = new SqlCommand("GetColumnValue", conn);
				
				cmd.CommandType = CommandType.StoredProcedure;

				cmd.Parameters.Add("@TableName",	SqlDbType.VarChar);
				cmd.Parameters.Add("@TableType",	SqlDbType.BigInt);
				cmd.Parameters.Add("@FormIDName",	SqlDbType.VarChar);
				cmd.Parameters.Add("@FormID",		SqlDbType.BigInt);
				cmd.Parameters.Add("@ColumnName",	SqlDbType.VarChar);
				cmd.Parameters.Add("@Code",			SqlDbType.BigInt);
				
				cmd.Parameters.Add("@Value",		SqlDbType.Float);
				cmd.Parameters.Add("@IsNull",		SqlDbType.BigInt);

				cmd.Parameters["@TableName"].Value	= column.table.Name;
				if (column.table.Type == TableTypes.ParentTable)
					cmd.Parameters["@TableType"].Value	= 1;
				else
					cmd.Parameters["@TableType"].Value	= 2;

				cmd.Parameters["@FormIDName"].Value	= column.table.ID_FormName;
				cmd.Parameters["@FormID"].Value		= id;
				cmd.Parameters["@ColumnName"].Value	= column.Name;
				cmd.Parameters["@Code"].Value		= column.Code;
	
				cmd.Parameters["@Value"].Direction = System.Data.ParameterDirection.Output;
				cmd.Parameters["@IsNull"].Direction = System.Data.ParameterDirection.Output;
			
				SqlDataReader dr = cmd.ExecuteReader();

				data[index] = (object)(cmd.Parameters["@Value"].Value);

				dr.Close();
			}
			else
			{
				string cn = column.Name;
				
				int n = cn.LastIndexOf("}");
				
				long c_id = Int32.Parse(cn.Substring(1, n-1));

				double c_val = Double.Parse(cn.Substring(n+2));

				long idx = qst.GetColumnCollection().GetIndexByID(c_id);

				switch(cn[n+1])
				{
					case '=' : data[index] = (double)(c_val==(double)data[idx]?1:2); break;
					case '#' : data[index] = (double)(c_val!=(double)data[idx]?1:2); break;
					case '>' : data[index] = (double)(c_val>(double)data[idx]?1:2); break;
					case '<' : data[index] = (double)(c_val<(double)data[idx]?1:2); break;
				}				
			}
		}
		

		public void Load(SqlConnection conn, long id, Questionarie qst)
		{
			int count = qst.GetColumnCollection().Count;

			for(int i=0; i<count; i++)
			{
				Load(conn, id, qst, i);
			}

			//Получить OKATO
			SqlCommand cmd = new SqlCommand("sp_getOKATO", conn);
				
			cmd.CommandType = CommandType.StoredProcedure;

			cmd.Parameters.Add("@id_form",	SqlDbType.BigInt);
			cmd.Parameters.Add("@okato",	SqlDbType.VarChar, 255);

			cmd.Parameters["@id_form"].Value = id;

			cmd.Parameters["@okato"].Direction = System.Data.ParameterDirection.Output;

			SqlDataReader dr = cmd.ExecuteReader();

			okato = ((string)cmd.Parameters["@okato"].Value).Substring(0,2); 

			dr.Close();
		}
	}

	/*[Serializable]
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
		}*/
//	}	
}
