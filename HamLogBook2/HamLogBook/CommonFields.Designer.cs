namespace HamLogBook
{
    partial class CommonFields
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.tbInitials = new System.Windows.Forms.TextBox();
            this.tbOperator = new System.Windows.Forms.TextBox();
            this.tbLongitude = new System.Windows.Forms.TextBox();
            this.tbLatitude = new System.Windows.Forms.TextBox();
            this.tbContinent = new System.Windows.Forms.TextBox();
            this.tbCountry = new System.Windows.Forms.TextBox();
            this.tbCall = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbAlwaysDisplay = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel3.Controls.Add(this.label13);
            this.panel3.Location = new System.Drawing.Point(1, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(582, 26);
            this.panel3.TabIndex = 35;
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(1, 1);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(578, 23);
            this.label13.TabIndex = 0;
            this.label13.Text = "Common Fields";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbInitials
            // 
            this.tbInitials.Location = new System.Drawing.Point(332, 125);
            this.tbInitials.MaxLength = 8;
            this.tbInitials.Name = "tbInitials";
            this.tbInitials.Size = new System.Drawing.Size(100, 20);
            this.tbInitials.TabIndex = 34;
            this.toolTip1.SetToolTip(this.tbInitials, "Initials of the Control Operator if not you");
            this.tbInitials.Leave += new System.EventHandler(this.tbCall_Leave);
            // 
            // tbOperator
            // 
            this.tbOperator.Location = new System.Drawing.Point(332, 99);
            this.tbOperator.MaxLength = 16;
            this.tbOperator.Name = "tbOperator";
            this.tbOperator.Size = new System.Drawing.Size(100, 20);
            this.tbOperator.TabIndex = 33;
            this.toolTip1.SetToolTip(this.tbOperator, "Call Sign of Control Operator if not you");
            this.tbOperator.Leave += new System.EventHandler(this.tbCall_Leave);
            // 
            // tbLongitude
            // 
            this.tbLongitude.Location = new System.Drawing.Point(126, 203);
            this.tbLongitude.MaxLength = 10;
            this.tbLongitude.Name = "tbLongitude";
            this.tbLongitude.Size = new System.Drawing.Size(100, 20);
            this.tbLongitude.TabIndex = 32;
            this.toolTip1.SetToolTip(this.tbLongitude, "Your Stations Longitude");
            this.tbLongitude.Leave += new System.EventHandler(this.tbCall_Leave);
            // 
            // tbLatitude
            // 
            this.tbLatitude.Location = new System.Drawing.Point(126, 177);
            this.tbLatitude.MaxLength = 10;
            this.tbLatitude.Name = "tbLatitude";
            this.tbLatitude.Size = new System.Drawing.Size(100, 20);
            this.tbLatitude.TabIndex = 31;
            this.toolTip1.SetToolTip(this.tbLatitude, "Your Stations Latitude");
            this.tbLatitude.Leave += new System.EventHandler(this.tbCall_Leave);
            // 
            // tbContinent
            // 
            this.tbContinent.Location = new System.Drawing.Point(126, 151);
            this.tbContinent.MaxLength = 40;
            this.tbContinent.Name = "tbContinent";
            this.tbContinent.Size = new System.Drawing.Size(100, 20);
            this.tbContinent.TabIndex = 30;
            this.toolTip1.SetToolTip(this.tbContinent, "Continent you are operating from");
            this.tbContinent.Leave += new System.EventHandler(this.tbCall_Leave);
            // 
            // tbCountry
            // 
            this.tbCountry.Location = new System.Drawing.Point(126, 125);
            this.tbCountry.MaxLength = 40;
            this.tbCountry.Name = "tbCountry";
            this.tbCountry.Size = new System.Drawing.Size(100, 20);
            this.tbCountry.TabIndex = 29;
            this.toolTip1.SetToolTip(this.tbCountry, "The Country you are operating from");
            this.tbCountry.Leave += new System.EventHandler(this.tbCall_Leave);
            // 
            // tbCall
            // 
            this.tbCall.Location = new System.Drawing.Point(126, 99);
            this.tbCall.MaxLength = 16;
            this.tbCall.Name = "tbCall";
            this.tbCall.Size = new System.Drawing.Size(100, 20);
            this.tbCall.TabIndex = 28;
            this.toolTip1.SetToolTip(this.tbCall, "Your Call Sign");
            this.tbCall.Leave += new System.EventHandler(this.tbCall_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(122, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 20);
            this.label9.TabIndex = 27;
            this.label9.Text = "Common Fields";
            // 
            // cbAlwaysDisplay
            // 
            this.cbAlwaysDisplay.AutoSize = true;
            this.cbAlwaysDisplay.Location = new System.Drawing.Point(269, 169);
            this.cbAlwaysDisplay.Name = "cbAlwaysDisplay";
            this.cbAlwaysDisplay.Size = new System.Drawing.Size(144, 17);
            this.cbAlwaysDisplay.TabIndex = 26;
            this.cbAlwaysDisplay.Text = "Always display on startup";
            this.cbAlwaysDisplay.UseVisualStyleBackColor = true;
            this.cbAlwaysDisplay.CheckedChanged += new System.EventHandler(this.cbAlwaysDisplay_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(269, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "Initials";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(269, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 24;
            this.label7.Text = "Operator";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(328, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 20);
            this.label6.TabIndex = 23;
            this.label6.Text = "Optional";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(64, 203);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Longitude";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(64, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Latitude";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(64, 151);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Continent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(64, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Country";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Call";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(357, 201);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 36;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // CommonFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(585, 348);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tbInitials);
            this.Controls.Add(this.tbOperator);
            this.Controls.Add(this.tbLongitude);
            this.Controls.Add(this.tbLatitude);
            this.Controls.Add(this.tbContinent);
            this.Controls.Add(this.tbCountry);
            this.Controls.Add(this.tbCall);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cbAlwaysDisplay);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "CommonFields";
            this.Text = "CommonFields";
            this.Load += new System.EventHandler(this.CommonFields_Load);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tbInitials;
        private System.Windows.Forms.TextBox tbOperator;
        private System.Windows.Forms.TextBox tbLongitude;
        private System.Windows.Forms.TextBox tbLatitude;
        private System.Windows.Forms.TextBox tbContinent;
        private System.Windows.Forms.TextBox tbCountry;
        private System.Windows.Forms.TextBox tbCall;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cbAlwaysDisplay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}