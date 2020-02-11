using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RCEIS
{
	[Serializable]
	public class ProtocolRecord
	{
		private long id;		
		private DateTime writed;
		private long id_form;
		private long id_value;
		private long id_column;
		private double old_value;
		private double new_value;
		private long id_ruletype;
		private string comment;

		public Questionarie Questionarie;
		public Column Column;

		public long ID
		{
			get {return id;}
			set {id = value;}
		}

		public DateTime Writed
		{
			get {return writed;}
			set {writed = value;}
		}

		public long ID_Form
		{
			get {return id_form;}
			set {id_form = value;}
		}

		public long ID_Value
		{
			get {return id_value;}
			set {id_value = value;}
		}

		public long ID_Column
		{
			get {return id_column;}
			set {id_column = value;}
		}

		public double OldValue
		{
			get {return old_value;}
			set {old_value = value;}
		}

		public double NewValue
		{
			get {return new_value;}
			set {new_value = value;}
		}

		public long ID_RuleType
		{
			get {return id_ruletype;}
			set {id_ruletype = value;}
		}

		public string Comment
		{
			get {return comment;}
			set {comment = value;}

		}

	}

	[Serializable]
	public class ProtocolRecordCollection  : System.Collections.CollectionBase
	{
		
		public virtual void Add (ProtocolRecord pr)
		{
			this.List.Add(pr);
		}
		
		public virtual ProtocolRecord this[int Index] 
		{ 
			get 
			{ 
				return (ProtocolRecord)this.List[Index];            
			}        
		} 

		public void Load(SqlConnection conn, QuestionarieCollection qq)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getProtocolRecordList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				ProtocolRecord pr = new ProtocolRecord();
				
				pr.ID		= dr.GetInt64(0);
				pr.Writed	= dr.GetDateTime(1);
				pr.ID_Form	= dr.GetInt64(2);
				pr.ID_Value = dr.GetInt64(3);
				pr.ID_Column= dr.GetInt64(4);
				pr.OldValue = dr.GetDouble(5);
				pr.NewValue = dr.GetDouble(6);
				pr.ID_RuleType = dr.GetInt64(7);
				pr.Comment = dr.GetString(8);				

				pr.Questionarie = qq.FindByID(pr.ID_Form);

				pr.Column = pr.Questionarie.GetColumnCollection().FindByID(pr.ID_Column);

				Add( pr );
			}
			dr.Close();
		}

		public void Insert(SqlConnection conn, ProtocolRecord pr)
		{
			SqlCommand cmd = new SqlCommand("sp_addProtocolRecord", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			cmd.Parameters.Add("@id_protocol", SqlDbType.BigInt);
			cmd.Parameters.Add("@writed", SqlDbType.DateTime);
			cmd.Parameters.Add("@id_form", SqlDbType.BigInt);
			cmd.Parameters.Add("@id_value", SqlDbType.BigInt);
			cmd.Parameters.Add("@id_column", SqlDbType.BigInt);
			cmd.Parameters.Add("@old_value", SqlDbType.Float);
			cmd.Parameters.Add("@new_value", SqlDbType.Float);
			cmd.Parameters.Add("@id_ruletype", SqlDbType.BigInt);
			cmd.Parameters.Add("@comment", SqlDbType.VarChar);
	
			cmd.Parameters["id_protocol"].Value = pr.ID;
			cmd.Parameters["writed"].Value = pr.Writed;
			cmd.Parameters["id_form"].Value = pr.ID_Form;
			cmd.Parameters["id_value"].Value = pr.ID_Value;
			cmd.Parameters["id_column"].Value = pr.ID_Column;
			cmd.Parameters["old_value"].Value = pr.OldValue;
			cmd.Parameters["new_value"].Value = pr.NewValue;
			cmd.Parameters["id_ruletype"].Value = pr.ID_RuleType;
			cmd.Parameters["comment"].Value = pr.Comment;

			try
			{
				cmd.ExecuteNonQuery();				
			}
			catch(System.Data.SqlClient.SqlException ex)
			{	

			}
			catch(System.Exception ex)
			{

			}
		}

	}
	
}
