namespace Microsoft.SystemCenter.Orchestrator.Integration.Examples.IPCleaner
{
    partial class ConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            this.buttonOk = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDBUserName = new System.Windows.Forms.TextBox();
            this.textBoxSQLServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDBPassword = new System.Windows.Forms.TextBox();
            this.textBoxDatabase = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.groupBoxDatabase = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBoxManagementServer = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBoxMSPassword = new System.Windows.Forms.TextBox();
            this.textBoxMSUserName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxManagementServer = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBoxWMIPassword = new System.Windows.Forms.TextBox();
            this.numericTimeout = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxWMIUserName = new System.Windows.Forms.TextBox();
            this.checkBoxEnablePrivileges = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboAuthenticationLevel = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboImpersonationLevel = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxSaveSettings = new System.Windows.Forms.CheckBox();
            this.groupBoxDatabase.SuspendLayout();
            this.groupBoxManagementServer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(244, 553);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 7;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Domain\\Username";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "SQL Server\\Instance";
            // 
            // textBoxDBUserName
            // 
            this.textBoxDBUserName.Location = new System.Drawing.Point(138, 82);
            this.textBoxDBUserName.Name = "textBoxDBUserName";
            this.textBoxDBUserName.Size = new System.Drawing.Size(176, 20);
            this.textBoxDBUserName.TabIndex = 5;
            // 
            // textBoxSQLServer
            // 
            this.textBoxSQLServer.Location = new System.Drawing.Point(138, 30);
            this.textBoxSQLServer.Name = "textBoxSQLServer";
            this.textBoxSQLServer.Size = new System.Drawing.Size(176, 20);
            this.textBoxSQLServer.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(79, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Database Name";
            // 
            // textBoxDBPassword
            // 
            this.textBoxDBPassword.Location = new System.Drawing.Point(138, 108);
            this.textBoxDBPassword.Name = "textBoxDBPassword";
            this.textBoxDBPassword.Size = new System.Drawing.Size(176, 20);
            this.textBoxDBPassword.TabIndex = 6;
            this.textBoxDBPassword.UseSystemPasswordChar = true;
            // 
            // textBoxDatabase
            // 
            this.textBoxDatabase.Location = new System.Drawing.Point(138, 56);
            this.textBoxDatabase.Name = "textBoxDatabase";
            this.textBoxDatabase.Size = new System.Drawing.Size(176, 20);
            this.textBoxDatabase.TabIndex = 4;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(325, 553);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 8;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // groupBoxDatabase
            // 
            this.groupBoxDatabase.Controls.Add(this.textBoxDBPassword);
            this.groupBoxDatabase.Controls.Add(this.textBox2);
            this.groupBoxDatabase.Controls.Add(this.label4);
            this.groupBoxDatabase.Controls.Add(this.label3);
            this.groupBoxDatabase.Controls.Add(this.label2);
            this.groupBoxDatabase.Controls.Add(this.textBoxDatabase);
            this.groupBoxDatabase.Controls.Add(this.textBoxSQLServer);
            this.groupBoxDatabase.Controls.Add(this.label1);
            this.groupBoxDatabase.Controls.Add(this.textBoxDBUserName);
            this.groupBoxDatabase.Location = new System.Drawing.Point(15, 125);
            this.groupBoxDatabase.Name = "groupBoxDatabase";
            this.groupBoxDatabase.Size = new System.Drawing.Size(617, 144);
            this.groupBoxDatabase.TabIndex = 14;
            this.groupBoxDatabase.TabStop = false;
            this.groupBoxDatabase.Text = "Database";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(327, 26);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(279, 118);
            this.textBox2.TabIndex = 18;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // groupBoxManagementServer
            // 
            this.groupBoxManagementServer.Controls.Add(this.label7);
            this.groupBoxManagementServer.Controls.Add(this.label6);
            this.groupBoxManagementServer.Controls.Add(this.textBox1);
            this.groupBoxManagementServer.Controls.Add(this.textBoxMSPassword);
            this.groupBoxManagementServer.Controls.Add(this.textBoxMSUserName);
            this.groupBoxManagementServer.Controls.Add(this.label5);
            this.groupBoxManagementServer.Controls.Add(this.textBoxManagementServer);
            this.groupBoxManagementServer.Location = new System.Drawing.Point(15, 12);
            this.groupBoxManagementServer.Name = "groupBoxManagementServer";
            this.groupBoxManagementServer.Size = new System.Drawing.Size(617, 107);
            this.groupBoxManagementServer.TabIndex = 15;
            this.groupBoxManagementServer.TabStop = false;
            this.groupBoxManagementServer.Text = "Management Server";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(79, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Password";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(36, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Domain\\Username";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(327, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(279, 73);
            this.textBox1.TabIndex = 17;
            this.textBox1.Text = "These properties are used to connect to the Orchestrator Management Server. \r\n\r\nI" +
    "f you leave these settings blank, the current computer and logged-on user creden" +
    "tials will be assumed.";
            // 
            // textBoxMSPassword
            // 
            this.textBoxMSPassword.Location = new System.Drawing.Point(138, 72);
            this.textBoxMSPassword.Name = "textBoxMSPassword";
            this.textBoxMSPassword.Size = new System.Drawing.Size(176, 20);
            this.textBoxMSPassword.TabIndex = 2;
            this.textBoxMSPassword.UseSystemPasswordChar = true;
            // 
            // textBoxMSUserName
            // 
            this.textBoxMSUserName.Location = new System.Drawing.Point(138, 46);
            this.textBoxMSUserName.Name = "textBoxMSUserName";
            this.textBoxMSUserName.Size = new System.Drawing.Size(176, 20);
            this.textBoxMSUserName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(63, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Server Name";
            // 
            // textBoxManagementServer
            // 
            this.textBoxManagementServer.Location = new System.Drawing.Point(138, 20);
            this.textBoxManagementServer.Name = "textBoxManagementServer";
            this.textBoxManagementServer.Size = new System.Drawing.Size(176, 20);
            this.textBoxManagementServer.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBoxWMIPassword);
            this.groupBox1.Controls.Add(this.numericTimeout);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.textBoxWMIUserName);
            this.groupBox1.Controls.Add(this.checkBoxEnablePrivileges);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cboAuthenticationLevel);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cboImpersonationLevel);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(12, 275);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(620, 183);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "WMI Options";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(330, 19);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(279, 153);
            this.textBox3.TabIndex = 19;
            this.textBox3.Text = resources.GetString("textBox3.Text");
            // 
            // textBoxWMIPassword
            // 
            this.textBoxWMIPassword.Location = new System.Drawing.Point(141, 151);
            this.textBoxWMIPassword.Name = "textBoxWMIPassword";
            this.textBoxWMIPassword.Size = new System.Drawing.Size(176, 20);
            this.textBoxWMIPassword.TabIndex = 20;
            this.textBoxWMIPassword.UseSystemPasswordChar = true;
            // 
            // numericTimeout
            // 
            this.numericTimeout.Location = new System.Drawing.Point(142, 99);
            this.numericTimeout.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.numericTimeout.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericTimeout.Name = "numericTimeout";
            this.numericTimeout.Size = new System.Drawing.Size(52, 20);
            this.numericTimeout.TabIndex = 8;
            this.numericTimeout.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(82, 154);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Password";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(41, 101);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 7;
            this.label11.Text = "Timeout (seconds)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(41, 128);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 13);
            this.label13.TabIndex = 21;
            this.label13.Text = "Domain\\Username";
            // 
            // textBoxWMIUserName
            // 
            this.textBoxWMIUserName.Location = new System.Drawing.Point(142, 125);
            this.textBoxWMIUserName.Name = "textBoxWMIUserName";
            this.textBoxWMIUserName.Size = new System.Drawing.Size(175, 20);
            this.textBoxWMIUserName.TabIndex = 19;
            // 
            // checkBoxEnablePrivileges
            // 
            this.checkBoxEnablePrivileges.AutoSize = true;
            this.checkBoxEnablePrivileges.Location = new System.Drawing.Point(141, 78);
            this.checkBoxEnablePrivileges.Name = "checkBoxEnablePrivileges";
            this.checkBoxEnablePrivileges.Size = new System.Drawing.Size(15, 14);
            this.checkBoxEnablePrivileges.TabIndex = 6;
            this.checkBoxEnablePrivileges.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxEnablePrivileges.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(47, 78);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 13);
            this.label10.TabIndex = 5;
            this.label10.Text = "Enable Privileges";
            // 
            // cboAuthenticationLevel
            // 
            this.cboAuthenticationLevel.FormattingEnabled = true;
            this.cboAuthenticationLevel.Items.AddRange(new object[] {
            "Call",
            "Connect",
            "Default",
            "None",
            "Packet",
            "PacketIntegrity",
            "PacketPrivacy",
            "Unchanged"});
            this.cboAuthenticationLevel.Location = new System.Drawing.Point(141, 47);
            this.cboAuthenticationLevel.Name = "cboAuthenticationLevel";
            this.cboAuthenticationLevel.Size = new System.Drawing.Size(176, 21);
            this.cboAuthenticationLevel.TabIndex = 4;
            this.cboAuthenticationLevel.Text = "Default";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(31, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(104, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Authentication Level";
            // 
            // cboImpersonationLevel
            // 
            this.cboImpersonationLevel.FormattingEnabled = true;
            this.cboImpersonationLevel.Items.AddRange(new object[] {
            "Anonymous",
            "Default",
            "Delegate",
            "Identify",
            "Impersonate"});
            this.cboImpersonationLevel.Location = new System.Drawing.Point(141, 20);
            this.cboImpersonationLevel.Name = "cboImpersonationLevel";
            this.cboImpersonationLevel.Size = new System.Drawing.Size(176, 21);
            this.cboImpersonationLevel.TabIndex = 2;
            this.cboImpersonationLevel.Text = "Default";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 23);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Impersonation Level";
            // 
            // checkBoxSaveSettings
            // 
            this.checkBoxSaveSettings.AutoSize = true;
            this.checkBoxSaveSettings.Location = new System.Drawing.Point(28, 474);
            this.checkBoxSaveSettings.Name = "checkBoxSaveSettings";
            this.checkBoxSaveSettings.Size = new System.Drawing.Size(481, 17);
            this.checkBoxSaveSettings.TabIndex = 17;
            this.checkBoxSaveSettings.Text = "Save these settings so they load automatically when the application starts (passw" +
    "ords not saved)";
            this.checkBoxSaveSettings.UseVisualStyleBackColor = true;
            // 
            // ConfigurationForm
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(644, 588);
            this.ControlBox = false;
            this.Controls.Add(this.checkBoxSaveSettings);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxManagementServer);
            this.Controls.Add(this.groupBoxDatabase);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigurationForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration Settings";
            this.groupBoxDatabase.ResumeLayout(false);
            this.groupBoxDatabase.PerformLayout();
            this.groupBoxManagementServer.ResumeLayout(false);
            this.groupBoxManagementServer.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDBUserName;
        private System.Windows.Forms.TextBox textBoxSQLServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDBPassword;
        private System.Windows.Forms.TextBox textBoxDatabase;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.GroupBox groupBoxDatabase;
        private System.Windows.Forms.GroupBox groupBoxManagementServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxMSPassword;
        private System.Windows.Forms.TextBox textBoxMSUserName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxManagementServer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboImpersonationLevel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboAuthenticationLevel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBoxEnablePrivileges;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericTimeout;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBoxWMIPassword;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxWMIUserName;
        private System.Windows.Forms.CheckBox checkBoxSaveSettings;
    }
}