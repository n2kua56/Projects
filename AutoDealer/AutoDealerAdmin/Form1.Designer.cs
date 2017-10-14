namespace AutoDealerAdmin
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpVehicles = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvVehicles = new System.Windows.Forms.DataGridView();
            this.mnuVehicles = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tpCategories = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvCatagories = new System.Windows.Forms.DataGridView();
            this.mnuCategory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCategoryAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCategoryEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCategoryDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tpPhotos = new System.Windows.Forms.TabPage();
            this.label15 = new System.Windows.Forms.Label();
            this.tpProperties = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvProperties = new System.Windows.Forms.DataGridView();
            this.mnuProperties = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuPropertiesAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPropertiesEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPropertiesDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tbMySQLPassword = new System.Windows.Forms.TextBox();
            this.tbMySQLUserName = new System.Windows.Forms.TextBox();
            this.tbMySQLDatabase = new System.Windows.Forms.TextBox();
            this.tbMySQLServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbGallaryPhotos = new System.Windows.Forms.TextBox();
            this.tbLargePhotos = new System.Windows.Forms.TextBox();
            this.tbSmallPhotos = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbVehicles = new System.Windows.Forms.TextBox();
            this.tbCat = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbPicsToConvert = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dgvPicsToUpload = new System.Windows.Forms.DataGridView();
            this.btnUpload = new System.Windows.Forms.Button();
            this.psGallaryPath = new AutoDealerAdmin.PathSearch();
            this.psRootPath = new AutoDealerAdmin.PathSearch();
            this.psLargePicPath = new AutoDealerAdmin.PathSearch();
            this.psSmallPicPath = new AutoDealerAdmin.PathSearch();
            this.tabControl1.SuspendLayout();
            this.tpVehicles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicles)).BeginInit();
            this.mnuVehicles.SuspendLayout();
            this.tpCategories.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatagories)).BeginInit();
            this.mnuCategory.SuspendLayout();
            this.tpPhotos.SuspendLayout();
            this.tpProperties.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).BeginInit();
            this.mnuProperties.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPicsToUpload)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tpVehicles);
            this.tabControl1.Controls.Add(this.tpCategories);
            this.tabControl1.Controls.Add(this.tpPhotos);
            this.tabControl1.Controls.Add(this.tpProperties);
            this.tabControl1.Controls.Add(this.tpSettings);
            this.tabControl1.Location = new System.Drawing.Point(12, 87);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(761, 247);
            this.tabControl1.TabIndex = 0;
            // 
            // tpVehicles
            // 
            this.tpVehicles.Controls.Add(this.label6);
            this.tpVehicles.Controls.Add(this.dgvVehicles);
            this.tpVehicles.Location = new System.Drawing.Point(4, 22);
            this.tpVehicles.Name = "tpVehicles";
            this.tpVehicles.Padding = new System.Windows.Forms.Padding(3);
            this.tpVehicles.Size = new System.Drawing.Size(753, 221);
            this.tpVehicles.TabIndex = 1;
            this.tpVehicles.Text = "Vehicles";
            this.tpVehicles.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(741, 23);
            this.label6.TabIndex = 2;
            this.label6.Text = "VEHICLES";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvVehicles
            // 
            this.dgvVehicles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVehicles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVehicles.ContextMenuStrip = this.mnuVehicles;
            this.dgvVehicles.Location = new System.Drawing.Point(6, 31);
            this.dgvVehicles.MultiSelect = false;
            this.dgvVehicles.Name = "dgvVehicles";
            this.dgvVehicles.ReadOnly = true;
            this.dgvVehicles.RowHeadersVisible = false;
            this.dgvVehicles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVehicles.Size = new System.Drawing.Size(741, 184);
            this.dgvVehicles.TabIndex = 0;
            this.dgvVehicles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvVehicles_MouseDoubleClick);
            this.dgvVehicles.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvVehicles_MouseDown);
            // 
            // mnuVehicles
            // 
            this.mnuVehicles.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.mnuVehicles.Name = "mnuVehicles";
            this.mnuVehicles.Size = new System.Drawing.Size(157, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItem1.Text = "Add a Vehicle";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItem2.Text = "Edit a Vehicle";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(156, 22);
            this.toolStripMenuItem3.Text = "Delete a Vehicle";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // tpCategories
            // 
            this.tpCategories.Controls.Add(this.label5);
            this.tpCategories.Controls.Add(this.dgvCatagories);
            this.tpCategories.Location = new System.Drawing.Point(4, 22);
            this.tpCategories.Name = "tpCategories";
            this.tpCategories.Padding = new System.Windows.Forms.Padding(3);
            this.tpCategories.Size = new System.Drawing.Size(753, 221);
            this.tpCategories.TabIndex = 0;
            this.tpCategories.Text = "Categories";
            this.tpCategories.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(741, 23);
            this.label5.TabIndex = 1;
            this.label5.Text = "CATAGORIES";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvCatagories
            // 
            this.dgvCatagories.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCatagories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCatagories.ContextMenuStrip = this.mnuCategory;
            this.dgvCatagories.Location = new System.Drawing.Point(6, 31);
            this.dgvCatagories.MultiSelect = false;
            this.dgvCatagories.Name = "dgvCatagories";
            this.dgvCatagories.ReadOnly = true;
            this.dgvCatagories.RowHeadersVisible = false;
            this.dgvCatagories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCatagories.Size = new System.Drawing.Size(741, 184);
            this.dgvCatagories.TabIndex = 0;
            this.dgvCatagories.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvCatagories_MouseDoubleClick);
            this.dgvCatagories.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvCatagories_MouseDown);
            // 
            // mnuCategory
            // 
            this.mnuCategory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCategoryAdd,
            this.mnuCategoryEdit,
            this.mnuCategoryDelete});
            this.mnuCategory.Name = "mnuCategory";
            this.mnuCategory.Size = new System.Drawing.Size(159, 70);
            // 
            // mnuCategoryAdd
            // 
            this.mnuCategoryAdd.Name = "mnuCategoryAdd";
            this.mnuCategoryAdd.Size = new System.Drawing.Size(158, 22);
            this.mnuCategoryAdd.Text = "Add Category";
            this.mnuCategoryAdd.Click += new System.EventHandler(this.mnuCategoryAdd_Click);
            // 
            // mnuCategoryEdit
            // 
            this.mnuCategoryEdit.Name = "mnuCategoryEdit";
            this.mnuCategoryEdit.Size = new System.Drawing.Size(158, 22);
            this.mnuCategoryEdit.Text = "Edit Category";
            this.mnuCategoryEdit.Click += new System.EventHandler(this.mnuCategoryEdit_Click);
            // 
            // mnuCategoryDelete
            // 
            this.mnuCategoryDelete.Name = "mnuCategoryDelete";
            this.mnuCategoryDelete.Size = new System.Drawing.Size(158, 22);
            this.mnuCategoryDelete.Text = "Delete Category";
            this.mnuCategoryDelete.Click += new System.EventHandler(this.mnuCategoryDelete_Click);
            // 
            // tpPhotos
            // 
            this.tpPhotos.Controls.Add(this.groupBox5);
            this.tpPhotos.Controls.Add(this.groupBox4);
            this.tpPhotos.Controls.Add(this.label15);
            this.tpPhotos.Location = new System.Drawing.Point(4, 22);
            this.tpPhotos.Name = "tpPhotos";
            this.tpPhotos.Padding = new System.Windows.Forms.Padding(3);
            this.tpPhotos.Size = new System.Drawing.Size(753, 221);
            this.tpPhotos.TabIndex = 4;
            this.tpPhotos.Text = "Photos";
            this.tpPhotos.UseVisualStyleBackColor = true;
            this.tpPhotos.Click += new System.EventHandler(this.tpPhotos_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(581, 97);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(120, 13);
            this.label15.TabIndex = 2;
            this.label15.Text = "Convert Gallary Pictures";
            // 
            // tpProperties
            // 
            this.tpProperties.Controls.Add(this.label7);
            this.tpProperties.Controls.Add(this.dgvProperties);
            this.tpProperties.Location = new System.Drawing.Point(4, 22);
            this.tpProperties.Name = "tpProperties";
            this.tpProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tpProperties.Size = new System.Drawing.Size(753, 221);
            this.tpProperties.TabIndex = 2;
            this.tpProperties.Text = "Properties";
            this.tpProperties.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(741, 23);
            this.label7.TabIndex = 4;
            this.label7.Text = "PROPERTIES";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvProperties
            // 
            this.dgvProperties.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProperties.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProperties.ContextMenuStrip = this.mnuProperties;
            this.dgvProperties.Location = new System.Drawing.Point(6, 31);
            this.dgvProperties.MultiSelect = false;
            this.dgvProperties.Name = "dgvProperties";
            this.dgvProperties.RowHeadersVisible = false;
            this.dgvProperties.Size = new System.Drawing.Size(741, 184);
            this.dgvProperties.TabIndex = 3;
            this.dgvProperties.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgvProperties_MouseDown);
            // 
            // mnuProperties
            // 
            this.mnuProperties.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPropertiesAdd,
            this.mnuPropertiesEdit,
            this.mnuPropertiesDelete});
            this.mnuProperties.Name = "mnuProperties";
            this.mnuProperties.Size = new System.Drawing.Size(165, 70);
            // 
            // mnuPropertiesAdd
            // 
            this.mnuPropertiesAdd.Name = "mnuPropertiesAdd";
            this.mnuPropertiesAdd.Size = new System.Drawing.Size(164, 22);
            this.mnuPropertiesAdd.Text = "Add a Property";
            this.mnuPropertiesAdd.Click += new System.EventHandler(this.mnuPropertiesAdd_Click);
            // 
            // mnuPropertiesEdit
            // 
            this.mnuPropertiesEdit.Name = "mnuPropertiesEdit";
            this.mnuPropertiesEdit.Size = new System.Drawing.Size(164, 22);
            this.mnuPropertiesEdit.Text = "Edit a Property";
            this.mnuPropertiesEdit.Click += new System.EventHandler(this.mnuPropertiesEdit_Click);
            // 
            // mnuPropertiesDelete
            // 
            this.mnuPropertiesDelete.Name = "mnuPropertiesDelete";
            this.mnuPropertiesDelete.Size = new System.Drawing.Size(164, 22);
            this.mnuPropertiesDelete.Text = "Delete a Property";
            this.mnuPropertiesDelete.Click += new System.EventHandler(this.mnuPropertiesDelete_Click);
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.btnSave);
            this.tpSettings.Controls.Add(this.groupBox3);
            this.tpSettings.Controls.Add(this.groupBox1);
            this.tpSettings.Location = new System.Drawing.Point(4, 22);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(753, 221);
            this.tpSettings.TabIndex = 3;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(672, 192);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.psGallaryPath);
            this.groupBox3.Controls.Add(this.psRootPath);
            this.groupBox3.Controls.Add(this.psLargePicPath);
            this.groupBox3.Controls.Add(this.psSmallPicPath);
            this.groupBox3.Location = new System.Drawing.Point(230, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(379, 140);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Staging";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbMySQLPassword);
            this.groupBox1.Controls.Add(this.tbMySQLUserName);
            this.groupBox1.Controls.Add(this.tbMySQLDatabase);
            this.groupBox1.Controls.Add(this.tbMySQLServer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 140);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "MySQL Settings";
            // 
            // tbMySQLPassword
            // 
            this.tbMySQLPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMySQLPassword.Location = new System.Drawing.Point(89, 107);
            this.tbMySQLPassword.Name = "tbMySQLPassword";
            this.tbMySQLPassword.Size = new System.Drawing.Size(113, 20);
            this.tbMySQLPassword.TabIndex = 7;
            // 
            // tbMySQLUserName
            // 
            this.tbMySQLUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMySQLUserName.Location = new System.Drawing.Point(89, 79);
            this.tbMySQLUserName.Name = "tbMySQLUserName";
            this.tbMySQLUserName.Size = new System.Drawing.Size(113, 20);
            this.tbMySQLUserName.TabIndex = 6;
            // 
            // tbMySQLDatabase
            // 
            this.tbMySQLDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMySQLDatabase.Location = new System.Drawing.Point(89, 51);
            this.tbMySQLDatabase.Name = "tbMySQLDatabase";
            this.tbMySQLDatabase.Size = new System.Drawing.Size(113, 20);
            this.tbMySQLDatabase.TabIndex = 5;
            // 
            // tbMySQLServer
            // 
            this.tbMySQLServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMySQLServer.Location = new System.Drawing.Point(89, 23);
            this.tbMySQLServer.Name = "tbMySQLServer";
            this.tbMySQLServer.Size = new System.Drawing.Size(113, 20);
            this.tbMySQLServer.TabIndex = 4;
            this.tbMySQLServer.TextChanged += new System.EventHandler(this.tbMySQLServer_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "User Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 337);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(785, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tbGallaryPhotos);
            this.groupBox2.Controls.Add(this.tbLargePhotos);
            this.groupBox2.Controls.Add(this.tbSmallPhotos);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tbVehicles);
            this.groupBox2.Controls.Add(this.tbCat);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(499, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(274, 78);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Database";
            // 
            // tbGallaryPhotos
            // 
            this.tbGallaryPhotos.Location = new System.Drawing.Point(206, 53);
            this.tbGallaryPhotos.Name = "tbGallaryPhotos";
            this.tbGallaryPhotos.Size = new System.Drawing.Size(51, 20);
            this.tbGallaryPhotos.TabIndex = 9;
            this.tbGallaryPhotos.Text = "999,999";
            // 
            // tbLargePhotos
            // 
            this.tbLargePhotos.Location = new System.Drawing.Point(206, 32);
            this.tbLargePhotos.Name = "tbLargePhotos";
            this.tbLargePhotos.Size = new System.Drawing.Size(51, 20);
            this.tbLargePhotos.TabIndex = 8;
            this.tbLargePhotos.Text = "999,999";
            // 
            // tbSmallPhotos
            // 
            this.tbSmallPhotos.Location = new System.Drawing.Point(206, 11);
            this.tbSmallPhotos.Name = "tbSmallPhotos";
            this.tbSmallPhotos.Size = new System.Drawing.Size(51, 20);
            this.tbSmallPhotos.TabIndex = 7;
            this.tbSmallPhotos.Text = "999,999";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(132, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(75, 13);
            this.label12.TabIndex = 6;
            this.label12.Text = "Gallary Photos";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(132, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 13);
            this.label11.TabIndex = 5;
            this.label11.Text = "Large Photos";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(132, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Small Photos";
            // 
            // tbVehicles
            // 
            this.tbVehicles.Location = new System.Drawing.Point(64, 42);
            this.tbVehicles.Name = "tbVehicles";
            this.tbVehicles.Size = new System.Drawing.Size(51, 20);
            this.tbVehicles.TabIndex = 3;
            this.tbVehicles.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbCat
            // 
            this.tbCat.Location = new System.Drawing.Point(64, 15);
            this.tbCat.Name = "tbCat";
            this.tbCat.Size = new System.Drawing.Size(51, 20);
            this.tbCat.TabIndex = 2;
            this.tbCat.Text = "999,999";
            this.tbCat.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Vehicles";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(26, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Cat:";
            // 
            // lbPicsToConvert
            // 
            this.lbPicsToConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPicsToConvert.FormattingEnabled = true;
            this.lbPicsToConvert.Location = new System.Drawing.Point(6, 19);
            this.lbPicsToConvert.Name = "lbPicsToConvert";
            this.lbPicsToConvert.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbPicsToConvert.Size = new System.Drawing.Size(201, 160);
            this.lbPicsToConvert.TabIndex = 3;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox4.Controls.Add(this.btnConvert);
            this.groupBox4.Controls.Add(this.lbPicsToConvert);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(213, 209);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Pictures to convert";
            // 
            // btnConvert
            // 
            this.btnConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConvert.Location = new System.Drawing.Point(132, 183);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(75, 23);
            this.btnConvert.TabIndex = 5;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox5.Controls.Add(this.btnUpload);
            this.groupBox5.Controls.Add(this.dgvPicsToUpload);
            this.groupBox5.Location = new System.Drawing.Point(225, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(285, 209);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Pictures to Upload";
            // 
            // dgvPicsToUpload
            // 
            this.dgvPicsToUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPicsToUpload.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPicsToUpload.Location = new System.Drawing.Point(6, 19);
            this.dgvPicsToUpload.Name = "dgvPicsToUpload";
            this.dgvPicsToUpload.Size = new System.Drawing.Size(273, 160);
            this.dgvPicsToUpload.TabIndex = 0;
            this.dgvPicsToUpload.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgvPicsToUpload_MouseDoubleClick);
            // 
            // btnUpload
            // 
            this.btnUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpload.Location = new System.Drawing.Point(204, 183);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 1;
            this.btnUpload.Text = "Upload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // psGallaryPath
            // 
            this.psGallaryPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.psGallaryPath.Changed = false;
            this.psGallaryPath.dirPath = "";
            this.psGallaryPath.LabelText = "Gallary Path:";
            this.psGallaryPath.Location = new System.Drawing.Point(18, 107);
            this.psGallaryPath.Name = "psGallaryPath";
            this.psGallaryPath.PathLeft = 80;
            this.psGallaryPath.RootPath = "";
            this.psGallaryPath.Size = new System.Drawing.Size(350, 22);
            this.psGallaryPath.TabIndex = 8;
            this.psGallaryPath.Enter += new System.EventHandler(this.psSmallPicPath_Enter);
            // 
            // psRootPath
            // 
            this.psRootPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.psRootPath.Changed = false;
            this.psRootPath.dirPath = "";
            this.psRootPath.LabelText = "Root Path:";
            this.psRootPath.Location = new System.Drawing.Point(18, 23);
            this.psRootPath.Name = "psRootPath";
            this.psRootPath.PathLeft = 80;
            this.psRootPath.RootPath = "";
            this.psRootPath.Size = new System.Drawing.Size(350, 22);
            this.psRootPath.TabIndex = 7;
            this.psRootPath.Leave += new System.EventHandler(this.psRootPath_Leave);
            // 
            // psLargePicPath
            // 
            this.psLargePicPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.psLargePicPath.Changed = false;
            this.psLargePicPath.dirPath = "";
            this.psLargePicPath.LabelText = "Large Pic Path:";
            this.psLargePicPath.Location = new System.Drawing.Point(18, 79);
            this.psLargePicPath.Name = "psLargePicPath";
            this.psLargePicPath.PathLeft = 80;
            this.psLargePicPath.RootPath = "";
            this.psLargePicPath.Size = new System.Drawing.Size(350, 22);
            this.psLargePicPath.TabIndex = 6;
            this.psLargePicPath.Enter += new System.EventHandler(this.psSmallPicPath_Enter);
            // 
            // psSmallPicPath
            // 
            this.psSmallPicPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.psSmallPicPath.Changed = false;
            this.psSmallPicPath.dirPath = "";
            this.psSmallPicPath.LabelText = "Small Pic Path:";
            this.psSmallPicPath.Location = new System.Drawing.Point(18, 51);
            this.psSmallPicPath.Name = "psSmallPicPath";
            this.psSmallPicPath.PathLeft = 80;
            this.psSmallPicPath.RootPath = "";
            this.psSmallPicPath.Size = new System.Drawing.Size(350, 22);
            this.psSmallPicPath.TabIndex = 5;
            this.psSmallPicPath.Enter += new System.EventHandler(this.psSmallPicPath_Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 359);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Auto Dealer Admin";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.tabControl1.ResumeLayout(false);
            this.tpVehicles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehicles)).EndInit();
            this.mnuVehicles.ResumeLayout(false);
            this.tpCategories.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCatagories)).EndInit();
            this.mnuCategory.ResumeLayout(false);
            this.tpPhotos.ResumeLayout(false);
            this.tpPhotos.PerformLayout();
            this.tpProperties.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProperties)).EndInit();
            this.mnuProperties.ResumeLayout(false);
            this.tpSettings.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPicsToUpload)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpCategories;
        private System.Windows.Forms.TabPage tpVehicles;
        private System.Windows.Forms.TabPage tpProperties;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridView dgvCatagories;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbMySQLPassword;
        private System.Windows.Forms.TextBox tbMySQLUserName;
        private System.Windows.Forms.TextBox tbMySQLDatabase;
        private System.Windows.Forms.TextBox tbMySQLServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvVehicles;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgvProperties;
        private System.Windows.Forms.TabPage tpPhotos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tbGallaryPhotos;
        private System.Windows.Forms.TextBox tbLargePhotos;
        private System.Windows.Forms.TextBox tbSmallPhotos;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbVehicles;
        private System.Windows.Forms.TextBox tbCat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip mnuCategory;
        private System.Windows.Forms.ToolStripMenuItem mnuCategoryAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuCategoryEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuCategoryDelete;
        private System.Windows.Forms.ContextMenuStrip mnuVehicles;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ContextMenuStrip mnuProperties;
        private System.Windows.Forms.ToolStripMenuItem mnuPropertiesAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuPropertiesEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuPropertiesDelete;
        private System.Windows.Forms.GroupBox groupBox3;
        private PathSearch psSmallPicPath;
        private PathSearch psGallaryPath;
        private PathSearch psRootPath;
        private PathSearch psLargePicPath;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnConvert;
        private System.Windows.Forms.ListBox lbPicsToConvert;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.DataGridView dgvPicsToUpload;
    }
}

