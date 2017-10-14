namespace EZDesk
{
    partial class frmViewRx
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
            this.dgvDrugHistory = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cbNoKnownActive = new System.Windows.Forms.CheckBox();
            this.cbPatientNoKnowAllergies = new System.Windows.Forms.CheckBox();
            this.cbCurrentMedsDocumented = new System.Windows.Forms.CheckBox();
            this.cbActive = new System.Windows.Forms.CheckBox();
            this.cbIntolerant = new System.Windows.Forms.CheckBox();
            this.cbDiscontinued = new System.Windows.Forms.CheckBox();
            this.cbShowDelCan = new System.Windows.Forms.CheckBox();
            this.dgvRxHistory = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDrugs = new System.Windows.Forms.DataGridView();
            this.cmdABC = new System.Windows.Forms.Button();
            this.cmdDEF = new System.Windows.Forms.Button();
            this.cmdGHI = new System.Windows.Forms.Button();
            this.cmdJKL = new System.Windows.Forms.Button();
            this.cmdMNO = new System.Windows.Forms.Button();
            this.cmdPQR = new System.Windows.Forms.Button();
            this.cmdSTU = new System.Windows.Forms.Button();
            this.cmdVWX = new System.Windows.Forms.Button();
            this.cmdYZ = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.cmdFind = new System.Windows.Forms.Button();
            this.rbDrugMaster = new System.Windows.Forms.RadioButton();
            this.rbFavorites = new System.Windows.Forms.RadioButton();
            this.cbSigsMaster = new System.Windows.Forms.CheckBox();
            this.cbSigsFavorites = new System.Windows.Forms.CheckBox();
            this.dgvSigs = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdSendRx1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdSendRx2 = new System.Windows.Forms.Button();
            this.cmdSendRx3 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cmdComplete1 = new System.Windows.Forms.Button();
            this.cmdComplete2 = new System.Windows.Forms.Button();
            this.cmdComplete3 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button11 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.checkBox10 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button12 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrugHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRxHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrugs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSigs)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDrugHistory
            // 
            this.dgvDrugHistory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDrugHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDrugHistory.Location = new System.Drawing.Point(12, 44);
            this.dgvDrugHistory.Name = "dgvDrugHistory";
            this.dgvDrugHistory.Size = new System.Drawing.Size(651, 87);
            this.dgvDrugHistory.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Patient Drug History";
            // 
            // cbNoKnownActive
            // 
            this.cbNoKnownActive.AutoSize = true;
            this.cbNoKnownActive.Location = new System.Drawing.Point(13, 133);
            this.cbNoKnownActive.Name = "cbNoKnownActive";
            this.cbNoKnownActive.Size = new System.Drawing.Size(161, 17);
            this.cbNoKnownActive.TabIndex = 2;
            this.cbNoKnownActive.Text = "No known active medication";
            this.cbNoKnownActive.UseVisualStyleBackColor = true;
            // 
            // cbPatientNoKnowAllergies
            // 
            this.cbPatientNoKnowAllergies.AutoSize = true;
            this.cbPatientNoKnowAllergies.Location = new System.Drawing.Point(181, 133);
            this.cbPatientNoKnowAllergies.Name = "cbPatientNoKnowAllergies";
            this.cbPatientNoKnowAllergies.Size = new System.Drawing.Size(194, 17);
            this.cbPatientNoKnowAllergies.TabIndex = 3;
            this.cbPatientNoKnowAllergies.Text = "Patient has no known drug allergies";
            this.cbPatientNoKnowAllergies.UseVisualStyleBackColor = true;
            // 
            // cbCurrentMedsDocumented
            // 
            this.cbCurrentMedsDocumented.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCurrentMedsDocumented.AutoSize = true;
            this.cbCurrentMedsDocumented.Location = new System.Drawing.Point(520, 133);
            this.cbCurrentMedsDocumented.Name = "cbCurrentMedsDocumented";
            this.cbCurrentMedsDocumented.Size = new System.Drawing.Size(150, 17);
            this.cbCurrentMedsDocumented.TabIndex = 4;
            this.cbCurrentMedsDocumented.Text = "Current meds documented";
            this.cbCurrentMedsDocumented.UseVisualStyleBackColor = true;
            // 
            // cbActive
            // 
            this.cbActive.AutoSize = true;
            this.cbActive.Location = new System.Drawing.Point(128, 27);
            this.cbActive.Name = "cbActive";
            this.cbActive.Size = new System.Drawing.Size(56, 17);
            this.cbActive.TabIndex = 5;
            this.cbActive.Text = "Active";
            this.cbActive.UseVisualStyleBackColor = true;
            // 
            // cbIntolerant
            // 
            this.cbIntolerant.AutoSize = true;
            this.cbIntolerant.Location = new System.Drawing.Point(189, 27);
            this.cbIntolerant.Name = "cbIntolerant";
            this.cbIntolerant.Size = new System.Drawing.Size(70, 17);
            this.cbIntolerant.TabIndex = 6;
            this.cbIntolerant.Text = "Intolerant";
            this.cbIntolerant.UseVisualStyleBackColor = true;
            // 
            // cbDiscontinued
            // 
            this.cbDiscontinued.AutoSize = true;
            this.cbDiscontinued.Location = new System.Drawing.Point(264, 27);
            this.cbDiscontinued.Name = "cbDiscontinued";
            this.cbDiscontinued.Size = new System.Drawing.Size(88, 17);
            this.cbDiscontinued.TabIndex = 7;
            this.cbDiscontinued.Text = "Discontinued";
            this.cbDiscontinued.UseVisualStyleBackColor = true;
            // 
            // cbShowDelCan
            // 
            this.cbShowDelCan.AutoSize = true;
            this.cbShowDelCan.Location = new System.Drawing.Point(476, 27);
            this.cbShowDelCan.Name = "cbShowDelCan";
            this.cbShowDelCan.Size = new System.Drawing.Size(145, 17);
            this.cbShowDelCan.TabIndex = 8;
            this.cbShowDelCan.Text = "Show Deleted/Cancelled";
            this.cbShowDelCan.UseVisualStyleBackColor = true;
            // 
            // dgvRxHistory
            // 
            this.dgvRxHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRxHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRxHistory.Location = new System.Drawing.Point(12, 188);
            this.dgvRxHistory.Name = "dgvRxHistory";
            this.dgvRxHistory.Size = new System.Drawing.Size(651, 23);
            this.dgvRxHistory.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 174);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "drug-name Rx History";
            // 
            // dgvDrugs
            // 
            this.dgvDrugs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvDrugs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDrugs.Location = new System.Drawing.Point(52, 236);
            this.dgvDrugs.Name = "dgvDrugs";
            this.dgvDrugs.Size = new System.Drawing.Size(282, 199);
            this.dgvDrugs.TabIndex = 11;
            // 
            // cmdABC
            // 
            this.cmdABC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdABC.Location = new System.Drawing.Point(11, 236);
            this.cmdABC.Name = "cmdABC";
            this.cmdABC.Size = new System.Drawing.Size(43, 23);
            this.cmdABC.TabIndex = 12;
            this.cmdABC.Text = "ABC";
            this.cmdABC.UseVisualStyleBackColor = true;
            // 
            // cmdDEF
            // 
            this.cmdDEF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDEF.Location = new System.Drawing.Point(11, 258);
            this.cmdDEF.Name = "cmdDEF";
            this.cmdDEF.Size = new System.Drawing.Size(43, 23);
            this.cmdDEF.TabIndex = 13;
            this.cmdDEF.Text = "DEF";
            this.cmdDEF.UseVisualStyleBackColor = true;
            // 
            // cmdGHI
            // 
            this.cmdGHI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdGHI.Location = new System.Drawing.Point(11, 280);
            this.cmdGHI.Name = "cmdGHI";
            this.cmdGHI.Size = new System.Drawing.Size(43, 23);
            this.cmdGHI.TabIndex = 14;
            this.cmdGHI.Text = "GHI";
            this.cmdGHI.UseVisualStyleBackColor = true;
            // 
            // cmdJKL
            // 
            this.cmdJKL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdJKL.Location = new System.Drawing.Point(11, 302);
            this.cmdJKL.Name = "cmdJKL";
            this.cmdJKL.Size = new System.Drawing.Size(43, 23);
            this.cmdJKL.TabIndex = 15;
            this.cmdJKL.Text = "JKL";
            this.cmdJKL.UseVisualStyleBackColor = true;
            // 
            // cmdMNO
            // 
            this.cmdMNO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdMNO.Location = new System.Drawing.Point(11, 324);
            this.cmdMNO.Name = "cmdMNO";
            this.cmdMNO.Size = new System.Drawing.Size(43, 23);
            this.cmdMNO.TabIndex = 16;
            this.cmdMNO.Text = "MNO";
            this.cmdMNO.UseVisualStyleBackColor = true;
            // 
            // cmdPQR
            // 
            this.cmdPQR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdPQR.Location = new System.Drawing.Point(11, 346);
            this.cmdPQR.Name = "cmdPQR";
            this.cmdPQR.Size = new System.Drawing.Size(43, 23);
            this.cmdPQR.TabIndex = 17;
            this.cmdPQR.Text = "PQR";
            this.cmdPQR.UseVisualStyleBackColor = true;
            // 
            // cmdSTU
            // 
            this.cmdSTU.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdSTU.Location = new System.Drawing.Point(11, 368);
            this.cmdSTU.Name = "cmdSTU";
            this.cmdSTU.Size = new System.Drawing.Size(43, 23);
            this.cmdSTU.TabIndex = 18;
            this.cmdSTU.Text = "STU";
            this.cmdSTU.UseVisualStyleBackColor = true;
            // 
            // cmdVWX
            // 
            this.cmdVWX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdVWX.Location = new System.Drawing.Point(11, 390);
            this.cmdVWX.Name = "cmdVWX";
            this.cmdVWX.Size = new System.Drawing.Size(43, 23);
            this.cmdVWX.TabIndex = 19;
            this.cmdVWX.Text = "VWX";
            this.cmdVWX.UseVisualStyleBackColor = true;
            // 
            // cmdYZ
            // 
            this.cmdYZ.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdYZ.Location = new System.Drawing.Point(11, 412);
            this.cmdYZ.Name = "cmdYZ";
            this.cmdYZ.Size = new System.Drawing.Size(43, 23);
            this.cmdYZ.TabIndex = 20;
            this.cmdYZ.Text = "YZ";
            this.cmdYZ.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Rx:";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox1.Location = new System.Drawing.Point(54, 215);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 20);
            this.textBox1.TabIndex = 22;
            // 
            // cmdFind
            // 
            this.cmdFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdFind.Location = new System.Drawing.Point(173, 215);
            this.cmdFind.Name = "cmdFind";
            this.cmdFind.Size = new System.Drawing.Size(25, 20);
            this.cmdFind.TabIndex = 23;
            this.cmdFind.Text = "F";
            this.cmdFind.UseVisualStyleBackColor = true;
            // 
            // rbDrugMaster
            // 
            this.rbDrugMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbDrugMaster.AutoSize = true;
            this.rbDrugMaster.Location = new System.Drawing.Point(205, 217);
            this.rbDrugMaster.Name = "rbDrugMaster";
            this.rbDrugMaster.Size = new System.Drawing.Size(57, 17);
            this.rbDrugMaster.TabIndex = 24;
            this.rbDrugMaster.TabStop = true;
            this.rbDrugMaster.Text = "Master";
            this.rbDrugMaster.UseVisualStyleBackColor = true;
            // 
            // rbFavorites
            // 
            this.rbFavorites.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.rbFavorites.AutoSize = true;
            this.rbFavorites.Location = new System.Drawing.Point(266, 217);
            this.rbFavorites.Name = "rbFavorites";
            this.rbFavorites.Size = new System.Drawing.Size(68, 17);
            this.rbFavorites.TabIndex = 25;
            this.rbFavorites.TabStop = true;
            this.rbFavorites.Text = "Favorites";
            this.rbFavorites.UseVisualStyleBackColor = true;
            // 
            // cbSigsMaster
            // 
            this.cbSigsMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSigsMaster.AutoSize = true;
            this.cbSigsMaster.Location = new System.Drawing.Point(345, 219);
            this.cbSigsMaster.Name = "cbSigsMaster";
            this.cbSigsMaster.Size = new System.Drawing.Size(58, 17);
            this.cbSigsMaster.TabIndex = 26;
            this.cbSigsMaster.Text = "Master";
            this.cbSigsMaster.UseVisualStyleBackColor = true;
            // 
            // cbSigsFavorites
            // 
            this.cbSigsFavorites.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbSigsFavorites.AutoSize = true;
            this.cbSigsFavorites.Location = new System.Drawing.Point(407, 219);
            this.cbSigsFavorites.Name = "cbSigsFavorites";
            this.cbSigsFavorites.Size = new System.Drawing.Size(69, 17);
            this.cbSigsFavorites.TabIndex = 27;
            this.cbSigsFavorites.Text = "Favorites";
            this.cbSigsFavorites.UseVisualStyleBackColor = true;
            // 
            // dgvSigs
            // 
            this.dgvSigs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSigs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSigs.Location = new System.Drawing.Point(344, 236);
            this.dgvSigs.Name = "dgvSigs";
            this.dgvSigs.Size = new System.Drawing.Size(216, 89);
            this.dgvSigs.TabIndex = 28;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmdComplete3);
            this.groupBox1.Controls.Add(this.cmdComplete2);
            this.groupBox1.Controls.Add(this.cmdComplete1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmdSendRx3);
            this.groupBox1.Controls.Add(this.cmdSendRx2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmdSendRx1);
            this.groupBox1.Location = new System.Drawing.Point(566, 230);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(97, 205);
            this.groupBox1.TabIndex = 29;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rx Messaging";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Send Rx";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdSendRx1
            // 
            this.cmdSendRx1.Location = new System.Drawing.Point(6, 36);
            this.cmdSendRx1.Name = "cmdSendRx1";
            this.cmdSendRx1.Size = new System.Drawing.Size(85, 23);
            this.cmdSendRx1.TabIndex = 0;
            this.cmdSendRx1.Text = "Hand Off";
            this.cmdSendRx1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBox10);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.button11);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Location = new System.Drawing.Point(345, 331);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(117, 104);
            this.groupBox2.TabIndex = 30;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rx Formulary Status";
            // 
            // cmdSendRx2
            // 
            this.cmdSendRx2.Location = new System.Drawing.Point(6, 58);
            this.cmdSendRx2.Name = "cmdSendRx2";
            this.cmdSendRx2.Size = new System.Drawing.Size(85, 23);
            this.cmdSendRx2.TabIndex = 2;
            this.cmdSendRx2.Text = "Disapprove";
            this.cmdSendRx2.UseVisualStyleBackColor = true;
            // 
            // cmdSendRx3
            // 
            this.cmdSendRx3.Location = new System.Drawing.Point(6, 80);
            this.cmdSendRx3.Name = "cmdSendRx3";
            this.cmdSendRx3.Size = new System.Drawing.Size(85, 23);
            this.cmdSendRx3.TabIndex = 3;
            this.cmdSendRx3.Text = "Get Approval";
            this.cmdSendRx3.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Complete";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdComplete1
            // 
            this.cmdComplete1.Location = new System.Drawing.Point(6, 125);
            this.cmdComplete1.Name = "cmdComplete1";
            this.cmdComplete1.Size = new System.Drawing.Size(85, 23);
            this.cmdComplete1.TabIndex = 5;
            this.cmdComplete1.Text = "Prescribe";
            this.cmdComplete1.UseVisualStyleBackColor = true;
            // 
            // cmdComplete2
            // 
            this.cmdComplete2.Location = new System.Drawing.Point(6, 147);
            this.cmdComplete2.Name = "cmdComplete2";
            this.cmdComplete2.Size = new System.Drawing.Size(85, 23);
            this.cmdComplete2.TabIndex = 6;
            this.cmdComplete2.Text = "Hand Off";
            this.cmdComplete2.UseVisualStyleBackColor = true;
            // 
            // cmdComplete3
            // 
            this.cmdComplete3.Location = new System.Drawing.Point(6, 169);
            this.cmdComplete3.Name = "cmdComplete3";
            this.cmdComplete3.Size = new System.Drawing.Size(85, 23);
            this.cmdComplete3.TabIndex = 7;
            this.cmdComplete3.Text = "Modify Rx";
            this.cmdComplete3.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(34, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(6, 46);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(24, 20);
            this.button11.TabIndex = 1;
            this.button11.Text = "...";
            this.button11.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(36, 46);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(75, 20);
            this.textBox2.TabIndex = 2;
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(6, 72);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(105, 20);
            this.textBox3.TabIndex = 3;
            // 
            // checkBox10
            // 
            this.checkBox10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox10.AutoSize = true;
            this.checkBox10.Location = new System.Drawing.Point(43, 21);
            this.checkBox10.Name = "checkBox10";
            this.checkBox10.Size = new System.Drawing.Size(69, 17);
            this.checkBox10.TabIndex = 4;
            this.checkBox10.Text = "Preferred";
            this.checkBox10.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.button12);
            this.groupBox3.Location = new System.Drawing.Point(468, 331);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(92, 104);
            this.groupBox3.TabIndex = 31;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rx Alerts";
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(9, 75);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(75, 23);
            this.button12.TabIndex = 0;
            this.button12.Text = "Check Alerts";
            this.button12.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Intolerance";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(62, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Interactions";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(25, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Dosage";
            // 
            // frmViewRx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 443);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvSigs);
            this.Controls.Add(this.cbSigsFavorites);
            this.Controls.Add(this.cbSigsMaster);
            this.Controls.Add(this.rbFavorites);
            this.Controls.Add(this.rbDrugMaster);
            this.Controls.Add(this.cmdFind);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmdYZ);
            this.Controls.Add(this.cmdVWX);
            this.Controls.Add(this.cmdSTU);
            this.Controls.Add(this.cmdPQR);
            this.Controls.Add(this.cmdMNO);
            this.Controls.Add(this.cmdJKL);
            this.Controls.Add(this.cmdGHI);
            this.Controls.Add(this.cmdDEF);
            this.Controls.Add(this.cmdABC);
            this.Controls.Add(this.dgvDrugs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvRxHistory);
            this.Controls.Add(this.cbShowDelCan);
            this.Controls.Add(this.cbDiscontinued);
            this.Controls.Add(this.cbIntolerant);
            this.Controls.Add(this.cbActive);
            this.Controls.Add(this.cbCurrentMedsDocumented);
            this.Controls.Add(this.cbPatientNoKnowAllergies);
            this.Controls.Add(this.cbNoKnownActive);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDrugHistory);
            this.Name = "frmViewRx";
            this.Text = "frmViewRx";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrugHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRxHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDrugs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSigs)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDrugHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbNoKnownActive;
        private System.Windows.Forms.CheckBox cbPatientNoKnowAllergies;
        private System.Windows.Forms.CheckBox cbCurrentMedsDocumented;
        private System.Windows.Forms.CheckBox cbActive;
        private System.Windows.Forms.CheckBox cbIntolerant;
        private System.Windows.Forms.CheckBox cbDiscontinued;
        private System.Windows.Forms.CheckBox cbShowDelCan;
        private System.Windows.Forms.DataGridView dgvRxHistory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDrugs;
        private System.Windows.Forms.Button cmdABC;
        private System.Windows.Forms.Button cmdDEF;
        private System.Windows.Forms.Button cmdGHI;
        private System.Windows.Forms.Button cmdJKL;
        private System.Windows.Forms.Button cmdMNO;
        private System.Windows.Forms.Button cmdPQR;
        private System.Windows.Forms.Button cmdSTU;
        private System.Windows.Forms.Button cmdVWX;
        private System.Windows.Forms.Button cmdYZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button cmdFind;
        private System.Windows.Forms.RadioButton rbDrugMaster;
        private System.Windows.Forms.RadioButton rbFavorites;
        private System.Windows.Forms.CheckBox cbSigsMaster;
        private System.Windows.Forms.CheckBox cbSigsFavorites;
        private System.Windows.Forms.DataGridView dgvSigs;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdSendRx1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdComplete3;
        private System.Windows.Forms.Button cmdComplete2;
        private System.Windows.Forms.Button cmdComplete1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button cmdSendRx3;
        private System.Windows.Forms.Button cmdSendRx2;
        private System.Windows.Forms.CheckBox checkBox10;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button12;
    }
}