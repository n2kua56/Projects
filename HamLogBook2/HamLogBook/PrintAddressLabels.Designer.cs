namespace HamLogBook
{
    partial class PrintAddressLabels
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.nudLabelRowsPerPage = new System.Windows.Forms.NumericUpDown();
            this.nudLabelsPerRow = new System.Windows.Forms.NumericUpDown();
            this.nudSartPrintRow = new System.Windows.Forms.NumericUpDown();
            this.nudLabelHeight = new System.Windows.Forms.NumericUpDown();
            this.nudLabelWidth = new System.Windows.Forms.NumericUpDown();
            this.gbSingleLabel = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tbCall = new System.Windows.Forms.TextBox();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbAddress = new System.Windows.Forms.TextBox();
            this.tbCity = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tbState = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tbZip = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tbCountry = new System.Windows.Forms.TextBox();
            this.nudTopMargin = new System.Windows.Forms.NumericUpDown();
            this.nudLeftMargin = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.nudStartPrintingColumn = new System.Windows.Forms.NumericUpDown();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblPrintMode = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudLabelRowsPerPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLabelsPerRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSartPrintRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLabelHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLabelWidth)).BeginInit();
            this.gbSingleLabel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftMargin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartPrintingColumn)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Label rows (top to bottom):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Labels per row (left to right)?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Label Height (inches):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Label Width (inches):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Top Margin (inches):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Left Margin (Inches):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 213);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Start Printing on Row:";
            // 
            // nudLabelRowsPerPage
            // 
            this.nudLabelRowsPerPage.Location = new System.Drawing.Point(151, 46);
            this.nudLabelRowsPerPage.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudLabelRowsPerPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLabelRowsPerPage.Name = "nudLabelRowsPerPage";
            this.nudLabelRowsPerPage.Size = new System.Drawing.Size(51, 20);
            this.nudLabelRowsPerPage.TabIndex = 14;
            this.nudLabelRowsPerPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudLabelsPerRow
            // 
            this.nudLabelsPerRow.Location = new System.Drawing.Point(151, 72);
            this.nudLabelsPerRow.Name = "nudLabelsPerRow";
            this.nudLabelsPerRow.Size = new System.Drawing.Size(51, 20);
            this.nudLabelsPerRow.TabIndex = 15;
            // 
            // nudSartPrintRow
            // 
            this.nudSartPrintRow.Location = new System.Drawing.Point(151, 213);
            this.nudSartPrintRow.Name = "nudSartPrintRow";
            this.nudSartPrintRow.Size = new System.Drawing.Size(51, 20);
            this.nudSartPrintRow.TabIndex = 16;
            // 
            // nudLabelHeight
            // 
            this.nudLabelHeight.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudLabelHeight.Location = new System.Drawing.Point(151, 98);
            this.nudLabelHeight.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.nudLabelHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLabelHeight.Name = "nudLabelHeight";
            this.nudLabelHeight.Size = new System.Drawing.Size(51, 20);
            this.nudLabelHeight.TabIndex = 17;
            this.nudLabelHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // nudLabelWidth
            // 
            this.nudLabelWidth.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudLabelWidth.Location = new System.Drawing.Point(151, 124);
            this.nudLabelWidth.Maximum = new decimal(new int[] {
            85,
            0,
            0,
            65536});
            this.nudLabelWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLabelWidth.Name = "nudLabelWidth";
            this.nudLabelWidth.Size = new System.Drawing.Size(51, 20);
            this.nudLabelWidth.TabIndex = 18;
            this.nudLabelWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // gbSingleLabel
            // 
            this.gbSingleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSingleLabel.Controls.Add(this.btnClear);
            this.gbSingleLabel.Controls.Add(this.tbCountry);
            this.gbSingleLabel.Controls.Add(this.label14);
            this.gbSingleLabel.Controls.Add(this.tbZip);
            this.gbSingleLabel.Controls.Add(this.label13);
            this.gbSingleLabel.Controls.Add(this.tbState);
            this.gbSingleLabel.Controls.Add(this.label12);
            this.gbSingleLabel.Controls.Add(this.tbCity);
            this.gbSingleLabel.Controls.Add(this.tbAddress);
            this.gbSingleLabel.Controls.Add(this.tbName);
            this.gbSingleLabel.Controls.Add(this.tbCall);
            this.gbSingleLabel.Controls.Add(this.label11);
            this.gbSingleLabel.Controls.Add(this.label10);
            this.gbSingleLabel.Controls.Add(this.label9);
            this.gbSingleLabel.Controls.Add(this.label8);
            this.gbSingleLabel.Location = new System.Drawing.Point(210, 41);
            this.gbSingleLabel.Name = "gbSingleLabel";
            this.gbSingleLabel.Size = new System.Drawing.Size(253, 211);
            this.gbSingleLabel.TabIndex = 19;
            this.gbSingleLabel.TabStop = false;
            this.gbSingleLabel.Text = "Single Label";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(24, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Call";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Name";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Address";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "City";
            // 
            // tbCall
            // 
            this.tbCall.Location = new System.Drawing.Point(51, 21);
            this.tbCall.Name = "tbCall";
            this.tbCall.Size = new System.Drawing.Size(100, 20);
            this.tbCall.TabIndex = 4;
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(51, 47);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(187, 20);
            this.tbName.TabIndex = 5;
            // 
            // tbAddress
            // 
            this.tbAddress.Location = new System.Drawing.Point(51, 73);
            this.tbAddress.Name = "tbAddress";
            this.tbAddress.Size = new System.Drawing.Size(187, 20);
            this.tbAddress.TabIndex = 6;
            // 
            // tbCity
            // 
            this.tbCity.Location = new System.Drawing.Point(51, 99);
            this.tbCity.Name = "tbCity";
            this.tbCity.Size = new System.Drawing.Size(110, 20);
            this.tbCity.TabIndex = 7;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(167, 101);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "St";
            // 
            // tbState
            // 
            this.tbState.Location = new System.Drawing.Point(190, 99);
            this.tbState.Name = "tbState";
            this.tbState.Size = new System.Drawing.Size(48, 20);
            this.tbState.TabIndex = 9;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 125);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Zip";
            // 
            // tbZip
            // 
            this.tbZip.Location = new System.Drawing.Point(51, 125);
            this.tbZip.Name = "tbZip";
            this.tbZip.Size = new System.Drawing.Size(62, 20);
            this.tbZip.TabIndex = 11;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 154);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 13);
            this.label14.TabIndex = 12;
            this.label14.Text = "Country";
            // 
            // tbCountry
            // 
            this.tbCountry.Location = new System.Drawing.Point(51, 151);
            this.tbCountry.Name = "tbCountry";
            this.tbCountry.Size = new System.Drawing.Size(187, 20);
            this.tbCountry.TabIndex = 13;
            // 
            // nudTopMargin
            // 
            this.nudTopMargin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudTopMargin.Location = new System.Drawing.Point(151, 150);
            this.nudTopMargin.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTopMargin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudTopMargin.Name = "nudTopMargin";
            this.nudTopMargin.Size = new System.Drawing.Size(51, 20);
            this.nudTopMargin.TabIndex = 20;
            this.nudTopMargin.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // nudLeftMargin
            // 
            this.nudLeftMargin.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudLeftMargin.Location = new System.Drawing.Point(151, 176);
            this.nudLeftMargin.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLeftMargin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudLeftMargin.Name = "nudLeftMargin";
            this.nudLeftMargin.Size = new System.Drawing.Size(51, 20);
            this.nudLeftMargin.TabIndex = 21;
            this.nudLeftMargin.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(10, 239);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(123, 13);
            this.label15.TabIndex = 22;
            this.label15.Text = "Start Printing on Column:";
            // 
            // nudStartPrintingColumn
            // 
            this.nudStartPrintingColumn.Location = new System.Drawing.Point(151, 239);
            this.nudStartPrintingColumn.Name = "nudStartPrintingColumn";
            this.nudStartPrintingColumn.Size = new System.Drawing.Size(51, 20);
            this.nudStartPrintingColumn.TabIndex = 23;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(388, 261);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 24;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(296, 261);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblPrintMode
            // 
            this.lblPrintMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrintMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrintMode.Location = new System.Drawing.Point(12, 8);
            this.lblPrintMode.Name = "lblPrintMode";
            this.lblPrintMode.Size = new System.Drawing.Size(451, 23);
            this.lblPrintMode.TabIndex = 26;
            this.lblPrintMode.Text = "Manual Print";
            this.lblPrintMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(163, 180);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // PrintAddressLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(479, 293);
            this.Controls.Add(this.lblPrintMode);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.nudStartPrintingColumn);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.nudLeftMargin);
            this.Controls.Add(this.nudTopMargin);
            this.Controls.Add(this.gbSingleLabel);
            this.Controls.Add(this.nudLabelWidth);
            this.Controls.Add(this.nudLabelHeight);
            this.Controls.Add(this.nudSartPrintRow);
            this.Controls.Add(this.nudLabelsPerRow);
            this.Controls.Add(this.nudLabelRowsPerPage);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "PrintAddressLabels";
            this.Text = "Print Address Labels";
            ((System.ComponentModel.ISupportInitialize)(this.nudLabelRowsPerPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLabelsPerRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSartPrintRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLabelHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLabelWidth)).EndInit();
            this.gbSingleLabel.ResumeLayout(false);
            this.gbSingleLabel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTopMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLeftMargin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudStartPrintingColumn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudLabelRowsPerPage;
        private System.Windows.Forms.NumericUpDown nudLabelsPerRow;
        private System.Windows.Forms.NumericUpDown nudSartPrintRow;
        private System.Windows.Forms.NumericUpDown nudLabelHeight;
        private System.Windows.Forms.NumericUpDown nudLabelWidth;
        private System.Windows.Forms.GroupBox gbSingleLabel;
        private System.Windows.Forms.TextBox tbCountry;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tbZip;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbState;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbCity;
        private System.Windows.Forms.TextBox tbAddress;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbCall;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudTopMargin;
        private System.Windows.Forms.NumericUpDown nudLeftMargin;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown nudStartPrintingColumn;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblPrintMode;
        private System.Windows.Forms.Button btnClear;
    }
}