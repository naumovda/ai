using System;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RCEIS
{
	/// <summary>
	/// Summary description for FormRegionView.
	/// </summary>
		
	public class FormRegionView : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Panel panel;
		private System.Windows.Forms.Button button1;

		public SqlConnection conn;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.ListView lvRegions;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormRegionView()
		{
			InitializeComponent();
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
			this.panel = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.lvRegions = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.label1 = new System.Windows.Forms.Label();
			this.panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel
			// 
			this.panel.Controls.Add(this.button1);
			this.panel.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel.Location = new System.Drawing.Point(546, 0);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(88, 279);
			this.panel.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.Location = new System.Drawing.Point(8, 8);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "Закрыть";
			// 
			// lvRegions
			// 
			this.lvRegions.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvRegions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader6,
																						this.columnHeader7,
																						this.columnHeader8});
			this.lvRegions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvRegions.FullRowSelect = true;
			this.lvRegions.GridLines = true;
			this.lvRegions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvRegions.Location = new System.Drawing.Point(0, 23);
			this.lvRegions.Name = "lvRegions";
			this.lvRegions.Size = new System.Drawing.Size(546, 256);
			this.lvRegions.TabIndex = 24;
			this.lvRegions.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "№";
			this.columnHeader6.Width = 31;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "ОКАТО";
			this.columnHeader7.Width = 89;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Наименование";
			this.columnHeader8.Width = 400;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(546, 23);
			this.label1.TabIndex = 23;
			this.label1.Text = "Регионы";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormRegionView
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(634, 279);
			this.ControlBox = false;
			this.Controls.Add(this.lvRegions);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormRegionView";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Просмотр регионов";
			this.panel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

	}
}
