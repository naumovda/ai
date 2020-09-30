using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RCEIS
{
	/// <summary>
	/// Summary description for FormDomainView.
	/// </summary>
	public class FormDomainView : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Panel panel1;
		public System.Windows.Forms.ListView lvDomains;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button closeButton;
		private System.Windows.Forms.Splitter splitter1;
		public System.Windows.Forms.ListView lvDomainValues;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.Label label1;

		public RCEIS EIS;

		public FormDomainView()
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.lvDomains = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.label4 = new System.Windows.Forms.Label();
			this.closeButton = new System.Windows.Forms.Button();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.lvDomainValues = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.closeButton);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.panel1.Location = new System.Drawing.Point(666, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(112, 543);
			this.panel1.TabIndex = 14;
			// 
			// lvDomains
			// 
			this.lvDomains.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvDomains.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader1,
																						this.columnHeader2,
																						this.columnHeader4,
																						this.columnHeader3,
																						this.columnHeader5});
			this.lvDomains.Dock = System.Windows.Forms.DockStyle.Top;
			this.lvDomains.FullRowSelect = true;
			this.lvDomains.GridLines = true;
			this.lvDomains.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvDomains.Location = new System.Drawing.Point(0, 23);
			this.lvDomains.MultiSelect = false;
			this.lvDomains.Name = "lvDomains";
			this.lvDomains.Size = new System.Drawing.Size(666, 265);
			this.lvDomains.TabIndex = 18;
			this.lvDomains.View = System.Windows.Forms.View.Details;
			this.lvDomains.SelectedIndexChanged += new System.EventHandler(this.lvDomains_SelectedIndexChanged);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "№";
			this.columnHeader1.Width = 31;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Обозначение";
			this.columnHeader2.Width = 251;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Тип";
			this.columnHeader4.Width = 175;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Мин. ";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Макс.";
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(0, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(666, 23);
			this.label4.TabIndex = 17;
			this.label4.Text = "Домены показателей";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// closeButton
			// 
			this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.closeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.closeButton.Location = new System.Drawing.Point(8, 11);
			this.closeButton.Name = "closeButton";
			this.closeButton.Size = new System.Drawing.Size(96, 23);
			this.closeButton.TabIndex = 7;
			this.closeButton.Text = "Закрыть";
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 288);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(666, 3);
			this.splitter1.TabIndex = 19;
			this.splitter1.TabStop = false;
			// 
			// lvDomainValues
			// 
			this.lvDomainValues.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvDomainValues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.columnHeader6,
																							 this.columnHeader7,
																							 this.columnHeader8});
			this.lvDomainValues.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvDomainValues.FullRowSelect = true;
			this.lvDomainValues.GridLines = true;
			this.lvDomainValues.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvDomainValues.Location = new System.Drawing.Point(0, 314);
			this.lvDomainValues.Name = "lvDomainValues";
			this.lvDomainValues.Size = new System.Drawing.Size(666, 229);
			this.lvDomainValues.TabIndex = 22;
			this.lvDomainValues.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "№";
			this.columnHeader6.Width = 31;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Значение";
			this.columnHeader7.Width = 89;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Описание";
			this.columnHeader8.Width = 458;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 291);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(666, 23);
			this.label1.TabIndex = 21;
			this.label1.Text = "Значения показателей";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormDomainView
			// 
			this.AcceptButton = this.closeButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(778, 543);
			this.ControlBox = false;
			this.Controls.Add(this.lvDomainValues);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.lvDomains);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDomainView";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Домены показателей";
			this.Load += new System.EventHandler(this.lvDomains_SelectedIndexChanged);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		private void lvDomains_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			EIS.UpdateDomainValues(lvDomains, lvDomainValues);
		}
	}
}
