using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace RCEIS
{
	public enum ImputeTypes
	{
		Logical		= 1,
		Statistical	= 2,
		Balance		= 3,
		HotDeck		= 4,
		ZeroValue	= 5
	};

	[Serializable]
	public class ImputeRule
	{
		private long id;		
		private ImputeTypes type;
		private long id_column;
		private long number;
		private string parameter;

		private Column column;
        
		public ImputeRule()
		{
			column = null;
		}

		public long ID
		{
			get {return id;}
			set {id = value;}
		}

		public long Number
		{
			get {return number;}
			set {number = value;}
		}

		public ImputeTypes ImputeType
		{
			get	{return type;}
			set {type = value;}
		}

		public long ID_Column 
		{
			get {return id_column;}
			set {id_column = value;}
		}

		public Column Column
		{
			get {return column;}
			set {column = value;}
		}

		public string Parameter
		{
			get {return parameter;}
			set {parameter = value;}
		}

		public override string ToString()
		{
			switch (type)
			{
				case ImputeTypes.Balance: return "Балансовая импутация";

				case ImputeTypes.HotDeck: return "Импутация методом ближайшего соседа";

				case ImputeTypes.Logical: return "Логическая импутация";

				case ImputeTypes.Statistical: return "Импутация по линейной регрессии";

				case ImputeTypes.ZeroValue: return "Импутация нулевых показателей";
			}
			return base.ToString ();
		}

		public static string ToString(ImputeTypes type)
		{
			switch (type)
			{
				case ImputeTypes.Balance: return "Балансовая импутация";

				case ImputeTypes.HotDeck: return "Импутация методом ближайшего соседа";

				case ImputeTypes.Logical: return "Логическая импутация";

				case ImputeTypes.Statistical: return "Импутация по линейной регрессии";

				case ImputeTypes.ZeroValue: return "Импутация нулевых показателей";
			}
			return "";
		}

	}

	[Serializable]
	public class ImputeRuleCollection  : System.Collections.CollectionBase
	{
		public virtual void Add (ImputeRule ir)
		{
			this.List.Add(ir);
		}
		
		public virtual ImputeRule this[int Index] 
		{ 
			get 
			{ 
				return (ImputeRule)this.List[Index];            
			}        
		} 

		public ImputeRule FindByID(long id)
		{
			foreach(ImputeRule ir in this)
			{
				if (ir.ID == id)
					return ir;
			}			
			return null;
		}

		public int FindIndexByID(long id)
		{
			int i = 0;
			foreach(ImputeRule ir in this)
			{
				if (ir.ID == id)
					return i;
				i++;
			}			
			return -1;
		}

		public void Load(SqlConnection conn, long id_column, Column column)
		{
			Clear();

			SqlCommand cmd = new SqlCommand("sp_getImputeRuleList", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			cmd.Parameters.Add("@id_column", SqlDbType.BigInt);

			cmd.Parameters["@id_column"].Value = id_column;

			SqlDataReader dr = cmd.ExecuteReader();
			
			while( dr.Read() )
			{
				ImputeRule ir = new ImputeRule();
				
				ir.ID		 = dr.GetInt64(0);
				ir.Number	 = dr.GetInt64(2);
				ir.ID_Column = id_column;
				ir.Parameter = dr.GetString(4);

				switch (dr.GetInt64(1))
				{
					case 1:
						ir.ImputeType = ImputeTypes.Logical; break;
					case 2:
						ir.ImputeType = ImputeTypes.Statistical; break;
					case 3:
						ir.ImputeType = ImputeTypes.Balance; break;
					case 4:
						ir.ImputeType = ImputeTypes.HotDeck; break;
					case 5:
						ir.ImputeType = ImputeTypes.ZeroValue; break;
				}

				ir.Column = column;

				Add( ir );
			}
			dr.Close();

		}

		public void Insert(SqlConnection conn, ImputeRule ir)
		{
			SqlCommand cmd = new SqlCommand("sp_addImputeRule", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			

			cmd.Parameters.Add("@id_ruletype", SqlDbType.BigInt);
			cmd.Parameters.Add("@id_column", SqlDbType.BigInt);
			cmd.Parameters.Add("@number", SqlDbType.BigInt);
			cmd.Parameters.Add("@parameter", SqlDbType.VarChar);


			cmd.Parameters["@id_ruletype"].Value = ir.ImputeType;
			cmd.Parameters["@id_column"].Value = ir.ID_Column;
			cmd.Parameters["@number"].Value = ir.Number;
			cmd.Parameters["@parameter"].Value = "";
			
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

			Add(ir);
		}

		public void Update(SqlConnection conn, ImputeRule ir)
		{
			/*SqlCommand cmd = new SqlCommand("sp_updateRegion", conn);
			
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
			}*/
		}

		public void DeleteAll(SqlConnection conn)
		{
			foreach(ImputeRule ir in this)
			{
				Delete(conn, ir);
			}
			Clear();
		}

		public void Delete(SqlConnection conn, ImputeRule ir)
		{
			SqlCommand cmd = new SqlCommand("sp_deleteImputeRule", conn);
			
			cmd.CommandType = CommandType.StoredProcedure;
			
			cmd.Parameters.Add("@id_imputerule", SqlDbType.VarChar);

			cmd.Parameters["@id_imputerule"].Value = ir.ID;
			
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

	}
	
}
