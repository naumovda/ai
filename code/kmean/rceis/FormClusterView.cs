using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RCEIS
{
	/// <summary>
	/// Summary description for FormClusterView.
	/// </summary>
	public class FormClusterView : System.Windows.Forms.Form
	{
		public RCEIS EIS;

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.ListView lvSplittingClusters;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ListView lvClusterParams;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormClusterView()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.closeButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.lvSplittingClusters = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.lvClusterParams = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.closeButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(612, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(112, 747);
			this.panel1.TabIndex = 15;
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.closeButton.Location = new System.Drawing.Point(8, 4);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(96, 23);
			this.closeButton.TabIndex = 7;
			this.closeButton.Text = "Закрыть";
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(0, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(612, 23);
			this.label4.TabIndex = 19;
			this.label4.Text = "Кластеры";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lvSplittingClusters
			// 
			this.lvSplittingClusters.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvSplittingClusters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								  this.columnHeader6,
																								  this.columnHeader7,
																								  this.columnHeader8});
			this.lvSplittingClusters.Dock = System.Windows.Forms.DockStyle.Top;
			this.lvSplittingClusters.FullRowSelect = true;
			this.lvSplittingClusters.GridLines = true;
			this.lvSplittingClusters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvSplittingClusters.Location = new System.Drawing.Point(0, 23);
			this.lvSplittingClusters.Name = "lvSplittingClusters";
			this.lvSplittingClusters.Size = new System.Drawing.Size(612, 245);
			this.lvSplittingClusters.TabIndex = 20;
			this.lvSplittingClusters.View = System.Windows.Forms.View.Details;
			this.lvSplittingClusters.SelectedIndexChanged += new System.EventHandler(this.lvSplittingClusters_SelectedIndexChanged);
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "№";
			this.columnHeader6.Width = 30;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Обозначение";
			this.columnHeader7.Width = 80;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Количество элементов";
			this.columnHeader8.Width = 194;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 268);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(612, 3);
			this.splitter1.TabIndex = 21;
			this.splitter1.TabStop = false;
			// 
			// lvClusterParams
			// 
			this.lvClusterParams.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvClusterParams.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							  this.columnHeader1,
																							  this.columnHeader4,
																							  this.columnHeader2,
																							  this.columnHeader5,
																							  this.columnHeader9,
																							  this.columnHeader10,
																							  this.columnHeader11});
			this.lvClusterParams.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvClusterParams.FullRowSelect = true;
			this.lvClusterParams.GridLines = true;
			this.lvClusterParams.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvClusterParams.Location = new System.Drawing.Point(0, 294);
			this.lvClusterParams.Name = "lvClusterParams";
			this.lvClusterParams.Size = new System.Drawing.Size(612, 453);
			this.lvClusterParams.TabIndex = 23;
			this.lvClusterParams.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 30;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Код";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Обозначение";
			this.columnHeader2.Width = 80;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Мин.";
			this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Макс.";
			this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Среднее";
			this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "Ст. откл.";
			this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 271);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(612, 23);
			this.label1.TabIndex = 22;
			this.label1.Text = "Показатели кластера";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormClusterView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(724, 747);
			this.ControlBox = false;
			this.Controls.Add(this.lvClusterParams);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.lvSplittingClusters);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormClusterView";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Параметры кластера";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void lvSplittingClusters_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			EIS.UpdateClusterParams(lvSplittingClusters, lvClusterParams);
		}
	}
}
