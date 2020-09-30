using System;
using System.Data;
using System.Data.SqlClient;

namespace RCEIS
{
	public class ControlDonorRecipient
	{
		public long DataTotal;
		public long DataDonor;
		public long DataRecipient;


		private double [,] data;

		private double [,] donors;

		private bool [] donor;
		private bool [] recipient;

		static Questionarie qst;
		static Region region;

		public static double [,] LoadData(SqlConnection conn, Questionarie _qst, Region _region, bool is_donor, bool is_recipient, long id_cluster)
		{
			qst = _qst;
			region = _region;

			string formname = qst.tableCollection[0].Name.Trim();

			int d = is_donor?1:0, r=is_recipient?1:0;

			string sqlText = "select IValues_" + formname + ".* FROM";
			sqlText += " ClusterValues INNER JOIN [Values] ON ClusterValues.id_value = [Values].id_value INNER JOIN IValues_"+formname+" ON [Values].id_value = IValues_"+formname+".FormID";
			sqlText += " WHERE left(OKATO, 2)='"+region.OKATO.Trim()+"' AND is_donor=" + d.ToString() + " AND is_recipient = " + r.ToString() + " AND id_cluster = " + id_cluster.ToString();
			
			SqlDataAdapter da = new SqlDataAdapter(sqlText, conn);

			DataSet ds = new DataSet();

			da.Fill(ds);

			return KMeans.KMeans.ConvertDataTableToArray(ds.Tables[0]);			
		}

		public void SplitData(SqlConnection conn, Questionarie _qst, Region _region, double alpha)
		{
			qst = _qst;
			region = _region;

			string sqlText = "select * from IValues_" + qst.tableCollection[0].Name.Trim()+" where left(OKATO, 2)='"+region.OKATO.Trim()+"'";
				
			SqlDataAdapter da = new SqlDataAdapter(sqlText, conn);

			DataSet ds = new DataSet();

			da.Fill(ds);

			data = KMeans.KMeans.ConvertDataTableToArray(ds.Tables[0]);

			ColumnCollection cc = qst.GetColumnCollection();

			long c_count = cc.Count + 2; //первые два пол€ - formID и OKATO

			double [] v_min = new double[ c_count ];
			double [] v_max = new double[ c_count ];
				
			for(int i=2; i<c_count; i++)
			{
				double avg = Maths.Average(data, i);
				double dev = Maths.StandardDeviation(data, i);

				v_min[ i ] = avg - alpha * dev;
				v_max[ i ] = avg + alpha * dev;
			}	

			long v_count = data.GetUpperBound(0) + 1;

			donor = new bool[v_count];
			recipient = new bool[v_count];

			long nd_count = 0;
			long rc_count = 0;

			for(int i=0; i<v_count; i++)
			{
				donor[ i ] = true;

				for(int j=2; j<c_count; j++)
				{
					if (cc[j-2].Domain.DomainType == DomainType.QualitativeDomain)
						continue;

					if ( data[ i, j ] == -1.0)
					{
						recipient[ i ] = true;
						rc_count++;
						break;
					}

					if ( (data[ i, j ] < v_min[ j ]) || (data[ i, j ] > v_max[ j ]) )
					{
						donor[ i ] = false;
						nd_count++;
						break;
					}
				}				
			}	

			DataTotal = v_count;
			DataDonor = v_count - nd_count - rc_count;
			DataRecipient = rc_count;
		}

		private void SaveSplit(SqlConnection conn, long id_value, long id_form, long id_region, bool is_donor, bool is_recipient)
		{
			SqlCommand cmd = new SqlCommand("sp_updateValue", conn);
		
			cmd.CommandType = CommandType.StoredProcedure;
		
			cmd.Parameters.Add("@id_value", SqlDbType.BigInt);
			cmd.Parameters.Add("@id_form", SqlDbType.BigInt);
			cmd.Parameters.Add("@id_region", SqlDbType.BigInt);
			cmd.Parameters.Add("@is_donor", SqlDbType.Bit);
			cmd.Parameters.Add("@is_recipient", SqlDbType.Bit);

			cmd.Parameters["@id_value"].Value = id_value;
			cmd.Parameters["@id_form"].Value = id_form;
			cmd.Parameters["@id_region"].Value = id_region;
			cmd.Parameters["@is_donor"].Value = is_donor;
			cmd.Parameters["@is_recipient"].Value = is_recipient;
		
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

		public void SaveSplit(SqlConnection conn)
		{
			for(int i=0; i<DataTotal; i++)
			{
				long id_value = (long)data[i, 0];

				SaveSplit(conn, id_value, qst.ID, region.ID, donor[i], recipient[i]);
			}
		}
	}

	public class ControlImpute
	{
		SqlConnection conn;

		public Questionarie qst;

		public Region region;

		public Value val;
		public Value ngb;
		public Value corrected;

		private bool [,] failEditMatrix;

		public double [,] all_data;

		ColumnCollection   fcColl;

		public EditRuleCollection failedEdits;

		public EditRuleCollection failedQntEdits;
		public EditRuleCollection failedQltEdits;
		
		public EditRuleCollection satisfiedEdits;

		public ClusterSplitting clusterSplitting;

		public Cluster nearestCluster;

		public long totalValuesCount;
		public long donorValuesCount;

		public ColumnCollection FailedColumnCollection
		{
			get {return fcColl;}
		}

		public ControlImpute(SqlConnection _conn, Questionarie _qst)
		{
			conn = _conn;

			qst = _qst;

			failedEdits = new EditRuleCollection();

			failedQntEdits = new EditRuleCollection();
			failedQltEdits = new EditRuleCollection();
			satisfiedEdits = new EditRuleCollection();

			fcColl = new ColumnCollection ();

			all_data = null;
		}

		public void LoadValue(SqlConnection conn, long id, RegionCollection rc)
		{
			val = new Value(qst);			
		
			val.Load(conn, id, qst);

			region = rc.FindByOKATO(val.OKATO);
		}

		public void CheckEdits()
		{
			bool [][] qlt = val.GetQLT(qst);
			double [] qnt = val.GetQNT(qst);

			bool check;

			foreach(EditRule er in qst.editRuleCollection)
			{
				if (er.RuleType == RuleTypes.Qualitative)
				{
					Edits.QLT_EditRule qlt_er = er.GetQLT(qst);

					check = qlt_er.CheckValue(qlt);

					if (!check)
					{
						failedQltEdits.Add(er);
						failedEdits.Add(er);
					}
				}
				else
				{
					Edits.QNT_EditRule qnt_er = er.GetQNT(qst);

					check = qnt_er.CheckValue(qnt);

					if (!check)
					{
						failedQntEdits.Add(er);
						failedEdits.Add(er);
					}
				}

				if (check)
					satisfiedEdits.Add(er);
			}			
		}

		public void LocaliseError()
		{
			EditRuleCollection erColl = failedEdits /*satisfiedEdits*/;

			if (erColl.Count == 0) return;
			
			ColumnCollection   icColl = qst.GetColumnCollection();

			long e_count = erColl.Count;
			
			long c_count = icColl.Count;

			failEditMatrix = new bool[ c_count, e_count ];

			long i = 0;
			
			foreach(EditRule rule in erColl)
			{
                long j = 0;

				foreach(Column column in icColl)
				{
					failEditMatrix [ j , i ] = rule.IncludeExplicitly( column );
					
					j ++;
				}
				i ++;		
			}
			
			fcColl = new ColumnCollection();

			do 
			{				
				//»щем показатель с максимальной характеристикой
				double max_column = 0.0;
				int   idx_column = 0;
				
				int   cur_idx    = 0;
				
				foreach(Column column in icColl)
				{		
					double cur_column = 0;

					foreach(EditRule rule in erColl)
					{
						if (rule.IncludeExplicitly( column ))
							cur_column += column.Weight;
					}

					if ( cur_column >= max_column )
					{
						max_column = cur_column;
						idx_column = cur_idx;
					}

					cur_idx ++; 
				}
		
				//”дал€ем правила, в которые показатель входит
				int k = 0;
				while (k < erColl.Count )
				{
					EditRule rule = erColl[ k ];

					Column column = icColl[idx_column];

					if ( rule.IncludeExplicitly(column) )
						erColl.RemoveAt( k );
					else
						k ++;
				}

				//ƒобавл€ем его в fcColl, удал€ем из icColl
				fcColl.Add(icColl[idx_column]);
				icColl.RemoveAt(idx_column);

			//ѕока не будет пустой коллекци€ правил
			} while(erColl.Count != 0);
		}

		public bool[] GetFailedQltColumnsIndex()
		{

			ColumnCollection cc = qst.GetColumnCollection(DomainType.QualitativeDomain);

			bool [] beImputed = new bool[cc.Count];

			int i = 0;
			foreach(Column column in cc)
			{
				if (fcColl.FindByID(column.ID) == null)
					beImputed[ i ] = false;
				else
					beImputed[ i ] = true;

				i++;
			}

			return beImputed;
		}

		public void FindNearestCluster()
		{
			ColumnCollection cc = qst.GetColumnCollection(DomainType.QuantitiveDomain);

			double min_distance = Double.MaxValue;
			int idx = -1;
			int cur = 0;

			double [] val_data = val.GetQNT(qst);

			foreach(Cluster cluster in clusterSplitting.clusterCollection)
			{
				//вычислим рассто€ние от центра кластера до точки
				double cur_distance = 0;

				foreach(ClusterParam cp in cluster.clusterParamCollection)
				{
					double mean = cp.Mean;

					double data = val_data[ cc.GetIndexByID(cp.Column.ID) ];

					double weight = clusterSplitting.GetWeight(cp.Column);

					if ( weight == 0) weight = 1;

					cur_distance += Math.Pow( mean-data , 2.0 ) / weight;
				}

				if ( cur_distance < min_distance )
				{
					min_distance = cur_distance;
					idx = cur;
				}

				cur++;
			}

			nearestCluster = clusterSplitting.clusterCollection[idx];
		}		

		public void FindNearestValue(ref double [,] all_data)
		{
			ColumnCollection cc = qst.GetColumnCollection();
			ColumnCollection cc_qnt = qst.GetColumnCollection(DomainType.QuantitiveDomain);

			Edits.QLT_EditRulesCollection qlt_rc = failedQltEdits.GetQLT_EditRuleCollection(qst);
			
			bool [][] filter = qlt_rc.GetFilter(qst, GetFailedQltColumnsIndex() ,val);
			
			double min_distance = Double.MaxValue;
			int idx = -1;

			double [] val_data = val.GetQNT(qst);

			if (all_data == null)
				this.all_data = ControlDonorRecipient.LoadData(conn, qst, region, true, false, nearestCluster.ID);
			else
				this.all_data = all_data/*ControlDonorRecipient.LoadData(conn, qst, region, true, false, nearestCluster.ID)*/;

			long v_count = all_data.GetUpperBound(0)+1;
			long c_count = val_data.GetUpperBound(0)+1;

			totalValuesCount = nearestCluster.RecordCount;
			donorValuesCount = v_count;

			//ƒл€ тестировани€ и восстановлени€ кач. показателей
			if (v_count == 0)
			{
				all_data = ControlDonorRecipient.LoadData(conn, qst, region, false, false, nearestCluster.ID);
				v_count = all_data.GetUpperBound(0)+1;
			}

			if (v_count != 0)
			{			
				double [] max_values = new double[all_data.GetUpperBound(1)+1];

				for(int i=2; i<all_data.GetUpperBound(1)+1; i++)
				{
					max_values[ i ] = Maths.Max(all_data, i);
					if (max_values[i] == 0)
						max_values[i] = 1;
				}				

				for (int i=0; i<v_count; i++)
				{
					Value newVal = new Value(qst);
					newVal.Load(qst, (long)all_data[i, 0], all_data[i, 1].ToString("F0"), ref all_data, i);

					if (newVal.ID == val.ID) //дл€ тестировани€, чтоб не находил тоже хоз€йство
						continue;

					bool f = qlt_rc.ApllyFilter(qst, filter, newVal);

					if (!f) continue;

					double cur_distance = 0;

					for(int j=0; j<c_count; j++)
					{				
						if (cc[j].Domain.DomainType == DomainType.QualitativeDomain)
							continue;
						
						long idx_col = cc[j].Number + 1;

						cur_distance += Math.Pow((val_data[j] - all_data[ i, idx_col])/ max_values[ idx_col ] , 2);
					}

					if ( cur_distance < min_distance )
					{
						min_distance = cur_distance;
						idx = i;
					}
				}
				ngb = new Value(qst);
				ngb.Load(qst, (long)all_data[idx, 0], all_data[idx, 1].ToString("F0"), ref all_data, idx);
			}
			else 
			{
				ngb = null;
			}			
			
			corrected = new Value(qst);
			/*corrected.Load(conn, val.ID, qst);*/
			int n = qst.GetColumnCollection().Count;
			for (int i=0; i<n; i++)
				corrected[i] = (object)(double)val[i];

			/*corrected.Load(qst, val.ID, val.OKATO, ref all_data);*/
		}

		public void DoImpute(DomainType dt)
		{
			long idx = 0;			

			if (dt == DomainType.QualitativeDomain)
			{

				//проводим импутацию качественных показателей
				foreach(Column column in fcColl.GetColumnCollection(DomainType.QualitativeDomain))
				{
					foreach(ImputeRule ir in column.imputeRuleCollection)
					{
						//проводим логическую импутацию, остальные - невозможны
						if (ir.ImputeType == ImputeTypes.Logical)
						{
							idx = qst.GetColumnCollection().GetIndexByID(column.ID);
							int num = (int)idx;
					
							if (ngb != null)					
								corrected[num] = ngb[num];
							else
								corrected[num] = null;
							break;
						}
					}
                }
			}			
			else
			{
				//проводим импутацию качественных показателей
				foreach(Column column in fcColl.GetColumnCollection(DomainType.QuantitiveDomain))
				{
					idx = qst.GetColumnCollection().GetIndexByID(column.ID);
					int num = (int)idx;
					foreach(ImputeRule ir in column.imputeRuleCollection)
					{
						//≈сли параметр можно восстановить по регрессии
						if (ir.ImputeType == ImputeTypes.Statistical)
						{
							long id_col_x = qst.GetColumnCollection().GetIndexByID(Int32.Parse(ir.Parameter));
							
							//≈сли св€занную колонку не требуетс€ импутировать
							if (fcColl.FindByID(Int32.Parse(ir.Parameter)) != null)
							{							
								double a, b;
								Maths.LinearRegression(this.all_data, (int)id_col_x, (int)idx, out a, out b);

								corrected[num] = a * (double)val[(int)id_col_x] + b;	

								break;
							}
						}	
						
						if (ir.ImputeType == ImputeTypes.HotDeck)
						{						
							//восстанавливаем по методу ближайшего соседа
							if (ngb != null)
								corrected[num] = ngb[num];
							else
								corrected[num] = null;
						}
					}
				}
			}
		}
	}
}
