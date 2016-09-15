namespace AutoDealerAdmin
{
    partial class frmVehicle
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbVehCatId = new System.Windows.Forms.ComboBox();
            this.tbVehPrice = new System.Windows.Forms.TextBox();
            this.tbVehVIN = new System.Windows.Forms.TextBox();
            this.tbVehName = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbVehRecNo = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cbVehFeatured = new System.Windows.Forms.CheckBox();
            this.dtpVehEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpVehStart = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.tbVehDesc = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbVehPageText = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSmallPicNotFound = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblLargePicNotFound = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addGalleryPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteGalleryPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadGalleryPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fsSmallPic = new AutoDealerAdmin.FileSearch();
            this.fsLargePic = new AutoDealerAdmin.FileSearch();
            this.cbSmallPicUpload = new System.Windows.Forms.CheckBox();
            this.cbLargePicUpload = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "VIN:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Price:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Start Date:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cbVehCatId);
            this.groupBox1.Controls.Add(this.tbVehPrice);
            this.groupBox1.Controls.Add(this.tbVehVIN);
            this.groupBox1.Controls.Add(this.tbVehName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 144);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vehicle:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Category:";
            // 
            // cbVehCatId
            // 
            this.cbVehCatId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbVehCatId.FormattingEnabled = true;
            this.cbVehCatId.Location = new System.Drawing.Point(69, 48);
            this.cbVehCatId.Name = "cbVehCatId";
            this.cbVehCatId.Size = new System.Drawing.Size(385, 21);
            this.cbVehCatId.TabIndex = 2;
            // 
            // tbVehPrice
            // 
            this.tbVehPrice.Location = new System.Drawing.Point(69, 110);
            this.tbVehPrice.Name = "tbVehPrice";
            this.tbVehPrice.Size = new System.Drawing.Size(105, 20);
            this.tbVehPrice.TabIndex = 4;
            this.tbVehPrice.Leave += new System.EventHandler(this.tbVehPrice_Leave);
            // 
            // tbVehVIN
            // 
            this.tbVehVIN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVehVIN.Location = new System.Drawing.Point(69, 79);
            this.tbVehVIN.MaxLength = 64;
            this.tbVehVIN.Name = "tbVehVIN";
            this.tbVehVIN.Size = new System.Drawing.Size(385, 20);
            this.tbVehVIN.TabIndex = 3;
            // 
            // tbVehName
            // 
            this.tbVehName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVehName.Location = new System.Drawing.Point(69, 18);
            this.tbVehName.MaxLength = 256;
            this.tbVehName.Name = "tbVehName";
            this.tbVehName.Size = new System.Drawing.Size(385, 20);
            this.tbVehName.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tbVehRecNo);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.cbVehFeatured);
            this.groupBox2.Controls.Add(this.dtpVehEnd);
            this.groupBox2.Controls.Add(this.dtpVehStart);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(485, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 144);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Publishing";
            // 
            // tbVehRecNo
            // 
            this.tbVehRecNo.Location = new System.Drawing.Point(151, 118);
            this.tbVehRecNo.Name = "tbVehRecNo";
            this.tbVehRecNo.ReadOnly = true;
            this.tbVehRecNo.Size = new System.Drawing.Size(43, 20);
            this.tbVehRecNo.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(126, 121);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 24;
            this.label12.Text = "ID:";
            // 
            // cbVehFeatured
            // 
            this.cbVehFeatured.AutoSize = true;
            this.cbVehFeatured.Location = new System.Drawing.Point(79, 81);
            this.cbVehFeatured.Name = "cbVehFeatured";
            this.cbVehFeatured.Size = new System.Drawing.Size(15, 14);
            this.cbVehFeatured.TabIndex = 7;
            this.cbVehFeatured.UseVisualStyleBackColor = true;
            // 
            // dtpVehEnd
            // 
            this.dtpVehEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVehEnd.Location = new System.Drawing.Point(79, 51);
            this.dtpVehEnd.Name = "dtpVehEnd";
            this.dtpVehEnd.Size = new System.Drawing.Size(103, 20);
            this.dtpVehEnd.TabIndex = 6;
            // 
            // dtpVehStart
            // 
            this.dtpVehStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpVehStart.Location = new System.Drawing.Point(79, 21);
            this.dtpVehStart.Name = "dtpVehStart";
            this.dtpVehStart.Size = new System.Drawing.Size(103, 20);
            this.dtpVehStart.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Featured:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "End Date:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(23, 172);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(91, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Short Description:";
            // 
            // tbVehDesc
            // 
            this.tbVehDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVehDesc.Location = new System.Drawing.Point(23, 186);
            this.tbVehDesc.MaxLength = 256;
            this.tbVehDesc.Multiline = true;
            this.tbVehDesc.Name = "tbVehDesc";
            this.tbVehDesc.Size = new System.Drawing.Size(456, 49);
            this.tbVehDesc.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 249);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Page Text:";
            // 
            // tbVehPageText
            // 
            this.tbVehPageText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbVehPageText.Location = new System.Drawing.Point(23, 265);
            this.tbVehPageText.MaxLength = 2048;
            this.tbVehPageText.Multiline = true;
            this.tbVehPageText.Name = "tbVehPageText";
            this.tbVehPageText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbVehPageText.Size = new System.Drawing.Size(456, 200);
            this.tbVehPageText.TabIndex = 14;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(914, 442);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(1005, 442);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "Add/Update";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(258, 171);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 28;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(4, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(318, 228);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 29;
            this.pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.lblSmallPicNotFound);
            this.panel1.Location = new System.Drawing.Point(490, 196);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 181);
            this.panel1.TabIndex = 30;
            // 
            // lblSmallPicNotFound
            // 
            this.lblSmallPicNotFound.Location = new System.Drawing.Point(4, 4);
            this.lblSmallPicNotFound.Name = "lblSmallPicNotFound";
            this.lblSmallPicNotFound.Size = new System.Drawing.Size(259, 173);
            this.lblSmallPicNotFound.TabIndex = 0;
            this.lblSmallPicNotFound.Text = "No Picture Found";
            this.lblSmallPicNotFound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.lblLargePicNotFound);
            this.panel2.Location = new System.Drawing.Point(760, 196);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(326, 240);
            this.panel2.TabIndex = 31;
            // 
            // lblLargePicNotFound
            // 
            this.lblLargePicNotFound.Location = new System.Drawing.Point(4, 4);
            this.lblLargePicNotFound.Name = "lblLargePicNotFound";
            this.lblLargePicNotFound.Size = new System.Drawing.Size(310, 224);
            this.lblLargePicNotFound.TabIndex = 0;
            this.lblLargePicNotFound.Text = "No Picture Found";
            this.lblLargePicNotFound.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.panel3);
            this.groupBox3.Controls.Add(this.listBox1);
            this.groupBox3.Location = new System.Drawing.Point(691, 15);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(393, 144);
            this.groupBox3.TabIndex = 34;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Gallery";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(6, 19);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(172, 121);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.pictureBox3);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(184, 18);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(200, 120);
            this.panel3.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(3, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(198, 111);
            this.label8.TabIndex = 0;
            this.label8.Text = "Picture Not Found";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox3.Location = new System.Drawing.Point(0, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(198, 116);
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addGalleryPicToolStripMenuItem,
            this.deleteGalleryPicToolStripMenuItem,
            this.uploadGalleryPicToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(171, 70);
            // 
            // addGalleryPicToolStripMenuItem
            // 
            this.addGalleryPicToolStripMenuItem.Name = "addGalleryPicToolStripMenuItem";
            this.addGalleryPicToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.addGalleryPicToolStripMenuItem.Text = "Add Gallery Pic";
            // 
            // deleteGalleryPicToolStripMenuItem
            // 
            this.deleteGalleryPicToolStripMenuItem.Name = "deleteGalleryPicToolStripMenuItem";
            this.deleteGalleryPicToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.deleteGalleryPicToolStripMenuItem.Text = "Delete Gallery Pic";
            // 
            // uploadGalleryPicToolStripMenuItem
            // 
            this.uploadGalleryPicToolStripMenuItem.Name = "uploadGalleryPicToolStripMenuItem";
            this.uploadGalleryPicToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.uploadGalleryPicToolStripMenuItem.Text = "Upload Gallery Pic";
            // 
            // fsSmallPic
            // 
            this.fsSmallPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fsSmallPic.fsChanged = false;
            this.fsSmallPic.fsExtentions = "";
            this.fsSmallPic.fsFileName = "";
            this.fsSmallPic.fsInitialPath = "";
            this.fsSmallPic.fsLabelText = "Small Pic:";
            this.fsSmallPic.fsPathLeft = 58;
            this.fsSmallPic.fsRootPath = "";
            this.fsSmallPic.fsTitle = "";
            this.fsSmallPic.Location = new System.Drawing.Point(490, 171);
            this.fsSmallPic.Name = "fsSmallPic";
            this.fsSmallPic.Size = new System.Drawing.Size(264, 22);
            this.fsSmallPic.TabIndex = 33;
            this.fsSmallPic.fsValueChanged += new AutoDealerAdmin.fsValueChangedEventHandler(this.fsSmallPic_fsValueChanged);
            // 
            // fsLargePic
            // 
            this.fsLargePic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fsLargePic.fsChanged = false;
            this.fsLargePic.fsExtentions = "";
            this.fsLargePic.fsFileName = "";
            this.fsLargePic.fsInitialPath = "";
            this.fsLargePic.fsLabelText = "Large Pic:";
            this.fsLargePic.fsPathLeft = 62;
            this.fsLargePic.fsRootPath = "";
            this.fsLargePic.fsTitle = "";
            this.fsLargePic.Location = new System.Drawing.Point(760, 171);
            this.fsLargePic.Name = "fsLargePic";
            this.fsLargePic.Size = new System.Drawing.Size(326, 22);
            this.fsLargePic.TabIndex = 32;
            this.fsLargePic.fsValueChanged += new AutoDealerAdmin.fsValueChangedEventHandler(this.fsLargePic_fsValueChanged);
            // 
            // cbSmallPicUpload
            // 
            this.cbSmallPicUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbSmallPicUpload.AutoSize = true;
            this.cbSmallPicUpload.Location = new System.Drawing.Point(490, 383);
            this.cbSmallPicUpload.Name = "cbSmallPicUpload";
            this.cbSmallPicUpload.Size = new System.Drawing.Size(60, 17);
            this.cbSmallPicUpload.TabIndex = 36;
            this.cbSmallPicUpload.Text = "Upload";
            this.cbSmallPicUpload.UseVisualStyleBackColor = true;
            // 
            // cbLargePicUpload
            // 
            this.cbLargePicUpload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbLargePicUpload.AutoSize = true;
            this.cbLargePicUpload.Location = new System.Drawing.Point(760, 442);
            this.cbLargePicUpload.Name = "cbLargePicUpload";
            this.cbLargePicUpload.Size = new System.Drawing.Size(60, 17);
            this.cbLargePicUpload.TabIndex = 37;
            this.cbLargePicUpload.Text = "Upload";
            this.cbLargePicUpload.UseVisualStyleBackColor = true;
            // 
            // frmVehicle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1098, 477);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.cbLargePicUpload);
            this.Controls.Add(this.cbSmallPicUpload);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.fsSmallPic);
            this.Controls.Add(this.fsLargePic);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tbVehPageText);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.tbVehDesc);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmVehicle";
            this.Text = "frmVehicle";
            this.Load += new System.EventHandler(this.frmVehicle_Load);
            this.Shown += new System.EventHandler(this.frmVehicle_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbVehPrice;
        private System.Windows.Forms.TextBox tbVehVIN;
        private System.Windows.Forms.TextBox tbVehName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox cbVehFeatured;
        private System.Windows.Forms.DateTimePicker dtpVehEnd;
        private System.Windows.Forms.DateTimePicker dtpVehStart;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbVehCatId;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbVehDesc;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tbVehPageText;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbVehRecNo;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblLargePicNotFound;
        private System.Windows.Forms.Label lblSmallPicNotFound;
        private FileSearch fsLargePic;
        private FileSearch fsSmallPic;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addGalleryPicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteGalleryPicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadGalleryPicToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbSmallPicUpload;
        private System.Windows.Forms.CheckBox cbLargePicUpload;
    }
}