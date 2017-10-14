namespace HamLogBook
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TSStLLocalTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runBackupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupOptionsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.printLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.resetCounterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.markSelectedRecordsAsQSLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.receivedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.editCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCurrentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.eQSLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.printLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qSLSentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.qSLReceivedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbLast50 = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.cbListen = new System.Windows.Forms.CheckBox();
            this.lblEntryMode = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.cbMode = new System.Windows.Forms.ComboBox();
            this.pnlBand = new System.Windows.Forms.Panel();
            this.pnlPower = new System.Windows.Forms.Panel();
            this.lblPower = new System.Windows.Forms.Label();
            this.tbPower = new System.Windows.Forms.TextBox();
            this.dtpTimeOff = new System.Windows.Forms.DateTimePicker();
            this.pnlTimeOff = new System.Windows.Forms.Panel();
            this.lblTimeOff = new System.Windows.Forms.Label();
            this.tbComments = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnLogContact = new System.Windows.Forms.Button();
            this.cbQSLRcvd = new System.Windows.Forms.CheckBox();
            this.cbQSLSent = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblComments = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbOther = new System.Windows.Forms.TextBox();
            this.tbFrequency = new System.Windows.Forms.TextBox();
            this.cbCounty = new System.Windows.Forms.ComboBox();
            this.cbState = new System.Windows.Forms.ComboBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.pnlOther = new System.Windows.Forms.Panel();
            this.lblOther = new System.Windows.Forms.Label();
            this.pnlFrequency = new System.Windows.Forms.Panel();
            this.lblFrequency = new System.Windows.Forms.Label();
            this.pnlCounty = new System.Windows.Forms.Panel();
            this.lblCounty = new System.Windows.Forms.Label();
            this.pnlState = new System.Windows.Forms.Panel();
            this.lblState = new System.Windows.Forms.Label();
            this.pnlName = new System.Windows.Forms.Panel();
            this.lblName = new System.Windows.Forms.Label();
            this.cbCountry = new System.Windows.Forms.ComboBox();
            this.pnlCountry = new System.Windows.Forms.Panel();
            this.lblCountry = new System.Windows.Forms.Label();
            this.tbRec = new System.Windows.Forms.TextBox();
            this.pnlRec = new System.Windows.Forms.Panel();
            this.lblRec = new System.Windows.Forms.Label();
            this.tbSent = new System.Windows.Forms.TextBox();
            this.pnlSent = new System.Windows.Forms.Panel();
            this.lblSent = new System.Windows.Forms.Label();
            this.dtpTimeOn = new System.Windows.Forms.DateTimePicker();
            this.pnlTimeOn = new System.Windows.Forms.Panel();
            this.lblTimeOn = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.pnlDate = new System.Windows.Forms.Panel();
            this.lblDate = new System.Windows.Forms.Label();
            this.pnlMode = new System.Windows.Forms.Panel();
            this.cbBand = new System.Windows.Forms.ComboBox();
            this.lblBand = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.hlbInputBox1 = new HamLogBook.HLBInputBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlMode.SuspendLayout();
            this.pnlBand.SuspendLayout();
            this.pnlPower.SuspendLayout();
            this.pnlTimeOff.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlOther.SuspendLayout();
            this.pnlFrequency.SuspendLayout();
            this.pnlCounty.SuspendLayout();
            this.pnlState.SuspendLayout();
            this.pnlName.SuspendLayout();
            this.pnlCountry.SuspendLayout();
            this.pnlRec.SuspendLayout();
            this.pnlSent.SuspendLayout();
            this.pnlTimeOn.SuspendLayout();
            this.pnlDate.SuspendLayout();
            this.pnlPower.SuspendLayout();
            this.pnlMode.SuspendLayout();
            this.pnlBand.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSStLLocalTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 502);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(889, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TSStLLocalTime
            // 
            this.TSStLLocalTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSStLLocalTime.Name = "TSStLLocalTime";
            this.TSStLLocalTime.Size = new System.Drawing.Size(62, 17);
            this.TSStLLocalTime.Text = "LocalTime";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolStripMenuItem1,
            this.helpToolStripMenuItem,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem7,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(889, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createBackupToolStripMenuItem,
            this.toolStripMenuItem2,
            this.printLogToolStripMenuItem,
            this.printAddressToolStripMenuItem,
            this.toolStripMenuItem4,
            this.resetCounterToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // createBackupToolStripMenuItem
            // 
            this.createBackupToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runBackupToolStripMenuItem,
            this.backupOptionsToolStripMenuItem1});
            this.createBackupToolStripMenuItem.Name = "createBackupToolStripMenuItem";
            this.createBackupToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.createBackupToolStripMenuItem.Text = "Create Backup";
            // 
            // runBackupToolStripMenuItem
            // 
            this.runBackupToolStripMenuItem.Name = "runBackupToolStripMenuItem";
            this.runBackupToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.runBackupToolStripMenuItem.Text = "Run Backup";
            this.runBackupToolStripMenuItem.Click += new System.EventHandler(this.runBackupToolStripMenuItem_Click);
            // 
            // backupOptionsToolStripMenuItem1
            // 
            this.backupOptionsToolStripMenuItem1.Name = "backupOptionsToolStripMenuItem1";
            this.backupOptionsToolStripMenuItem1.Size = new System.Drawing.Size(158, 22);
            this.backupOptionsToolStripMenuItem1.Text = "Backup Options";
            this.backupOptionsToolStripMenuItem1.Click += new System.EventHandler(this.backupOptionsToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(147, 6);
            // 
            // printLogToolStripMenuItem
            // 
            this.printLogToolStripMenuItem.Name = "printLogToolStripMenuItem";
            this.printLogToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.printLogToolStripMenuItem.Text = "Print Log";
            this.printLogToolStripMenuItem.Click += new System.EventHandler(this.printLogToolStripMenuItem_Click);
            // 
            // printAddressToolStripMenuItem
            // 
            this.printAddressToolStripMenuItem.Name = "printAddressToolStripMenuItem";
            this.printAddressToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.printAddressToolStripMenuItem.Text = "Print Address";
            this.printAddressToolStripMenuItem.Click += new System.EventHandler(this.printAddressToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(147, 6);
            // 
            // resetCounterToolStripMenuItem
            // 
            this.resetCounterToolStripMenuItem.Name = "resetCounterToolStripMenuItem";
            this.resetCounterToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.resetCounterToolStripMenuItem.Text = "Reset Counter";
            this.resetCounterToolStripMenuItem.Click += new System.EventHandler(this.resetCounterToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markSelectedRecordsAsQSLToolStripMenuItem,
            this.toolStripMenuItem5,
            this.editCurrentToolStripMenuItem,
            this.deleteCurrentToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(39, 20);
            this.toolStripMenuItem1.Text = "Edit";
            // 
            // markSelectedRecordsAsQSLToolStripMenuItem
            // 
            this.markSelectedRecordsAsQSLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sentToolStripMenuItem,
            this.receivedToolStripMenuItem});
            this.markSelectedRecordsAsQSLToolStripMenuItem.Name = "markSelectedRecordsAsQSLToolStripMenuItem";
            this.markSelectedRecordsAsQSLToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.markSelectedRecordsAsQSLToolStripMenuItem.Text = "Mark Selected Records as QSL";
            // 
            // sentToolStripMenuItem
            // 
            this.sentToolStripMenuItem.Name = "sentToolStripMenuItem";
            this.sentToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.sentToolStripMenuItem.Text = "Sent";
            this.sentToolStripMenuItem.Click += new System.EventHandler(this.qSLSentToolStripMenuItem_Click);
            // 
            // receivedToolStripMenuItem
            // 
            this.receivedToolStripMenuItem.Name = "receivedToolStripMenuItem";
            this.receivedToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.receivedToolStripMenuItem.Text = "Received";
            this.receivedToolStripMenuItem.Click += new System.EventHandler(this.receivedToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(228, 6);
            // 
            // editCurrentToolStripMenuItem
            // 
            this.editCurrentToolStripMenuItem.Name = "editCurrentToolStripMenuItem";
            this.editCurrentToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.editCurrentToolStripMenuItem.Text = "Edit Current";
            this.editCurrentToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteCurrentToolStripMenuItem
            // 
            this.deleteCurrentToolStripMenuItem.Name = "deleteCurrentToolStripMenuItem";
            this.deleteCurrentToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.deleteCurrentToolStripMenuItem.Text = "Delete Current";
            this.deleteCurrentToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.helpToolStripMenuItem.Text = "Settings";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(54, 20);
            this.toolStripMenuItem8.Text = "Search";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.eQSLToolStripMenuItem});
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(50, 20);
            this.toolStripMenuItem9.Text = "eLogs";
            // 
            // eQSLToolStripMenuItem
            // 
            this.eQSLToolStripMenuItem.Name = "eQSLToolStripMenuItem";
            this.eQSLToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.eQSLToolStripMenuItem.Text = "eQSL";
            this.eQSLToolStripMenuItem.Click += new System.EventHandler(this.eQSLToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(44, 20);
            this.toolStripMenuItem7.Text = "View";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cbListen);
            this.splitContainer1.Panel2.Controls.Add(this.hlbInputBox1);
            this.splitContainer1.Panel2.Controls.Add(this.lblEntryMode);
            this.splitContainer1.Panel2.Controls.Add(this.pnlMode);
            this.splitContainer1.Panel2.Controls.Add(this.cbMode);
            this.splitContainer1.Panel2.Controls.Add(this.pnlBand);
            this.splitContainer1.Panel2.Controls.Add(this.pnlPower);
            this.splitContainer1.Panel2.Controls.Add(this.cbBand);
            this.splitContainer1.Panel2.Controls.Add(this.tbPower);
            this.splitContainer1.Panel2.Controls.Add(this.dtpTimeOff);
            this.splitContainer1.Panel2.Controls.Add(this.pnlTimeOff);
            this.splitContainer1.Panel2.Controls.Add(this.tbComments);
            this.splitContainer1.Panel2.Controls.Add(this.btnClear);
            this.splitContainer1.Panel2.Controls.Add(this.btnLogContact);
            this.splitContainer1.Panel2.Controls.Add(this.cbQSLRcvd);
            this.splitContainer1.Panel2.Controls.Add(this.cbQSLSent);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.tbOther);
            this.splitContainer1.Panel2.Controls.Add(this.tbFrequency);
            this.splitContainer1.Panel2.Controls.Add(this.cbCounty);
            this.splitContainer1.Panel2.Controls.Add(this.cbState);
            this.splitContainer1.Panel2.Controls.Add(this.tbName);
            this.splitContainer1.Panel2.Controls.Add(this.pnlOther);
            this.splitContainer1.Panel2.Controls.Add(this.pnlFrequency);
            this.splitContainer1.Panel2.Controls.Add(this.pnlCounty);
            this.splitContainer1.Panel2.Controls.Add(this.pnlState);
            this.splitContainer1.Panel2.Controls.Add(this.pnlName);
            this.splitContainer1.Panel2.Controls.Add(this.cbCountry);
            this.splitContainer1.Panel2.Controls.Add(this.pnlCountry);
            this.splitContainer1.Panel2.Controls.Add(this.tbRec);
            this.splitContainer1.Panel2.Controls.Add(this.pnlRec);
            this.splitContainer1.Panel2.Controls.Add(this.tbSent);
            this.splitContainer1.Panel2.Controls.Add(this.pnlSent);
            this.splitContainer1.Panel2.Controls.Add(this.dtpTimeOn);
            this.splitContainer1.Panel2.Controls.Add(this.pnlTimeOn);
            this.splitContainer1.Panel2.Controls.Add(this.dtpDate);
            this.splitContainer1.Panel2.Controls.Add(this.pnlDate);
            this.splitContainer1.Size = new System.Drawing.Size(889, 478);
            this.splitContainer1.SplitterDistance = 272;
            this.splitContainer1.TabIndex = 3;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(1, 37);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(885, 232);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem3,
            this.printLabelToolStripMenuItem,
            this.qSLSentToolStripMenuItem,
            this.qSLReceivedToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(146, 120);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            this.editToolStripMenuItem.DoubleClick += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(142, 6);
            // 
            // printLabelToolStripMenuItem
            // 
            this.printLabelToolStripMenuItem.Name = "printLabelToolStripMenuItem";
            this.printLabelToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.printLabelToolStripMenuItem.Text = "Print Label";
            this.printLabelToolStripMenuItem.Click += new System.EventHandler(this.printLabelToolStripMenuItem_Click);
            // 
            // qSLSentToolStripMenuItem
            // 
            this.qSLSentToolStripMenuItem.Name = "qSLSentToolStripMenuItem";
            this.qSLSentToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.qSLSentToolStripMenuItem.Text = "QSL Sent";
            this.qSLSentToolStripMenuItem.Click += new System.EventHandler(this.qSLSentToolStripMenuItem_Click);
            // 
            // qSLReceivedToolStripMenuItem
            // 
            this.qSLReceivedToolStripMenuItem.Name = "qSLReceivedToolStripMenuItem";
            this.qSLReceivedToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.qSLReceivedToolStripMenuItem.Text = "QSL Received";
            this.qSLReceivedToolStripMenuItem.Click += new System.EventHandler(this.receivedToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.rbAll);
            this.panel2.Controls.Add(this.rbLast50);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnFind);
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(885, 36);
            this.panel2.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(779, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "x Listed Items";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.ForeColor = System.Drawing.Color.White;
            this.rbAll.Location = new System.Drawing.Point(689, 10);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(36, 17);
            this.rbAll.TabIndex = 3;
            this.rbAll.Text = "All";
            this.rbAll.UseVisualStyleBackColor = true;
            // 
            // rbLast50
            // 
            this.rbLast50.AutoSize = true;
            this.rbLast50.Checked = true;
            this.rbLast50.ForeColor = System.Drawing.Color.White;
            this.rbLast50.Location = new System.Drawing.Point(614, 10);
            this.rbLast50.Name = "rbLast50";
            this.rbLast50.Size = new System.Drawing.Size(60, 17);
            this.rbLast50.TabIndex = 2;
            this.rbLast50.TabStop = true;
            this.rbLast50.Text = "Last 50";
            this.rbLast50.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(95, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(700, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "Recent Contacts";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFind
            // 
            this.btnFind.BackColor = System.Drawing.SystemColors.Control;
            this.btnFind.Location = new System.Drawing.Point(10, 7);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 0;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = false;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // cbListen
            // 
            this.cbListen.AutoSize = true;
            this.cbListen.Location = new System.Drawing.Point(579, 91);
            this.cbListen.Name = "cbListen";
            this.cbListen.Size = new System.Drawing.Size(78, 17);
            this.cbListen.TabIndex = 43;
            this.cbListen.Text = "Listen Only";
            this.cbListen.UseVisualStyleBackColor = true;
            // 
            // lblEntryMode
            // 
            this.lblEntryMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryMode.Location = new System.Drawing.Point(665, 59);
            this.lblEntryMode.Name = "lblEntryMode";
            this.lblEntryMode.Size = new System.Drawing.Size(131, 49);
            this.lblEntryMode.TabIndex = 42;
            this.lblEntryMode.Text = "Entry Mode";
            this.lblEntryMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblEntryMode, "Indicates when adding a contact\r\nvrs editing a contact.");
            // 
            // pnlMode
            // 
            this.pnlMode.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlMode.Controls.Add(this.lblMode);
            this.pnlMode.Location = new System.Drawing.Point(390, 3);
            this.pnlMode.Name = "pnlMode";
            this.pnlMode.Size = new System.Drawing.Size(71, 28);
            this.pnlMode.TabIndex = 4;
            // 
            // lblMode
            // 
            this.lblMode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMode.ForeColor = System.Drawing.Color.White;
            this.lblMode.Location = new System.Drawing.Point(1, 1);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(67, 24);
            this.lblMode.TabIndex = 0;
            this.lblMode.Text = "Mode";
            this.lblMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblMode, "Mode of the contact.");
            this.lblMode.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // cbMode
            // 
            this.cbMode.FormattingEnabled = true;
            this.cbMode.Location = new System.Drawing.Point(390, 31);
            this.cbMode.Name = "cbMode";
            this.cbMode.Size = new System.Drawing.Size(71, 21);
            this.cbMode.TabIndex = 5;
            this.toolTip1.SetToolTip(this.cbMode, "Mode of the contact.");
            // 
            // pnlBand
            // 
            this.pnlBand.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlBand.Controls.Add(this.lblBand);
            this.pnlBand.Location = new System.Drawing.Point(291, 4);
            this.pnlBand.Name = "pnlBand";
            this.pnlBand.Size = new System.Drawing.Size(94, 28);
            this.pnlBand.TabIndex = 2;
            // 
            // lblBand
            // 
            this.lblBand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBand.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBand.ForeColor = System.Drawing.Color.White;
            this.lblBand.Location = new System.Drawing.Point(1, 1);
            this.lblBand.Name = "lblBand";
            this.lblBand.Size = new System.Drawing.Size(90, 24);
            this.lblBand.TabIndex = 0;
            this.lblBand.Text = "Band";
            this.lblBand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblBand, "Band of this contact.");
            this.lblBand.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // pnlPower
            // 
            this.pnlPower.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlPower.Controls.Add(this.lblPower);
            this.pnlPower.Location = new System.Drawing.Point(467, 3);
            this.pnlPower.Name = "pnlPower";
            this.pnlPower.Size = new System.Drawing.Size(81, 28);
            this.pnlPower.TabIndex = 6;
            // 
            // lblPower
            // 
            this.lblPower.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPower.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPower.ForeColor = System.Drawing.Color.White;
            this.lblPower.Location = new System.Drawing.Point(1, 1);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(77, 25);
            this.lblPower.TabIndex = 0;
            this.lblPower.Text = "Power";
            this.lblPower.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblPower, "Power in watts.");
            this.lblPower.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // cbBand
            // 
            this.cbBand.FormattingEnabled = true;
            this.cbBand.Location = new System.Drawing.Point(291, 31);
            this.cbBand.Name = "cbBand";
            this.cbBand.Size = new System.Drawing.Size(94, 21);
            this.cbBand.TabIndex = 4;
            this.toolTip1.SetToolTip(this.cbBand, "Band of this contact.");
            // 
            // tbPower
            // 
            this.tbPower.Location = new System.Drawing.Point(467, 31);
            this.tbPower.MaxLength = 4;
            this.tbPower.Name = "tbPower";
            this.tbPower.Size = new System.Drawing.Size(81, 20);
            this.tbPower.TabIndex = 6;
            this.toolTip1.SetToolTip(this.tbPower, "Power in watts.");
            // 
            // dtpTimeOff
            // 
            this.dtpTimeOff.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeOff.Location = new System.Drawing.Point(483, 86);
            this.dtpTimeOff.Name = "dtpTimeOff";
            this.dtpTimeOff.Size = new System.Drawing.Size(88, 20);
            this.dtpTimeOff.TabIndex = 15;
            this.dtpTimeOff.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // pnlTimeOff
            // 
            this.pnlTimeOff.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlTimeOff.Controls.Add(this.lblTimeOff);
            this.pnlTimeOff.Location = new System.Drawing.Point(483, 58);
            this.pnlTimeOff.Name = "pnlTimeOff";
            this.pnlTimeOff.Size = new System.Drawing.Size(88, 28);
            this.pnlTimeOff.TabIndex = 40;
            // 
            // lblTimeOff
            // 
            this.lblTimeOff.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimeOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeOff.ForeColor = System.Drawing.Color.White;
            this.lblTimeOff.Location = new System.Drawing.Point(1, 1);
            this.lblTimeOff.Name = "lblTimeOff";
            this.lblTimeOff.Size = new System.Drawing.Size(84, 24);
            this.lblTimeOff.TabIndex = 0;
            this.lblTimeOff.Text = "Time Off";
            this.lblTimeOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTimeOff.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // tbComments
            // 
            this.tbComments.Location = new System.Drawing.Point(111, 140);
            this.tbComments.Multiline = true;
            this.tbComments.Name = "tbComments";
            this.tbComments.Size = new System.Drawing.Size(766, 59);
            this.tbComments.TabIndex = 18;
            this.toolTip1.SetToolTip(this.tbComments, "Comments about this contact.");
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(802, 85);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "Clear";
            this.toolTip1.SetToolTip(this.btnClear, "Click to clear fields without\r\nrecording the contact.");
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            this.btnClear.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // btnLogContact
            // 
            this.btnLogContact.Location = new System.Drawing.Point(802, 58);
            this.btnLogContact.Name = "btnLogContact";
            this.btnLogContact.Size = new System.Drawing.Size(75, 23);
            this.btnLogContact.TabIndex = 19;
            this.btnLogContact.Text = "Log Contact";
            this.toolTip1.SetToolTip(this.btnLogContact, "Click to record the contact.");
            this.btnLogContact.UseVisualStyleBackColor = true;
            this.btnLogContact.Click += new System.EventHandler(this.btnLogContact_Click);
            this.btnLogContact.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // cbQSLRcvd
            // 
            this.cbQSLRcvd.AutoSize = true;
            this.cbQSLRcvd.Location = new System.Drawing.Point(579, 74);
            this.cbQSLRcvd.Name = "cbQSLRcvd";
            this.cbQSLRcvd.Size = new System.Drawing.Size(76, 17);
            this.cbQSLRcvd.TabIndex = 17;
            this.cbQSLRcvd.Text = "QSL Rcvd";
            this.toolTip1.SetToolTip(this.cbQSLRcvd, "Click when a QSL is received.");
            this.cbQSLRcvd.UseVisualStyleBackColor = true;
            // 
            // cbQSLSent
            // 
            this.cbQSLSent.AutoSize = true;
            this.cbQSLSent.Location = new System.Drawing.Point(579, 57);
            this.cbQSLSent.Name = "cbQSLSent";
            this.cbQSLSent.Size = new System.Drawing.Size(72, 17);
            this.cbQSLSent.TabIndex = 16;
            this.cbQSLSent.Text = "QSL Sent";
            this.toolTip1.SetToolTip(this.cbQSLSent, "Check when a QSL was sent.");
            this.cbQSLSent.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel1.Controls.Add(this.lblComments);
            this.panel1.Location = new System.Drawing.Point(111, 112);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(766, 28);
            this.panel1.TabIndex = 34;
            // 
            // lblComments
            // 
            this.lblComments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComments.ForeColor = System.Drawing.Color.White;
            this.lblComments.Location = new System.Drawing.Point(1, 3);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(762, 25);
            this.lblComments.TabIndex = 0;
            this.lblComments.Text = "Comments";
            this.lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblComments.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Cont:";
            this.label3.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Miles:";
            this.label2.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Bearing:";
            this.label1.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // tbOther
            // 
            this.tbOther.Location = new System.Drawing.Point(397, 86);
            this.tbOther.MaxLength = 128;
            this.tbOther.Name = "tbOther";
            this.tbOther.Size = new System.Drawing.Size(81, 20);
            this.tbOther.TabIndex = 14;
            // 
            // tbFrequency
            // 
            this.tbFrequency.Location = new System.Drawing.Point(296, 86);
            this.tbFrequency.MaxLength = 9;
            this.tbFrequency.Name = "tbFrequency";
            this.tbFrequency.Size = new System.Drawing.Size(95, 20);
            this.tbFrequency.TabIndex = 13;
            // 
            // cbCounty
            // 
            this.cbCounty.FormattingEnabled = true;
            this.cbCounty.Location = new System.Drawing.Point(197, 85);
            this.cbCounty.Name = "cbCounty";
            this.cbCounty.Size = new System.Drawing.Size(94, 21);
            this.cbCounty.TabIndex = 12;
            // 
            // cbState
            // 
            this.cbState.FormattingEnabled = true;
            this.cbState.Location = new System.Drawing.Point(112, 85);
            this.cbState.Name = "cbState";
            this.cbState.Size = new System.Drawing.Size(80, 21);
            this.cbState.TabIndex = 11;
            this.cbState.SelectedIndexChanged += new System.EventHandler(this.cbState_SelectedIndexChanged);
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(12, 85);
            this.tbName.MaxLength = 50;
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(93, 20);
            this.tbName.TabIndex = 10;
            this.toolTip1.SetToolTip(this.tbName, "Other operator\'s name.");
            // 
            // pnlOther
            // 
            this.pnlOther.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlOther.Controls.Add(this.lblOther);
            this.pnlOther.Location = new System.Drawing.Point(397, 58);
            this.pnlOther.Name = "pnlOther";
            this.pnlOther.Size = new System.Drawing.Size(81, 28);
            this.pnlOther.TabIndex = 25;
            // 
            // lblOther
            // 
            this.lblOther.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOther.ForeColor = System.Drawing.Color.White;
            this.lblOther.Location = new System.Drawing.Point(1, 1);
            this.lblOther.Name = "lblOther";
            this.lblOther.Size = new System.Drawing.Size(77, 25);
            this.lblOther.TabIndex = 0;
            this.lblOther.Text = "Other";
            this.lblOther.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOther.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // pnlFrequency
            // 
            this.pnlFrequency.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlFrequency.Controls.Add(this.lblFrequency);
            this.pnlFrequency.Location = new System.Drawing.Point(296, 58);
            this.pnlFrequency.Name = "pnlFrequency";
            this.pnlFrequency.Size = new System.Drawing.Size(95, 28);
            this.pnlFrequency.TabIndex = 24;
            // 
            // lblFrequency
            // 
            this.lblFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrequency.ForeColor = System.Drawing.Color.White;
            this.lblFrequency.Location = new System.Drawing.Point(1, 1);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(91, 24);
            this.lblFrequency.TabIndex = 0;
            this.lblFrequency.Text = "Frequency";
            this.lblFrequency.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFrequency.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // pnlCounty
            // 
            this.pnlCounty.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlCounty.Controls.Add(this.lblCounty);
            this.pnlCounty.Location = new System.Drawing.Point(197, 57);
            this.pnlCounty.Name = "pnlCounty";
            this.pnlCounty.Size = new System.Drawing.Size(93, 28);
            this.pnlCounty.TabIndex = 23;
            // 
            // lblCounty
            // 
            this.lblCounty.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCounty.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCounty.ForeColor = System.Drawing.Color.White;
            this.lblCounty.Location = new System.Drawing.Point(1, 1);
            this.lblCounty.Name = "lblCounty";
            this.lblCounty.Size = new System.Drawing.Size(89, 24);
            this.lblCounty.TabIndex = 0;
            this.lblCounty.Text = "County";
            this.lblCounty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCounty.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // pnlState
            // 
            this.pnlState.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlState.Controls.Add(this.lblState);
            this.pnlState.Location = new System.Drawing.Point(112, 58);
            this.pnlState.Name = "pnlState";
            this.pnlState.Size = new System.Drawing.Size(80, 28);
            this.pnlState.TabIndex = 22;
            // 
            // lblState
            // 
            this.lblState.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblState.ForeColor = System.Drawing.Color.White;
            this.lblState.Location = new System.Drawing.Point(1, 1);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(75, 23);
            this.lblState.TabIndex = 0;
            this.lblState.Text = "State";
            this.lblState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblState.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // pnlName
            // 
            this.pnlName.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlName.Controls.Add(this.lblName);
            this.pnlName.Location = new System.Drawing.Point(12, 57);
            this.pnlName.Name = "pnlName";
            this.pnlName.Size = new System.Drawing.Size(93, 28);
            this.pnlName.TabIndex = 21;
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.White;
            this.lblName.Location = new System.Drawing.Point(1, 1);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(89, 26);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblName, "Other operator\'s name.");
            this.lblName.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // cbCountry
            // 
            this.cbCountry.FormattingEnabled = true;
            this.cbCountry.Location = new System.Drawing.Point(677, 31);
            this.cbCountry.Name = "cbCountry";
            this.cbCountry.Size = new System.Drawing.Size(200, 21);
            this.cbCountry.TabIndex = 9;
            this.cbCountry.SelectedValueChanged += new System.EventHandler(this.cbCountry_SelectedValueChanged);
            // 
            // pnlCountry
            // 
            this.pnlCountry.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlCountry.Controls.Add(this.lblCountry);
            this.pnlCountry.Location = new System.Drawing.Point(677, 3);
            this.pnlCountry.Name = "pnlCountry";
            this.pnlCountry.Size = new System.Drawing.Size(200, 28);
            this.pnlCountry.TabIndex = 19;
            // 
            // lblCountry
            // 
            this.lblCountry.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCountry.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.ForeColor = System.Drawing.Color.White;
            this.lblCountry.Location = new System.Drawing.Point(4, 2);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(193, 25);
            this.lblCountry.TabIndex = 0;
            this.lblCountry.Text = "Country";
            this.lblCountry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCountry.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // tbRec
            // 
            this.tbRec.Location = new System.Drawing.Point(615, 31);
            this.tbRec.MaxLength = 3;
            this.tbRec.Name = "tbRec";
            this.tbRec.Size = new System.Drawing.Size(56, 20);
            this.tbRec.TabIndex = 8;
            // 
            // pnlRec
            // 
            this.pnlRec.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlRec.Controls.Add(this.lblRec);
            this.pnlRec.Location = new System.Drawing.Point(615, 3);
            this.pnlRec.Name = "pnlRec";
            this.pnlRec.Size = new System.Drawing.Size(56, 28);
            this.pnlRec.TabIndex = 17;
            // 
            // lblRec
            // 
            this.lblRec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRec.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRec.ForeColor = System.Drawing.Color.White;
            this.lblRec.Location = new System.Drawing.Point(1, 1);
            this.lblRec.Name = "lblRec";
            this.lblRec.Size = new System.Drawing.Size(52, 23);
            this.lblRec.TabIndex = 0;
            this.lblRec.Text = "Rec";
            this.lblRec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRec.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // tbSent
            // 
            this.tbSent.Location = new System.Drawing.Point(553, 31);
            this.tbSent.MaxLength = 3;
            this.tbSent.Name = "tbSent";
            this.tbSent.Size = new System.Drawing.Size(56, 20);
            this.tbSent.TabIndex = 7;
            // 
            // pnlSent
            // 
            this.pnlSent.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlSent.Controls.Add(this.lblSent);
            this.pnlSent.Location = new System.Drawing.Point(553, 3);
            this.pnlSent.Name = "pnlSent";
            this.pnlSent.Size = new System.Drawing.Size(56, 28);
            this.pnlSent.TabIndex = 15;
            // 
            // lblSent
            // 
            this.lblSent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSent.ForeColor = System.Drawing.Color.White;
            this.lblSent.Location = new System.Drawing.Point(1, 1);
            this.lblSent.Name = "lblSent";
            this.lblSent.Size = new System.Drawing.Size(52, 25);
            this.lblSent.TabIndex = 0;
            this.lblSent.Text = "Sent";
            this.lblSent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSent.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // dtpTimeOn
            // 
            this.dtpTimeOn.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTimeOn.Location = new System.Drawing.Point(197, 31);
            this.dtpTimeOn.Name = "dtpTimeOn";
            this.dtpTimeOn.Size = new System.Drawing.Size(88, 20);
            this.dtpTimeOn.TabIndex = 3;
            // 
            // pnlTimeOn
            // 
            this.pnlTimeOn.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlTimeOn.Controls.Add(this.lblTimeOn);
            this.pnlTimeOn.Location = new System.Drawing.Point(197, 3);
            this.pnlTimeOn.Name = "pnlTimeOn";
            this.pnlTimeOn.Size = new System.Drawing.Size(88, 28);
            this.pnlTimeOn.TabIndex = 13;
            // 
            // lblTimeOn
            // 
            this.lblTimeOn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTimeOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeOn.ForeColor = System.Drawing.Color.White;
            this.lblTimeOn.Location = new System.Drawing.Point(1, 1);
            this.lblTimeOn.Name = "lblTimeOn";
            this.lblTimeOn.Size = new System.Drawing.Size(84, 24);
            this.lblTimeOn.TabIndex = 0;
            this.lblTimeOn.Text = "Time On";
            this.lblTimeOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTimeOn.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(111, 31);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(80, 20);
            this.dtpDate.TabIndex = 2;
            this.toolTip1.SetToolTip(this.dtpDate, "Date of the contact.");
            // 
            // pnlDate
            // 
            this.pnlDate.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlDate.Controls.Add(this.lblDate);
            this.pnlDate.Location = new System.Drawing.Point(111, 3);
            this.pnlDate.Name = "pnlDate";
            this.pnlDate.Size = new System.Drawing.Size(80, 28);
            this.pnlDate.TabIndex = 10;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(1, 1);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(76, 23);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.lblDate, "Date of the contact.");
            this.lblDate.Enter += new System.EventHandler(this.dtpDate_Enter);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // hlbInputBox1
            // 
            this.hlbInputBox1.Location = new System.Drawing.Point(12, 3);
            this.hlbInputBox1.Name = "hlbInputBox1";
            this.hlbInputBox1.Size = new System.Drawing.Size(94, 45);
            this.hlbInputBox1.TabIndex = 1;
            this.toolTip1.SetToolTip(this.hlbInputBox1, "Call sign of the station you are \r\ncommunicating with. REQUIRED\r\nbefore moving on" +
        " to other fields.");
            this.hlbInputBox1.Enter += new System.EventHandler(this.dtpDate_Enter);
            this.hlbInputBox1.Leave += new System.EventHandler(this.hlbInputBox1_Leave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 524);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "HamLogBook";
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlMode.ResumeLayout(false);
            this.pnlBand.ResumeLayout(false);
            this.pnlPower.ResumeLayout(false);
            this.pnlTimeOff.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.pnlOther.ResumeLayout(false);
            this.pnlFrequency.ResumeLayout(false);
            this.pnlCounty.ResumeLayout(false);
            this.pnlState.ResumeLayout(false);
            this.pnlName.ResumeLayout(false);
            this.pnlCountry.ResumeLayout(false);
            this.pnlRec.ResumeLayout(false);
            this.pnlSent.ResumeLayout(false);
            this.pnlTimeOn.ResumeLayout(false);
            this.pnlDate.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel TSStLLocalTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbPower;
        private System.Windows.Forms.Panel pnlPower;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.ComboBox cbMode;
        private System.Windows.Forms.Panel pnlMode;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox cbBand;
        private System.Windows.Forms.Panel pnlBand;
        private System.Windows.Forms.Label lblBand;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Panel pnlDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox tbSent;
        private System.Windows.Forms.Panel pnlSent;
        private System.Windows.Forms.Label lblSent;
        private System.Windows.Forms.DateTimePicker dtpTimeOn;
        private System.Windows.Forms.Panel pnlTimeOn;
        private System.Windows.Forms.Label lblTimeOn;
        private System.Windows.Forms.ComboBox cbCountry;
        private System.Windows.Forms.Panel pnlCountry;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.TextBox tbRec;
        private System.Windows.Forms.Panel pnlRec;
        private System.Windows.Forms.Label lblRec;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Panel pnlOther;
        private System.Windows.Forms.Label lblOther;
        private System.Windows.Forms.Panel pnlFrequency;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.Panel pnlCounty;
        private System.Windows.Forms.Label lblCounty;
        private System.Windows.Forms.Panel pnlState;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Panel pnlName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.CheckBox cbQSLRcvd;
        private System.Windows.Forms.CheckBox cbQSLSent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblComments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbOther;
        private System.Windows.Forms.TextBox tbFrequency;
        private System.Windows.Forms.ComboBox cbCounty;
        private System.Windows.Forms.ComboBox cbState;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLogContact;
        private System.Windows.Forms.TextBox tbComments;
        private System.Windows.Forms.DateTimePicker dtpTimeOff;
        private System.Windows.Forms.Panel pnlTimeOff;
        private System.Windows.Forms.Label lblTimeOff;
        private System.Windows.Forms.Label lblEntryMode;
        private System.Windows.Forms.ToolStripMenuItem createBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem runBackupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupOptionsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem printLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printAddressToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem resetCounterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem markSelectedRecordsAsQSLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem receivedToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem eQSLToolStripMenuItem;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rbAll;
        private System.Windows.Forms.RadioButton rbLast50;
        private System.Windows.Forms.Label label4;
        private HLBInputBox hlbInputBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cbListen;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem printLabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qSLSentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem qSLReceivedToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem editCurrentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCurrentToolStripMenuItem;
    }
}

