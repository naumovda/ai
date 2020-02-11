using System;
using System.Drawing;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace RCEIS
{

	public enum FormStates
	{
		efsConnected,
		efsDisconnected
	};

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FormMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ToolBarButton tbConnect;
		private System.Windows.Forms.ImageList imageList;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.StatusBarPanel sbpConnectState;
		private System.Windows.Forms.MenuItem miExit;
		private System.Windows.Forms.MenuItem miConnectRCEIS;
		private System.Windows.Forms.MenuItem miConnectTo;
		private System.Windows.Forms.ImageList imageListTree;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem miViewRegions;
		private System.Windows.Forms.Panel pnRegion;
		private System.Windows.Forms.Button btDeleteRegion;
		private System.Windows.Forms.Button btUpdateRegion;
		private System.Windows.Forms.Button btNewRegion;
		private System.Windows.Forms.ComboBox cbRegions;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TreeView tvQuestionarie;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TreeView tvClusters;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.Splitter splitter3;
		private System.Windows.Forms.ListView lvSplittingColumns;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ListView lvSplittingClusters;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Splitter splitter4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.MenuItem miDomainView;
		private System.Windows.Forms.Panel pnEditRule;
		private System.Windows.Forms.ComboBox cbQuestionarie;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Splitter splitter5;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox cbQuestionarieImpute;
		private System.Windows.Forms.Panel pnImputeRule;
		private System.Windows.Forms.ListView lvEditRules;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.RadioButton rgQualitaty;
		private System.Windows.Forms.RadioButton rgQuantity;
		private System.Windows.Forms.ToolBarButton tbCluster;
		private System.Windows.Forms.MenuItem miDisconnect;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TreeView tvImputeColumns;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.Splitter splitter6;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ComboBox cbQuestionarieData;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Panel pnQuestionarieData;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader19;
		private System.Windows.Forms.ListView lvQuestionarieData;
		private System.Windows.Forms.LinkLabel lbFirst;
		private System.Windows.Forms.TextBox teFormID;
		private System.Windows.Forms.LinkLabel lbLast;
		private System.Windows.Forms.Panel panel10;
		private System.Windows.Forms.ColumnHeader columnHeader20;
		private System.Windows.Forms.ColumnHeader columnHeader21;
		private System.Windows.Forms.ColumnHeader columnHeader22;
		private System.Windows.Forms.ColumnHeader columnHeader23;
		private System.Windows.Forms.ColumnHeader columnHeader24;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TabControl tabControl2;
		private System.Windows.Forms.TabPage tabPage6;
		private System.Windows.Forms.TabPage tabPage7;
		private System.Windows.Forms.TabPage tabPage8;
		private System.Windows.Forms.TabPage tabPage9;
		private System.Windows.Forms.Panel panel12;
		private System.Windows.Forms.Panel panel13;
		private System.Windows.Forms.Panel panel14;
		private System.Windows.Forms.Panel panel15;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.ColumnHeader columnHeader25;
		private System.Windows.Forms.ColumnHeader columnHeader26;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.ColumnHeader columnHeader27;
		private System.Windows.Forms.ColumnHeader columnHeader28;
		private System.Windows.Forms.ColumnHeader columnHeader29;
		private System.Windows.Forms.ColumnHeader columnHeader30;
		private System.Windows.Forms.ListView lvClusterParams;
		private System.Windows.Forms.ColumnHeader columnHeader32;
		private System.Windows.Forms.ColumnHeader columnHeader33;
		private System.Windows.Forms.ColumnHeader columnHeader34;
		private System.Windows.Forms.ColumnHeader columnHeader35;
		private System.Windows.Forms.ColumnHeader columnHeader36;
		private System.Windows.Forms.ColumnHeader columnHeader37;
		private System.Windows.Forms.ColumnHeader columnHeader38;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.ColumnHeader columnHeader39;
		private System.Windows.Forms.ColumnHeader columnHeader40;
		private System.Windows.Forms.ColumnHeader columnHeader41;
		private System.Windows.Forms.ColumnHeader columnHeader42;
		private System.Windows.Forms.ColumnHeader columnHeader43;
		private System.Windows.Forms.ColumnHeader columnHeader44;
		private System.Windows.Forms.ColumnHeader columnHeader45;
		public System.Windows.Forms.Panel pnEdit;
		public System.Windows.Forms.ComboBox cbQuestionarieEdit;
		private System.Windows.Forms.ListView lvData;
		private System.Windows.Forms.LinkLabel lbFirstE;
		private System.Windows.Forms.TextBox teFormEID;
		private System.Windows.Forms.LinkLabel lbLastE;
		private System.Windows.Forms.ToolBarButton tbEdits;
		public System.Windows.Forms.ListView lvEditProtocol;

		public RCEIS EIS;
		public System.Windows.Forms.TextBox teEditsAll;
		public System.Windows.Forms.TextBox teEditsAllFail;
		public System.Windows.Forms.TextBox teEditsQNT;
		public System.Windows.Forms.TextBox teEditsQNTFail;
		public System.Windows.Forms.TextBox teEditsQLTFail;
		public System.Windows.Forms.TextBox teEditsQLT;
		public System.Windows.Forms.TextBox teColumnAll;
		public System.Windows.Forms.TextBox teColumnQnt;
		public System.Windows.Forms.TextBox teColumnQlt;
		private System.Windows.Forms.ListView lvFailedEditMatrix;
		public System.Windows.Forms.TextBox teColumnAllFail;
		public System.Windows.Forms.TextBox teColumnQntFail;
		public System.Windows.Forms.TextBox teColumnQltFail;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.TextBox teRegion;
		private System.Windows.Forms.TextBox teNearestCluster;
		private System.Windows.Forms.TextBox teTotalClusters;
		private System.Windows.Forms.ToolBarButton tbGenerateImputeRules;
		private System.Windows.Forms.TabControl tabControlMain;
		private System.Windows.Forms.TabPage tpImputeRules;
		private System.Windows.Forms.ToolBarButton tbNormalizeData;
		private System.Windows.Forms.ToolBarButton tbDonorRecipient;
		private System.Windows.Forms.ColumnHeader columnHeader31;
		private System.Windows.Forms.ColumnHeader columnHeader46;
		private System.Windows.Forms.ColumnHeader columnHeader47;
		private System.Windows.Forms.ColumnHeader columnHeader48;
		private System.Windows.Forms.ColumnHeader columnHeader49;
		private System.Windows.Forms.ListView lvImputeColumns;
		private System.Windows.Forms.ListView lvCorrectedData;
		private System.Windows.Forms.TabPage tbData;
		private System.Windows.Forms.TabPage tbClusters;
		private System.Windows.Forms.TabPage tbEditRules;
		private System.Windows.Forms.TabPage tbAutoCorrect;
		private System.Windows.Forms.TabPage tbProtocol;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.TextBox teTotal;
		private System.Windows.Forms.TextBox teDonor;
		private System.Windows.Forms.TextBox teNearestValue;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.ColumnHeader columnHeader50;
		private System.Windows.Forms.ColumnHeader columnHeader51;
		private System.Windows.Forms.ColumnHeader columnHeader52;
		private System.Windows.Forms.ColumnHeader columnHeader53;
		private System.Windows.Forms.ColumnHeader columnHeader54;
		private System.Windows.Forms.ColumnHeader columnHeader55;
		private System.Windows.Forms.ColumnHeader columnHeader56;
		private System.Windows.Forms.ColumnHeader columnHeader57;
		private System.Windows.Forms.ListView lvProtocol;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.ColumnHeader columnHeader58;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton tbTestImpute;
		private System.Windows.Forms.ListView lvInfo;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.TextBox teEditRulesCount;
		private System.Windows.Forms.Label label36;

		public bool CanChangeEditCheck;

		public FormMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			EIS = new RCEIS();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormMain));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "1",
																													 "Удовлетворенное правило"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.PaleGreen, null);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
																													 "2",
																													 "Неудовлетворенное правило"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.LightPink, null);
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.tbConnect = new System.Windows.Forms.ToolBarButton();
			this.tbNormalizeData = new System.Windows.Forms.ToolBarButton();
			this.tbDonorRecipient = new System.Windows.Forms.ToolBarButton();
			this.tbCluster = new System.Windows.Forms.ToolBarButton();
			this.tbGenerateImputeRules = new System.Windows.Forms.ToolBarButton();
			this.tbEdits = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
			this.tbTestImpute = new System.Windows.Forms.ToolBarButton();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.miConnectRCEIS = new System.Windows.Forms.MenuItem();
			this.miConnectTo = new System.Windows.Forms.MenuItem();
			this.miDisconnect = new System.Windows.Forms.MenuItem();
			this.miExit = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.miDomainView = new System.Windows.Forms.MenuItem();
			this.miViewRegions = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.sbpConnectState = new System.Windows.Forms.StatusBarPanel();
			this.imageListTree = new System.Windows.Forms.ImageList(this.components);
			this.tabControlMain = new System.Windows.Forms.TabControl();
			this.tbData = new System.Windows.Forms.TabPage();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.lvQuestionarieData = new System.Windows.Forms.ListView();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
			this.pnQuestionarieData = new System.Windows.Forms.Panel();
			this.cbQuestionarieData = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.lbFirst = new System.Windows.Forms.LinkLabel();
			this.teFormID = new System.Windows.Forms.TextBox();
			this.lbLast = new System.Windows.Forms.LinkLabel();
			this.label11 = new System.Windows.Forms.Label();
			this.splitter6 = new System.Windows.Forms.Splitter();
			this.lvInfo = new System.Windows.Forms.ListView();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.label10 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tvQuestionarie = new System.Windows.Forms.TreeView();
			this.label2 = new System.Windows.Forms.Label();
			this.tbClusters = new System.Windows.Forms.TabPage();
			this.splitter3 = new System.Windows.Forms.Splitter();
			this.panel5 = new System.Windows.Forms.Panel();
			this.lvSplittingClusters = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader58 = new System.Windows.Forms.ColumnHeader();
			this.label5 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.lvSplittingColumns = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.label4 = new System.Windows.Forms.Label();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.panel3 = new System.Windows.Forms.Panel();
			this.tvClusters = new System.Windows.Forms.TreeView();
			this.label3 = new System.Windows.Forms.Label();
			this.pnRegion = new System.Windows.Forms.Panel();
			this.btDeleteRegion = new System.Windows.Forms.Button();
			this.btUpdateRegion = new System.Windows.Forms.Button();
			this.btNewRegion = new System.Windows.Forms.Button();
			this.cbRegions = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tbEditRules = new System.Windows.Forms.TabPage();
			this.splitter4 = new System.Windows.Forms.Splitter();
			this.panel8 = new System.Windows.Forms.Panel();
			this.lvEditRules = new System.Windows.Forms.ListView();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.label8 = new System.Windows.Forms.Label();
			this.pnEditRule = new System.Windows.Forms.Panel();
			this.teEditRulesCount = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rgQualitaty = new System.Windows.Forms.RadioButton();
			this.rgQuantity = new System.Windows.Forms.RadioButton();
			this.cbQuestionarie = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label35 = new System.Windows.Forms.Label();
			this.tpImputeRules = new System.Windows.Forms.TabPage();
			this.splitter5 = new System.Windows.Forms.Splitter();
			this.panel9 = new System.Windows.Forms.Panel();
			this.lvImputeColumns = new System.Windows.Forms.ListView();
			this.columnHeader31 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader46 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader47 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader48 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader49 = new System.Windows.Forms.ColumnHeader();
			this.panel7 = new System.Windows.Forms.Panel();
			this.tvImputeColumns = new System.Windows.Forms.TreeView();
			this.label9 = new System.Windows.Forms.Label();
			this.pnImputeRule = new System.Windows.Forms.Panel();
			this.cbQuestionarieImpute = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.tbAutoCorrect = new System.Windows.Forms.TabPage();
			this.tabControl2 = new System.Windows.Forms.TabControl();
			this.tabPage6 = new System.Windows.Forms.TabPage();
			this.lvEditProtocol = new System.Windows.Forms.ListView();
			this.columnHeader25 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader26 = new System.Windows.Forms.ColumnHeader();
			this.panel12 = new System.Windows.Forms.Panel();
			this.label15 = new System.Windows.Forms.Label();
			this.teEditsAll = new System.Windows.Forms.TextBox();
			this.teEditsAllFail = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.teEditsQNT = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.teEditsQNTFail = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.teEditsQLTFail = new System.Windows.Forms.TextBox();
			this.teEditsQLT = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.tabPage7 = new System.Windows.Forms.TabPage();
			this.lvFailedEditMatrix = new System.Windows.Forms.ListView();
			this.columnHeader27 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader28 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader29 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader30 = new System.Windows.Forms.ColumnHeader();
			this.panel13 = new System.Windows.Forms.Panel();
			this.label21 = new System.Windows.Forms.Label();
			this.teColumnAll = new System.Windows.Forms.TextBox();
			this.teColumnAllFail = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.teColumnQnt = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.teColumnQntFail = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.teColumnQltFail = new System.Windows.Forms.TextBox();
			this.teColumnQlt = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.label26 = new System.Windows.Forms.Label();
			this.tabPage8 = new System.Windows.Forms.TabPage();
			this.lvClusterParams = new System.Windows.Forms.ListView();
			this.columnHeader32 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader33 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader34 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader35 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader36 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader37 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader38 = new System.Windows.Forms.ColumnHeader();
			this.label27 = new System.Windows.Forms.Label();
			this.panel14 = new System.Windows.Forms.Panel();
			this.label28 = new System.Windows.Forms.Label();
			this.teRegion = new System.Windows.Forms.TextBox();
			this.teNearestCluster = new System.Windows.Forms.TextBox();
			this.label29 = new System.Windows.Forms.Label();
			this.label30 = new System.Windows.Forms.Label();
			this.teTotalClusters = new System.Windows.Forms.TextBox();
			this.label31 = new System.Windows.Forms.Label();
			this.label32 = new System.Windows.Forms.Label();
			this.teTotal = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.teDonor = new System.Windows.Forms.TextBox();
			this.teNearestValue = new System.Windows.Forms.TextBox();
			this.tabPage9 = new System.Windows.Forms.TabPage();
			this.lvCorrectedData = new System.Windows.Forms.ListView();
			this.columnHeader39 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader40 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader41 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader42 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader43 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader44 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader45 = new System.Windows.Forms.ColumnHeader();
			this.panel15 = new System.Windows.Forms.Panel();
			this.panel10 = new System.Windows.Forms.Panel();
			this.lvData = new System.Windows.Forms.ListView();
			this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader23 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader24 = new System.Windows.Forms.ColumnHeader();
			this.pnEdit = new System.Windows.Forms.Panel();
			this.cbQuestionarieEdit = new System.Windows.Forms.ComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.lbFirstE = new System.Windows.Forms.LinkLabel();
			this.teFormEID = new System.Windows.Forms.TextBox();
			this.lbLastE = new System.Windows.Forms.LinkLabel();
			this.label14 = new System.Windows.Forms.Label();
			this.tbProtocol = new System.Windows.Forms.TabPage();
			this.lvProtocol = new System.Windows.Forms.ListView();
			this.columnHeader50 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader51 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader52 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader53 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader54 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader55 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader56 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader57 = new System.Windows.Forms.ColumnHeader();
			this.label34 = new System.Windows.Forms.Label();
			this.label36 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.sbpConnectState)).BeginInit();
			this.tabControlMain.SuspendLayout();
			this.tbData.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel6.SuspendLayout();
			this.pnQuestionarieData.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tbClusters.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.pnRegion.SuspendLayout();
			this.tbEditRules.SuspendLayout();
			this.panel8.SuspendLayout();
			this.pnEditRule.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tpImputeRules.SuspendLayout();
			this.panel9.SuspendLayout();
			this.panel7.SuspendLayout();
			this.pnImputeRule.SuspendLayout();
			this.tbAutoCorrect.SuspendLayout();
			this.tabControl2.SuspendLayout();
			this.tabPage6.SuspendLayout();
			this.panel12.SuspendLayout();
			this.tabPage7.SuspendLayout();
			this.panel13.SuspendLayout();
			this.tabPage8.SuspendLayout();
			this.panel14.SuspendLayout();
			this.tabPage9.SuspendLayout();
			this.panel10.SuspendLayout();
			this.pnEdit.SuspendLayout();
			this.tbProtocol.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolBar
			// 
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.tbConnect,
																					   this.tbNormalizeData,
																					   this.tbDonorRecipient,
																					   this.tbCluster,
																					   this.tbGenerateImputeRules,
																					   this.tbEdits,
																					   this.toolBarButton1,
																					   this.tbTestImpute});
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.imageList;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(954, 44);
			this.toolBar.TabIndex = 0;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// tbConnect
			// 
			this.tbConnect.ImageIndex = 0;
			this.tbConnect.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// tbNormalizeData
			// 
			this.tbNormalizeData.ImageIndex = 4;
			// 
			// tbDonorRecipient
			// 
			this.tbDonorRecipient.ImageIndex = 5;
			// 
			// tbCluster
			// 
			this.tbCluster.ImageIndex = 1;
			// 
			// tbGenerateImputeRules
			// 
			this.tbGenerateImputeRules.ImageIndex = 3;
			// 
			// tbEdits
			// 
			this.tbEdits.ImageIndex = 2;
			// 
			// toolBarButton1
			// 
			this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// tbTestImpute
			// 
			this.tbTestImpute.ImageIndex = 6;
			// 
			// imageList
			// 
			this.imageList.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItem1,
																					 this.menuItem2,
																					 this.menuItem6,
																					 this.menuItem8,
																					 this.menuItem9});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.miConnectRCEIS,
																					  this.miConnectTo,
																					  this.miDisconnect,
																					  this.miExit});
			this.menuItem1.Text = "Файл";
			// 
			// miConnectRCEIS
			// 
			this.miConnectRCEIS.Index = 0;
			this.miConnectRCEIS.Text = "Подключиться к RCEIS";
			this.miConnectRCEIS.Click += new System.EventHandler(this.miConnectRCEIS_Click);
			// 
			// miConnectTo
			// 
			this.miConnectTo.Index = 1;
			this.miConnectTo.Text = "Подключиться к...";
			this.miConnectTo.Click += new System.EventHandler(this.miConnectTo_Click);
			// 
			// miDisconnect
			// 
			this.miDisconnect.Index = 2;
			this.miDisconnect.Text = "Отключиться";
			this.miDisconnect.Click += new System.EventHandler(this.miDisconnect_Click);
			// 
			// miExit
			// 
			this.miExit.Index = 3;
			this.miExit.Text = "Выход";
			this.miExit.Click += new System.EventHandler(this.miExit_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.miDomainView,
																					  this.miViewRegions,
																					  this.menuItem5});
			this.menuItem2.Text = "Справочники";
			// 
			// miDomainView
			// 
			this.miDomainView.Index = 0;
			this.miDomainView.Text = "Диапазоны значений";
			this.miDomainView.Click += new System.EventHandler(this.miDomainView_Click);
			// 
			// miViewRegions
			// 
			this.miViewRegions.Index = 1;
			this.miViewRegions.Text = "Регионы";
			this.miViewRegions.Click += new System.EventHandler(this.miViewRegions_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 2;
			this.menuItem5.Text = "Формы (типы анкет)";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 2;
			this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem7,
																					  this.menuItem3,
																					  this.menuItem4});
			this.menuItem6.Text = "Данные";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 0;
			this.menuItem7.Text = "Получение данных";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Номализация данных";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 2;
			this.menuItem4.Text = "Получение базы доноров";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 3;
			this.menuItem8.Text = "Кластерный анализ";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 4;
			this.menuItem9.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem10,
																					  this.menuItem11});
			this.menuItem9.Text = "Импутация";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 0;
			this.menuItem10.Text = "Генерация правил импутации";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 1;
			this.menuItem11.Text = "Автокррекция и импутация";
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 533);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.sbpConnectState});
			this.statusBar.Size = new System.Drawing.Size(954, 22);
			this.statusBar.SizingGrip = false;
			this.statusBar.TabIndex = 2;
			// 
			// sbpConnectState
			// 
			this.sbpConnectState.Text = "Подключение неактивно";
			this.sbpConnectState.Width = 275;
			// 
			// imageListTree
			// 
			this.imageListTree.ImageSize = new System.Drawing.Size(16, 16);
			this.imageListTree.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListTree.ImageStream")));
			this.imageListTree.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tabControlMain
			// 
			this.tabControlMain.Controls.Add(this.tbData);
			this.tabControlMain.Controls.Add(this.tbClusters);
			this.tabControlMain.Controls.Add(this.tbEditRules);
			this.tabControlMain.Controls.Add(this.tpImputeRules);
			this.tabControlMain.Controls.Add(this.tbAutoCorrect);
			this.tabControlMain.Controls.Add(this.tbProtocol);
			this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControlMain.Location = new System.Drawing.Point(0, 44);
			this.tabControlMain.Name = "tabControlMain";
			this.tabControlMain.SelectedIndex = 0;
			this.tabControlMain.Size = new System.Drawing.Size(954, 489);
			this.tabControlMain.TabIndex = 6;
			// 
			// tbData
			// 
			this.tbData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbData.Controls.Add(this.splitter1);
			this.tbData.Controls.Add(this.panel2);
			this.tbData.Controls.Add(this.panel1);
			this.tbData.Location = new System.Drawing.Point(4, 22);
			this.tbData.Name = "tbData";
			this.tbData.Size = new System.Drawing.Size(946, 463);
			this.tbData.TabIndex = 0;
			this.tbData.Text = "Данные";
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(360, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 461);
			this.splitter1.TabIndex = 10;
			this.splitter1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel6);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(360, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(584, 461);
			this.panel2.TabIndex = 9;
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.lvQuestionarieData);
			this.panel6.Controls.Add(this.pnQuestionarieData);
			this.panel6.Controls.Add(this.label11);
			this.panel6.Controls.Add(this.splitter6);
			this.panel6.Controls.Add(this.lvInfo);
			this.panel6.Controls.Add(this.label10);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel6.Location = new System.Drawing.Point(0, 0);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(584, 461);
			this.panel6.TabIndex = 0;
			// 
			// lvQuestionarieData
			// 
			this.lvQuestionarieData.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvQuestionarieData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								 this.columnHeader15,
																								 this.columnHeader16,
																								 this.columnHeader17,
																								 this.columnHeader18,
																								 this.columnHeader19});
			this.lvQuestionarieData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvQuestionarieData.FullRowSelect = true;
			this.lvQuestionarieData.GridLines = true;
			this.lvQuestionarieData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvQuestionarieData.Location = new System.Drawing.Point(0, 200);
			this.lvQuestionarieData.Name = "lvQuestionarieData";
			this.lvQuestionarieData.Size = new System.Drawing.Size(584, 261);
			this.lvQuestionarieData.TabIndex = 15;
			this.lvQuestionarieData.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader15
			// 
			this.columnHeader15.Text = "№";
			this.columnHeader15.Width = 30;
			// 
			// columnHeader16
			// 
			this.columnHeader16.Text = "Код";
			this.columnHeader16.Width = 50;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "Обозначение";
			this.columnHeader17.Width = 100;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "Название";
			this.columnHeader18.Width = 310;
			// 
			// columnHeader19
			// 
			this.columnHeader19.Text = "Значение";
			this.columnHeader19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// pnQuestionarieData
			// 
			this.pnQuestionarieData.BackColor = System.Drawing.Color.NavajoWhite;
			this.pnQuestionarieData.Controls.Add(this.cbQuestionarieData);
			this.pnQuestionarieData.Controls.Add(this.label12);
			this.pnQuestionarieData.Controls.Add(this.lbFirst);
			this.pnQuestionarieData.Controls.Add(this.teFormID);
			this.pnQuestionarieData.Controls.Add(this.lbLast);
			this.pnQuestionarieData.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnQuestionarieData.Enabled = false;
			this.pnQuestionarieData.Location = new System.Drawing.Point(0, 162);
			this.pnQuestionarieData.Name = "pnQuestionarieData";
			this.pnQuestionarieData.Size = new System.Drawing.Size(584, 38);
			this.pnQuestionarieData.TabIndex = 19;
			// 
			// cbQuestionarieData
			// 
			this.cbQuestionarieData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbQuestionarieData.Location = new System.Drawing.Point(116, 8);
			this.cbQuestionarieData.Name = "cbQuestionarieData";
			this.cbQuestionarieData.Size = new System.Drawing.Size(316, 21);
			this.cbQuestionarieData.TabIndex = 1;
			this.cbQuestionarieData.SelectedIndexChanged += new System.EventHandler(this.cbQuestionarieData_SelectedIndexChanged);
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(8, 8);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 17);
			this.label12.TabIndex = 0;
			this.label12.Text = "Выбор формы : ";
			// 
			// lbFirst
			// 
			this.lbFirst.BackColor = System.Drawing.Color.NavajoWhite;
			this.lbFirst.Location = new System.Drawing.Point(444, 8);
			this.lbFirst.Name = "lbFirst";
			this.lbFirst.Size = new System.Drawing.Size(24, 20);
			this.lbFirst.TabIndex = 16;
			this.lbFirst.TabStop = true;
			this.lbFirst.Text = "<<";
			this.lbFirst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbFirst.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbFirst_LinkClicked);
			// 
			// teFormID
			// 
			this.teFormID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teFormID.Location = new System.Drawing.Point(468, 8);
			this.teFormID.Name = "teFormID";
			this.teFormID.Size = new System.Drawing.Size(64, 20);
			this.teFormID.TabIndex = 18;
			this.teFormID.Text = "";
			this.teFormID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.teFormID.TextChanged += new System.EventHandler(this.teFormID_TextChanged);
			this.teFormID.Leave += new System.EventHandler(this.teFormID_Leave);
			// 
			// lbLast
			// 
			this.lbLast.BackColor = System.Drawing.Color.NavajoWhite;
			this.lbLast.Location = new System.Drawing.Point(532, 8);
			this.lbLast.Name = "lbLast";
			this.lbLast.Size = new System.Drawing.Size(24, 20);
			this.lbLast.TabIndex = 17;
			this.lbLast.TabStop = true;
			this.lbLast.Text = ">>";
			this.lbLast.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbLast.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbLast_LinkClicked);
			// 
			// label11
			// 
			this.label11.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label11.Dock = System.Windows.Forms.DockStyle.Top;
			this.label11.Location = new System.Drawing.Point(0, 139);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(584, 23);
			this.label11.TabIndex = 14;
			this.label11.Text = "Информация по данным";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitter6
			// 
			this.splitter6.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter6.Location = new System.Drawing.Point(0, 136);
			this.splitter6.Name = "splitter6";
			this.splitter6.Size = new System.Drawing.Size(584, 3);
			this.splitter6.TabIndex = 13;
			this.splitter6.TabStop = false;
			// 
			// lvInfo
			// 
			this.lvInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					 this.columnHeader11,
																					 this.columnHeader13,
																					 this.columnHeader14});
			this.lvInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.lvInfo.FullRowSelect = true;
			this.lvInfo.GridLines = true;
			this.lvInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvInfo.Location = new System.Drawing.Point(0, 23);
			this.lvInfo.Name = "lvInfo";
			this.lvInfo.Size = new System.Drawing.Size(584, 113);
			this.lvInfo.TabIndex = 12;
			this.lvInfo.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "№";
			this.columnHeader11.Width = 30;
			// 
			// columnHeader13
			// 
			this.columnHeader13.Text = "Параметр";
			this.columnHeader13.Width = 233;
			// 
			// columnHeader14
			// 
			this.columnHeader14.Text = "Значение";
			this.columnHeader14.Width = 295;
			// 
			// label10
			// 
			this.label10.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label10.Dock = System.Windows.Forms.DockStyle.Top;
			this.label10.Location = new System.Drawing.Point(0, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(584, 23);
			this.label10.TabIndex = 8;
			this.label10.Text = "Общая информация";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tvQuestionarie);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(360, 461);
			this.panel1.TabIndex = 8;
			// 
			// tvQuestionarie
			// 
			this.tvQuestionarie.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tvQuestionarie.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvQuestionarie.ImageList = this.imageListTree;
			this.tvQuestionarie.Location = new System.Drawing.Point(0, 23);
			this.tvQuestionarie.Name = "tvQuestionarie";
			this.tvQuestionarie.Size = new System.Drawing.Size(360, 438);
			this.tvQuestionarie.TabIndex = 6;
			this.tvQuestionarie.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvQuestionarie_AfterSelect);
			// 
			// label2
			// 
			this.label2.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(360, 23);
			this.label2.TabIndex = 7;
			this.label2.Text = "Структура исходных данных";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbClusters
			// 
			this.tbClusters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbClusters.Controls.Add(this.splitter3);
			this.tbClusters.Controls.Add(this.panel5);
			this.tbClusters.Controls.Add(this.panel4);
			this.tbClusters.Controls.Add(this.splitter2);
			this.tbClusters.Controls.Add(this.panel3);
			this.tbClusters.Controls.Add(this.pnRegion);
			this.tbClusters.Location = new System.Drawing.Point(4, 22);
			this.tbClusters.Name = "tbClusters";
			this.tbClusters.Size = new System.Drawing.Size(946, 463);
			this.tbClusters.TabIndex = 1;
			this.tbClusters.Text = "Кластеры";
			// 
			// splitter3
			// 
			this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter3.Location = new System.Drawing.Point(323, 248);
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(621, 3);
			this.splitter3.TabIndex = 7;
			this.splitter3.TabStop = false;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.lvSplittingClusters);
			this.panel5.Controls.Add(this.label5);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(323, 248);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(621, 213);
			this.panel5.TabIndex = 6;
			// 
			// lvSplittingClusters
			// 
			this.lvSplittingClusters.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvSplittingClusters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								  this.columnHeader6,
																								  this.columnHeader7,
																								  this.columnHeader8,
																								  this.columnHeader58});
			this.lvSplittingClusters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSplittingClusters.FullRowSelect = true;
			this.lvSplittingClusters.GridLines = true;
			this.lvSplittingClusters.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvSplittingClusters.Location = new System.Drawing.Point(0, 24);
			this.lvSplittingClusters.Name = "lvSplittingClusters";
			this.lvSplittingClusters.Size = new System.Drawing.Size(621, 189);
			this.lvSplittingClusters.TabIndex = 10;
			this.lvSplittingClusters.View = System.Windows.Forms.View.Details;
			this.lvSplittingClusters.DoubleClick += new System.EventHandler(this.lvSplittingClusters_DoubleClick);
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
			this.columnHeader8.Text = "Записей";
			this.columnHeader8.Width = 80;
			// 
			// columnHeader58
			// 
			this.columnHeader58.Text = "Доноров";
			this.columnHeader58.Width = 80;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label5.Dock = System.Windows.Forms.DockStyle.Top;
			this.label5.Location = new System.Drawing.Point(0, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(621, 24);
			this.label5.TabIndex = 9;
			this.label5.Text = "Кластеры";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.lvSplittingColumns);
			this.panel4.Controls.Add(this.label4);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(323, 32);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(621, 216);
			this.panel4.TabIndex = 5;
			// 
			// lvSplittingColumns
			// 
			this.lvSplittingColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvSplittingColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								 this.columnHeader1,
																								 this.columnHeader4,
																								 this.columnHeader2,
																								 this.columnHeader3});
			this.lvSplittingColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSplittingColumns.FullRowSelect = true;
			this.lvSplittingColumns.GridLines = true;
			this.lvSplittingColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvSplittingColumns.Location = new System.Drawing.Point(0, 23);
			this.lvSplittingColumns.Name = "lvSplittingColumns";
			this.lvSplittingColumns.Size = new System.Drawing.Size(621, 193);
			this.lvSplittingColumns.TabIndex = 10;
			this.lvSplittingColumns.View = System.Windows.Forms.View.Details;
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
			// columnHeader3
			// 
			this.columnHeader3.Text = "Название";
			this.columnHeader3.Width = 430;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label4.Dock = System.Windows.Forms.DockStyle.Top;
			this.label4.Location = new System.Drawing.Point(0, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(621, 23);
			this.label4.TabIndex = 9;
			this.label4.Text = "Показатели кластреного анализа";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// splitter2
			// 
			this.splitter2.Location = new System.Drawing.Point(320, 32);
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(3, 429);
			this.splitter2.TabIndex = 4;
			this.splitter2.TabStop = false;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.tvClusters);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel3.Location = new System.Drawing.Point(0, 32);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(320, 429);
			this.panel3.TabIndex = 3;
			// 
			// tvClusters
			// 
			this.tvClusters.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tvClusters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvClusters.ImageList = this.imageListTree;
			this.tvClusters.Location = new System.Drawing.Point(0, 23);
			this.tvClusters.Name = "tvClusters";
			this.tvClusters.Size = new System.Drawing.Size(320, 406);
			this.tvClusters.TabIndex = 9;
			this.tvClusters.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvClusters_AfterSelect);
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(320, 23);
			this.label3.TabIndex = 8;
			this.label3.Text = "Кластерный анализ";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnRegion
			// 
			this.pnRegion.BackColor = System.Drawing.Color.NavajoWhite;
			this.pnRegion.Controls.Add(this.btDeleteRegion);
			this.pnRegion.Controls.Add(this.btUpdateRegion);
			this.pnRegion.Controls.Add(this.btNewRegion);
			this.pnRegion.Controls.Add(this.cbRegions);
			this.pnRegion.Controls.Add(this.label1);
			this.pnRegion.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnRegion.Enabled = false;
			this.pnRegion.Location = new System.Drawing.Point(0, 0);
			this.pnRegion.Name = "pnRegion";
			this.pnRegion.Size = new System.Drawing.Size(944, 32);
			this.pnRegion.TabIndex = 2;
			// 
			// btDeleteRegion
			// 
			this.btDeleteRegion.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.btDeleteRegion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btDeleteRegion.Image = ((System.Drawing.Image)(resources.GetObject("btDeleteRegion.Image")));
			this.btDeleteRegion.Location = new System.Drawing.Point(920, 8);
			this.btDeleteRegion.Name = "btDeleteRegion";
			this.btDeleteRegion.Size = new System.Drawing.Size(16, 16);
			this.btDeleteRegion.TabIndex = 4;
			this.btDeleteRegion.Visible = false;
			// 
			// btUpdateRegion
			// 
			this.btUpdateRegion.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.btUpdateRegion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btUpdateRegion.Image = ((System.Drawing.Image)(resources.GetObject("btUpdateRegion.Image")));
			this.btUpdateRegion.Location = new System.Drawing.Point(888, 8);
			this.btUpdateRegion.Name = "btUpdateRegion";
			this.btUpdateRegion.Size = new System.Drawing.Size(24, 16);
			this.btUpdateRegion.TabIndex = 3;
			this.btUpdateRegion.Visible = false;
			// 
			// btNewRegion
			// 
			this.btNewRegion.BackColor = System.Drawing.SystemColors.ActiveBorder;
			this.btNewRegion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btNewRegion.Image = ((System.Drawing.Image)(resources.GetObject("btNewRegion.Image")));
			this.btNewRegion.Location = new System.Drawing.Point(856, 8);
			this.btNewRegion.Name = "btNewRegion";
			this.btNewRegion.Size = new System.Drawing.Size(24, 16);
			this.btNewRegion.TabIndex = 2;
			this.btNewRegion.Visible = false;
			// 
			// cbRegions
			// 
			this.cbRegions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbRegions.Location = new System.Drawing.Point(112, 8);
			this.cbRegions.Name = "cbRegions";
			this.cbRegions.Size = new System.Drawing.Size(208, 21);
			this.cbRegions.TabIndex = 1;
			this.cbRegions.SelectedIndexChanged += new System.EventHandler(this.cbRegions_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Выбор региона : ";
			// 
			// tbEditRules
			// 
			this.tbEditRules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbEditRules.Controls.Add(this.splitter4);
			this.tbEditRules.Controls.Add(this.panel8);
			this.tbEditRules.Controls.Add(this.pnEditRule);
			this.tbEditRules.Location = new System.Drawing.Point(4, 22);
			this.tbEditRules.Name = "tbEditRules";
			this.tbEditRules.Size = new System.Drawing.Size(946, 463);
			this.tbEditRules.TabIndex = 2;
			this.tbEditRules.Text = "Правила редактирования";
			// 
			// splitter4
			// 
			this.splitter4.Location = new System.Drawing.Point(0, 64);
			this.splitter4.Name = "splitter4";
			this.splitter4.Size = new System.Drawing.Size(3, 397);
			this.splitter4.TabIndex = 6;
			this.splitter4.TabStop = false;
			// 
			// panel8
			// 
			this.panel8.Controls.Add(this.lvEditRules);
			this.panel8.Controls.Add(this.label8);
			this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel8.Location = new System.Drawing.Point(0, 64);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(944, 397);
			this.panel8.TabIndex = 5;
			// 
			// lvEditRules
			// 
			this.lvEditRules.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvEditRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						  this.columnHeader5,
																						  this.columnHeader9,
																						  this.columnHeader10});
			this.lvEditRules.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvEditRules.FullRowSelect = true;
			this.lvEditRules.GridLines = true;
			this.lvEditRules.Location = new System.Drawing.Point(0, 23);
			this.lvEditRules.Name = "lvEditRules";
			this.lvEditRules.Size = new System.Drawing.Size(944, 374);
			this.lvEditRules.TabIndex = 11;
			this.lvEditRules.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "№";
			this.columnHeader5.Width = 30;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Код";
			this.columnHeader9.Width = 139;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Значение";
			this.columnHeader10.Width = 80;
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label8.Dock = System.Windows.Forms.DockStyle.Top;
			this.label8.Location = new System.Drawing.Point(0, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(944, 23);
			this.label8.TabIndex = 10;
			this.label8.Text = "Правила";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnEditRule
			// 
			this.pnEditRule.BackColor = System.Drawing.Color.NavajoWhite;
			this.pnEditRule.Controls.Add(this.teEditRulesCount);
			this.pnEditRule.Controls.Add(this.groupBox1);
			this.pnEditRule.Controls.Add(this.cbQuestionarie);
			this.pnEditRule.Controls.Add(this.label6);
			this.pnEditRule.Controls.Add(this.label35);
			this.pnEditRule.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnEditRule.Enabled = false;
			this.pnEditRule.Location = new System.Drawing.Point(0, 0);
			this.pnEditRule.Name = "pnEditRule";
			this.pnEditRule.Size = new System.Drawing.Size(944, 64);
			this.pnEditRule.TabIndex = 3;
			// 
			// teEditRulesCount
			// 
			this.teEditRulesCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teEditRulesCount.Enabled = false;
			this.teEditRulesCount.Location = new System.Drawing.Point(116, 36);
			this.teEditRulesCount.Name = "teEditRulesCount";
			this.teEditRulesCount.Size = new System.Drawing.Size(96, 20);
			this.teEditRulesCount.TabIndex = 6;
			this.teEditRulesCount.Text = "";
			this.teEditRulesCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rgQualitaty);
			this.groupBox1.Controls.Add(this.rgQuantity);
			this.groupBox1.Location = new System.Drawing.Point(436, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(400, 56);
			this.groupBox1.TabIndex = 5;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Отображать правила редактирования";
			// 
			// rgQualitaty
			// 
			this.rgQualitaty.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rgQualitaty.Location = new System.Drawing.Point(208, 24);
			this.rgQualitaty.Name = "rgQualitaty";
			this.rgQualitaty.Size = new System.Drawing.Size(184, 24);
			this.rgQualitaty.TabIndex = 1;
			this.rgQualitaty.Text = "качественных показателей";
			// 
			// rgQuantity
			// 
			this.rgQuantity.Checked = true;
			this.rgQuantity.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.rgQuantity.Location = new System.Drawing.Point(16, 24);
			this.rgQuantity.Name = "rgQuantity";
			this.rgQuantity.Size = new System.Drawing.Size(184, 24);
			this.rgQuantity.TabIndex = 0;
			this.rgQuantity.TabStop = true;
			this.rgQuantity.Text = "количественных показателей";
			this.rgQuantity.CheckedChanged += new System.EventHandler(this.rgQuantity_CheckedChanged);
			// 
			// cbQuestionarie
			// 
			this.cbQuestionarie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbQuestionarie.Location = new System.Drawing.Point(116, 8);
			this.cbQuestionarie.Name = "cbQuestionarie";
			this.cbQuestionarie.Size = new System.Drawing.Size(316, 21);
			this.cbQuestionarie.TabIndex = 1;
			this.cbQuestionarie.SelectedIndexChanged += new System.EventHandler(this.cbQuestionarie_SelectedIndexChanged);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(8, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 17);
			this.label6.TabIndex = 0;
			this.label6.Text = "Выбор формы : ";
			// 
			// label35
			// 
			this.label35.Location = new System.Drawing.Point(8, 40);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(100, 17);
			this.label35.TabIndex = 0;
			this.label35.Text = "Всего правил :";
			// 
			// tpImputeRules
			// 
			this.tpImputeRules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tpImputeRules.Controls.Add(this.splitter5);
			this.tpImputeRules.Controls.Add(this.panel9);
			this.tpImputeRules.Controls.Add(this.panel7);
			this.tpImputeRules.Controls.Add(this.pnImputeRule);
			this.tpImputeRules.Location = new System.Drawing.Point(4, 22);
			this.tpImputeRules.Name = "tpImputeRules";
			this.tpImputeRules.Size = new System.Drawing.Size(946, 463);
			this.tpImputeRules.TabIndex = 3;
			this.tpImputeRules.Text = "Правила импуатции";
			// 
			// splitter5
			// 
			this.splitter5.Location = new System.Drawing.Point(320, 32);
			this.splitter5.Name = "splitter5";
			this.splitter5.Size = new System.Drawing.Size(3, 429);
			this.splitter5.TabIndex = 7;
			this.splitter5.TabStop = false;
			// 
			// panel9
			// 
			this.panel9.Controls.Add(this.lvImputeColumns);
			this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel9.Location = new System.Drawing.Point(320, 32);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(624, 429);
			this.panel9.TabIndex = 6;
			// 
			// lvImputeColumns
			// 
			this.lvImputeColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvImputeColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							  this.columnHeader31,
																							  this.columnHeader46,
																							  this.columnHeader47,
																							  this.columnHeader48,
																							  this.columnHeader49});
			this.lvImputeColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvImputeColumns.FullRowSelect = true;
			this.lvImputeColumns.GridLines = true;
			this.lvImputeColumns.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvImputeColumns.Location = new System.Drawing.Point(0, 0);
			this.lvImputeColumns.Name = "lvImputeColumns";
			this.lvImputeColumns.Size = new System.Drawing.Size(624, 429);
			this.lvImputeColumns.TabIndex = 16;
			this.lvImputeColumns.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader31
			// 
			this.columnHeader31.Text = "№";
			this.columnHeader31.Width = 30;
			// 
			// columnHeader46
			// 
			this.columnHeader46.Text = "Код";
			this.columnHeader46.Width = 50;
			// 
			// columnHeader47
			// 
			this.columnHeader47.Text = "Обозначение";
			this.columnHeader47.Width = 81;
			// 
			// columnHeader48
			// 
			this.columnHeader48.Text = "Название";
			this.columnHeader48.Width = 200;
			// 
			// columnHeader49
			// 
			this.columnHeader49.Text = "Правило импутации";
			this.columnHeader49.Width = 235;
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.tvImputeColumns);
			this.panel7.Controls.Add(this.label9);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel7.Location = new System.Drawing.Point(0, 32);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(320, 429);
			this.panel7.TabIndex = 5;
			// 
			// tvImputeColumns
			// 
			this.tvImputeColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.tvImputeColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvImputeColumns.ImageList = this.imageListTree;
			this.tvImputeColumns.Location = new System.Drawing.Point(0, 23);
			this.tvImputeColumns.Name = "tvImputeColumns";
			this.tvImputeColumns.Size = new System.Drawing.Size(320, 406);
			this.tvImputeColumns.TabIndex = 10;
			// 
			// label9
			// 
			this.label9.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label9.Dock = System.Windows.Forms.DockStyle.Top;
			this.label9.Location = new System.Drawing.Point(0, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(320, 23);
			this.label9.TabIndex = 9;
			this.label9.Text = "Импутируемые показатели";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnImputeRule
			// 
			this.pnImputeRule.BackColor = System.Drawing.Color.NavajoWhite;
			this.pnImputeRule.Controls.Add(this.cbQuestionarieImpute);
			this.pnImputeRule.Controls.Add(this.label7);
			this.pnImputeRule.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnImputeRule.Enabled = false;
			this.pnImputeRule.Location = new System.Drawing.Point(0, 0);
			this.pnImputeRule.Name = "pnImputeRule";
			this.pnImputeRule.Size = new System.Drawing.Size(944, 32);
			this.pnImputeRule.TabIndex = 4;
			// 
			// cbQuestionarieImpute
			// 
			this.cbQuestionarieImpute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbQuestionarieImpute.Location = new System.Drawing.Point(116, 8);
			this.cbQuestionarieImpute.Name = "cbQuestionarieImpute";
			this.cbQuestionarieImpute.Size = new System.Drawing.Size(316, 21);
			this.cbQuestionarieImpute.TabIndex = 1;
			this.cbQuestionarieImpute.SelectedIndexChanged += new System.EventHandler(this.cbQuestionarieImpute_SelectedIndexChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(8, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 17);
			this.label7.TabIndex = 0;
			this.label7.Text = "Выбор формы : ";
			// 
			// tbAutoCorrect
			// 
			this.tbAutoCorrect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tbAutoCorrect.Controls.Add(this.tabControl2);
			this.tbAutoCorrect.Controls.Add(this.panel10);
			this.tbAutoCorrect.Location = new System.Drawing.Point(4, 22);
			this.tbAutoCorrect.Name = "tbAutoCorrect";
			this.tbAutoCorrect.Size = new System.Drawing.Size(946, 463);
			this.tbAutoCorrect.TabIndex = 4;
			this.tbAutoCorrect.Text = "Автокоррекция и импутация";
			// 
			// tabControl2
			// 
			this.tabControl2.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl2.Controls.Add(this.tabPage6);
			this.tabControl2.Controls.Add(this.tabPage7);
			this.tabControl2.Controls.Add(this.tabPage8);
			this.tabControl2.Controls.Add(this.tabPage9);
			this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl2.HotTrack = true;
			this.tabControl2.Location = new System.Drawing.Point(264, 0);
			this.tabControl2.Name = "tabControl2";
			this.tabControl2.SelectedIndex = 0;
			this.tabControl2.Size = new System.Drawing.Size(680, 461);
			this.tabControl2.TabIndex = 1;
			// 
			// tabPage6
			// 
			this.tabPage6.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage6.Controls.Add(this.lvEditProtocol);
			this.tabPage6.Controls.Add(this.panel12);
			this.tabPage6.Location = new System.Drawing.Point(4, 25);
			this.tabPage6.Name = "tabPage6";
			this.tabPage6.Size = new System.Drawing.Size(672, 432);
			this.tabPage6.TabIndex = 0;
			this.tabPage6.Text = "Этап 1. Автокоррекция";
			// 
			// lvEditProtocol
			// 
			this.lvEditProtocol.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvEditProtocol.CheckBoxes = true;
			this.lvEditProtocol.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.columnHeader25,
																							 this.columnHeader26});
			this.lvEditProtocol.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvEditProtocol.FullRowSelect = true;
			this.lvEditProtocol.GridLines = true;
			this.lvEditProtocol.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			listViewItem1.Checked = true;
			listViewItem1.StateImageIndex = 1;
			listViewItem2.StateImageIndex = 0;
			this.lvEditProtocol.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																						   listViewItem1,
																						   listViewItem2});
			this.lvEditProtocol.Location = new System.Drawing.Point(0, 63);
			this.lvEditProtocol.Name = "lvEditProtocol";
			this.lvEditProtocol.Size = new System.Drawing.Size(672, 369);
			this.lvEditProtocol.TabIndex = 24;
			this.lvEditProtocol.View = System.Windows.Forms.View.Details;
			this.lvEditProtocol.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lvEditProtocol_ItemCheck);
			// 
			// columnHeader25
			// 
			this.columnHeader25.Text = "№";
			this.columnHeader25.Width = 42;
			// 
			// columnHeader26
			// 
			this.columnHeader26.Text = "Правило";
			this.columnHeader26.Width = 550;
			// 
			// panel12
			// 
			this.panel12.BackColor = System.Drawing.Color.NavajoWhite;
			this.panel12.Controls.Add(this.label15);
			this.panel12.Controls.Add(this.teEditsAll);
			this.panel12.Controls.Add(this.teEditsAllFail);
			this.panel12.Controls.Add(this.label16);
			this.panel12.Controls.Add(this.teEditsQNT);
			this.panel12.Controls.Add(this.label17);
			this.panel12.Controls.Add(this.teEditsQNTFail);
			this.panel12.Controls.Add(this.label18);
			this.panel12.Controls.Add(this.teEditsQLTFail);
			this.panel12.Controls.Add(this.teEditsQLT);
			this.panel12.Controls.Add(this.label19);
			this.panel12.Controls.Add(this.label20);
			this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel12.Enabled = false;
			this.panel12.Location = new System.Drawing.Point(0, 0);
			this.panel12.Name = "panel12";
			this.panel12.Size = new System.Drawing.Size(672, 63);
			this.panel12.TabIndex = 23;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(8, 8);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(184, 17);
			this.label15.TabIndex = 0;
			this.label15.Text = "Всего правил редактирования:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// teEditsAll
			// 
			this.teEditsAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teEditsAll.Location = new System.Drawing.Point(196, 8);
			this.teEditsAll.Name = "teEditsAll";
			this.teEditsAll.Size = new System.Drawing.Size(64, 20);
			this.teEditsAll.TabIndex = 18;
			this.teEditsAll.Text = "";
			this.teEditsAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// teEditsAllFail
			// 
			this.teEditsAllFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teEditsAllFail.Location = new System.Drawing.Point(196, 32);
			this.teEditsAllFail.Name = "teEditsAllFail";
			this.teEditsAllFail.Size = new System.Drawing.Size(64, 20);
			this.teEditsAllFail.TabIndex = 18;
			this.teEditsAllFail.Text = "";
			this.teEditsAllFail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(8, 32);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(184, 17);
			this.label16.TabIndex = 0;
			this.label16.Text = "Из них неудовлетворено:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// teEditsQNT
			// 
			this.teEditsQNT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teEditsQNT.Location = new System.Drawing.Point(372, 8);
			this.teEditsQNT.Name = "teEditsQNT";
			this.teEditsQNT.Size = new System.Drawing.Size(64, 20);
			this.teEditsQNT.TabIndex = 18;
			this.teEditsQNT.Text = "";
			this.teEditsQNT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(268, 8);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(100, 17);
			this.label17.TabIndex = 0;
			this.label17.Text = "Количественных:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// teEditsQNTFail
			// 
			this.teEditsQNTFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teEditsQNTFail.Location = new System.Drawing.Point(372, 32);
			this.teEditsQNTFail.Name = "teEditsQNTFail";
			this.teEditsQNTFail.Size = new System.Drawing.Size(64, 20);
			this.teEditsQNTFail.TabIndex = 18;
			this.teEditsQNTFail.Text = "";
			this.teEditsQNTFail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(268, 32);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(100, 17);
			this.label18.TabIndex = 0;
			this.label18.Text = "Количественных:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// teEditsQLTFail
			// 
			this.teEditsQLTFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teEditsQLTFail.Location = new System.Drawing.Point(548, 32);
			this.teEditsQLTFail.Name = "teEditsQLTFail";
			this.teEditsQLTFail.Size = new System.Drawing.Size(64, 20);
			this.teEditsQLTFail.TabIndex = 18;
			this.teEditsQLTFail.Text = "";
			this.teEditsQLTFail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// teEditsQLT
			// 
			this.teEditsQLT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teEditsQLT.Location = new System.Drawing.Point(548, 8);
			this.teEditsQLT.Name = "teEditsQLT";
			this.teEditsQLT.Size = new System.Drawing.Size(64, 20);
			this.teEditsQLT.TabIndex = 18;
			this.teEditsQLT.Text = "";
			this.teEditsQLT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(444, 8);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(100, 17);
			this.label19.TabIndex = 0;
			this.label19.Text = "Качественных:";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(444, 32);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(100, 17);
			this.label20.TabIndex = 0;
			this.label20.Text = "Качественных:";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabPage7
			// 
			this.tabPage7.Controls.Add(this.lvFailedEditMatrix);
			this.tabPage7.Controls.Add(this.panel13);
			this.tabPage7.Location = new System.Drawing.Point(4, 25);
			this.tabPage7.Name = "tabPage7";
			this.tabPage7.Size = new System.Drawing.Size(672, 432);
			this.tabPage7.TabIndex = 1;
			this.tabPage7.Text = "Этап 2. Локализация ошибки";
			// 
			// lvFailedEditMatrix
			// 
			this.lvFailedEditMatrix.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvFailedEditMatrix.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																								 this.columnHeader27,
																								 this.columnHeader28,
																								 this.columnHeader29,
																								 this.columnHeader30});
			this.lvFailedEditMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvFailedEditMatrix.FullRowSelect = true;
			this.lvFailedEditMatrix.GridLines = true;
			this.lvFailedEditMatrix.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvFailedEditMatrix.Location = new System.Drawing.Point(0, 63);
			this.lvFailedEditMatrix.Name = "lvFailedEditMatrix";
			this.lvFailedEditMatrix.Size = new System.Drawing.Size(672, 369);
			this.lvFailedEditMatrix.TabIndex = 21;
			this.lvFailedEditMatrix.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader27
			// 
			this.columnHeader27.Text = "№";
			this.columnHeader27.Width = 35;
			// 
			// columnHeader28
			// 
			this.columnHeader28.Text = "Код";
			this.columnHeader28.Width = 50;
			// 
			// columnHeader29
			// 
			this.columnHeader29.Text = "Обозначение";
			this.columnHeader29.Width = 100;
			// 
			// columnHeader30
			// 
			this.columnHeader30.Text = "Название";
			this.columnHeader30.Width = 0;
			// 
			// panel13
			// 
			this.panel13.BackColor = System.Drawing.Color.NavajoWhite;
			this.panel13.Controls.Add(this.label21);
			this.panel13.Controls.Add(this.teColumnAll);
			this.panel13.Controls.Add(this.teColumnAllFail);
			this.panel13.Controls.Add(this.label22);
			this.panel13.Controls.Add(this.teColumnQnt);
			this.panel13.Controls.Add(this.label23);
			this.panel13.Controls.Add(this.teColumnQntFail);
			this.panel13.Controls.Add(this.label24);
			this.panel13.Controls.Add(this.teColumnQltFail);
			this.panel13.Controls.Add(this.teColumnQlt);
			this.panel13.Controls.Add(this.label25);
			this.panel13.Controls.Add(this.label26);
			this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel13.Enabled = false;
			this.panel13.Location = new System.Drawing.Point(0, 0);
			this.panel13.Name = "panel13";
			this.panel13.Size = new System.Drawing.Size(672, 63);
			this.panel13.TabIndex = 24;
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(34, 9);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(184, 17);
			this.label21.TabIndex = 24;
			this.label21.Text = "Всего показателей:";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// teColumnAll
			// 
			this.teColumnAll.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teColumnAll.Location = new System.Drawing.Point(222, 9);
			this.teColumnAll.Name = "teColumnAll";
			this.teColumnAll.Size = new System.Drawing.Size(64, 20);
			this.teColumnAll.TabIndex = 26;
			this.teColumnAll.Text = "";
			this.teColumnAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// teColumnAllFail
			// 
			this.teColumnAllFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teColumnAllFail.Location = new System.Drawing.Point(222, 33);
			this.teColumnAllFail.Name = "teColumnAllFail";
			this.teColumnAllFail.Size = new System.Drawing.Size(64, 20);
			this.teColumnAllFail.TabIndex = 30;
			this.teColumnAllFail.Text = "";
			this.teColumnAllFail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label22
			// 
			this.label22.Location = new System.Drawing.Point(34, 33);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(184, 17);
			this.label22.TabIndex = 23;
			this.label22.Text = "Из них требуют импутации:";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// teColumnQnt
			// 
			this.teColumnQnt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teColumnQnt.Location = new System.Drawing.Point(398, 9);
			this.teColumnQnt.Name = "teColumnQnt";
			this.teColumnQnt.Size = new System.Drawing.Size(64, 20);
			this.teColumnQnt.TabIndex = 28;
			this.teColumnQnt.Text = "";
			this.teColumnQnt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label23
			// 
			this.label23.Location = new System.Drawing.Point(294, 9);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(100, 17);
			this.label23.TabIndex = 20;
			this.label23.Text = "Количественных:";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// teColumnQntFail
			// 
			this.teColumnQntFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teColumnQntFail.Location = new System.Drawing.Point(398, 33);
			this.teColumnQntFail.Name = "teColumnQntFail";
			this.teColumnQntFail.Size = new System.Drawing.Size(64, 20);
			this.teColumnQntFail.TabIndex = 29;
			this.teColumnQntFail.Text = "";
			this.teColumnQntFail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label24
			// 
			this.label24.Location = new System.Drawing.Point(294, 33);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(100, 17);
			this.label24.TabIndex = 19;
			this.label24.Text = "Количественных:";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// teColumnQltFail
			// 
			this.teColumnQltFail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teColumnQltFail.Location = new System.Drawing.Point(574, 33);
			this.teColumnQltFail.Name = "teColumnQltFail";
			this.teColumnQltFail.Size = new System.Drawing.Size(64, 20);
			this.teColumnQltFail.TabIndex = 27;
			this.teColumnQltFail.Text = "";
			this.teColumnQltFail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// teColumnQlt
			// 
			this.teColumnQlt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teColumnQlt.Location = new System.Drawing.Point(574, 9);
			this.teColumnQlt.Name = "teColumnQlt";
			this.teColumnQlt.Size = new System.Drawing.Size(64, 20);
			this.teColumnQlt.TabIndex = 25;
			this.teColumnQlt.Text = "";
			this.teColumnQlt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label25
			// 
			this.label25.Location = new System.Drawing.Point(470, 9);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(100, 17);
			this.label25.TabIndex = 22;
			this.label25.Text = "Качественных:";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label26
			// 
			this.label26.Location = new System.Drawing.Point(470, 33);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(100, 17);
			this.label26.TabIndex = 21;
			this.label26.Text = "Качественных:";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabPage8
			// 
			this.tabPage8.Controls.Add(this.lvClusterParams);
			this.tabPage8.Controls.Add(this.label27);
			this.tabPage8.Controls.Add(this.panel14);
			this.tabPage8.Location = new System.Drawing.Point(4, 25);
			this.tabPage8.Name = "tabPage8";
			this.tabPage8.Size = new System.Drawing.Size(672, 432);
			this.tabPage8.TabIndex = 2;
			this.tabPage8.Text = "Этап 3. Идентификация кластера";
			// 
			// lvClusterParams
			// 
			this.lvClusterParams.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvClusterParams.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							  this.columnHeader32,
																							  this.columnHeader33,
																							  this.columnHeader34,
																							  this.columnHeader35,
																							  this.columnHeader36,
																							  this.columnHeader37,
																							  this.columnHeader38});
			this.lvClusterParams.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvClusterParams.FullRowSelect = true;
			this.lvClusterParams.GridLines = true;
			this.lvClusterParams.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvClusterParams.Location = new System.Drawing.Point(0, 87);
			this.lvClusterParams.Name = "lvClusterParams";
			this.lvClusterParams.Size = new System.Drawing.Size(672, 345);
			this.lvClusterParams.TabIndex = 26;
			this.lvClusterParams.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader32
			// 
			this.columnHeader32.Text = "№";
			this.columnHeader32.Width = 30;
			// 
			// columnHeader33
			// 
			this.columnHeader33.Text = "Код";
			// 
			// columnHeader34
			// 
			this.columnHeader34.Text = "Обозначение";
			this.columnHeader34.Width = 80;
			// 
			// columnHeader35
			// 
			this.columnHeader35.Text = "Мин.";
			this.columnHeader35.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader36
			// 
			this.columnHeader36.Text = "Макс.";
			this.columnHeader36.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader37
			// 
			this.columnHeader37.Text = "Среднее";
			this.columnHeader37.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader38
			// 
			this.columnHeader38.Text = "Ст. откл.";
			this.columnHeader38.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// label27
			// 
			this.label27.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label27.Dock = System.Windows.Forms.DockStyle.Top;
			this.label27.Location = new System.Drawing.Point(0, 64);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(672, 23);
			this.label27.TabIndex = 25;
			this.label27.Text = "Показатели кластера";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel14
			// 
			this.panel14.BackColor = System.Drawing.Color.NavajoWhite;
			this.panel14.Controls.Add(this.label28);
			this.panel14.Controls.Add(this.teRegion);
			this.panel14.Controls.Add(this.teNearestCluster);
			this.panel14.Controls.Add(this.label29);
			this.panel14.Controls.Add(this.label30);
			this.panel14.Controls.Add(this.teTotalClusters);
			this.panel14.Controls.Add(this.label31);
			this.panel14.Controls.Add(this.label32);
			this.panel14.Controls.Add(this.teTotal);
			this.panel14.Controls.Add(this.label33);
			this.panel14.Controls.Add(this.teDonor);
			this.panel14.Controls.Add(this.teNearestValue);
			this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel14.Enabled = false;
			this.panel14.Location = new System.Drawing.Point(0, 0);
			this.panel14.Name = "panel14";
			this.panel14.Size = new System.Drawing.Size(672, 64);
			this.panel14.TabIndex = 24;
			// 
			// label28
			// 
			this.label28.Location = new System.Drawing.Point(8, 8);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(44, 17);
			this.label28.TabIndex = 0;
			this.label28.Text = "Регион:";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// teRegion
			// 
			this.teRegion.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teRegion.Location = new System.Drawing.Point(56, 8);
			this.teRegion.Name = "teRegion";
			this.teRegion.Size = new System.Drawing.Size(224, 20);
			this.teRegion.TabIndex = 18;
			this.teRegion.Text = "";
			this.teRegion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// teNearestCluster
			// 
			this.teNearestCluster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teNearestCluster.Location = new System.Drawing.Point(604, 8);
			this.teNearestCluster.Name = "teNearestCluster";
			this.teNearestCluster.Size = new System.Drawing.Size(64, 20);
			this.teNearestCluster.TabIndex = 18;
			this.teNearestCluster.Text = "";
			this.teNearestCluster.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label29
			// 
			this.label29.Location = new System.Drawing.Point(480, 8);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(120, 17);
			this.label29.TabIndex = 0;
			this.label29.Text = "Ближайший кластер:";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label30
			// 
			this.label30.Location = new System.Drawing.Point(300, 8);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(100, 17);
			this.label30.TabIndex = 0;
			this.label30.Text = "Всего кластеров:";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// teTotalClusters
			// 
			this.teTotalClusters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teTotalClusters.Location = new System.Drawing.Point(408, 8);
			this.teTotalClusters.Name = "teTotalClusters";
			this.teTotalClusters.Size = new System.Drawing.Size(64, 20);
			this.teTotalClusters.TabIndex = 18;
			this.teTotalClusters.Text = "";
			this.teTotalClusters.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label31
			// 
			this.label31.Location = new System.Drawing.Point(284, 36);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(120, 17);
			this.label31.TabIndex = 0;
			this.label31.Text = "Записей в кластере:";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label32
			// 
			this.label32.Location = new System.Drawing.Point(92, 36);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(120, 17);
			this.label32.TabIndex = 0;
			this.label32.Text = "\"Ближайший\" донор:";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// teTotal
			// 
			this.teTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teTotal.Location = new System.Drawing.Point(408, 36);
			this.teTotal.Name = "teTotal";
			this.teTotal.Size = new System.Drawing.Size(64, 20);
			this.teTotal.TabIndex = 18;
			this.teTotal.Text = "";
			this.teTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label33
			// 
			this.label33.Location = new System.Drawing.Point(480, 36);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(120, 17);
			this.label33.TabIndex = 0;
			this.label33.Text = "Доноров в кластере:";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// teDonor
			// 
			this.teDonor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teDonor.Location = new System.Drawing.Point(604, 36);
			this.teDonor.Name = "teDonor";
			this.teDonor.Size = new System.Drawing.Size(64, 20);
			this.teDonor.TabIndex = 18;
			this.teDonor.Text = "";
			this.teDonor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// teNearestValue
			// 
			this.teNearestValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teNearestValue.Location = new System.Drawing.Point(216, 36);
			this.teNearestValue.Name = "teNearestValue";
			this.teNearestValue.Size = new System.Drawing.Size(64, 20);
			this.teNearestValue.TabIndex = 18;
			this.teNearestValue.Text = "";
			this.teNearestValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tabPage9
			// 
			this.tabPage9.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage9.Controls.Add(this.lvCorrectedData);
			this.tabPage9.Controls.Add(this.panel15);
			this.tabPage9.Location = new System.Drawing.Point(4, 25);
			this.tabPage9.Name = "tabPage9";
			this.tabPage9.Size = new System.Drawing.Size(672, 432);
			this.tabPage9.TabIndex = 3;
			this.tabPage9.Text = "Этап 4. Импутация";
			// 
			// lvCorrectedData
			// 
			this.lvCorrectedData.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvCorrectedData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							  this.columnHeader39,
																							  this.columnHeader40,
																							  this.columnHeader41,
																							  this.columnHeader42,
																							  this.columnHeader43,
																							  this.columnHeader44,
																							  this.columnHeader45});
			this.lvCorrectedData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvCorrectedData.FullRowSelect = true;
			this.lvCorrectedData.GridLines = true;
			this.lvCorrectedData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvCorrectedData.Location = new System.Drawing.Point(0, 63);
			this.lvCorrectedData.Name = "lvCorrectedData";
			this.lvCorrectedData.Size = new System.Drawing.Size(672, 369);
			this.lvCorrectedData.TabIndex = 21;
			this.lvCorrectedData.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader39
			// 
			this.columnHeader39.Text = "№";
			this.columnHeader39.Width = 30;
			// 
			// columnHeader40
			// 
			this.columnHeader40.Text = "Код";
			this.columnHeader40.Width = 50;
			// 
			// columnHeader41
			// 
			this.columnHeader41.Text = "Обозначение";
			this.columnHeader41.Width = 100;
			// 
			// columnHeader42
			// 
			this.columnHeader42.Text = "Название";
			this.columnHeader42.Width = 0;
			// 
			// columnHeader43
			// 
			this.columnHeader43.Text = "Значение";
			this.columnHeader43.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// columnHeader44
			// 
			this.columnHeader44.Text = "Восстановлено";
			this.columnHeader44.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader44.Width = 95;
			// 
			// columnHeader45
			// 
			this.columnHeader45.Text = "Примененные правила";
			this.columnHeader45.Width = 312;
			// 
			// panel15
			// 
			this.panel15.BackColor = System.Drawing.Color.NavajoWhite;
			this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel15.Enabled = false;
			this.panel15.Location = new System.Drawing.Point(0, 0);
			this.panel15.Name = "panel15";
			this.panel15.Size = new System.Drawing.Size(672, 63);
			this.panel15.TabIndex = 24;
			// 
			// panel10
			// 
			this.panel10.Controls.Add(this.lvData);
			this.panel10.Controls.Add(this.pnEdit);
			this.panel10.Controls.Add(this.label14);
			this.panel10.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel10.Location = new System.Drawing.Point(0, 0);
			this.panel10.Name = "panel10";
			this.panel10.Size = new System.Drawing.Size(264, 461);
			this.panel10.TabIndex = 0;
			// 
			// lvData
			// 
			this.lvData.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					 this.columnHeader20,
																					 this.columnHeader21,
																					 this.columnHeader22,
																					 this.columnHeader23,
																					 this.columnHeader24});
			this.lvData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvData.FullRowSelect = true;
			this.lvData.GridLines = true;
			this.lvData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvData.Location = new System.Drawing.Point(0, 88);
			this.lvData.Name = "lvData";
			this.lvData.Size = new System.Drawing.Size(264, 373);
			this.lvData.TabIndex = 21;
			this.lvData.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader20
			// 
			this.columnHeader20.Text = "№";
			this.columnHeader20.Width = 30;
			// 
			// columnHeader21
			// 
			this.columnHeader21.Text = "Код";
			this.columnHeader21.Width = 50;
			// 
			// columnHeader22
			// 
			this.columnHeader22.Text = "Обозначение";
			this.columnHeader22.Width = 100;
			// 
			// columnHeader23
			// 
			this.columnHeader23.Text = "Название";
			this.columnHeader23.Width = 0;
			// 
			// columnHeader24
			// 
			this.columnHeader24.Text = "Значение";
			this.columnHeader24.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// pnEdit
			// 
			this.pnEdit.BackColor = System.Drawing.Color.NavajoWhite;
			this.pnEdit.Controls.Add(this.cbQuestionarieEdit);
			this.pnEdit.Controls.Add(this.label13);
			this.pnEdit.Controls.Add(this.lbFirstE);
			this.pnEdit.Controls.Add(this.teFormEID);
			this.pnEdit.Controls.Add(this.lbLastE);
			this.pnEdit.Controls.Add(this.label36);
			this.pnEdit.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnEdit.Enabled = false;
			this.pnEdit.Location = new System.Drawing.Point(0, 23);
			this.pnEdit.Name = "pnEdit";
			this.pnEdit.Size = new System.Drawing.Size(264, 65);
			this.pnEdit.TabIndex = 22;
			// 
			// cbQuestionarieEdit
			// 
			this.cbQuestionarieEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbQuestionarieEdit.Location = new System.Drawing.Point(4, 8);
			this.cbQuestionarieEdit.Name = "cbQuestionarieEdit";
			this.cbQuestionarieEdit.Size = new System.Drawing.Size(252, 21);
			this.cbQuestionarieEdit.TabIndex = 1;
			this.cbQuestionarieEdit.SelectedIndexChanged += new System.EventHandler(this.cbQuestionarieEdit_SelectedIndexChanged);
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(8, 8);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(100, 17);
			this.label13.TabIndex = 0;
			this.label13.Text = "Выбор формы : ";
			this.label13.Visible = false;
			// 
			// lbFirstE
			// 
			this.lbFirstE.BackColor = System.Drawing.Color.NavajoWhite;
			this.lbFirstE.Enabled = false;
			this.lbFirstE.Location = new System.Drawing.Point(140, 40);
			this.lbFirstE.Name = "lbFirstE";
			this.lbFirstE.Size = new System.Drawing.Size(24, 20);
			this.lbFirstE.TabIndex = 16;
			this.lbFirstE.TabStop = true;
			this.lbFirstE.Text = "<<";
			this.lbFirstE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbFirstE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbFirstE_LinkClicked);
			// 
			// teFormEID
			// 
			this.teFormEID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.teFormEID.Enabled = false;
			this.teFormEID.Location = new System.Drawing.Point(164, 40);
			this.teFormEID.Name = "teFormEID";
			this.teFormEID.Size = new System.Drawing.Size(64, 20);
			this.teFormEID.TabIndex = 18;
			this.teFormEID.Text = "";
			this.teFormEID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.teFormEID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.teFormEID_KeyPress);
			this.teFormEID.Leave += new System.EventHandler(this.teFormEID_Leave);
			// 
			// lbLastE
			// 
			this.lbLastE.BackColor = System.Drawing.Color.NavajoWhite;
			this.lbLastE.Enabled = false;
			this.lbLastE.Location = new System.Drawing.Point(228, 40);
			this.lbLastE.Name = "lbLastE";
			this.lbLastE.Size = new System.Drawing.Size(24, 20);
			this.lbLastE.TabIndex = 17;
			this.lbLastE.TabStop = true;
			this.lbLastE.Text = ">>";
			this.lbLastE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lbLastE.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbLastE_LinkClicked);
			// 
			// label14
			// 
			this.label14.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label14.Dock = System.Windows.Forms.DockStyle.Top;
			this.label14.Location = new System.Drawing.Point(0, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(264, 23);
			this.label14.TabIndex = 20;
			this.label14.Text = "Выбор формы и хозяйства";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tbProtocol
			// 
			this.tbProtocol.Controls.Add(this.lvProtocol);
			this.tbProtocol.Controls.Add(this.label34);
			this.tbProtocol.Location = new System.Drawing.Point(4, 22);
			this.tbProtocol.Name = "tbProtocol";
			this.tbProtocol.Size = new System.Drawing.Size(946, 463);
			this.tbProtocol.TabIndex = 5;
			this.tbProtocol.Text = "Протокол";
			// 
			// lvProtocol
			// 
			this.lvProtocol.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.lvProtocol.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.columnHeader50,
																						 this.columnHeader51,
																						 this.columnHeader52,
																						 this.columnHeader53,
																						 this.columnHeader54,
																						 this.columnHeader55,
																						 this.columnHeader56,
																						 this.columnHeader57});
			this.lvProtocol.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvProtocol.FullRowSelect = true;
			this.lvProtocol.GridLines = true;
			this.lvProtocol.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvProtocol.Location = new System.Drawing.Point(0, 23);
			this.lvProtocol.Name = "lvProtocol";
			this.lvProtocol.Size = new System.Drawing.Size(946, 440);
			this.lvProtocol.TabIndex = 22;
			this.lvProtocol.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader50
			// 
			this.columnHeader50.Text = "№";
			this.columnHeader50.Width = 30;
			// 
			// columnHeader51
			// 
			this.columnHeader51.Text = "Дата/Время";
			this.columnHeader51.Width = 84;
			// 
			// columnHeader52
			// 
			this.columnHeader52.Text = "Форма";
			this.columnHeader52.Width = 261;
			// 
			// columnHeader53
			// 
			this.columnHeader53.Text = "Код записи";
			this.columnHeader53.Width = 71;
			// 
			// columnHeader54
			// 
			this.columnHeader54.Text = "Показатель";
			this.columnHeader54.Width = 102;
			// 
			// columnHeader55
			// 
			this.columnHeader55.Text = "Старое значение";
			this.columnHeader55.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader55.Width = 99;
			// 
			// columnHeader56
			// 
			this.columnHeader56.Text = "Новое значение";
			this.columnHeader56.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.columnHeader56.Width = 96;
			// 
			// columnHeader57
			// 
			this.columnHeader57.Text = "Правило";
			this.columnHeader57.Width = 177;
			// 
			// label34
			// 
			this.label34.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label34.Dock = System.Windows.Forms.DockStyle.Top;
			this.label34.Location = new System.Drawing.Point(0, 0);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(946, 23);
			this.label34.TabIndex = 21;
			this.label34.Text = "Протокол автокррекции и импутации";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label36
			// 
			this.label36.Location = new System.Drawing.Point(32, 40);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(100, 17);
			this.label36.TabIndex = 0;
			this.label36.Text = "Номер записи : ";
			this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FormMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(954, 555);
			this.Controls.Add(this.tabControlMain);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.toolBar);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Menu = this.mainMenu;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Система импутации и автокоррекции";
			this.Load += new System.EventHandler(this.FormMain_Load);
			((System.ComponentModel.ISupportInitialize)(this.sbpConnectState)).EndInit();
			this.tabControlMain.ResumeLayout(false);
			this.tbData.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.pnQuestionarieData.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.tbClusters.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.pnRegion.ResumeLayout(false);
			this.tbEditRules.ResumeLayout(false);
			this.panel8.ResumeLayout(false);
			this.pnEditRule.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tpImputeRules.ResumeLayout(false);
			this.panel9.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			this.pnImputeRule.ResumeLayout(false);
			this.tbAutoCorrect.ResumeLayout(false);
			this.tabControl2.ResumeLayout(false);
			this.tabPage6.ResumeLayout(false);
			this.panel12.ResumeLayout(false);
			this.tabPage7.ResumeLayout(false);
			this.panel13.ResumeLayout(false);
			this.tabPage8.ResumeLayout(false);
			this.panel14.ResumeLayout(false);
			this.tabPage9.ResumeLayout(false);
			this.panel10.ResumeLayout(false);
			this.pnEdit.ResumeLayout(false);
			this.tbProtocol.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FormMain());
		}


		private void SetFormState(FormStates fs, string strConnection)
		{
			switch(fs)
			{
				case FormStates.efsDisconnected:
					EIS.Disconnect();
					statusBar.Text = "Подключение неактивно";
					pnRegion.Enabled = false;
					pnEditRule.Enabled = false;
					pnImputeRule.Enabled = false;
					pnQuestionarieData.Enabled = false;
					pnEdit.Enabled = false;
					
					miConnectRCEIS.Enabled = true;
					miConnectTo.Enabled = true;
					miDisconnect.Enabled = false;

					tbConnect.Pushed = false;

					lbFirst.Enabled = false;
					lbLast.Enabled = false;
					teFormID.Enabled = false;

					lbFirstE.Enabled = false;
					lbLastE.Enabled = false;
					teFormEID.Enabled = false;
					break;
				case FormStates.efsConnected:
					EIS.Connect(strConnection);
					if (EIS.Connected)
					{
						statusBar.Text = "Подключено к " + EIS.conn.Database;
						pnRegion.Enabled = true;
						pnEditRule.Enabled = true;
						pnImputeRule.Enabled = true;
						pnQuestionarieData.Enabled = true;
						pnEdit.Enabled = true;

						miConnectRCEIS.Enabled = false;
						miConnectTo.Enabled = false;
						miDisconnect.Enabled = true;

						lbFirstE.Enabled = false;
						lbLastE.Enabled = false;
						teFormEID.Enabled = false;
						
						tbConnect.Pushed = true;

						EIS.LoadRegionCollection();
						EIS.LoadDomainCollection();
						EIS.LoadQuestionarieCollection();
						EIS.LoadClusterSplittingCollection();
						EIS.LoadProtocolCollection();
						
						EIS.UpdateRegion(cbRegions);
						
						EIS.UpdateQuestionarie(tvQuestionarie);
						EIS.UpdateQuestionarie(cbQuestionarie);
						EIS.UpdateQuestionarie(cbQuestionarieData);
						EIS.UpdateQuestionarie(cbQuestionarieEdit);
						EIS.UpdateQuestionarie(cbQuestionarieImpute);
						EIS.UpdateProtocol(lvProtocol);
					}
					else
					{
						SetFormState(FormStates.efsDisconnected, "");
					}
					break;
			}
		}

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			//Пользователь выбрал кнопку соединения с базой данных
			if (e.Button == tbConnect)
			{
				if (EIS.Connected)
				{
					SetFormState(FormStates.efsDisconnected, "");
				}
				else
				{
					//Подключаемся
					SetFormState(FormStates.efsConnected, ConfigurationSettings.AppSettings["Sql_ConnectString"]);
				}
				
			}					
			
			//Нормализация данных
			if (e.Button == tbNormalizeData)
			{
				/*Questionarie qst = (Questionarie)cbQuestionarieData.Items[cbQuestionarieData.SelectedIndex];*/
				MessageBox.Show("Данная функция реализована в виде хранимых процедур");
			}

			//Разбиение данных на доноров/реципиентов
			if (e.Button == tbDonorRecipient)
			{
				if (EIS.Connected)
				{					
					ControlDonorRecipient cdr = new ControlDonorRecipient();
                
					double a=8;
				
					foreach(Questionarie qst in EIS.questionarieCollection)
						foreach(Region region in EIS.regionCollection)
						{				
							cdr.SplitData(EIS.conn, qst, region, a);
							cdr.SaveSplit(EIS.conn);
						}
					MessageBox.Show("Установка признаков донор/реципиент завершена");
				}
				else
				{
					MessageBox.Show("Требуется подключение к базе данных!");
				}
			}

			if (e.Button == tbCluster)
			{
				/*
				SqlCommand cmd = new SqlCommand("sp_getValueList", EIS.conn);
			
				cmd.CommandType = CommandType.StoredProcedure;
			
				cmd.Parameters.Add("@id_region", SqlDbType.VarChar);

				cmd.Parameters["@id_region"].Value = "56";

				SqlDataReader dr = cmd.ExecuteReader();

				DateTime begin = DateTime.Now;
				
				KMeans.KMeansUnitTest.ClusterDataReaderTestCase(dr, 23470);				
				
				DateTime end = DateTime.Now;
				TimeSpan span = end - begin;
				
				dr.Close();

				MessageBox.Show(span.TotalSeconds.ToString());*/

				MessageBox.Show("Алгоритм кластерного анализа реализован в виде отдельного приложения!");				
            }

			if (e.Button == tbEdits)
			{
				tabControlMain.SelectedIndex = 4;

				if (!EIS.Connected)
				{
					MessageBox.Show("Требуется подключение к базе данных!");
					return;
				}

				long id; 

				try
				{
					id = Int32.Parse(teFormEID.Text);
				}
				catch(Exception ex)
				{
					MessageBox.Show("Требуется указать номер записи!");
					return;
				}

				Questionarie qst = (Questionarie)cbQuestionarieEdit.Items[cbQuestionarieEdit.SelectedIndex];

				if (qst == null)
				{
					MessageBox.Show("Требуется выбрать анкетную форму!");
					return;
				}

                Value val = new Value(qst);
				val.Load(EIS.conn, id, qst);

				Region region = EIS.regionCollection.FindByOKATO(val.OKATO);
				
				//Проверка записи на удовлетворение правил редактирования
				CanChangeEditCheck = true;
				
				EIS.UpdateEditChecking(cbQuestionarieEdit, id, lvEditProtocol, val, region);
				
				teEditsAllFail.Text = EIS.failedAllEditCount.ToString();
				teEditsQNTFail.Text = EIS.failedQntEditCount.ToString();
				teEditsQLTFail.Text = EIS.failedQltEditCount.ToString();

				CanChangeEditCheck = false;

				//Получение матрицы неудовлетворенных правил
				EIS.UpdateFailedEdits(lvFailedEditMatrix);

				//Локализуем ошибку
				EIS.UpdateLocalizedError(lvFailedEditMatrix);

				teColumnAllFail.Text = EIS.failedAllColumnCount.ToString();
				teColumnQntFail.Text = EIS.failedQntColumnCount.ToString();
				teColumnQltFail.Text = EIS.failedQltColumnCount.ToString();

				//Ищем ближайший кластер
				EIS.UpdateClusterIndentifiction(lvClusterParams, teTotalClusters, teNearestCluster, teRegion);

				//Загружаем доноров и ищем ближайшего соседа
				EIS.UpdateNearestValue(teTotal, teDonor, teNearestValue);

				//Импутируем данные
				EIS.UpdateImputedValue(lvCorrectedData);
			}

			//Автоматическая генерация правил импутации показателей
			if (e.Button == tbGenerateImputeRules)
			{
				if (!EIS.Connected)
				{
					MessageBox.Show("Требуется подключение к базе данных!");
					return;
				}

				tabControlMain.SelectedIndex = 3;

				if (cbQuestionarieImpute.SelectedIndex != -1)
				{
					Questionarie qst = (Questionarie)cbQuestionarieImpute.Items[cbQuestionarieImpute.SelectedIndex];
				
					EIS.GenerateAllImputeRules(qst.ID);

					EIS.UpdateQuestionarieColumns(tvImputeColumns, qst.ID);
				}
				else
				{
					MessageBox.Show("Требуется выбрать анкетyю форму!");
				}
			}

			if (e.Button == tbTestImpute)
			{
				if (!EIS.Connected)
				{
					MessageBox.Show("Требуется подключение к базе данных!");
					return;
				}

				DateTime dt1 = DateTime.Now;

				// Тестируем импутацию

				long id_form = 1;

				for(long id_cluster = 17; id_cluster <33; id_cluster ++)				{		

					Region region = EIS.regionCollection.FindByOKATO("03");

					Questionarie qst = (Questionarie)cbQuestionarieEdit.Items[cbQuestionarieEdit.SelectedIndex];

					ClusterSplitting cs = EIS.csCollection.Find(region.ID, id_form);

					double [,] data = ControlDonorRecipient.LoadData(EIS.conn, qst, region, false, false, id_cluster);
					if (data.GetUpperBound(0) == -1)
						data = ControlDonorRecipient.LoadData(EIS.conn, qst, region, true, false, id_cluster);

					double [,] donors = ControlDonorRecipient.LoadData(EIS.conn, qst, region, true, false, id_cluster);
					if (donors.GetUpperBound(0) == -1)
						donors = ControlDonorRecipient.LoadData(EIS.conn, qst, region, false, false, id_cluster);

					FileInfo f = new FileInfo("abs_err"+id_cluster.ToString()+".txt");

					StreamWriter writer = f.CreateText(); 

					int n = 20, k = 45;

					for (int idx = 0; idx < k; idx++)
					{		
						double abs_err = 0.0;

						for (int i=0; i<n; i++)
						{
				
							if (i>data.GetUpperBound(0)) break;
							long id = (long)(data[i,0]);				
							
							Value val = new Value(qst);
							val.Load(qst, id, region.OKATO, ref data, i);

							double old_val = (double)val[idx];

							val[idx] = (double)(-1.0);			
			
							EIS.DoOnlyImpute(cbQuestionarieEdit, id, lvEditProtocol, val, region, ref donors);				

							if (EIS.controlImpute.corrected[0] != null)
							{
								abs_err += Math.Abs((double)EIS.controlImpute.corrected[idx] - old_val);
							}
						}
						writer.WriteLine("{0:F4}", abs_err / n);
					}
					writer.Close();
				}
				
				DateTime dt2 = DateTime.Now;
				TimeSpan ts = dt2 - dt1;

				MessageBox.Show(ts.TotalSeconds.ToString());
			}

			Cursor.Current = Cursors.Default;
		}

		private void btNewRegion_Click(object sender, System.EventArgs e)
		{
			FormRegion f = new FormRegion();
			if (f.ShowDialog() == DialogResult.OK)
			{
				Region region = new Region();
				region.Name = f.teName.Text;
				region.OKATO = f.teOKATO.Text;
				
				EIS.regionCollection.Insert(EIS.conn, region);
				EIS.UpdateRegion(cbRegions);
			}
		}

		private void btUpdateRegion_Click(object sender, System.EventArgs e)
		{
			Region region = (Region)cbRegions.SelectedItem;
			if (region == null)
			{
				MessageBox.Show("Не выбран регион");
			}

			FormRegion f = new FormRegion();
			f.teName.Text = region.Name;
			f.teOKATO.Text = region.OKATO;

			if (f.ShowDialog() == DialogResult.OK)
			{
				region.Name = f.teName.Text;
				region.OKATO = f.teOKATO.Text;
				
				EIS.regionCollection.Update(EIS.conn, region);
				EIS.UpdateRegion(cbRegions);
			}		
		}

		private void miConnectRCEIS_Click(object sender, System.EventArgs e)
		{
			if (EIS.Connected)
			{
				SetFormState(FormStates.efsDisconnected, "");
			}
			else
			{
				SetFormState(FormStates.efsConnected, ConfigurationSettings.AppSettings["Sql_ConnectString"]);
			}		
		}

		private void miExit_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void miConnectTo_Click(object sender, System.EventArgs e)
		{
			Cursor.Current = Cursors.WaitCursor;

			//Пользователь выбрал кнопку соединения с базой данных
			if (!EIS.Connected)
			{
				//Подключаемся
				ConnectForm f = new ConnectForm();	
				if (f.ShowDialog() == DialogResult.OK)
				{
					SetFormState(FormStates.efsConnected, f.teConnect.Text);
				}	
			}
			Cursor.Current = Cursors.Default;
		}

		private void btDeleteRegion_Click(object sender, System.EventArgs e)
		{
			Region region = (Region)cbRegions.SelectedItem;
			if (region == null)
			{
				MessageBox.Show("Не выбран регион");
			}		
			else
			{
				EIS.regionCollection.Delete(EIS.conn, region);
				EIS.UpdateRegion(cbRegions);
			}
		}

		private void miViewRegions_Click(object sender, System.EventArgs e)
		{
			FormRegionView f = new FormRegionView();
			EIS.UpdateRegions(f.lvRegions);
			f.ShowDialog();
		}

		private void cbRegions_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (cbRegions.SelectedIndex == -1) return;
			
			Region region = (Region)(cbRegions.Items[cbRegions.SelectedIndex]);
			EIS.UpdateClustersSplitting(tvClusters, region.ID);
			lvSplittingColumns.Items.Clear();
			lvSplittingClusters.Items.Clear();
		}

		private void tvClusters_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			EIS.UpdateSplittingColumns(tvClusters, lvSplittingColumns);
			EIS.UpdateSplittingClusters(tvClusters, lvSplittingClusters);
		}

		private void miDomainView_Click(object sender, System.EventArgs e)
		{
			FormDomainView f = new FormDomainView();			
			f.EIS = EIS;
			EIS.UpdateDomains(f.lvDomains);
			f.ShowDialog();
		}

		private void UpdateEditRules()
		{
			if (cbQuestionarie.SelectedIndex == -1) return;

			Questionarie qst =(Questionarie)(cbQuestionarie.Items[cbQuestionarie.SelectedIndex]);

			teEditRulesCount.Text = qst.editRuleCollection.Count.ToString();

			DomainType domain_type;

			if (rgQuantity.Checked)
				domain_type = DomainType.QuantitiveDomain;
			else
				domain_type = DomainType.QualitativeDomain;

			EIS.UpdateColumnValues(lvEditRules, qst.ID, domain_type);
		}

		private void cbQuestionarie_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			UpdateEditRules();
		}

		private void rgQuantity_CheckedChanged(object sender, System.EventArgs e)
		{
			UpdateEditRules();
		}

		private void miDisconnect_Click(object sender, System.EventArgs e)
		{
			SetFormState(FormStates.efsDisconnected, "");
		}

		private void FormMain_Load(object sender, System.EventArgs e)
		{
			SetFormState(FormStates.efsDisconnected, "");
		}

		private void lvSplittingClusters_DoubleClick(object sender, System.EventArgs e)
		{
			FormClusterView f = new FormClusterView();
			f.EIS = EIS;
			EIS.UpdateSplittingClusters(tvClusters, f.lvSplittingClusters);			
			f.ShowDialog();
		}

		private void cbQuestionarieImpute_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Questionarie qst = (Questionarie)cbQuestionarieImpute.Items[cbQuestionarieImpute.SelectedIndex];

			EIS.UpdateQuestionarieColumns(tvImputeColumns, qst.ID);
			EIS.UpdateQuestionarieColumns(lvImputeColumns, qst.ID);
		}

		private void cbQuestionarieData_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Questionarie qst = (Questionarie)cbQuestionarieData.Items[cbQuestionarieData.SelectedIndex];

			EIS.UpdateQuestionarieColumns(lvQuestionarieData, qst.ID, true);			

			if ((qst == null) || (qst.CountID==0))
			{
                lbFirst.Enabled = false;
				lbLast.Enabled = false;
				teFormID.Enabled = false;
			}
			else
			{
				lbFirst.Enabled = true;
				lbLast.Enabled = true;
				teFormID.Enabled = true;				
				teFormID.Text = qst.MinID.ToString();
			}

		}

		private void lbFirst_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Questionarie qst = (Questionarie)cbQuestionarieData.Items[cbQuestionarieData.SelectedIndex];

			Cursor.Current = Cursors.WaitCursor;

			long id = Int32.Parse(teFormID.Text);

			id--;

			if (id < qst.MinID)
				id = qst.MinID;

			teFormID.Text = id.ToString();
			
			EIS.UpdateValueData(lvQuestionarieData, cbQuestionarieData, id);						

			Cursor.Current = Cursors.Default;
		}

		private void lbLast_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Questionarie qst = (Questionarie)cbQuestionarieData.Items[cbQuestionarieData.SelectedIndex];

			Cursor.Current = Cursors.WaitCursor;

			long id = Int32.Parse(teFormID.Text);

			id++;

			if (id > qst.MaxID)
				id = qst.MaxID;

			teFormID.Text = id.ToString();
			
			EIS.UpdateValueData(lvQuestionarieData, cbQuestionarieData, id);			

			Cursor.Current = Cursors.Default;
		}

		private void teFormID_TextChanged(object sender, System.EventArgs e)
		{
			
		}

		private void teFormID_Leave(object sender, System.EventArgs e)
		{
			Questionarie qst = (Questionarie)cbQuestionarieData.Items[cbQuestionarieData.SelectedIndex];

			Cursor.Current = Cursors.WaitCursor;

			long id = Int32.Parse(teFormID.Text);

			if ((id > qst.MaxID) || (id < qst.MinID))
				id = qst.MinID;

			teFormID.Text = id.ToString();
			
			EIS.UpdateValueData(lvQuestionarieData, cbQuestionarieData, id);	

			Cursor.Current = Cursors.Default;
		}

		private void cbQuestionarieEdit_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Questionarie qst = (Questionarie)cbQuestionarieEdit.Items[cbQuestionarieEdit.SelectedIndex];

			EIS.UpdateQuestionarieColumns(lvData, qst.ID, true);			
			EIS.UpdateQuestionarieColumns(lvCorrectedData, qst.ID, true);			
			EIS.UpdateQuestionarieColumns(lvFailedEditMatrix, qst.ID, false);

			if (qst == null)
			{
				lbFirstE.Enabled = false;
				lbLastE.Enabled = false;
				teFormEID.Enabled = false;

				teEditsAll.Text = "";
				teEditsQNT.Text = "";
				teEditsQLT.Text = "";

				teEditsAllFail.Text = "";
				teEditsQNTFail.Text = "";
				teEditsQLTFail.Text = "";

				teColumnAll.Text = "";
				teColumnQlt.Text = "";
				teColumnQnt.Text = "";

				teColumnAllFail.Text = "";
				teColumnQntFail.Text = "";
				teColumnQltFail.Text = "";
			}
			else
			{
				lbFirstE.Enabled = true;
				lbLastE.Enabled = true;
				teFormEID.Enabled = true;

				teEditsAll.Text = qst.editRuleCollection.Count.ToString();
				teEditsQNT.Text = qst.editRuleCollection.GetCountQNT().ToString();
				teEditsQLT.Text = qst.editRuleCollection.GetCountQLT().ToString();

				teEditsAllFail.Text = "";
				teEditsQNTFail.Text = "";
				teEditsQLTFail.Text = "";

				teColumnAll.Text = qst.GetColumnCollection().Count.ToString();
				teColumnQlt.Text = qst.GetColumnCollection(DomainType.QualitativeDomain).Count.ToString();
				teColumnQnt.Text = qst.GetColumnCollection(DomainType.QuantitiveDomain).Count.ToString();

				teColumnAllFail.Text = "";
				teColumnQntFail.Text = "";
				teColumnQltFail.Text = "";

				teFormEID.Text = qst.MinID.ToString();
			}
		}

		private void lbFirstE_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Questionarie qst = (Questionarie)cbQuestionarieEdit.Items[cbQuestionarieEdit.SelectedIndex];

			Cursor.Current = Cursors.WaitCursor;

			long id = Int32.Parse(teFormEID.Text);

			id--;

			if (id < qst.MinID)
				id = qst.MinID;

			teFormEID.Text = id.ToString();
			
			EIS.UpdateValueData(lvData, cbQuestionarieEdit, id);	
			EIS.UpdateValueData(lvCorrectedData, cbQuestionarieEdit, id);		

			teRegion.Text = EIS.region.Name;

			Cursor.Current = Cursors.Default;
		}

		private void lbLastE_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Questionarie qst = (Questionarie)cbQuestionarieEdit.Items[cbQuestionarieEdit.SelectedIndex];

			Cursor.Current = Cursors.WaitCursor;

			long id = Int32.Parse(teFormEID.Text);

			id++;

			if (id > qst.MaxID)
				id = qst.MaxID;

			teFormEID.Text = id.ToString();
			
			EIS.UpdateValueData(lvData, cbQuestionarieEdit, id);	
			EIS.UpdateValueData(lvCorrectedData, cbQuestionarieEdit, id);		

			teRegion.Text = EIS.region.Name;

			Cursor.Current = Cursors.Default;
		}

		private void teFormEID_Leave(object sender, System.EventArgs e)
		{
			Questionarie qst = (Questionarie)cbQuestionarieEdit.Items[cbQuestionarieEdit.SelectedIndex];

			Cursor.Current = Cursors.WaitCursor;

			long id = Int32.Parse(teFormEID.Text);
			
			if ((id > qst.MaxID) || ((id < qst.MinID)))
				id = qst.MinID;

			teFormEID.Text = id.ToString();

			EIS.UpdateValueData(lvData, cbQuestionarieEdit, id);	
			EIS.UpdateValueData(lvCorrectedData, cbQuestionarieEdit, id);

			teRegion.Text = EIS.region.Name;

			Cursor.Current = Cursors.Default;
		}

		private void lvEditProtocol_ItemCheck(object sender, System.Windows.Forms.ItemCheckEventArgs e)
		{
			if (!CanChangeEditCheck)
				e.NewValue = e.CurrentValue;
		}

		private void teFormEID_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\n')
			{
				Cursor.Current = Cursors.WaitCursor;

				long id = Int32.Parse(teFormEID.Text);
			
				EIS.UpdateValueData(lvData, cbQuestionarieEdit, id);	
				EIS.UpdateValueData(lvCorrectedData, cbQuestionarieEdit, id);

				teRegion.Text = EIS.region.Name;

				Cursor.Current = Cursors.Default;			
			}
		}

		private void tvQuestionarie_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			EIS.UpdateQuestionarieInfo(tvQuestionarie, lvInfo);
		}

	}
}
