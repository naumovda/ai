using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace RCEIS
{
	/// <summary>
	/// Summary description for FormRegion.
	/// </summary>
	public class FormRegion : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.TextBox teOKATO;
		public System.Windows.Forms.TextBox teName;
		private System.Windows.Forms.Button btOK;
		private System.Windows.Forms.Button btCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormRegion()
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
			this.label1 = new System.Windows.Forms.Label();
			this.teOKATO = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.teName = new System.Windows.Forms.TextBox();
			this.btOK = new System.Windows.Forms.Button();
			this.btCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "ОКАТО :";
			// 
			// teOKATO
			// 
			this.teOKATO.Location = new System.Drawing.Point(128, 8);
			this.teOKATO.Name = "teOKATO";
			this.teOKATO.TabIndex = 1;
			this.teOKATO.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Название региона :";
			// 
			// teName
			// 
			this.teName.Location = new System.Drawing.Point(128, 40);
			this.teName.Name = "teName";
			this.teName.Size = new System.Drawing.Size(312, 20);
			this.teName.TabIndex = 3;
			this.teName.Text = "";
			// 
			// btOK
			// 
			this.btOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btOK.Location = new System.Drawing.Point(448, 8);
			this.btOK.Name = "btOK";
			this.btOK.TabIndex = 4;
			this.btOK.Text = "ОК";
			this.btOK.Click += new System.EventHandler(this.btOK_Click);
			// 
			// btCancel
			// 
			this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btCancel.Location = new System.Drawing.Point(448, 40);
			this.btCancel.Name = "btCancel";
			this.btCancel.TabIndex = 5;
			this.btCancel.Text = "Отмена";
			this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
			// 
			// FormRegion
			// 
			this.AcceptButton = this.btOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btCancel;
			this.ClientSize = new System.Drawing.Size(530, 71);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.btCancel,
																		  this.btOK,
																		  this.teName,
																		  this.label2,
																		  this.teOKATO,
																		  this.label1});
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormRegion";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Редактирование региона";
			this.ResumeLayout(false);

		}
		#endregion

		private void btOK_Click(object sender, System.EventArgs e)
		{
			Close();		
		}

		private void btCancel_Click(object sender, System.EventArgs e)
		{
			Close();
		}
	}
}
