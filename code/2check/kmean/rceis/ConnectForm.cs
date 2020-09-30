using System;
using System.Configuration;
using System.Windows.Forms;
using System.Text;
using System.Data.SqlClient;

public class ConnectForm : System.Windows.Forms.Form
{
	private System.Windows.Forms.Button connectButton;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Button cancelButton;
	public  System.Windows.Forms.TextBox teConnect;
	/// <summary>
	/// Required designer variable.
	/// </summary>
	private System.ComponentModel.Container components = null;

	public ConnectForm()
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
		this.connectButton = new System.Windows.Forms.Button();
		this.teConnect = new System.Windows.Forms.TextBox();
		this.label1 = new System.Windows.Forms.Label();
		this.cancelButton = new System.Windows.Forms.Button();
		this.SuspendLayout();
		// 
		// connectButton
		// 
		this.connectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.connectButton.DialogResult = System.Windows.Forms.DialogResult.OK;
		this.connectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.connectButton.Location = new System.Drawing.Point(488, 6);
		this.connectButton.Name = "connectButton";
		this.connectButton.Size = new System.Drawing.Size(96, 23);
		this.connectButton.TabIndex = 2;
		this.connectButton.Text = "Подключить!";
		this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
		// 
		// teConnect
		// 
		this.teConnect.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.teConnect.Location = new System.Drawing.Point(8, 28);
		this.teConnect.Name = "teConnect";
		this.teConnect.Size = new System.Drawing.Size(472, 13);
		this.teConnect.TabIndex = 4;
		this.teConnect.Text = "Data Source=(local);Integrated security=SSPI;Initial Catalog=RCEIS;";
		// 
		// label1
		// 
		this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
		this.label1.Location = new System.Drawing.Point(8, 8);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(472, 20);
		this.label1.TabIndex = 5;
		this.label1.Text = "Строка подключения :";
		// 
		// cancelButton
		// 
		this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
		this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.cancelButton.Location = new System.Drawing.Point(488, 34);
		this.cancelButton.Name = "cancelButton";
		this.cancelButton.Size = new System.Drawing.Size(96, 23);
		this.cancelButton.TabIndex = 6;
		this.cancelButton.Text = "Отмена";
		this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
		// 
		// ConnectForm
		// 
		this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
		this.ClientSize = new System.Drawing.Size(592, 67);
		this.ControlBox = false;
		this.Controls.Add(this.cancelButton);
		this.Controls.Add(this.label1);
		this.Controls.Add(this.teConnect);
		this.Controls.Add(this.connectButton);
		this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		this.MaximizeBox = false;
		this.MinimizeBox = false;
		this.Name = "ConnectForm";
		this.ShowInTaskbar = false;
		this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Подключение";
		this.Load += new System.EventHandler(this.ConnectForm_Load);
		this.ResumeLayout(false);

	}
	#endregion

	private void connectButton_Click(object sender, System.EventArgs e)
	{
		Close();
	}

	private void cancelButton_Click(object sender, System.EventArgs e)
	{
		Close();
	}

	private void ConnectForm_Load(object sender, System.EventArgs e)
	{
		this.teConnect.Text = ConfigurationSettings.AppSettings["SqlMSDE_ConnectString"];
	}

}