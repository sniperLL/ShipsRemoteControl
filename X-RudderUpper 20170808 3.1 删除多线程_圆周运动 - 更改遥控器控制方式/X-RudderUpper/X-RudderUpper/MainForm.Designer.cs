namespace X_RudderUpper
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_Tail_s = new System.Windows.Forms.TextBox();
            this.txt_MotorSpeed = new System.Windows.Forms.TextBox();
            this.txt_Tail_r = new System.Windows.Forms.TextBox();
            this.btn_CtlDataSend = new System.Windows.Forms.Button();
            this.txt_HorizRudder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnWorkRightDown = new System.Windows.Forms.Button();
            this.btnStopRightDown = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnWorkLeftDown = new System.Windows.Forms.Button();
            this.btnStopLeftDown = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnWorkRightUp = new System.Windows.Forms.Button();
            this.btnStopRightUp = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnWorkLeftUp = new System.Windows.Forms.Button();
            this.btnStopLeftUp = new System.Windows.Forms.Button();
            this.btn_CtrlDepthSend = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCtrlDepth = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnChangePID = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.txtCtrlKd = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.txtCtrlKi = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.txtCtrlKp = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsPortName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsBaudRate = new System.Windows.Forms.ToolStripStatusLabel();
            this.XRudderSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.btn_SerialPortSwitch = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.combo_Baudrate = new System.Windows.Forms.ComboBox();
            this.combo_PortName = new System.Windows.Forms.ComboBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.pbxLegend = new System.Windows.Forms.PictureBox();
            this.pbxCurve = new System.Windows.Forms.PictureBox();
            this.btnWaterIN = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.btn_DrawSwitch = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.ZAccTextBox = new System.Windows.Forms.TextBox();
            this.YAccTextBox = new System.Windows.Forms.TextBox();
            this.XAccTextBox = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_Depth_theory = new System.Windows.Forms.TextBox();
            this.txt_tailRightDown_theory = new System.Windows.Forms.TextBox();
            this.txt_tailLeftDown_theory = new System.Windows.Forms.TextBox();
            this.txt_tailRightUp_theory = new System.Windows.Forms.TextBox();
            this.txt_tailLeftUp_theory = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.txt_Depth_real = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_tailRightDown_real = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_tailLeftDown_real = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_tailRightUp_real = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_tailLeftUp_real = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.txt_morotSpeed_theory = new System.Windows.Forms.TextBox();
            this.txt_horizRight_theory = new System.Windows.Forms.TextBox();
            this.txt_horizLeft_theory = new System.Windows.Forms.TextBox();
            this.txt_morotSpeed_real = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_horizRight_real = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_horizLeft_real = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.axUnityWebPlayer1 = new AxUnityWebPlayerAXLib.AxUnityWebPlayer();
            this.btnDataSave = new System.Windows.Forms.Button();
            this.dataSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btn_dataSave = new System.Windows.Forms.Button();
            this.cbxTailLeftUpRudder = new System.Windows.Forms.CheckBox();
            this.cbxTailRightUpRudder = new System.Windows.Forms.CheckBox();
            this.cbxTailLeftDownRudder = new System.Windows.Forms.CheckBox();
            this.cbxTailRightDownRudder = new System.Windows.Forms.CheckBox();
            this.cbxLeftHydroplane = new System.Windows.Forms.CheckBox();
            this.cbxRightHydroplane = new System.Windows.Forms.CheckBox();
            this.cbxMotorSpeed = new System.Windows.Forms.CheckBox();
            this.cbxDepth = new System.Windows.Forms.CheckBox();
            this.btn_straight = new System.Windows.Forms.Button();
            this.btn_control = new System.Windows.Forms.Button();
            this.RemoteSerialPort = new System.IO.Ports.SerialPort(this.components);
            this.RemoteBtn = new System.Windows.Forms.Button();
            this.btn_dive = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLegend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCurve)).BeginInit();
            this.groupBox10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axUnityWebPlayer1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_Tail_s);
            this.groupBox1.Controls.Add(this.txt_MotorSpeed);
            this.groupBox1.Controls.Add(this.txt_Tail_r);
            this.groupBox1.Controls.Add(this.btn_CtlDataSend);
            this.groupBox1.Controls.Add(this.txt_HorizRudder);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 71);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 90);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "舵机、电机控制";
            // 
            // txt_Tail_s
            // 
            this.txt_Tail_s.Location = new System.Drawing.Point(221, 53);
            this.txt_Tail_s.Name = "txt_Tail_s";
            this.txt_Tail_s.Size = new System.Drawing.Size(56, 21);
            this.txt_Tail_s.TabIndex = 4;
            this.txt_Tail_s.Text = "0";
            this.txt_Tail_s.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_MotorSpeed
            // 
            this.txt_MotorSpeed.Location = new System.Drawing.Point(221, 25);
            this.txt_MotorSpeed.Name = "txt_MotorSpeed";
            this.txt_MotorSpeed.Size = new System.Drawing.Size(56, 21);
            this.txt_MotorSpeed.TabIndex = 2;
            this.txt_MotorSpeed.Text = "0";
            this.txt_MotorSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_Tail_r
            // 
            this.txt_Tail_r.Location = new System.Drawing.Point(85, 51);
            this.txt_Tail_r.Name = "txt_Tail_r";
            this.txt_Tail_r.Size = new System.Drawing.Size(56, 21);
            this.txt_Tail_r.TabIndex = 3;
            this.txt_Tail_r.Text = "0";
            this.txt_Tail_r.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_CtlDataSend
            // 
            this.btn_CtlDataSend.Location = new System.Drawing.Point(302, 51);
            this.btn_CtlDataSend.Name = "btn_CtlDataSend";
            this.btn_CtlDataSend.Size = new System.Drawing.Size(75, 23);
            this.btn_CtlDataSend.TabIndex = 7;
            this.btn_CtlDataSend.Text = "数据发送";
            this.btn_CtlDataSend.UseVisualStyleBackColor = true;
            this.btn_CtlDataSend.Click += new System.EventHandler(this.btn_CtlDataSend_Click);
            // 
            // txt_HorizRudder
            // 
            this.txt_HorizRudder.Location = new System.Drawing.Point(85, 24);
            this.txt_HorizRudder.Name = "txt_HorizRudder";
            this.txt_HorizRudder.Size = new System.Drawing.Size(56, 21);
            this.txt_HorizRudder.TabIndex = 1;
            this.txt_HorizRudder.Text = "0";
            this.txt_HorizRudder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "首    舵：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "尾方向舵：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(158, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "电机转速：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(158, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "尾升降舵：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.groupBox6);
            this.groupBox5.Controls.Add(this.groupBox7);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Controls.Add(this.groupBox2);
            this.groupBox5.Location = new System.Drawing.Point(425, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(500, 92);
            this.groupBox5.TabIndex = 11;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "X舵失效控制模拟";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnWorkRightDown);
            this.groupBox6.Controls.Add(this.btnStopRightDown);
            this.groupBox6.Location = new System.Drawing.Point(372, 20);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(117, 54);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "右下舵";
            // 
            // btnWorkRightDown
            // 
            this.btnWorkRightDown.Enabled = false;
            this.btnWorkRightDown.Location = new System.Drawing.Point(9, 20);
            this.btnWorkRightDown.Name = "btnWorkRightDown";
            this.btnWorkRightDown.Size = new System.Drawing.Size(48, 23);
            this.btnWorkRightDown.TabIndex = 1;
            this.btnWorkRightDown.Text = "正常";
            this.btnWorkRightDown.UseVisualStyleBackColor = true;
            this.btnWorkRightDown.Click += new System.EventHandler(this.btnWorkRightDown_Click);
            // 
            // btnStopRightDown
            // 
            this.btnStopRightDown.Location = new System.Drawing.Point(63, 20);
            this.btnStopRightDown.Name = "btnStopRightDown";
            this.btnStopRightDown.Size = new System.Drawing.Size(48, 23);
            this.btnStopRightDown.TabIndex = 0;
            this.btnStopRightDown.Text = "卡舵";
            this.btnStopRightDown.UseVisualStyleBackColor = true;
            this.btnStopRightDown.Click += new System.EventHandler(this.btnStopRightDown_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnWorkLeftDown);
            this.groupBox7.Controls.Add(this.btnStopLeftDown);
            this.groupBox7.Location = new System.Drawing.Point(251, 21);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(117, 54);
            this.groupBox7.TabIndex = 2;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "左下舵";
            // 
            // btnWorkLeftDown
            // 
            this.btnWorkLeftDown.Enabled = false;
            this.btnWorkLeftDown.Location = new System.Drawing.Point(9, 20);
            this.btnWorkLeftDown.Name = "btnWorkLeftDown";
            this.btnWorkLeftDown.Size = new System.Drawing.Size(48, 23);
            this.btnWorkLeftDown.TabIndex = 1;
            this.btnWorkLeftDown.Text = "正常";
            this.btnWorkLeftDown.UseVisualStyleBackColor = true;
            this.btnWorkLeftDown.Click += new System.EventHandler(this.btnWorkLeftDown_Click);
            // 
            // btnStopLeftDown
            // 
            this.btnStopLeftDown.Location = new System.Drawing.Point(63, 20);
            this.btnStopLeftDown.Name = "btnStopLeftDown";
            this.btnStopLeftDown.Size = new System.Drawing.Size(48, 23);
            this.btnStopLeftDown.TabIndex = 0;
            this.btnStopLeftDown.Text = "卡舵";
            this.btnStopLeftDown.UseVisualStyleBackColor = true;
            this.btnStopLeftDown.Click += new System.EventHandler(this.btnStopLeftDown_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnWorkRightUp);
            this.groupBox3.Controls.Add(this.btnStopRightUp);
            this.groupBox3.Location = new System.Drawing.Point(130, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(117, 54);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "右上舵";
            // 
            // btnWorkRightUp
            // 
            this.btnWorkRightUp.Enabled = false;
            this.btnWorkRightUp.Location = new System.Drawing.Point(9, 20);
            this.btnWorkRightUp.Name = "btnWorkRightUp";
            this.btnWorkRightUp.Size = new System.Drawing.Size(48, 23);
            this.btnWorkRightUp.TabIndex = 1;
            this.btnWorkRightUp.Text = "正常";
            this.btnWorkRightUp.UseVisualStyleBackColor = true;
            this.btnWorkRightUp.Click += new System.EventHandler(this.btnWorkRightUp_Click);
            // 
            // btnStopRightUp
            // 
            this.btnStopRightUp.Location = new System.Drawing.Point(63, 20);
            this.btnStopRightUp.Name = "btnStopRightUp";
            this.btnStopRightUp.Size = new System.Drawing.Size(48, 23);
            this.btnStopRightUp.TabIndex = 0;
            this.btnStopRightUp.Text = "卡舵";
            this.btnStopRightUp.UseVisualStyleBackColor = true;
            this.btnStopRightUp.Click += new System.EventHandler(this.btnStopRightUp_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnWorkLeftUp);
            this.groupBox2.Controls.Add(this.btnStopLeftUp);
            this.groupBox2.Location = new System.Drawing.Point(9, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(117, 54);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "左上舵";
            // 
            // btnWorkLeftUp
            // 
            this.btnWorkLeftUp.Enabled = false;
            this.btnWorkLeftUp.Location = new System.Drawing.Point(8, 20);
            this.btnWorkLeftUp.Name = "btnWorkLeftUp";
            this.btnWorkLeftUp.Size = new System.Drawing.Size(48, 23);
            this.btnWorkLeftUp.TabIndex = 1;
            this.btnWorkLeftUp.Text = "正常";
            this.btnWorkLeftUp.UseVisualStyleBackColor = true;
            this.btnWorkLeftUp.Click += new System.EventHandler(this.btnWorkLeftUp_Click);
            // 
            // btnStopLeftUp
            // 
            this.btnStopLeftUp.Location = new System.Drawing.Point(62, 20);
            this.btnStopLeftUp.Name = "btnStopLeftUp";
            this.btnStopLeftUp.Size = new System.Drawing.Size(48, 23);
            this.btnStopLeftUp.TabIndex = 0;
            this.btnStopLeftUp.Text = "卡舵";
            this.btnStopLeftUp.UseVisualStyleBackColor = true;
            this.btnStopLeftUp.Click += new System.EventHandler(this.btnStopLeftUp_Click);
            // 
            // btn_CtrlDepthSend
            // 
            this.btn_CtrlDepthSend.Location = new System.Drawing.Point(302, 19);
            this.btn_CtrlDepthSend.Name = "btn_CtrlDepthSend";
            this.btn_CtrlDepthSend.Size = new System.Drawing.Size(75, 23);
            this.btn_CtrlDepthSend.TabIndex = 1;
            this.btn_CtrlDepthSend.Text = "确定";
            this.btn_CtrlDepthSend.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(191, 24);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 2;
            this.label9.Text = "m";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(60, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 1;
            this.label10.Text = "潜艇深度：";
            // 
            // txtCtrlDepth
            // 
            this.txtCtrlDepth.Location = new System.Drawing.Point(131, 19);
            this.txtCtrlDepth.Name = "txtCtrlDepth";
            this.txtCtrlDepth.Size = new System.Drawing.Size(57, 21);
            this.txtCtrlDepth.TabIndex = 0;
            this.txtCtrlDepth.Text = "0";
            this.txtCtrlDepth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnChangePID);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.txtCtrlKd);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.txtCtrlKi);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.txtCtrlKp);
            this.groupBox4.Location = new System.Drawing.Point(12, 225);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(393, 54);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "PID参数修改";
            // 
            // btnChangePID
            // 
            this.btnChangePID.Location = new System.Drawing.Point(302, 18);
            this.btnChangePID.Name = "btnChangePID";
            this.btnChangePID.Size = new System.Drawing.Size(75, 23);
            this.btnChangePID.TabIndex = 3;
            this.btnChangePID.Text = "修改";
            this.btnChangePID.UseVisualStyleBackColor = true;
            this.btnChangePID.Click += new System.EventHandler(this.btnChangePID_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(193, 23);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(23, 12);
            this.label21.TabIndex = 9;
            this.label21.Text = "D：";
            // 
            // txtCtrlKd
            // 
            this.txtCtrlKd.Location = new System.Drawing.Point(214, 20);
            this.txtCtrlKd.Name = "txtCtrlKd";
            this.txtCtrlKd.Size = new System.Drawing.Size(57, 21);
            this.txtCtrlKd.TabIndex = 2;
            this.txtCtrlKd.Text = "0";
            this.txtCtrlKd.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(108, 23);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(23, 12);
            this.label20.TabIndex = 7;
            this.label20.Text = "I：";
            // 
            // txtCtrlKi
            // 
            this.txtCtrlKi.Location = new System.Drawing.Point(129, 20);
            this.txtCtrlKi.Name = "txtCtrlKi";
            this.txtCtrlKi.Size = new System.Drawing.Size(57, 21);
            this.txtCtrlKi.TabIndex = 1;
            this.txtCtrlKi.Text = "0";
            this.txtCtrlKi.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(24, 23);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(23, 12);
            this.label19.TabIndex = 5;
            this.label19.Text = "P：";
            // 
            // txtCtrlKp
            // 
            this.txtCtrlKp.Location = new System.Drawing.Point(44, 20);
            this.txtCtrlKp.Name = "txtCtrlKp";
            this.txtCtrlKp.Size = new System.Drawing.Size(57, 21);
            this.txtCtrlKp.TabIndex = 0;
            this.txtCtrlKp.Text = "0";
            this.txtCtrlKp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsPortName,
            this.tsBaudRate});
            this.statusStrip1.Location = new System.Drawing.Point(0, 661);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1051, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsPortName
            // 
            this.tsPortName.Name = "tsPortName";
            this.tsPortName.Size = new System.Drawing.Size(99, 17);
            this.tsPortName.Text = "串口号：未指定 |";
            // 
            // tsBaudRate
            // 
            this.tsBaudRate.Name = "tsBaudRate";
            this.tsBaudRate.Size = new System.Drawing.Size(92, 17);
            this.tsBaudRate.Text = "波特率：未指定";
            // 
            // XRudderSerialPort
            // 
            this.XRudderSerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.XRudderSerialPort_DataReceived);
            // 
            // btn_SerialPortSwitch
            // 
            this.btn_SerialPortSwitch.Location = new System.Drawing.Point(302, 18);
            this.btn_SerialPortSwitch.Name = "btn_SerialPortSwitch";
            this.btn_SerialPortSwitch.Size = new System.Drawing.Size(75, 23);
            this.btn_SerialPortSwitch.TabIndex = 15;
            this.btn_SerialPortSwitch.Text = "打开串口";
            this.btn_SerialPortSwitch.UseVisualStyleBackColor = true;
            this.btn_SerialPortSwitch.Click += new System.EventHandler(this.btn_SerialPortSwitch_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(158, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "波特率：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(20, 24);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 13;
            this.label11.Text = "串口号：";
            // 
            // combo_Baudrate
            // 
            this.combo_Baudrate.Cursor = System.Windows.Forms.Cursors.No;
            this.combo_Baudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_Baudrate.FormattingEnabled = true;
            this.combo_Baudrate.Items.AddRange(new object[] {
            "115200",
            "57600",
            "9600"});
            this.combo_Baudrate.Location = new System.Drawing.Point(217, 21);
            this.combo_Baudrate.Name = "combo_Baudrate";
            this.combo_Baudrate.Size = new System.Drawing.Size(63, 20);
            this.combo_Baudrate.TabIndex = 10;
            // 
            // combo_PortName
            // 
            this.combo_PortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combo_PortName.FormattingEnabled = true;
            this.combo_PortName.Location = new System.Drawing.Point(79, 21);
            this.combo_PortName.Name = "combo_PortName";
            this.combo_PortName.Size = new System.Drawing.Size(62, 20);
            this.combo_PortName.TabIndex = 11;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label11);
            this.groupBox8.Controls.Add(this.combo_PortName);
            this.groupBox8.Controls.Add(this.btn_SerialPortSwitch);
            this.groupBox8.Controls.Add(this.label8);
            this.groupBox8.Controls.Add(this.combo_Baudrate);
            this.groupBox8.Location = new System.Drawing.Point(12, 12);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(393, 55);
            this.groupBox8.TabIndex = 0;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "串口设置";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label10);
            this.groupBox9.Controls.Add(this.txtCtrlDepth);
            this.groupBox9.Controls.Add(this.label9);
            this.groupBox9.Controls.Add(this.btn_CtrlDepthSend);
            this.groupBox9.Location = new System.Drawing.Point(12, 166);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(393, 53);
            this.groupBox9.TabIndex = 2;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "定深控制";
            // 
            // pbxLegend
            // 
            this.pbxLegend.Location = new System.Drawing.Point(891, 285);
            this.pbxLegend.Name = "pbxLegend";
            this.pbxLegend.Size = new System.Drawing.Size(145, 324);
            this.pbxLegend.TabIndex = 19;
            this.pbxLegend.TabStop = false;
            // 
            // pbxCurve
            // 
            this.pbxCurve.Location = new System.Drawing.Point(425, 285);
            this.pbxCurve.Name = "pbxCurve";
            this.pbxCurve.Size = new System.Drawing.Size(460, 349);
            this.pbxCurve.TabIndex = 18;
            this.pbxCurve.TabStop = false;
            // 
            // btnWaterIN
            // 
            this.btnWaterIN.BackColor = System.Drawing.Color.Green;
            this.btnWaterIN.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnWaterIN.Location = new System.Drawing.Point(953, 25);
            this.btnWaterIN.Name = "btnWaterIN";
            this.btnWaterIN.Size = new System.Drawing.Size(75, 23);
            this.btnWaterIN.TabIndex = 12;
            this.btnWaterIN.Text = "进水报警";
            this.btnWaterIN.UseVisualStyleBackColor = false;
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(953, 53);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.btn_Reset.TabIndex = 4;
            this.btn_Reset.Text = "重置复位";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // btn_DrawSwitch
            // 
            this.btn_DrawSwitch.Location = new System.Drawing.Point(953, 109);
            this.btn_DrawSwitch.Name = "btn_DrawSwitch";
            this.btn_DrawSwitch.Size = new System.Drawing.Size(75, 23);
            this.btn_DrawSwitch.TabIndex = 6;
            this.btn_DrawSwitch.Text = "开始绘图";
            this.btn_DrawSwitch.UseVisualStyleBackColor = true;
            this.btn_DrawSwitch.Click += new System.EventHandler(this.btn_DrawSwitch_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.ZAccTextBox);
            this.groupBox10.Controls.Add(this.YAccTextBox);
            this.groupBox10.Controls.Add(this.XAccTextBox);
            this.groupBox10.Controls.Add(this.label30);
            this.groupBox10.Controls.Add(this.label29);
            this.groupBox10.Controls.Add(this.label28);
            this.groupBox10.Controls.Add(this.label23);
            this.groupBox10.Controls.Add(this.label18);
            this.groupBox10.Controls.Add(this.label22);
            this.groupBox10.Controls.Add(this.label17);
            this.groupBox10.Controls.Add(this.txt_Depth_theory);
            this.groupBox10.Controls.Add(this.txt_tailRightDown_theory);
            this.groupBox10.Controls.Add(this.txt_tailLeftDown_theory);
            this.groupBox10.Controls.Add(this.txt_tailRightUp_theory);
            this.groupBox10.Controls.Add(this.txt_tailLeftUp_theory);
            this.groupBox10.Controls.Add(this.label27);
            this.groupBox10.Controls.Add(this.txt_Depth_real);
            this.groupBox10.Controls.Add(this.label13);
            this.groupBox10.Controls.Add(this.txt_tailRightDown_real);
            this.groupBox10.Controls.Add(this.label6);
            this.groupBox10.Controls.Add(this.txt_tailLeftDown_real);
            this.groupBox10.Controls.Add(this.label12);
            this.groupBox10.Controls.Add(this.txt_tailRightUp_real);
            this.groupBox10.Controls.Add(this.label5);
            this.groupBox10.Controls.Add(this.txt_tailLeftUp_real);
            this.groupBox10.Controls.Add(this.label4);
            this.groupBox10.Location = new System.Drawing.Point(424, 114);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(501, 165);
            this.groupBox10.TabIndex = 22;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "数据监控";
            // 
            // ZAccTextBox
            // 
            this.ZAccTextBox.Location = new System.Drawing.Point(78, 129);
            this.ZAccTextBox.Name = "ZAccTextBox";
            this.ZAccTextBox.ReadOnly = true;
            this.ZAccTextBox.Size = new System.Drawing.Size(51, 21);
            this.ZAccTextBox.TabIndex = 40;
            // 
            // YAccTextBox
            // 
            this.YAccTextBox.Location = new System.Drawing.Point(78, 104);
            this.YAccTextBox.Name = "YAccTextBox";
            this.YAccTextBox.ReadOnly = true;
            this.YAccTextBox.Size = new System.Drawing.Size(51, 21);
            this.YAccTextBox.TabIndex = 40;
            // 
            // XAccTextBox
            // 
            this.XAccTextBox.Location = new System.Drawing.Point(78, 78);
            this.XAccTextBox.Name = "XAccTextBox";
            this.XAccTextBox.ReadOnly = true;
            this.XAccTextBox.Size = new System.Drawing.Size(51, 21);
            this.XAccTextBox.TabIndex = 40;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(17, 133);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(59, 12);
            this.label30.TabIndex = 39;
            this.label30.Text = "Z轴加速度";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(17, 108);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(59, 12);
            this.label29.TabIndex = 39;
            this.label29.Text = "Y轴加速度";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(17, 82);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(59, 12);
            this.label28.TabIndex = 39;
            this.label28.Text = "X轴加速度";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(373, 120);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 12);
            this.label23.TabIndex = 38;
            this.label23.Text = "理论值：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(372, 51);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(53, 12);
            this.label18.TabIndex = 34;
            this.label18.Text = "理论值：";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(251, 51);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(53, 12);
            this.label22.TabIndex = 33;
            this.label22.Text = "理论值：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(138, 51);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(53, 12);
            this.label17.TabIndex = 32;
            this.label17.Text = "理论值：";
            // 
            // txt_Depth_theory
            // 
            this.txt_Depth_theory.Location = new System.Drawing.Point(424, 117);
            this.txt_Depth_theory.Name = "txt_Depth_theory";
            this.txt_Depth_theory.ReadOnly = true;
            this.txt_Depth_theory.Size = new System.Drawing.Size(58, 21);
            this.txt_Depth_theory.TabIndex = 31;
            this.txt_Depth_theory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tailRightDown_theory
            // 
            this.txt_tailRightDown_theory.Location = new System.Drawing.Point(424, 44);
            this.txt_tailRightDown_theory.Name = "txt_tailRightDown_theory";
            this.txt_tailRightDown_theory.ReadOnly = true;
            this.txt_tailRightDown_theory.Size = new System.Drawing.Size(58, 21);
            this.txt_tailRightDown_theory.TabIndex = 23;
            this.txt_tailRightDown_theory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tailLeftDown_theory
            // 
            this.txt_tailLeftDown_theory.Location = new System.Drawing.Point(305, 44);
            this.txt_tailLeftDown_theory.Name = "txt_tailLeftDown_theory";
            this.txt_tailLeftDown_theory.ReadOnly = true;
            this.txt_tailLeftDown_theory.Size = new System.Drawing.Size(58, 21);
            this.txt_tailLeftDown_theory.TabIndex = 21;
            this.txt_tailLeftDown_theory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tailRightUp_theory
            // 
            this.txt_tailRightUp_theory.Location = new System.Drawing.Point(190, 44);
            this.txt_tailRightUp_theory.Name = "txt_tailRightUp_theory";
            this.txt_tailRightUp_theory.ReadOnly = true;
            this.txt_tailRightUp_theory.Size = new System.Drawing.Size(58, 21);
            this.txt_tailRightUp_theory.TabIndex = 19;
            this.txt_tailRightUp_theory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_tailLeftUp_theory
            // 
            this.txt_tailLeftUp_theory.Location = new System.Drawing.Point(71, 44);
            this.txt_tailLeftUp_theory.Name = "txt_tailLeftUp_theory";
            this.txt_tailLeftUp_theory.ReadOnly = true;
            this.txt_tailLeftUp_theory.Size = new System.Drawing.Size(58, 21);
            this.txt_tailLeftUp_theory.TabIndex = 17;
            this.txt_tailLeftUp_theory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(17, 51);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(53, 12);
            this.label27.TabIndex = 16;
            this.label27.Text = "理论值：";
            // 
            // txt_Depth_real
            // 
            this.txt_Depth_real.Location = new System.Drawing.Point(424, 90);
            this.txt_Depth_real.Name = "txt_Depth_real";
            this.txt_Depth_real.ReadOnly = true;
            this.txt_Depth_real.Size = new System.Drawing.Size(58, 21);
            this.txt_Depth_real.TabIndex = 15;
            this.txt_Depth_real.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(367, 95);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 14;
            this.label13.Text = "潜艇深度：";
            // 
            // txt_tailRightDown_real
            // 
            this.txt_tailRightDown_real.Location = new System.Drawing.Point(424, 18);
            this.txt_tailRightDown_real.Name = "txt_tailRightDown_real";
            this.txt_tailRightDown_real.ReadOnly = true;
            this.txt_tailRightDown_real.Size = new System.Drawing.Size(58, 21);
            this.txt_tailRightDown_real.TabIndex = 7;
            this.txt_tailRightDown_real.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(372, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "右下舵：";
            // 
            // txt_tailLeftDown_real
            // 
            this.txt_tailLeftDown_real.Location = new System.Drawing.Point(305, 18);
            this.txt_tailLeftDown_real.Name = "txt_tailLeftDown_real";
            this.txt_tailLeftDown_real.ReadOnly = true;
            this.txt_tailLeftDown_real.Size = new System.Drawing.Size(58, 21);
            this.txt_tailLeftDown_real.TabIndex = 5;
            this.txt_tailLeftDown_real.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(253, 25);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 4;
            this.label12.Text = "左下舵：";
            // 
            // txt_tailRightUp_real
            // 
            this.txt_tailRightUp_real.Location = new System.Drawing.Point(190, 18);
            this.txt_tailRightUp_real.Name = "txt_tailRightUp_real";
            this.txt_tailRightUp_real.ReadOnly = true;
            this.txt_tailRightUp_real.Size = new System.Drawing.Size(58, 21);
            this.txt_tailRightUp_real.TabIndex = 3;
            this.txt_tailRightUp_real.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(136, 25);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "右上舵：";
            // 
            // txt_tailLeftUp_real
            // 
            this.txt_tailLeftUp_real.Location = new System.Drawing.Point(71, 18);
            this.txt_tailLeftUp_real.Name = "txt_tailLeftUp_real";
            this.txt_tailLeftUp_real.ReadOnly = true;
            this.txt_tailLeftUp_real.Size = new System.Drawing.Size(58, 21);
            this.txt_tailLeftUp_real.TabIndex = 1;
            this.txt_tailLeftUp_real.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "左上舵：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(1285, 439);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 37;
            this.label24.Text = "理论值：";
            this.label24.Visible = false;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(1167, 439);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(53, 12);
            this.label25.TabIndex = 36;
            this.label25.Text = "理论值：";
            this.label25.Visible = false;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(1045, 439);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 12);
            this.label26.TabIndex = 35;
            this.label26.Text = "理论值：";
            this.label26.Visible = false;
            // 
            // txt_morotSpeed_theory
            // 
            this.txt_morotSpeed_theory.Location = new System.Drawing.Point(1360, 336);
            this.txt_morotSpeed_theory.Name = "txt_morotSpeed_theory";
            this.txt_morotSpeed_theory.ReadOnly = true;
            this.txt_morotSpeed_theory.Size = new System.Drawing.Size(58, 21);
            this.txt_morotSpeed_theory.TabIndex = 29;
            this.txt_morotSpeed_theory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_horizRight_theory
            // 
            this.txt_horizRight_theory.Location = new System.Drawing.Point(1218, 436);
            this.txt_horizRight_theory.Name = "txt_horizRight_theory";
            this.txt_horizRight_theory.ReadOnly = true;
            this.txt_horizRight_theory.Size = new System.Drawing.Size(58, 21);
            this.txt_horizRight_theory.TabIndex = 27;
            this.txt_horizRight_theory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_horizRight_theory.Visible = false;
            // 
            // txt_horizLeft_theory
            // 
            this.txt_horizLeft_theory.Location = new System.Drawing.Point(1099, 436);
            this.txt_horizLeft_theory.Name = "txt_horizLeft_theory";
            this.txt_horizLeft_theory.ReadOnly = true;
            this.txt_horizLeft_theory.Size = new System.Drawing.Size(58, 21);
            this.txt_horizLeft_theory.TabIndex = 25;
            this.txt_horizLeft_theory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_horizLeft_theory.Visible = false;
            // 
            // txt_morotSpeed_real
            // 
            this.txt_morotSpeed_real.Location = new System.Drawing.Point(1360, 309);
            this.txt_morotSpeed_real.Name = "txt_morotSpeed_real";
            this.txt_morotSpeed_real.ReadOnly = true;
            this.txt_morotSpeed_real.Size = new System.Drawing.Size(58, 21);
            this.txt_morotSpeed_real.TabIndex = 13;
            this.txt_morotSpeed_real.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1278, 414);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 12;
            this.label14.Text = "电机转速：";
            this.label14.Visible = false;
            // 
            // txt_horizRight_real
            // 
            this.txt_horizRight_real.Location = new System.Drawing.Point(1218, 409);
            this.txt_horizRight_real.Name = "txt_horizRight_real";
            this.txt_horizRight_real.ReadOnly = true;
            this.txt_horizRight_real.Size = new System.Drawing.Size(58, 21);
            this.txt_horizRight_real.TabIndex = 11;
            this.txt_horizRight_real.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_horizRight_real.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1161, 414);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 10;
            this.label15.Text = "右水平舵：";
            this.label15.Visible = false;
            // 
            // txt_horizLeft_real
            // 
            this.txt_horizLeft_real.Location = new System.Drawing.Point(1099, 409);
            this.txt_horizLeft_real.Name = "txt_horizLeft_real";
            this.txt_horizLeft_real.ReadOnly = true;
            this.txt_horizLeft_real.Size = new System.Drawing.Size(58, 21);
            this.txt_horizLeft_real.TabIndex = 9;
            this.txt_horizLeft_real.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_horizLeft_real.Visible = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1042, 414);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 8;
            this.label16.Text = "左水平舵：";
            this.label16.Visible = false;
            // 
            // axUnityWebPlayer1
            // 
            this.axUnityWebPlayer1.Enabled = true;
            this.axUnityWebPlayer1.Location = new System.Drawing.Point(12, 285);
            this.axUnityWebPlayer1.Name = "axUnityWebPlayer1";
            this.axUnityWebPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axUnityWebPlayer1.OcxState")));
            this.axUnityWebPlayer1.Size = new System.Drawing.Size(393, 349);
            this.axUnityWebPlayer1.TabIndex = 20;
            // 
            // btnDataSave
            // 
            this.btnDataSave.Location = new System.Drawing.Point(1060, 369);
            this.btnDataSave.Name = "btnDataSave";
            this.btnDataSave.Size = new System.Drawing.Size(97, 23);
            this.btnDataSave.TabIndex = 23;
            this.btnDataSave.Text = "低效保存数据";
            this.btnDataSave.UseVisualStyleBackColor = true;
            this.btnDataSave.Visible = false;
            this.btnDataSave.Click += new System.EventHandler(this.btnDataSave_Click);
            // 
            // btn_dataSave
            // 
            this.btn_dataSave.Location = new System.Drawing.Point(953, 81);
            this.btn_dataSave.Name = "btn_dataSave";
            this.btn_dataSave.Size = new System.Drawing.Size(75, 23);
            this.btn_dataSave.TabIndex = 5;
            this.btn_dataSave.Text = "保存数据";
            this.btn_dataSave.UseVisualStyleBackColor = true;
            this.btn_dataSave.Click += new System.EventHandler(this.btn_dataSave_Click);
            // 
            // cbxTailLeftUpRudder
            // 
            this.cbxTailLeftUpRudder.AutoSize = true;
            this.cbxTailLeftUpRudder.Location = new System.Drawing.Point(425, 640);
            this.cbxTailLeftUpRudder.Name = "cbxTailLeftUpRudder";
            this.cbxTailLeftUpRudder.Size = new System.Drawing.Size(84, 16);
            this.cbxTailLeftUpRudder.TabIndex = 25;
            this.cbxTailLeftUpRudder.Text = "左上舵曲线";
            this.cbxTailLeftUpRudder.UseVisualStyleBackColor = true;
            // 
            // cbxTailRightUpRudder
            // 
            this.cbxTailRightUpRudder.AutoSize = true;
            this.cbxTailRightUpRudder.Location = new System.Drawing.Point(511, 640);
            this.cbxTailRightUpRudder.Name = "cbxTailRightUpRudder";
            this.cbxTailRightUpRudder.Size = new System.Drawing.Size(84, 16);
            this.cbxTailRightUpRudder.TabIndex = 25;
            this.cbxTailRightUpRudder.Text = "右上舵曲线";
            this.cbxTailRightUpRudder.UseVisualStyleBackColor = true;
            // 
            // cbxTailLeftDownRudder
            // 
            this.cbxTailLeftDownRudder.AutoSize = true;
            this.cbxTailLeftDownRudder.Location = new System.Drawing.Point(599, 640);
            this.cbxTailLeftDownRudder.Name = "cbxTailLeftDownRudder";
            this.cbxTailLeftDownRudder.Size = new System.Drawing.Size(84, 16);
            this.cbxTailLeftDownRudder.TabIndex = 25;
            this.cbxTailLeftDownRudder.Text = "左下舵曲线";
            this.cbxTailLeftDownRudder.UseVisualStyleBackColor = true;
            // 
            // cbxTailRightDownRudder
            // 
            this.cbxTailRightDownRudder.AutoSize = true;
            this.cbxTailRightDownRudder.Location = new System.Drawing.Point(687, 640);
            this.cbxTailRightDownRudder.Name = "cbxTailRightDownRudder";
            this.cbxTailRightDownRudder.Size = new System.Drawing.Size(84, 16);
            this.cbxTailRightDownRudder.TabIndex = 25;
            this.cbxTailRightDownRudder.Text = "右下舵曲线";
            this.cbxTailRightDownRudder.UseVisualStyleBackColor = true;
            // 
            // cbxLeftHydroplane
            // 
            this.cbxLeftHydroplane.AutoSize = true;
            this.cbxLeftHydroplane.Location = new System.Drawing.Point(775, 640);
            this.cbxLeftHydroplane.Name = "cbxLeftHydroplane";
            this.cbxLeftHydroplane.Size = new System.Drawing.Size(84, 16);
            this.cbxLeftHydroplane.TabIndex = 25;
            this.cbxLeftHydroplane.Text = "左水平舵线";
            this.cbxLeftHydroplane.UseVisualStyleBackColor = true;
            // 
            // cbxRightHydroplane
            // 
            this.cbxRightHydroplane.AutoSize = true;
            this.cbxRightHydroplane.Location = new System.Drawing.Point(865, 640);
            this.cbxRightHydroplane.Name = "cbxRightHydroplane";
            this.cbxRightHydroplane.Size = new System.Drawing.Size(84, 16);
            this.cbxRightHydroplane.TabIndex = 25;
            this.cbxRightHydroplane.Text = "右水平舵线";
            this.cbxRightHydroplane.UseVisualStyleBackColor = true;
            // 
            // cbxMotorSpeed
            // 
            this.cbxMotorSpeed.AutoSize = true;
            this.cbxMotorSpeed.Location = new System.Drawing.Point(953, 640);
            this.cbxMotorSpeed.Name = "cbxMotorSpeed";
            this.cbxMotorSpeed.Size = new System.Drawing.Size(84, 16);
            this.cbxMotorSpeed.TabIndex = 25;
            this.cbxMotorSpeed.Text = "电机转速线";
            this.cbxMotorSpeed.UseVisualStyleBackColor = true;
            // 
            // cbxDepth
            // 
            this.cbxDepth.AutoSize = true;
            this.cbxDepth.Location = new System.Drawing.Point(909, 615);
            this.cbxDepth.Name = "cbxDepth";
            this.cbxDepth.Size = new System.Drawing.Size(96, 16);
            this.cbxDepth.TabIndex = 25;
            this.cbxDepth.Text = "潜艇深度曲线";
            this.cbxDepth.UseVisualStyleBackColor = true;
            // 
            // btn_straight
            // 
            this.btn_straight.Location = new System.Drawing.Point(953, 193);
            this.btn_straight.Name = "btn_straight";
            this.btn_straight.Size = new System.Drawing.Size(75, 23);
            this.btn_straight.TabIndex = 8;
            this.btn_straight.Text = "直线航行";
            this.btn_straight.UseVisualStyleBackColor = true;
            this.btn_straight.Click += new System.EventHandler(this.btn_straight_Click);
            // 
            // btn_control
            // 
            this.btn_control.Location = new System.Drawing.Point(1060, 327);
            this.btn_control.Name = "btn_control";
            this.btn_control.Size = new System.Drawing.Size(75, 23);
            this.btn_control.TabIndex = 28;
            this.btn_control.Text = "PID控制";
            this.btn_control.UseVisualStyleBackColor = true;
            this.btn_control.Visible = false;
            this.btn_control.Click += new System.EventHandler(this.btn_control_Click);
            // 
            // RemoteSerialPort
            // 
            this.RemoteSerialPort.BaudRate = 115200;
            this.RemoteSerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.RemoteSerialPort_DataReceived);
            // 
            // RemoteBtn
            // 
            this.RemoteBtn.Location = new System.Drawing.Point(953, 137);
            this.RemoteBtn.Name = "RemoteBtn";
            this.RemoteBtn.Size = new System.Drawing.Size(75, 23);
            this.RemoteBtn.TabIndex = 7;
            this.RemoteBtn.Text = "开启遥控";
            this.RemoteBtn.UseVisualStyleBackColor = true;
            this.RemoteBtn.Click += new System.EventHandler(this.RemoteBtn_Click);
            // 
            // btn_dive
            // 
            this.btn_dive.Location = new System.Drawing.Point(953, 165);
            this.btn_dive.Name = "btn_dive";
            this.btn_dive.Size = new System.Drawing.Size(75, 23);
            this.btn_dive.TabIndex = 38;
            this.btn_dive.Text = "开启下潜";
            this.btn_dive.UseVisualStyleBackColor = true;
            this.btn_dive.Click += new System.EventHandler(this.btn_dive_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 683);
            this.Controls.Add(this.btn_dive);
            this.Controls.Add(this.RemoteBtn);
            this.Controls.Add(this.btn_control);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.btn_straight);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.cbxDepth);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.cbxMotorSpeed);
            this.Controls.Add(this.cbxRightHydroplane);
            this.Controls.Add(this.cbxLeftHydroplane);
            this.Controls.Add(this.cbxTailRightDownRudder);
            this.Controls.Add(this.cbxTailLeftDownRudder);
            this.Controls.Add(this.txt_morotSpeed_theory);
            this.Controls.Add(this.cbxTailRightUpRudder);
            this.Controls.Add(this.txt_horizRight_theory);
            this.Controls.Add(this.cbxTailLeftUpRudder);
            this.Controls.Add(this.txt_horizLeft_theory);
            this.Controls.Add(this.btn_dataSave);
            this.Controls.Add(this.btnDataSave);
            this.Controls.Add(this.groupBox10);
            this.Controls.Add(this.btn_DrawSwitch);
            this.Controls.Add(this.axUnityWebPlayer1);
            this.Controls.Add(this.pbxLegend);
            this.Controls.Add(this.pbxCurve);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.txt_morotSpeed_real);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.txt_horizRight_real);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.txt_horizLeft_real);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnWaterIN);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "X-Shaped Rudder Submarine Control System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxLegend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxCurve)).EndInit();
            this.groupBox10.ResumeLayout(false);
            this.groupBox10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axUnityWebPlayer1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

	private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label label7;
	private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnWorkRightDown;
        private System.Windows.Forms.Button btnStopRightDown;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnWorkLeftDown;
        private System.Windows.Forms.Button btnStopLeftDown;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnWorkRightUp;
        private System.Windows.Forms.Button btnStopRightUp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnWorkLeftUp;
        private System.Windows.Forms.Button btnStopLeftUp;
        private System.Windows.Forms.Button btn_CtlDataSend;
        public System.Windows.Forms.Button btn_CtrlDepthSend;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
	private System.Windows.Forms.TextBox txtCtrlDepth;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnChangePID;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtCtrlKd;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtCtrlKi;
        private System.Windows.Forms.Label label19;
	private System.Windows.Forms.TextBox txtCtrlKp;
        private System.Windows.Forms.StatusStrip statusStrip1;
	public System.IO.Ports.SerialPort XRudderSerialPort;	//串口对象
        public System.Windows.Forms.ToolStripStatusLabel tsPortName;
	private System.Windows.Forms.Button btn_SerialPortSwitch;
	private System.Windows.Forms.Label label8;
	private System.Windows.Forms.Label label11;
	private System.Windows.Forms.ComboBox combo_Baudrate;
	private System.Windows.Forms.ComboBox combo_PortName;
	private System.Windows.Forms.GroupBox groupBox8;
	private System.Windows.Forms.ToolStripStatusLabel tsBaudRate;
	private System.Windows.Forms.TextBox txt_Tail_s;
	private System.Windows.Forms.TextBox txt_MotorSpeed;
	private System.Windows.Forms.TextBox txt_Tail_r;
	private System.Windows.Forms.TextBox txt_HorizRudder;
	private System.Windows.Forms.GroupBox groupBox9;
	private System.Windows.Forms.PictureBox pbxLegend;
	private System.Windows.Forms.PictureBox pbxCurve;
	private AxUnityWebPlayerAXLib.AxUnityWebPlayer axUnityWebPlayer1;
	private System.Windows.Forms.Button btnWaterIN;
	private System.Windows.Forms.Button btn_Reset;
	private System.Windows.Forms.Button btn_DrawSwitch;
	private System.Windows.Forms.GroupBox groupBox10;
	private System.Windows.Forms.Label label23;
	private System.Windows.Forms.Label label24;
	private System.Windows.Forms.Label label25;
	private System.Windows.Forms.Label label26;
	private System.Windows.Forms.Label label18;
	private System.Windows.Forms.Label label22;
	private System.Windows.Forms.Label label17;
	private System.Windows.Forms.TextBox txt_Depth_theory;
	private System.Windows.Forms.TextBox txt_morotSpeed_theory;
	private System.Windows.Forms.TextBox txt_horizRight_theory;
	private System.Windows.Forms.TextBox txt_horizLeft_theory;
	private System.Windows.Forms.TextBox txt_tailRightDown_theory;
	private System.Windows.Forms.TextBox txt_tailLeftDown_theory;
	private System.Windows.Forms.TextBox txt_tailRightUp_theory;
	private System.Windows.Forms.TextBox txt_tailLeftUp_theory;
	private System.Windows.Forms.Label label27;
	private System.Windows.Forms.TextBox txt_Depth_real;
	private System.Windows.Forms.Label label13;
	private System.Windows.Forms.TextBox txt_morotSpeed_real;
	private System.Windows.Forms.Label label14;
	private System.Windows.Forms.TextBox txt_horizRight_real;
	private System.Windows.Forms.Label label15;
	private System.Windows.Forms.TextBox txt_horizLeft_real;
	private System.Windows.Forms.Label label16;
	private System.Windows.Forms.TextBox txt_tailRightDown_real;
	private System.Windows.Forms.Label label6;
	private System.Windows.Forms.TextBox txt_tailLeftDown_real;
	private System.Windows.Forms.Label label12;
	private System.Windows.Forms.TextBox txt_tailRightUp_real;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.TextBox txt_tailLeftUp_real;
	private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Button btnDataSave;
    private System.Windows.Forms.SaveFileDialog dataSaveFileDialog;
    private System.Windows.Forms.Button btn_dataSave;
    private System.Windows.Forms.CheckBox cbxTailLeftUpRudder;
    private System.Windows.Forms.CheckBox cbxTailRightUpRudder;
    private System.Windows.Forms.CheckBox cbxTailLeftDownRudder;
    private System.Windows.Forms.CheckBox cbxTailRightDownRudder;
    private System.Windows.Forms.CheckBox cbxLeftHydroplane;
    private System.Windows.Forms.CheckBox cbxRightHydroplane;
    private System.Windows.Forms.CheckBox cbxMotorSpeed;
    private System.Windows.Forms.CheckBox cbxDepth;
    private System.Windows.Forms.Button btn_straight;
    private System.Windows.Forms.Button btn_control;
    private System.Windows.Forms.TextBox ZAccTextBox;
    private System.Windows.Forms.TextBox YAccTextBox;
    private System.Windows.Forms.TextBox XAccTextBox;
    private System.Windows.Forms.Label label30;
    private System.Windows.Forms.Label label29;
    private System.Windows.Forms.Label label28;
    private System.IO.Ports.SerialPort RemoteSerialPort;
    private System.Windows.Forms.Button RemoteBtn;
    private System.Windows.Forms.Button btn_dive;
    }
}

