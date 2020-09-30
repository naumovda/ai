using System;
using System.Text;
using System.Configuration;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RCEIS
{
	public struct ColumnValue
	{
		public Column column;
		public DomainValue value;
	}

	public class RCEIS
	{
		private bool is_connected;

		public SqlConnection conn;

		public RegionCollection				regionCollection;
		public QuestionarieCollection		questionarieCollection;
		public ClusterSplittingCollection	csCollection;
		public DomainCollection				domainCollection;
		public ProtocolRecordCollection		protocolCollection;
	
		public long failedAllEditCount;
		public long failedQntEditCount;
		public long failedQltEditCount;

		public long failedAllColumnCount;
		public long failedQntColumnCount;
		public long failedQltColumnCount;

		public Region region;
		
		public ControlImpute controlImpute;

		public RCEIS()
		{
			is_connected = false;	
			regionCollection		= new RegionCollection();
			questionarieCollection	= new QuestionarieCollection();
			csCollection			= new ClusterSplittingCollection(); 
			domainCollection		= new DomainCollection();
			protocolCollection		= new ProtocolRecordCollection();
		}

		public bool Connected
		{
			get
			{
				return is_connected;
			}
			set
			{
				is_connected = value;
			}
		}

		public void Connect(string cstr)
		{
			StringBuilder result = new StringBuilder();
			conn = new SqlConnection(cstr);
			try
			{
				conn.Open();
			} 
			catch(Exception ex) 
			{
				MessageBox.Show(ex.Message);
				return;
			}
			is_connected = true;
		}

		public void Disconnect()
		{
			if (is_connected)
			{
				is_connected = false;
				conn.Close();
			}
		}

		public void LoadRegionCollection()
		{
			if (is_connected) regionCollection.Load(conn);
		}

		public void LoadQuestionarieCollection()
		{
			if (is_connected) questionarieCollection.Load(conn, domainCollection);
		}

		public void LoadClusterSplittingCollection()
		{
			if (is_connected) csCollection.Load(conn, questionarieCollection);
		}

		public void LoadDomainCollection()
		{
			if (is_connected) domainCollection.Load(conn);
		}

		public void LoadProtocolCollection()
		{
			if (is_connected) protocolCollection.Load(conn, questionarieCollection);
		}

		public void UpdateProtocol(ListView lv)
		{
			lv.Items.Clear();

			int i = 0;
			foreach(ProtocolRecord pr in protocolCollection)
			{
				i++;
				ListViewItem li = lv.Items.Add(i.ToString());
				
				li.SubItems.Add(pr.Writed.ToShortDateString());
				li.SubItems.Add(pr.Questionarie.ToString());
				li.SubItems.Add(pr.ID_Value.ToString());
				li.SubItems.Add(pr.Column.ToString());
				li.SubItems.Add(pr.OldValue.ToString());
				li.SubItems.Add(pr.NewValue.ToString());
				li.SubItems.Add(ImputeRule.ToString((ImputeTypes)pr.ID_RuleType));

				li.Tag = pr;
			}
		}

		public void UpdateRegion(ComboBox cb)
		{          
			cb.Items.Clear();

			foreach(Region region in regionCollection)
			{
				cb.Items.Add(region);
			}
		}
		
		public void UpdateQuestionarie(ComboBox cb)
		{			           
			cb.Items.Clear();

			foreach(Questionarie qst in questionarieCollection)
			{
				cb.Items.Add(qst);
			}
		}

		public void UpdateQuestionarie(TreeView tv)
		{
			LoadQuestionarieCollection();
            
			tv.Nodes.Clear();

			foreach(Questionarie qst in questionarieCollection)
			{
				TreeNode tn = tv.Nodes.Add(qst.ToString());
				tn.ImageIndex = 0;
				tn.SelectedImageIndex = 0;
				tn.Tag = qst;

				foreach(Table table in qst.tableCollection)
				{
					TreeNode tn_child = tn.Nodes.Add(table.ToString());
					tn_child.ImageIndex = 1;
					tn_child.SelectedImageIndex = 1;
					tn_child.Tag = table;

					foreach(Column column in table.columnCollection)
					{
						TreeNode tn_child_2 = tn_child.Nodes.Add(column.ToString());
						tn_child_2.ImageIndex = 2;
						tn_child_2.SelectedImageIndex = 2;
						tn_child_2.Tag = column;
					}
				}
			}
		}

		public void UpdateClustersSplitting(TreeView tv, long id_region)
		{
			LoadClusterSplittingCollection();
			tv.Nodes.Clear();
			foreach(ClusterSplitting cs in csCollection)
			{
				if (cs.ID_Region == id_region)
				{
					Questionarie qst = questionarieCollection.FindByID(cs.ID_Form);
					TreeNode tn = tv.Nodes.Add(qst.ToString());
					tn.ImageIndex = 0;
					tn.SelectedImageIndex = 0;
					tn.Tag = cs;

					TreeNode tn_child1 = tn.Nodes.Add("Показатели");
					tn_child1.ImageIndex = 1;
					tn_child1.SelectedImageIndex = 1;
					tn_child1.Tag = cs.columnCollection;

					foreach(Column column in cs.columnCollection)
					{
						TreeNode tn_child3 = tn_child1.Nodes.Add(column.ToString());
						tn_child3.ImageIndex = 2;
						tn_child3.SelectedImageIndex = 2;
						tn_child3.Tag = column;
					}

					TreeNode tn_child2 = tn.Nodes.Add("Кластеры");
					tn_child2.ImageIndex = 1;
					tn_child2.SelectedImageIndex = 1;
					tn_child2.Tag = cs.clusterCollection;
					
					foreach(Cluster cluster in cs.clusterCollection)
					{
						TreeNode tn_child3 = tn_child2.Nodes.Add(cluster.ToString() +  "[ " + cluster.RecordCount.ToString("D5") + " ]");
						tn_child3.ImageIndex = 2;
						tn_child3.SelectedImageIndex = 2;
						tn_child3.Tag = cluster;
					}
				}
			}
		}

		public void UpdateSplittingColumns(TreeView tv, ListView lv)
		{
			TreeNode node = tv.SelectedNode;

			if (node != null)
			{	
				if (node.Parent != null)
					return;

				ClusterSplitting cs = (ClusterSplitting)(node.Tag);
				
				lv.Items.Clear();	
				foreach(Column column in cs.columnCollection)
				{
					ListViewItem li = lv.Items.Add(column.Number.ToString());
					li.SubItems.Add(column.UniqueCode.ToString());
					li.SubItems.Add(column.ShortName.ToString());
					li.SubItems.Add(column.Title.ToString());
					li.Tag = column;
				}
			}
		}

		public void UpdateClusterParams(ListView lvCluster, ListView lv)
		{
			foreach(ListViewItem liCluster in lvCluster.SelectedItems)
			{
				Cluster cluster = (Cluster)(liCluster.Tag);
				
				UpdateClusterParams(cluster, lv);
			}
		}

		public void UpdateClusterParams(Cluster cluster, ListView lv)
		{
			lv.Items.Clear();

			int i=0;
			foreach(ClusterParam param in cluster.clusterParamCollection)
			{
				i++;
				ListViewItem li = lv.Items.Add(i.ToString());
				li.SubItems.Add(param.column.Code.ToString());
				li.SubItems.Add(param.column.ShortName);
				li.SubItems.Add(param.MinValue.ToString("F3"));
				li.SubItems.Add(param.MaxValue.ToString("F3"));
				li.SubItems.Add(param.Mean.ToString("F3"));
				li.SubItems.Add(param.StdDev.ToString("F3"));
				li.Tag = param;
			}
		}

		public void UpdateSplittingClusters(TreeView tv, ListView lv)
		{
			TreeNode node = tv.SelectedNode;

			if (node != null)
			{	
				if (node.Parent != null)
					return;

				ClusterSplitting cs = (ClusterSplitting)(node.Tag);
				
				lv.Items.Clear();	
				foreach(Cluster cluster in cs.clusterCollection)
				{
					ListViewItem li = lv.Items.Add(cluster.Number.ToString());
					li.SubItems.Add(cluster.Name);
					li.SubItems.Add(cluster.RecordCount.ToString());
					li.SubItems.Add(cluster.DonorCount.ToString());
					li.Tag = cluster;
				}
			}
		}

		public void UpdateDomains(ListView lv)
		{
			LoadDomainCollection();

			lv.Items.Clear();

			int i = 0;
			foreach(Domain domain in domainCollection)
			{
				i++;
				ListViewItem li = lv.Items.Add(i.ToString());
				li.SubItems.Add(domain.ToString());
				if (domain.DomainType == DomainType.QuantitiveDomain)
				{
					li.SubItems.Add("Количественный");
					li.SubItems.Add(domain.MinValue.ToString());
					li.SubItems.Add(domain.MaxValue.ToString());
				}
				else
					li.SubItems.Add("Качественный");
				li.Tag = domain;
			}
		}

		public void UpdateDomainValues(ListView lvDomain, ListView lv)
		{
			lv.Items.Clear();

			foreach(ListViewItem lidomain in lvDomain.SelectedItems)
			{            
				int i = 0;
				
				Domain domain = (Domain)lidomain.Tag;

				foreach(DomainValue domain_value in domain.valueCollection)
				{
					i++;
					ListViewItem li = lv.Items.Add(i.ToString());					
                    li.SubItems.Add(domain_value.Value.ToString());
					li.SubItems.Add(domain_value.Name);
					li.Tag = domain_value;
				}
			}
		}

		public void UpdateColumnValues(ListView lv, long id_form, DomainType domain_type)
		{
	
			//обновление списка показателей
			lv.Items.Clear();
			
			int i = 0;
			bool first;
			System.Drawing.Color color = System.Drawing.Color.PaleGreen;

			Questionarie qst = questionarieCollection.FindByID(id_form);

			foreach(Column column in qst.GetColumnCollection())
			{
				if (color == System.Drawing.Color.PaleGreen)
					color = System.Drawing.Color.OldLace;
				else
					color = System.Drawing.Color.PaleGreen;

				if (column.Domain.DomainType == domain_type)
				{
					if (column.Domain.DomainType == DomainType.QuantitiveDomain)
					{
						i++;
						ListViewItem li = lv.Items.Add(i.ToString());					
						li.SubItems.Add(column.ToString());
						li.SubItems.Add("");
						li.BackColor = System.Drawing.Color.OldLace;

						ColumnValue cv = new ColumnValue();
						cv.column = column;
						cv.value = null;

						li.Tag = cv;

					}
					else
					{		
						first = true;			
						foreach(DomainValue domain_value in column.Domain.valueCollection)
						{
							i++;
							ListViewItem li = lv.Items.Add(i.ToString());					
							if (!first)
								li.SubItems.Add(" ");
							else
								li.SubItems.Add(column.ToString());
							li.SubItems.Add(domain_value.ToString());
							li.BackColor = color;

							ColumnValue cv = new ColumnValue();
							cv.column = column;
							cv.value = domain_value;

							li.Tag = cv;

							first = false;
						}
					}
				}
			}

			//обновление столбцов
			int k = lv.Columns.Count - 3;
			for(int j=1; j<=k; j++)
			{
				lv.Columns.RemoveAt(3);
			}
			
			k = 0;
			foreach(EditRule rule in qst.editRuleCollection)
			{
				if (
					(rule.RuleType == RuleTypes.Qualitative)&&(domain_type == DomainType.QualitativeDomain)||
					(rule.RuleType != RuleTypes.Qualitative)&&(domain_type != DomainType.QualitativeDomain)
					)
				{
					k++;
					ColumnHeader ch = lv.Columns.Add(k.ToString(), 30, HorizontalAlignment.Center);
				
					//добавляем для всех строк SubItems
					foreach(ListViewItem lvItem in lv.Items)
					{
						ColumnValue cv = (ColumnValue)(lvItem.Tag);
						lvItem.SubItems.Add(rule.GetCoeeficient(cv.column, cv.value).ToString());
					}
				}
			}
		}


		public void UpdateQuestionarieColumns(TreeView tv, long id_questionarie)
		{
			Questionarie qst = questionarieCollection.FindByID(id_questionarie);
            
			tv.Nodes.Clear();

			ColumnCollection cc = qst.GetColumnCollection();

			foreach(Column column in cc)
			{
				TreeNode tn_child = tv.Nodes.Add(column.ToString());
				tn_child.ImageIndex = 2;
				tn_child.SelectedImageIndex = 2;
				tn_child.Tag = column;

				foreach (ImputeRule ir in column.imputeRuleCollection)
				{
					TreeNode tn_child_1 = tn_child.Nodes.Add(ir.ToString());
					tn_child_1.ImageIndex = 3;
					tn_child_1.SelectedImageIndex = 3;
					tn_child_1.Tag = ir;
				}
			}
		}

		public void UpdateQuestionarieColumns(ListView lv, long id_questionarie)
		{
			Questionarie qst = questionarieCollection.FindByID(id_questionarie);
            
			lv.Items.Clear();

			ColumnCollection cc = qst.GetColumnCollection();

			int i = 0;

			foreach(Column column in cc)
			{
				ListViewItem li = lv.Items.Add((++i).ToString());			
				
				li.SubItems.Add(column.UniqueCode.ToString());
				li.SubItems.Add(column.ShortName);
				li.SubItems.Add(column.Title);
                li.BackColor = System.Drawing.Color.NavajoWhite;	
				li.Tag = column;

				foreach (ImputeRule ir in column.imputeRuleCollection)
				{
					ListViewItem li_r = lv.Items.Add((++i).ToString());			
				
					li_r.SubItems.Add("");
					li_r.SubItems.Add("");
					li_r.SubItems.Add("");
					li_r.SubItems.Add(ir.ToString());
					li_r.BackColor = System.Drawing.Color.OldLace;		
					
					li_r.Tag = ir;
				}
			}
		}

		public void UpdateRegions(ListView lv)
		{
			lv.Items.Clear();

			int i=0;

			foreach(Region region in regionCollection)
			{
				i++;
				ListViewItem li = lv.Items.Add(i.ToString());					
				li.SubItems.Add(region.OKATO);
				li.SubItems.Add(region.Name);
				li.Tag = region;
			}
		}

		public void UpdateQuestionarieColumns(ListView lv, long id_form, bool applyformat)
		{
			Questionarie qst = this.questionarieCollection.FindByID(id_form);

			ColumnCollection cc = qst.GetColumnCollection();
			
			lv.Items.Clear();	

			foreach(Column column in cc)
			{
				ListViewItem li = lv.Items.Add(column.Number.ToString());
				li.SubItems.Add(column.UniqueCode.ToString());
				li.SubItems.Add(column.ShortName.ToString());
				li.SubItems.Add(column.Title.ToString());
				li.SubItems.Add("");

				if(applyformat)
					switch(column.Domain.DomainType)
					{
						case DomainType.QualitativeDomain:						
							li.BackColor = System.Drawing.Color.PaleGreen;
							break;
						case DomainType.QuantitiveDomain:
							li.BackColor = System.Drawing.Color.OldLace;
							break;
					}

				li.Tag = column;
			}
		}

		public void UpdateValueData(ListView lv, ComboBox cb, long id)
		{
			Questionarie qst = (Questionarie)cb.Items[cb.SelectedIndex];
		
			Value v = new Value(qst);			
	
			v.Load(conn, id, qst);

			int count = qst.GetColumnCollection().Count;

			for(int i=0; i<count; i++)
			{
				lv.Items[i].SubItems[4].Text = v.ToString(qst, i);
			}			

			region = regionCollection.FindByOKATO(v.OKATO);
		}

		public void DoOnlyImpute(ComboBox cb, long id, ListView lv, Value val, Region region, ref double[,] data)
		{
			Questionarie qst = (Questionarie)cb.Items[cb.SelectedIndex];

			controlImpute = new ControlImpute(conn, qst);

			controlImpute.val = val/* LoadValue(conn, id, regionCollection)*/;

			controlImpute.region = region;
		
			controlImpute.CheckEdits();
			
			controlImpute.LocaliseError();

			controlImpute.clusterSplitting = this.csCollection.Find(region.ID, controlImpute.qst.ID);

			controlImpute.FindNearestCluster();

			controlImpute.FindNearestValue(ref data);

			controlImpute.DoImpute(DomainType.QualitativeDomain);

			controlImpute.DoImpute(DomainType.QuantitiveDomain);
		}

		public void UpdateEditChecking(ComboBox cb, long id, ListView lv, Value val, Region region)
		{
			Questionarie qst = (Questionarie)cb.Items[cb.SelectedIndex];

			controlImpute = new ControlImpute(conn, qst);

			controlImpute.val = val/* LoadValue(conn, id, regionCollection)*/;

			controlImpute.region = region;
		
			controlImpute.CheckEdits();

			
			failedQntEditCount = controlImpute.failedQntEdits.Count;
			failedQltEditCount = controlImpute.failedQltEdits.Count;
			failedAllEditCount = failedQntEditCount + failedQltEditCount;


			lv.Items.Clear();

			int i=0;

			foreach(EditRule er in controlImpute.failedQntEdits)
			{		
				ListViewItem li = lv.Items.Add((++i).ToString());
                
				li.Checked = false;
				li.SubItems.Add(er.ToDescriptionString(qst));

				li.BackColor = System.Drawing.Color.LightPink;
			}
			
			foreach(EditRule er in controlImpute.failedQltEdits)
			{		
				ListViewItem li = lv.Items.Add((++i).ToString());
                
				li.Checked = false;
				li.SubItems.Add(er.ToDescriptionString(qst));

				li.BackColor = System.Drawing.Color.LightPink;
			}

			foreach(EditRule er in controlImpute.satisfiedEdits)
			{		
				ListViewItem li = lv.Items.Add((++i).ToString());
                
				li.Checked = true;
				li.SubItems.Add(er.ToDescriptionString(qst));

				li.BackColor = System.Drawing.Color.PaleGreen;
			}
		}

		public void UpdateFailedEdits(ListView lv)
		{
			//обновление столбцов
			int k = lv.Columns.Count - 4;
			for(int j=1; j<=k; j++)
			{
				lv.Columns.RemoveAt(4);
			}

			foreach(ListViewItem lvItem in lv.Items)
			{
				lvItem.SubItems.RemoveAt(4);
			}

			k = 0;
			foreach(EditRule rule in controlImpute.failedEdits)
			{
				k++;
				ColumnHeader ch = lv.Columns.Add(k.ToString(), 30, HorizontalAlignment.Center);
				
				//добавляем для всех строк SubItems

				foreach(ListViewItem lvItem in lv.Items)
				{
					Column column = (Column)(lvItem.Tag);
					if(rule.IncludeExplicitly(column))
					{
						lvItem.SubItems.Add("1");						
					}
					else
						lvItem.SubItems.Add("0");
				}
			}
		}

		public void UpdateLocalizedError(ListView lv)
		{
			//обновление столбцов

			controlImpute.LocaliseError();

			foreach(ListViewItem lvItem in lv.Items)
			{
				Column column = (Column)(lvItem.Tag);
				
				if( controlImpute.FailedColumnCollection.FindByID(column.ID) != null )
				{
					if (column.Domain.DomainType == DomainType.QualitativeDomain)
						failedQltColumnCount++;
					else
						failedQntColumnCount++;

					lvItem.BackColor = System.Drawing.Color.LightPink;
				}
				else
				{
					lvItem.BackColor = System.Drawing.Color.PaleGreen;
				}
			}

			failedAllColumnCount = failedQltColumnCount + failedQntColumnCount;
		}

		public void UpdateClusterIndentifiction(ListView lv, TextBox te1, TextBox te2, TextBox te3)
		{
			Region region = regionCollection.FindByOKATO(controlImpute.val.OKATO);

			controlImpute.clusterSplitting = this.csCollection.Find(region.ID, controlImpute.qst.ID);

			controlImpute.FindNearestCluster();

			te1.Text = controlImpute.clusterSplitting.clusterCollection.Count.ToString();
			
			te2.Text = controlImpute.nearestCluster.Name;

			te3.Text = region.Name;

			UpdateClusterParams(controlImpute.nearestCluster, lv);
		}

		public void GenerateAllImputeRules(long id_form)
		{
			Questionarie qst = questionarieCollection.FindByID(id_form);

			foreach(Column column in qst.GetColumnCollection())
			{
				column.GenerateImputeRules(conn);
			}
		}

		public void UpdateNearestValue(TextBox teTotal, TextBox teDonor, TextBox teNearestValue)
		{
			controlImpute.FindNearestValue(ref controlImpute.all_data);

			teTotal.Text = controlImpute.totalValuesCount.ToString();
			teDonor.Text = controlImpute.donorValuesCount.ToString();
			
			if (controlImpute.ngb != null)
				teNearestValue.Text = controlImpute.ngb.ToString();
			else
				teNearestValue.Text = "-";
		}

		public void UpdateImputedValue(ListView lv)
		{
			controlImpute.DoImpute(DomainType.QualitativeDomain);
			controlImpute.DoImpute(DomainType.QuantitiveDomain);

			int i = 0;

			foreach(ListViewItem li in lv.Items)
			{
				while(li.SubItems.Count > 5)
					li.SubItems.RemoveAt(5);
				li.SubItems[4].Text = (controlImpute.val.ToString(controlImpute.qst, i));
				li.SubItems.Add(controlImpute.corrected.ToString(controlImpute.qst, i++));
			}
		}

		public void UpdateQuestionarieInfo(TreeView tv, ListView lv)
		{
			TreeNode ti = tv.SelectedNode ;
			while (ti.Parent != null)
				ti = ti.Parent;

			Questionarie qst = (Questionarie)ti.Tag;

			lv.Items.Clear();

			ListViewItem li = lv.Items.Add("1");
			li.SubItems.Add("Наименование формы");
			li.SubItems.Add(qst.Name);

			li = lv.Items.Add("2");
			li.SubItems.Add("Описание");
			li.SubItems.Add(qst.Comment);

			li = lv.Items.Add("3");
			li.SubItems.Add("Всего записей");
			li.SubItems.Add(qst.CountID.ToString());

			li = lv.Items.Add("4");
			li.SubItems.Add("Минимальный номер записи");
			li.SubItems.Add(qst.MinID.ToString());

			li = lv.Items.Add("5");
			li.SubItems.Add("Максимальый номер записи");
			li.SubItems.Add(qst.MaxID.ToString());
		}
	}
}
