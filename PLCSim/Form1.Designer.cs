namespace Symulator_PLC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button4 = new System.Windows.Forms.Button();
            this.panel2 = new Symulator_PLC.CustomPanel();
            this.virtualSystemPanel = new Symulator_PLC.CustomPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.welcomPanel = new Symulator_PLC.CustomPanel();
            this.label16 = new System.Windows.Forms.Label();
            this.devicesPanel = new Symulator_PLC.CustomPanel();
            this.panelPLCInfo = new System.Windows.Forms.Panel();
            this.labelFirmwareVersion = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.labelOrderNumber = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelShortDesignation = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.panel3 = new Symulator_PLC.CustomPanel();
            this.specialButton3 = new CustomButton.SpecialButton();
            this.specialButton2 = new CustomButton.SpecialButton();
            this.specialButton1 = new CustomButton.SpecialButton();
            this.networkPanel = new Symulator_PLC.CustomPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new Symulator_PLC.CustomGroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.devicePanelChooseModule = new Symulator_PLC.CustomPanel();
            this.button6 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.treeView1 = new Symulator_PLC.CustomTreeView();
            this.panel1 = new Symulator_PLC.CustomPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2.SuspendLayout();
            this.virtualSystemPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.welcomPanel.SuspendLayout();
            this.devicesPanel.SuspendLayout();
            this.panelPLCInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel3.SuspendLayout();
            this.networkPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.devicePanelChooseModule.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(559, 699);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 2;
            this.button4.Text = "Save";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.virtualSystemPanel);
            this.panel2.Controls.Add(this.welcomPanel);
            this.panel2.Controls.Add(this.devicesPanel);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.networkPanel);
            this.panel2.Controls.Add(this.devicePanelChooseModule);
            this.panel2.Location = new System.Drawing.Point(1, 28);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(871, 733);
            this.panel2.TabIndex = 11;
            // 
            // virtualSystemPanel
            // 
            this.virtualSystemPanel.BackgroundImage = global::Symulator_PLC.Properties.Resources.gears;
            this.virtualSystemPanel.Controls.Add(this.button2);
            this.virtualSystemPanel.Controls.Add(this.textBox2);
            this.virtualSystemPanel.Controls.Add(this.label6);
            this.virtualSystemPanel.Controls.Add(this.button5);
            this.virtualSystemPanel.Controls.Add(this.comboBox2);
            this.virtualSystemPanel.Controls.Add(this.label5);
            this.virtualSystemPanel.Controls.Add(this.pictureBox5);
            this.virtualSystemPanel.Controls.Add(this.label4);
            this.virtualSystemPanel.Controls.Add(this.label3);
            this.virtualSystemPanel.Location = new System.Drawing.Point(146, 0);
            this.virtualSystemPanel.Name = "virtualSystemPanel";
            this.virtualSystemPanel.Size = new System.Drawing.Size(726, 733);
            this.virtualSystemPanel.TabIndex = 7;
            this.virtualSystemPanel.Visible = false;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(568, 281);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 36);
            this.button2.TabIndex = 14;
            this.button2.Text = "Connection";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(41, 411);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(467, 169);
            this.textBox2.TabIndex = 13;
            this.textBox2.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6.Location = new System.Drawing.Point(38, 395);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 17);
            this.label6.TabIndex = 12;
            // 
            // button5
            // 
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Location = new System.Drawing.Point(568, 225);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 50);
            this.button5.TabIndex = 11;
            this.button5.Text = "Display";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(38, 36);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(470, 21);
            this.comboBox2.TabIndex = 7;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5.Location = new System.Drawing.Point(524, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 15);
            this.label5.TabIndex = 10;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox5.Location = new System.Drawing.Point(38, 70);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(470, 300);
            this.pictureBox5.TabIndex = 6;
            this.pictureBox5.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(524, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 15);
            this.label4.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(524, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 20);
            this.label3.TabIndex = 8;
            // 
            // welcomPanel
            // 
            this.welcomPanel.BackgroundImage = global::Symulator_PLC.Properties.Resources.gears;
            this.welcomPanel.Controls.Add(this.label16);
            this.welcomPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.welcomPanel.Location = new System.Drawing.Point(146, 0);
            this.welcomPanel.Name = "welcomPanel";
            this.welcomPanel.Size = new System.Drawing.Size(726, 733);
            this.welcomPanel.TabIndex = 15;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label16.ForeColor = System.Drawing.SystemColors.MenuText;
            this.label16.Location = new System.Drawing.Point(23, 33);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(616, 160);
            this.label16.TabIndex = 14;
            this.label16.Text = resources.GetString("label16.Text");
            // 
            // devicesPanel
            // 
            this.devicesPanel.BackgroundImage = global::Symulator_PLC.Properties.Resources.gears;
            this.devicesPanel.Controls.Add(this.panelPLCInfo);
            this.devicesPanel.Controls.Add(this.button1);
            this.devicesPanel.Controls.Add(this.pictureBox4);
            this.devicesPanel.Location = new System.Drawing.Point(146, 0);
            this.devicesPanel.Name = "devicesPanel";
            this.devicesPanel.Padding = new System.Windows.Forms.Padding(77, 0, 0, 0);
            this.devicesPanel.Size = new System.Drawing.Size(726, 733);
            this.devicesPanel.TabIndex = 1;
            this.devicesPanel.Visible = false;
            // 
            // panelPLCInfo
            // 
            this.panelPLCInfo.Controls.Add(this.labelFirmwareVersion);
            this.panelPLCInfo.Controls.Add(this.label13);
            this.panelPLCInfo.Controls.Add(this.labelOrderNumber);
            this.panelPLCInfo.Controls.Add(this.label10);
            this.panelPLCInfo.Controls.Add(this.labelShortDesignation);
            this.panelPLCInfo.Controls.Add(this.label7);
            this.panelPLCInfo.Location = new System.Drawing.Point(522, 32);
            this.panelPLCInfo.Name = "panelPLCInfo";
            this.panelPLCInfo.Size = new System.Drawing.Size(200, 104);
            this.panelPLCInfo.TabIndex = 5;
            this.panelPLCInfo.Visible = false;
            // 
            // labelFirmwareVersion
            // 
            this.labelFirmwareVersion.AutoSize = true;
            this.labelFirmwareVersion.Location = new System.Drawing.Point(5, 84);
            this.labelFirmwareVersion.Name = "labelFirmwareVersion";
            this.labelFirmwareVersion.Size = new System.Drawing.Size(0, 13);
            this.labelFirmwareVersion.TabIndex = 5;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.Location = new System.Drawing.Point(5, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "Firmware Version:";
            // 
            // labelOrderNumber
            // 
            this.labelOrderNumber.AutoSize = true;
            this.labelOrderNumber.Location = new System.Drawing.Point(5, 51);
            this.labelOrderNumber.Name = "labelOrderNumber";
            this.labelOrderNumber.Size = new System.Drawing.Size(0, 13);
            this.labelOrderNumber.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.Location = new System.Drawing.Point(5, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Order number:";
            // 
            // labelShortDesignation
            // 
            this.labelShortDesignation.AutoSize = true;
            this.labelShortDesignation.Location = new System.Drawing.Point(5, 18);
            this.labelShortDesignation.Name = "labelShortDesignation";
            this.labelShortDesignation.Size = new System.Drawing.Size(0, 13);
            this.labelShortDesignation.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label7.Location = new System.Drawing.Point(5, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Short designation:";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(657, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(65, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "PLC Info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.button1.MouseHover += new System.EventHandler(this.button1_MouseHover);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(273, 106);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(58, 43);
            this.pictureBox4.TabIndex = 3;
            this.pictureBox4.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
            this.panel3.Controls.Add(this.specialButton3);
            this.panel3.Controls.Add(this.specialButton2);
            this.panel3.Controls.Add(this.specialButton1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(145, 733);
            this.panel3.TabIndex = 0;
            // 
            // specialButton3
            // 
            this.specialButton3.BackgroundImage = global::Symulator_PLC.Properties.Resources.VirtualSystem1;
            this.specialButton3.HoverColor = System.Drawing.SystemColors.Highlight;
            this.specialButton3.Location = new System.Drawing.Point(3, 258);
            this.specialButton3.Name = "specialButton3";
            this.specialButton3.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(116)))), ((int)(((byte)(20)))));
            this.specialButton3.Size = new System.Drawing.Size(139, 109);
            this.specialButton3.TabIndex = 7;
            this.specialButton3.Text = "specialButton3";
            this.specialButton3.Click += new System.EventHandler(this.specialButton3_Click);
            // 
            // specialButton2
            // 
            this.specialButton2.BackgroundImage = global::Symulator_PLC.Properties.Resources.Network4;
            this.specialButton2.HoverColor = System.Drawing.SystemColors.Highlight;
            this.specialButton2.Location = new System.Drawing.Point(3, 147);
            this.specialButton2.Name = "specialButton2";
            this.specialButton2.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(116)))), ((int)(((byte)(20)))));
            this.specialButton2.Size = new System.Drawing.Size(139, 109);
            this.specialButton2.TabIndex = 6;
            this.specialButton2.Text = "specialButton2";
            this.specialButton2.Click += new System.EventHandler(this.specialButton2_Click);
            // 
            // specialButton1
            // 
            this.specialButton1.BackgroundImage = global::Symulator_PLC.Properties.Resources.Devices4;
            this.specialButton1.HoverColor = System.Drawing.SystemColors.Highlight;
            this.specialButton1.Location = new System.Drawing.Point(3, 36);
            this.specialButton1.Name = "specialButton1";
            this.specialButton1.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(116)))), ((int)(((byte)(20)))));
            this.specialButton1.Size = new System.Drawing.Size(139, 109);
            this.specialButton1.TabIndex = 5;
            this.specialButton1.Text = "zdol";
            this.specialButton1.Click += new System.EventHandler(this.specialButton1_Click);
            // 
            // networkPanel
            // 
            this.networkPanel.BackgroundImage = global::Symulator_PLC.Properties.Resources.gears;
            this.networkPanel.Controls.Add(this.label2);
            this.networkPanel.Controls.Add(this.groupBox1);
            this.networkPanel.Controls.Add(this.label1);
            this.networkPanel.Location = new System.Drawing.Point(146, 0);
            this.networkPanel.Name = "networkPanel";
            this.networkPanel.Size = new System.Drawing.Size(726, 733);
            this.networkPanel.TabIndex = 10;
            this.networkPanel.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(17, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "No network interfaces found";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.groupBox1.Location = new System.Drawing.Point(14, 66);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(684, 52);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose your network card";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(572, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 19);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(560, 23);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(11, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "TCP/IP settings:";
            // 
            // devicePanelChooseModule
            // 
            this.devicePanelChooseModule.BackgroundImage = global::Symulator_PLC.Properties.Resources.gears;
            this.devicePanelChooseModule.Controls.Add(this.button6);
            this.devicePanelChooseModule.Controls.Add(this.label11);
            this.devicePanelChooseModule.Controls.Add(this.treeView1);
            this.devicePanelChooseModule.Location = new System.Drawing.Point(146, 0);
            this.devicePanelChooseModule.Name = "devicePanelChooseModule";
            this.devicePanelChooseModule.Size = new System.Drawing.Size(726, 733);
            this.devicePanelChooseModule.TabIndex = 10;
            this.devicePanelChooseModule.Visible = false;
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Location = new System.Drawing.Point(640, 699);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 11;
            this.button6.Text = "Load";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label11.Location = new System.Drawing.Point(35, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(119, 17);
            this.label11.TabIndex = 10;
            this.label11.Text = "Choose device:";
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.treeView1.Location = new System.Drawing.Point(38, 70);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(209, 182);
            this.treeView1.TabIndex = 9;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BackgroundImage = global::Symulator_PLC.Properties.Resources.titleBar1;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(871, 27);
            this.panel1.TabIndex = 10;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::Symulator_PLC.Properties.Resources.minimize;
            this.pictureBox2.Location = new System.Drawing.Point(802, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 26);
            this.pictureBox2.TabIndex = 12;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            this.pictureBox2.MouseEnter += new System.EventHandler(this.pictureBox2_MouseEnter);
            this.pictureBox2.MouseLeave += new System.EventHandler(this.pictureBox2_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(837, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 26);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(160)))), ((int)(((byte)(160)))));
            this.ClientSize = new System.Drawing.Size(873, 762);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel2.ResumeLayout(false);
            this.virtualSystemPanel.ResumeLayout(false);
            this.virtualSystemPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.welcomPanel.ResumeLayout(false);
            this.welcomPanel.PerformLayout();
            this.devicesPanel.ResumeLayout(false);
            this.panelPLCInfo.ResumeLayout(false);
            this.panelPLCInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel3.ResumeLayout(false);
            this.networkPanel.ResumeLayout(false);
            this.networkPanel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.devicePanelChooseModule.ResumeLayout(false);
            this.devicePanelChooseModule.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomPanel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private CustomPanel panel3;
        private CustomPanel panel2;
        private CustomPanel devicesPanel;
        private CustomPanel networkPanel;
        private CustomGroupBox groupBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;        
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox pictureBox4;
        private CustomButton.SpecialButton specialButton1;
        private CustomButton.SpecialButton specialButton3;
        private CustomButton.SpecialButton specialButton2;
        private CustomPanel virtualSystemPanel;
        private CustomPanel devicePanelChooseModule;
        private System.Windows.Forms.Label label11;
        private CustomTreeView treeView1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox2;
        private CustomPanel welcomPanel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelPLCInfo;
        private System.Windows.Forms.Label labelFirmwareVersion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelOrderNumber;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelShortDesignation;
        private System.Windows.Forms.Label label7;
        
    }
}

