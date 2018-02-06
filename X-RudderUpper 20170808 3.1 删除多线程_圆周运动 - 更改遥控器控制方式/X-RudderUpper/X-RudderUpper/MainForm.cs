using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;	//串口
using System.Text.RegularExpressions;  //正则表达式类库
using Microsoft.Office.Interop;
using Microsoft.Office.Core;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace X_RudderUpper
{
	public partial class MainForm : Form
	{
		//定义全局变量、方法
		public static MainForm mainForm;
        private byte[] ctlData = new byte[37] { 0x5A, 0x01,         //数据校验位
                                                0x00, 0x00, 0x00, 0x00,     //左上舵角度
                                                0x00, 0x00, 0x00, 0x00,     //右上舵角度
                                                0x00, 0x00, 0x00, 0x00,     //左下舵角度
                                                0x00, 0x00, 0x00, 0x00,     //右下舵角度
                                                0x00, 0x00, 0x00, 0x00,     //左水平舵角度
                                                0x00, 0x00, 0x00, 0x00,     //右水平舵角度
                                                0x00, 0x00, 0x00, 0x00,     //电机
                                                0x00,        //重置复位标志位30：0xFF为重置复位
                                                0x33,                   //数据校验位
                                                0x00,       //电机正反转标志位32
                                                0x00,       //左上舵卡舵标志位33：0xFF为卡舵
                                                0x00,       //右上舵卡舵标志位34：0xFF为卡舵
                                                0x00,       //左下舵卡舵标志位35：0xFF为卡舵
                                                0x00,       //右下舵卡舵标志位36：0xFF为卡舵
        };
		public const int dataByteCount = 69;  //下位机上传数据总量
		public const int drawPointsTotalCount = 300; //绘图时使用的最大数据量
		public const int dataInterval = 4;
		public const int effectiveDecimal = 2;
        public const float PI = 3.14159f;
        public const float T = 0.05f;   //电机转速编码器的两个值相差时间
		public const float servoConvert = ((float)360) / (8 * 1024);
		public const float insAngleConvert = ((float)360) / ((float)2.0 * (float)3.14159);
        public const float XservoMaxAngle = 65.0f;
        public const float HservoMaxAngle = 35.0f;
		byte[] dataTemp = new byte[4] { 0x00, 0x00, 0x00, 0x00 };
        UInt64 excelRow = 2;

        public bool dive_mode_flag = false;     //下潜模式标志位
        public const float tailAngle_yaw_converter = 180.0f / 35.0f;      //遥控器模式下将舵角转换为直线航行yaw角改变值转换系数
        #region 控制算法变量
        public float tail_r_Angle = 0.0f;
        public float tail_s_Angle = 0.0f;
        public float horiz_Angle = 0.0f;
        public float motor_Speed = 0.0f;
        //直线航行变量
        public bool straight_naiv = false;      //直线航行标志位
        public float kp_input = 0.0f, ki_input = 0.0f, kd_input = 0.0f;
        public float e_k = 0.0f;      //当前误差
        public float e_k_1 = 0.0f;    //上一次误差
        public float e_k_2 = 0.0f;    //上两次误差
        public float last_tail_r_Angle = 0.0f;    //上次水平舵角度
        public float kp=0.0f, ki=0.0f, kd=0.0f;
        public float A = 0.0f, B = 0.0f, C = 0.0f;      //pid控制器系数
        public float standard_Yaw = 0.0f;     //初始角度标定
        public float standard_Yaw_change = 0.0f;    //遥控器更改角度变量，范围：-180°—180°
        //卡舵
        public bool leftUpRudder = true;        //卡舵标志位：true为工作正常；false为卡舵
        public bool rightUpRudder = true;
        public bool leftDownRudder = true;
        public bool rightDownRudder = true;
        #endregion
        #region 舵机、电机、压力传感器、水下航行器姿态数据存放List<T>
        //编码器通道	4：尾右上舵  5：尾右下舵  6：尾左上舵  7：尾左下舵  0：右水平舵   1：左水平舵  2：电机
		//舵机
		public List<float> tailRightUpRudder = new List<float> { 0x00 };
		public List<float> tailRightDownRudder = new List<float> { 0x00 };
		public List<float> tailLeftUpRudder = new List<float> { 0x00 };
		public List<float> tailLeftDownRudder = new List<float> { 0x00 };
		public List<float> rightHydroplane = new List<float> { 0x00 };
		public List<float> leftHydroplane = new List<float> { 0x00 };  
		public List<float> motorSpeed = new List<float> { 0x00 };  
        public UInt32[] motorEncoderData = new UInt32[2]{ 0, 0 };  //存储电机上传的码值
		//压力传感器  深度
		public List<float> depth = new List<float> { 0x00 };
		//姿态数据
		public float roll = 0.0f;
		public float pitch = 0.0f;
		public float yaw = 0.0f;
        //本地重力加速度
        public float localgravity = 9.8f;
        //MPU6050三轴加速度值
        public float MPU6050AccX = 0.0f;
        public float MPU6050AccY = 0.0f;
        public float MPU6050AccZ = 0.0f;
        //MPU6050三轴速度值
        public float MPU6050SpeedX = 0.0f;
        public float MPU6050SpeedY = 0.0f;
        public float MPU6050SpeedZ = 0.0f;
        //进水报警
        public byte waterInWarningFlag = 0x00;
        //绘图所用数据
		public static List<List<float>> curvePoint = new List<List<float>> { };
		public static int pointX = 0;
        //用于理论值存储
        public float tailRightUpRudderTheory = 0;
        public float tailRightDownRudderTheory = 0;
        public float tailLeftUpRudderTheory = 0;
        public float tailLeftDownRudderTheory = 0;
        public float rightHydroplaneTheory = 0;
        public float leftHydroplaneTheory = 0;
        public float motorSpeedTheory = 0;

		#endregion
        #region PID数据存取值
        public float[] depthPidPara = new float[3] { 0x00, 0x00, 0x00 }; //定深运动控制Kp,Ki,Kd
        public float [] pidDepthData = new float[2] {0x00,0x00}; //深度数据滑动平均后的值（滑动平均滤波） 需要过滤掉尖峰也即是突变
        #endregion

        public MainForm()
		{
			InitializeComponent();
            CreateExcel();
            CreateWorkSheetHead();
		}

		//窗体加载
		private void MainForm_Load(object sender, EventArgs e)
		{
            Control.CheckForIllegalCrossThreadCalls = false;

			#region 串口初始化
			//初始化下拉串口名列表框
			string[] ports = SerialPort.GetPortNames();
			Array.Sort(ports);
			combo_PortName.Items.AddRange(ports);
			combo_PortName.SelectedIndex = combo_PortName.Items.Count > 0 ? 0 : -1;
			combo_Baudrate.SelectedIndex = combo_Baudrate.Items.IndexOf("115200");
			//设置控件状态
			btn_SerialPortSwitch.Text = XRudderSerialPort.IsOpen ? "关闭串口" : "打开串口";
			btn_CtlDataSend.Enabled = XRudderSerialPort.IsOpen;
			btn_CtrlDepthSend.Enabled = XRudderSerialPort.IsOpen;
			combo_PortName.Enabled = !XRudderSerialPort.IsOpen;
			combo_Baudrate.Enabled = !XRudderSerialPort.IsOpen;
			#endregion

			#region 图形显示初始化
			DrawBackgroundAxis(pointsMaxNum);
			StartPosition = FormStartPosition.Manual;
			Location = new Point(Location.X + Width, Location.Y);
			Show();
			//设置控件状态
			btn_DrawSwitch.Text = DrawFlag ? "停止绘图" : "开始绘图";
			#endregion

		}

		//串口配置（打开串口/关闭串口按钮）
		private void btn_SerialPortSwitch_Click(object sender, EventArgs e)
		{
			try
			{
				//根据当前串口对象，判断操作
				if (XRudderSerialPort.IsOpen)
				{
					//串口打开时点击，则关闭
					XRudderSerialPort.Close();
					//状态栏设置
					tsPortName.Text = "串口号：未指定 |";
					tsBaudRate.Text = "波特率：未指定";
					combo_PortName.Enabled = !XRudderSerialPort.IsOpen;
					combo_Baudrate.Enabled = !XRudderSerialPort.IsOpen;
				}
				else
				{
					//串口关闭时点击，设置串口信息，后打开
					XRudderSerialPort.PortName = combo_PortName.Text;
					XRudderSerialPort.BaudRate = int.Parse(combo_Baudrate.Text);
					XRudderSerialPort.DataBits = 8;
					XRudderSerialPort.StopBits = StopBits.One;
					XRudderSerialPort.Parity = Parity.None;
					//状态栏设置
					tsPortName.Text = "串口号：" + combo_PortName.Text + " |";
					tsBaudRate.Text = "波特率：" + combo_Baudrate.Text;
					try
					{
						XRudderSerialPort.Open();
					}
					catch (Exception ex)
					{
						//捕获到异常信息，创建一个新的comm对象，之前的不能用了
						XRudderSerialPort = new SerialPort();
						MessageBox.Show(ex.Message, "Error");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
			}
			//设置控件状态
			btn_SerialPortSwitch.Text = XRudderSerialPort.IsOpen ? "关闭串口" : "打开串口";
			btn_CtlDataSend.Enabled = XRudderSerialPort.IsOpen;
			btn_CtrlDepthSend.Enabled = XRudderSerialPort.IsOpen;
			combo_PortName.Enabled = !XRudderSerialPort.IsOpen;
			combo_Baudrate.Enabled = !XRudderSerialPort.IsOpen;
		}
        //选择曲线显示
        void ChooseCurveShow()
        {
            if (cbxTailRightUpRudder.CheckState == CheckState.Checked)
                curvePoint.Add(tailRightUpRudder);//做测试用
            if (cbxTailRightDownRudder.CheckState == CheckState.Checked)
                curvePoint.Add(tailRightDownRudder);//做测试用

            if (cbxTailLeftUpRudder.CheckState == CheckState.Checked)
                curvePoint.Add(tailLeftUpRudder);//做测试用
            if (cbxTailLeftDownRudder.CheckState == CheckState.Checked)
                curvePoint.Add(tailLeftDownRudder);//做测试用

            if (cbxRightHydroplane.CheckState == CheckState.Checked)
                curvePoint.Add(rightHydroplane);//做测试用
            if (cbxLeftHydroplane.CheckState == CheckState.Checked)
                curvePoint.Add(leftHydroplane);//做测试用
            if (cbxMotorSpeed.CheckState == CheckState.Checked)
                curvePoint.Add(motorSpeed);//做测试用

            if (cbxDepth.CheckState == CheckState.Checked)
                curvePoint.Add(depth);//做测试用
        }

		//串口数据接收
		private void XRudderSerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
		{
			try
			{
				//等待数据传输结束
				int n = 0;
				while (n < dataByteCount)
				{
					if (XRudderSerialPort.IsOpen == false)  //解决串口关闭时引起的异常
					{
						n = 0;
						return;
					}
					n = XRudderSerialPort.BytesToRead;
				}
				//开始接收数据
                byte[] byteRead = new byte[dataByteCount];
				XRudderSerialPort.Read(byteRead, 0, byteRead.Length);
				XRudderSerialPort.DiscardInBuffer();

				if (byteRead[0] == 0x5A)
				{
					DataConvertToReal(byteRead, 3, 23);                   
                    DataDisplay();
                    //if (DrawFlag == true)  //监视窗口打开则显示数据并且绘图    暂时不用绘图功能
                    //{
                    //    pointX++;
                    //    if (pointX > 300)
                    //        pointX = 300;
                    //    curvePoint.Clear();
                    //    //此处用多线程编程是否可以消除窗口卡死的现象？
                    //    //Thread CurveShowThread = new Thread(new ThreadStart(ChooseCurveShow));
                    //    //CurveShowThread.Start();
                    //    //Thread CurveDrawThread = new Thread(new ThreadStart(DcDraw));
                    //    //CurveDrawThread.Start();
                    //    ChooseCurveShow();
                    //    DcDraw();
                    //}
                    //Thread DataSaveThread = new Thread(new ThreadStart(SaveDataToExcel));
                    //DataSaveThread.Start();
                    SaveDataToExcel();
				}

                if (true == straight_naiv)
                {
                    //若有偏差角度，则进行pid控制
                    if (yaw - standard_Yaw != 0)
                    {
                        if (Math.Abs(yaw - standard_Yaw) > 180)     //避免惯导数据在正负180度位置出现数据跳变
                        {
                            e_k = yaw - standard_Yaw + 360;
                        }
                        else
                            e_k = yaw - standard_Yaw;

                        tail_r_Angle = last_tail_r_Angle + A * e_k - B * e_k_1 + C * e_k_2;
                        e_k_2 = e_k_1;
                        e_k_1 = e_k;
                        if (tail_r_Angle < -50)
                            tail_r_Angle = -50;
                        if (tail_r_Angle > 50)
                            tail_r_Angle = 50;
                        last_tail_r_Angle = tail_r_Angle;
                        motor_Speed = Convert.ToSingle(txt_MotorSpeed.Text);

                        ten2x_Rudders(tail_r_Angle, tail_s_Angle, horiz_Angle, motor_Speed);    //十字舵与X舵转换
                    }
                    SerialPortDataSend();       //发送串口数据
                }
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
				XRudderSerialPort.Close();
			}
		}

        //串口数据发送函数(发送舵角为X舵舵角)
        public void SerialPortDataSend()
        {
            if (XRudderSerialPort.IsOpen == true)
                XRudderSerialPort.Write(ctlData, 0, ctlData.Length);
            else
                MessageBox.Show("请打开串口");
        }

		#region 数据转换代码
		public int SingleIntDataConvert(byte[] data, int offset)
		{
		    int i;
		    i = BitConverter.ToInt32(data, offset);
		    return i;
		}
		public uint SingleUintDataConvert(byte[] data, int offset)
		{
		    uint i;
		    i = BitConverter.ToUInt32(data, offset);
		    return i;
		}
		public float SingleFloatDataConvert(byte[] data, int offset)
		{
		    float i;
		    i = BitConverter.ToSingle(data, offset);
		    return i;
		}

        public void convertFloatToByte(float xRudderAngle, int de)
        {
            byte[] tempAngle = new byte[4];
            //int d = 110;
            tempAngle = BitConverter.GetBytes(xRudderAngle);
            //tempAngle = BitConverter.GetBytes(d);
            for (int i = de, j = 0; i < de + 4; i++, j++)
            {
                ctlData[i] = tempAngle[j];
            }
        }

        public void ten2x_Rudders(float dir, float tail, float head, float motor)       //十字舵与X舵等效转换函数
        {
            //判断电机正反转:ctlData[32] = 0x00,则正转；0xFF,则反转
            //if (motor < 0)
            //{
            //    ctlData[32] = 0xFF;
            //    motor = -motor;
            //}
            //else
            //    ctlData[32] = 0x00;

            //X舵与十字舵转换
            float left_up_Angle = (float)Math.Sqrt(2.0) / 2 * (dir + tail);         //1
            float right_down_Angle = (float)Math.Sqrt(2.0) / 2 * (dir + tail);      //2
            float right_up_Angle = (float)Math.Sqrt(2.0) / 2 * (dir - tail);        //3
            float left_down_Angle = (float)Math.Sqrt(2.0) / 2 * (dir - tail);       //4

            //舵角限位
            if (left_up_Angle <= -50)        //1
                left_up_Angle = -50;
            else if (left_up_Angle >= 50)
                left_up_Angle = 50;

            if (right_down_Angle <= -50)     //2
                right_down_Angle = -50;
            else if (right_down_Angle >= 50)
                right_down_Angle = 50;

            if (right_up_Angle <= -50)       //3
                right_up_Angle = -50;
            else if (right_up_Angle >= 50)
                right_up_Angle = 50;

            if (left_down_Angle <= -50)      //4
                left_down_Angle = -50;
            else if (left_down_Angle >= 50)
                left_down_Angle = 50;

            //卡舵情况舵角修正
            if (!leftUpRudder | !rightDownRudder)
            {
                right_up_Angle = (float)0.5 * right_up_Angle;
                left_down_Angle = (float)0.5 * left_down_Angle;
            }
            if (!rightUpRudder | !leftDownRudder)
            {
                left_up_Angle = (float)0.5 * left_up_Angle;
                right_down_Angle = (float)0.5 * right_down_Angle;
            }

            //用于理论值存储
            tailRightUpRudderTheory = right_up_Angle;
            tailRightDownRudderTheory = right_down_Angle;
            tailLeftUpRudderTheory = left_up_Angle;
            tailLeftDownRudderTheory = left_down_Angle;
            rightHydroplaneTheory = head;
            leftHydroplaneTheory = head;
            motorSpeedTheory = motor;

            convertFloatToByte(left_up_Angle + 90, 2);
            convertFloatToByte(right_up_Angle + 90, 6);
            convertFloatToByte(left_down_Angle + 90, 10);
            convertFloatToByte(right_down_Angle + 90, 14);
            convertFloatToByte(head + 90, 18);
            convertFloatToByte(head + 90, 22);
            convertFloatToByte(motor, 26);

            //显示理论值
            txt_tailLeftDown_theory.Text = Convert.ToString(left_down_Angle);
            txt_tailLeftUp_theory.Text = Convert.ToString(left_up_Angle);
            txt_tailRightDown_theory.Text = Convert.ToString(right_down_Angle);
            txt_tailRightUp_theory.Text = Convert.ToString(right_up_Angle);
            txt_horizLeft_theory.Text = Convert.ToString(head);
            txt_horizRight_theory.Text = Convert.ToString(head);
            txt_morotSpeed_theory.Text = Convert.ToString(motor);
        }
		#endregion

		#region  数据大头小头调换并且直接转换成int型数据 代码   编码器上传的数据需要大头小头进行调换
		public int DataChangeAndConvert(byte[] data, int offset)
		{
			dataTemp[0] = data[offset + 3];
			dataTemp[1] = data[offset + 2];
			dataTemp[2] = data[offset + 1];
			dataTemp[3] = data[offset];
			int j = BitConverter.ToInt32(dataTemp, 0);
			return j;
		}
		#endregion

		//编码器通道	4：尾右上舵  5：尾右下舵  6：尾左上舵  7：尾左下舵  0：右水平舵   1：左水平舵  2：电机  OFFSET 3 23
		public void DataConvertToReal(byte[] data, int offsetOne, int offsetTwo)
		{
			float i = 0.0f;
			//X舵
			i =-1 * (float)Math.Round(((double)DataChangeAndConvert(data, offsetOne)) * servoConvert, effectiveDecimal);
            if (Math.Abs(i) > XservoMaxAngle)  //防止数据突变，如果数据突变，取前一个数据作为现在的数据
                tailRightUpRudder.Add(tailRightUpRudder[tailRightUpRudder.Count - 1]);
            else
			    tailRightUpRudder.Add(i);
			if (tailRightUpRudder.Count > drawPointsTotalCount)
				tailRightUpRudder.RemoveAt(0);

			i =-1 * (float)Math.Round((double)DataChangeAndConvert(data, offsetOne + dataInterval) * servoConvert, effectiveDecimal);
            if (Math.Abs(i) > XservoMaxAngle)  //防止数据突变，如果数据突变，取前一个数据作为现在的数据
                tailRightDownRudder.Add(tailRightDownRudder[tailRightDownRudder.Count - 1]);
            else
                tailRightDownRudder.Add(i);
			if (tailRightDownRudder.Count > drawPointsTotalCount)
				tailRightDownRudder.RemoveAt(0);

			i =-1 * (float)Math.Round((double)DataChangeAndConvert(data, offsetOne + 2 * dataInterval) * servoConvert, effectiveDecimal);
            if (Math.Abs(i) > XservoMaxAngle)  //防止数据突变，如果数据突变，取前一个数据作为现在的数据
                tailLeftUpRudder.Add(tailLeftUpRudder[tailLeftUpRudder.Count - 1]);
            else
                tailLeftUpRudder.Add(i);
			if (tailLeftUpRudder.Count > drawPointsTotalCount)
				tailLeftUpRudder.RemoveAt(0);

			i =-1 * (float)Math.Round((double)DataChangeAndConvert(data, offsetOne + 3 * dataInterval) * servoConvert, effectiveDecimal);
            if (Math.Abs(i) > XservoMaxAngle)  //防止数据突变，如果数据突变，取前一个数据作为现在的数据
                tailLeftDownRudder.Add(tailLeftDownRudder[tailLeftDownRudder.Count - 1]);
            else
                tailLeftDownRudder.Add(i);
			if (tailLeftDownRudder.Count > drawPointsTotalCount)
				tailLeftDownRudder.RemoveAt(0);

			//水平舵
			i = (float)Math.Round((double)DataChangeAndConvert(data, offsetTwo + 0) * servoConvert, effectiveDecimal);
            if (Math.Abs(i) > HservoMaxAngle)  //防止数据突变，如果数据突变，取前一个数据作为现在的数据
                rightHydroplane.Add(rightHydroplane[rightHydroplane.Count - 1]);
            else
                rightHydroplane.Add(i);
			if (rightHydroplane.Count > drawPointsTotalCount)
				rightHydroplane.RemoveAt(0);

			i = (float)Math.Round((double)DataChangeAndConvert(data, offsetTwo + dataInterval) * servoConvert, effectiveDecimal);
            if (Math.Abs(i) > HservoMaxAngle)  //防止数据突变，如果数据突变，取前一个数据作为现在的数据
                leftHydroplane.Add(leftHydroplane[leftHydroplane.Count - 1]);
            else
                leftHydroplane.Add(i);
			if (leftHydroplane.Count > drawPointsTotalCount)
				leftHydroplane.RemoveAt(0);

			//电机速度  需要修改计算公式  最好需要三相编码器的Z相功能一圈一个信号
            //i = (float)Math.Round((double)DataChangeAndConvert(data, offsetTwo + 2 * dataInterval), effectiveDecimal);
            UInt32 motorSpeedTemp;
            motorSpeedTemp = (UInt32)Math.Round((double)DataChangeAndConvert(data, offsetTwo + 2 * dataInterval), effectiveDecimal);
            motorEncoderData[1] = motorSpeedTemp;
            i = (float)(motorEncoderData[1] - motorEncoderData[0]) * (2 * PI / (8 * 1024)) / T * (2 * PI / 60); //单位转每分
			motorSpeed.Add(i);
			if (motorSpeed.Count > drawPointsTotalCount)
				motorSpeed.RemoveAt(0);

			//深度   下位机ADC转换完成后上传的数据
			i = (float)Math.Round((double)(((float)SingleUintDataConvert(data, 40) / 4096) * ((float)50000 / 9800)), effectiveDecimal);
			depth.Add(i);
			if (depth.Count > drawPointsTotalCount)
				depth.RemoveAt(0);

			//姿态
			roll = (float)Math.Round((double)SingleFloatDataConvert(data, 44) * insAngleConvert, effectiveDecimal);
			pitch = (float)Math.Round((double)SingleFloatDataConvert(data, 48) * insAngleConvert, effectiveDecimal);
			yaw = (float)Math.Round((double)SingleFloatDataConvert(data, 52) * insAngleConvert, effectiveDecimal);

            //进水报警
            waterInWarningFlag = data[56];
            if (waterInWarningFlag == 0xFF)
            {
                btnWaterIN.BackColor = Color.Red;
            }
            else
            {
                btnWaterIN.BackColor = Color.Green;
            }

            //加速度值转换
            MPU6050AccX = (float)((((short)data[61] << 8) | data[60]) / 32768.0f * 16.8f * localgravity);
            MPU6050AccY = (float)((((short)data[63] << 8) | data[62]) / 32768.0f * 16.8f * localgravity);
            MPU6050AccZ = (float)((((short)data[65] << 8) | data[64]) / 32768.0f * 16.8f * localgravity);
		}

        #region 上位机控制  数据发送按钮
        private void btn_CtlDataSend_Click(object sender, EventArgs e)
		{
            #region  判断输入数据是否正确（输入舵角为十字舵舵角）
            //if (!Regex.IsMatch(txt_HorizRudder.Text.ToString(), @"^-?[1-9]\d*$|^0$") || int.Parse(txt_HorizRudder.Text) < -35 || int.Parse(txt_HorizRudder.Text) > 35)
            //{
            //    MessageBox.Show("首舵角度，请输入-35到+35的整数！");
            //    return;
            //}
            //if (!Regex.IsMatch(txt_Tail_r.Text.ToString(), @"^-?[1-9]\d*$|^0$") || int.Parse(txt_Tail_r.Text) < -35 || int.Parse(txt_Tail_r.Text) > 35)
            //{
            //    MessageBox.Show("尾方向舵角度，请输入-35到+35的整数！");
            //    return;
            //}
            //if (!Regex.IsMatch(txt_Tail_s.Text.ToString(), @"^-?[1-9]\d*$|^0$") || int.Parse(txt_Tail_s.Text) < -35 || int.Parse(txt_Tail_s.Text) > 35)
            //{
            //    MessageBox.Show("尾升降舵角度，请输入-35到+35的整数！");
            //    return;
            //}
            //if (!Regex.IsMatch(txt_MotorSpeed.Text.ToString(), @"^-?[1-9]\d*$|^0$") || int.Parse(txt_MotorSpeed.Text) < -1000 || int.Parse(txt_MotorSpeed.Text) > 1000)
            //{
            //    MessageBox.Show("电机转速，请输入-1000到1000的整数！");
            //    return;
            //}
            #endregion

            #region  数据转换（包括X舵与十字舵等效舵角转换）
            float tail_r_Angle = Convert.ToSingle(txt_Tail_r.Text);
            float tail_s_Angle = Convert.ToSingle(txt_Tail_s.Text);
            float horiz_Angle = Convert.ToSingle(txt_HorizRudder.Text);
            float motorSpeed = Convert.ToSingle(txt_MotorSpeed.Text);

            ten2x_Rudders(tail_r_Angle, tail_s_Angle, horiz_Angle, motorSpeed);    //十字舵与X舵转换
            SerialPortDataSend();       //发送串口数据
            #endregion
		}
        #endregion

        #region 重置复位按钮
        private void btn_Reset_Click(object sender, EventArgs e)
		{
            convertFloatToByte(90, 2);
            convertFloatToByte(90, 6);
            convertFloatToByte(90, 10);
            convertFloatToByte(90, 14);
            convertFloatToByte(90, 18);
            convertFloatToByte(90, 22);
            convertFloatToByte(0, 26);
            ctlData[30] = 0xff;     //重置复位标志 0xff:代表复位
            //ctlData[33] = 0x00;     //快速控制、慢速控制标志位：0x00代表慢速；0xff代表快速

            SerialPortDataSend();       //发送串口数据

            ctlData[30] = 0x00; //清楚重置复位标志位 0xff:代表复位

            txt_tailLeftDown_theory.Text = Convert.ToString(0);
            txt_tailLeftUp_theory.Text = Convert.ToString(0);
            txt_tailRightDown_theory.Text = Convert.ToString(0);
            txt_tailRightUp_theory.Text = Convert.ToString(0);
            txt_horizLeft_theory.Text = Convert.ToString(0);
            txt_horizRight_theory.Text = Convert.ToString(0);
            txt_morotSpeed_theory.Text = Convert.ToString(0);
		}
        #endregion

        #region 下潜模式开关按钮
        private void btn_dive_Click(object sender, EventArgs e)
        {
            dive_mode_flag = !dive_mode_flag;
            btn_dive.Text = dive_mode_flag ? "关闭下潜" : "开启下潜";
        }
        #endregion

        #region 绘制曲线区
        //存储背景图片
	    Bitmap DynamicBackgroundBmp;
		const int XaxisInterval = 50;        
		const int YaxisInterval = 3;
		const int pointsMaxNum = 300;
		const float curveWidth = 2.0f;
		//颜色表
		Color[] CurveColor = { Color.FromArgb(100, 200, 255), Color.FromArgb(89, 69, 61), Color.FromArgb(153, 77, 82), Color.FromArgb(217, 116, 43), 
					     Color.FromArgb(230, 180, 80), Color.FromArgb(255, 255, 255), Color.FromArgb(36, 169, 255), Color.FromArgb(91, 74, 66), 
					     Color.FromArgb(69, 39, 39), Color.FromArgb(29, 191, 151), Color.FromArgb(110, 112, 73), Color.FromArgb(117, 27, 19), 
					     Color.FromArgb(244, 208, 0), Color.FromArgb(29, 131, 8), Color.FromArgb(0, 0, 0), Color.FromArgb(220, 87, 18), 
					     Color.FromArgb(207, 191, 140), Color.FromArgb(90, 13, 67), Color.FromArgb(244, 13, 100), Color.FromArgb(179, 197, 135), 
					     Color.FromArgb(172, 81, 24), Color.FromArgb(255, 150, 128), Color.FromArgb(0, 34, 40), Color.FromArgb(220, 162, 151), 
					     Color.FromArgb(214, 200, 75), Color.FromArgb(167, 220, 224), Color.FromArgb(89, 69, 61), Color.FromArgb(252, 157, 154), 
					     Color.FromArgb(92, 167, 186), Color.FromArgb(147, 224, 255), Color.FromArgb(179, 214, 110), Color.FromArgb(17, 63, 61), 
					     Color.FromArgb(60, 79, 57), Color.FromArgb(186, 40, 53), Color.FromArgb(53, 44, 10), Color.FromArgb(56, 13, 49), 
					     Color.FromArgb(89, 61, 67), Color.FromArgb(227, 160, 93), Color.FromArgb(114, 111, 128), Color.FromArgb(156, 38, 50), 
					     Color.FromArgb(222, 211, 140), Color.FromArgb(226, 17, 0), Color.FromArgb(54, 66, 74), Color.FromArgb(170, 138, 87), 
					     Color.FromArgb(119, 52, 92), Color.FromArgb(78, 29, 76), Color.FromArgb(148, 41, 35) };
		//绘制背景及坐标
		void DrawBackgroundAxis(int pointXMax)
		{
			Bitmap bmp = new Bitmap(pbxCurve.Width, pbxCurve.Height);
			Graphics backGroundGra = Graphics.FromImage(bmp);
			Color backGroundColor = Color.FromArgb(203, 203, 203);//background color
			Color axisLineColor = Color.FromArgb(3, 35, 14); //axis color
			Brush backGroundBrush = new SolidBrush(backGroundColor);
			backGroundGra.FillRectangle(backGroundBrush, this.ClientRectangle);
			Pen dashPen = new Pen(Color.FromArgb(3, 35, 14), 1f);
			dashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
			Pen axisPen = new Pen(axisLineColor, 2.0F);
			//X
			for (int i = 0; i < ((pointXMax / XaxisInterval) - 1); i++)
			{
				backGroundGra.DrawLine(dashPen, (i + 1) * pbxCurve.Width * ((float)XaxisInterval / pointXMax), 0, (i + 1) * pbxCurve.Width * ((float)XaxisInterval / pointXMax), pbxCurve.Height);
			}
			//Y
			for (int i = 0; i < (YaxisInterval - 1); i++)
			{
				if (i == 0)
				{
					backGroundGra.DrawLine(axisPen, 0, pbxCurve.Height / 2 , pbxCurve.Width, pbxCurve.Height / 2);
				}
				backGroundGra.DrawLine(dashPen, 0, (pbxCurve.Height / 2) - (((float)(i + 1) / YaxisInterval) * (pbxCurve.Height / 2)), pbxCurve.Width, (pbxCurve.Height / 2) - (((float)(i + 1) / YaxisInterval) * (pbxCurve.Height / 2)));
				backGroundGra.DrawLine(dashPen, 0, (pbxCurve.Height / 2) + (((float)(i + 1) / YaxisInterval) * (pbxCurve.Height / 2)), pbxCurve.Width, (pbxCurve.Height / 2) + (((float)(i + 1) / YaxisInterval) * (pbxCurve.Height / 2)));
			}
			//边框
			backGroundGra.DrawLine(axisPen, 0, 0, pbxCurve.Width, 0);
			backGroundGra.DrawLine(axisPen, 0, pbxCurve.Height - 1, pbxCurve.Width, pbxCurve.Height - 1);
			backGroundGra.DrawLine(axisPen, 1, 0, 1, pbxCurve.Height);
			backGroundGra.DrawLine(axisPen, pbxCurve.Width, 0 , pbxCurve.Width, pbxCurve.Height);

			DynamicBackgroundBmp = bmp;
			this.pbxCurve.Image = bmp;
			backGroundGra.Dispose();
		}
		//绘图
		public void DrawLines(List<List<float>> pointsY, int pointsX)
		{
			if (pointsX < 2)
				return;
			if (pointsX > pointsMaxNum - 1)
			{
				pointsX = pointsMaxNum - 1;
			}
            
			float pointsYMax = 0.0f;  //最大值
			for (int CurveNum = 0; CurveNum < pointsY.Count; CurveNum++)
			{
				foreach (float pointsYMaxTemp in pointsY[CurveNum])
				{
					if (Math.Abs(pointsYMaxTemp) >= pointsYMax)
					{
						pointsYMax = Math.Abs(pointsYMaxTemp);
					}
				}
			}

            if (pointsYMax == 0.0f)
                pointsYMax = 1.0f;
			float axisYScale = ((float)pbxCurve.Height/2) / pointsYMax;  //Y轴坐标比例

			//绘图
			Bitmap CurveBmp = new Bitmap(DynamicBackgroundBmp);
			Graphics CurveDrawGraphics = Graphics.FromImage(CurveBmp);

			for (int pointNum = 1; pointNum <= pointsX; pointNum++)
			{
				for (int CurveNum = 0; CurveNum < pointsY.Count; CurveNum++)  //Curve Number
				{
					Pen CurveDrawPen = new Pen(CurveColor[CurveNum], curveWidth);
					CurveDrawGraphics.DrawLine(CurveDrawPen, (pointNum - 1) * ((float)pbxCurve.Width / pointsMaxNum), (pbxCurve.Height / 2) - pointsY[CurveNum][pointNum - 1] * axisYScale, pointNum * ((float)pbxCurve.Width / pointsMaxNum),(pbxCurve.Height / 2) - pointsY[CurveNum][pointNum] * axisYScale);
					CurveDrawPen.Dispose();
				}
			}

			pbxCurve.Image = CurveBmp;
			CurveDrawGraphics.Dispose();
            //pointsY.Clear();
		}
		#endregion

		#region 绘制图例 显示数据区
		Font legendFont = new Font("宋体", 10f, FontStyle.Bold);

		public void DrawLegendAndDisplayData(List<List<float>> pointsY)
		{
			Bitmap legendBmp = new Bitmap(pbxLegend.Width, pbxLegend.Height);
			Graphics LegendDrawGraphics = Graphics.FromImage(legendBmp);

			for (int CurveNum = 0; CurveNum < pointsY.Count; CurveNum++)  //Curve Number
			{
				Pen LengndDrawPen = new Pen(CurveColor[CurveNum], curveWidth);
                Brush LegendBrush = new SolidBrush(CurveColor[CurveNum]);
				LegendDrawGraphics.DrawLine(LengndDrawPen, 0, CurveNum * 20 + 20, 30, CurveNum * 20 + 20);
				LegendDrawGraphics.DrawString(Convert.ToString(pointsY[CurveNum][pointsY[CurveNum].Count - 1]), legendFont, LegendBrush, 40f, CurveNum * 20 + 14);
				LengndDrawPen.Dispose();
			}
			pbxLegend.Image = legendBmp;
			LegendDrawGraphics.Dispose();
		}
		#endregion

		#region 姿态显示区
		public float _Roll_Angle = 0;
		public float _Pitch_Angle = 0;
		public float _Yaw_Angle = 0;
		public float _Horiz_Left_Angle = 0;
		public float _Horiz_Right_Angle = 0;
		public float _X_Left_Up_Angle = 0;
		public float _X_Right_Up_Angle = 0;
		public float _X_Left_Down_Angle = 0;
		public float _X_Right_Down_Angle = 0;

		public float Roll_Angle = 0;
		public float Pitch_Angle = 0;
		public float Yaw_Angle = 0;
		public float Horiz_Left_Angle = 0;
		public float Horiz_Right_Angle = 0;
		public float X_Left_Up_Angle = 0;
		public float X_Right_Up_Angle = 0;
		public float X_Left_Down_Angle = 0;
		public float X_Right_Down_Angle = 0;

		public void XRudder3DAttitude()
		{
            //校验惯导数据是否可用，若不可用，则用上一次惯导数据
            if (roll >= -180 && roll <= 180)
            {
                Roll_Angle = roll;
            }
            else
                Roll_Angle = _Roll_Angle;
            if (pitch >= -180 && pitch <= 180)
            {
                Pitch_Angle = pitch;
            }
            else
                Pitch_Angle = _Pitch_Angle;
            if (yaw >= -180 && yaw <= 180)
            {
                Yaw_Angle = yaw;
            }
            else
                Yaw_Angle = _Yaw_Angle;
            //Roll_Angle = roll;
            //Pitch_Angle = pitch;
            //Yaw_Angle = yaw;
			Horiz_Left_Angle = leftHydroplane[leftHydroplane.Count - 1];
			Horiz_Right_Angle = rightHydroplane[rightHydroplane.Count - 1];
			X_Left_Up_Angle = tailLeftUpRudder[tailLeftUpRudder.Count - 1];
			X_Right_Up_Angle = tailRightUpRudder[tailRightUpRudder.Count - 1];
			X_Left_Down_Angle = tailLeftDownRudder[tailLeftDownRudder.Count - 1];
			X_Right_Down_Angle = tailRightDownRudder[tailRightDownRudder.Count - 1];

			//潜艇姿态
			if (0 != Roll_Angle - _Roll_Angle)	//横摇
			{
				axUnityWebPlayer1.SendMessage("XModel_1", "Roll", Roll_Angle - _Roll_Angle);
				_Roll_Angle = Roll_Angle;
			}
			if (0 != Pitch_Angle - _Pitch_Angle)	//纵摇
			{
				axUnityWebPlayer1.SendMessage("XModel_1", "Pitch", Pitch_Angle - _Pitch_Angle);
				_Pitch_Angle = Pitch_Angle;
			}
			if (0 != Yaw_Angle - _Yaw_Angle)	//首摇
			{
				axUnityWebPlayer1.SendMessage("XModel_1", "Yaw", Yaw_Angle - _Yaw_Angle);
				_Yaw_Angle = Yaw_Angle;
			}
			//舵姿态
			if (0 != Horiz_Left_Angle - _Horiz_Left_Angle)	//左水平舵
			{
				axUnityWebPlayer1.SendMessage("horiz_Left", "Horiz_Left", Horiz_Left_Angle - _Horiz_Left_Angle);
				_Horiz_Left_Angle = Horiz_Left_Angle;
			}
			if (0 != Horiz_Right_Angle - _Horiz_Right_Angle)	//右水平舵
			{
				axUnityWebPlayer1.SendMessage("horiz_Right", "Horiz_Right", Horiz_Right_Angle - _Horiz_Right_Angle);
				_Horiz_Right_Angle = Horiz_Right_Angle;
			}
			if (0 != X_Left_Up_Angle - _X_Left_Up_Angle)	//左上X舵
			{
				axUnityWebPlayer1.SendMessage("x_Left_Up", "X_Left_Up", X_Left_Up_Angle - _X_Left_Up_Angle);
				_X_Left_Up_Angle = X_Left_Up_Angle;
			}
			if (0 != X_Right_Up_Angle - _X_Right_Up_Angle)	//右上X舵
			{
				axUnityWebPlayer1.SendMessage("x_Right_Up", "X_Right_Up", X_Right_Up_Angle - _X_Right_Up_Angle);
				_X_Right_Up_Angle = X_Right_Up_Angle;
			}
			if (0 != X_Left_Down_Angle - _X_Left_Down_Angle)	//左下X舵
			{
				axUnityWebPlayer1.SendMessage("x_Left_Down", "X_Left_Down", X_Left_Down_Angle - _X_Left_Down_Angle);
				_X_Left_Down_Angle = X_Left_Down_Angle;
			}
			if (0 != X_Right_Down_Angle - _X_Right_Down_Angle)	//右下X舵
			{
				axUnityWebPlayer1.SendMessage("x_Right_Down", "X_Right_Down", X_Right_Down_Angle - _X_Right_Down_Angle);
				_X_Right_Down_Angle = X_Right_Down_Angle;
			}
		}
		#endregion

        #region  实际值数据显示
        public void DataDisplay()
        {
            txt_tailRightUp_real.Text = Convert.ToString(tailRightUpRudder[tailRightUpRudder.Count - 1]);
            txt_tailRightDown_real.Text = Convert.ToString(tailRightDownRudder[tailRightDownRudder.Count - 1]);
            txt_tailLeftUp_real.Text = Convert.ToString(tailLeftUpRudder[tailLeftUpRudder.Count - 1]);
            txt_tailLeftDown_real.Text = Convert.ToString(tailLeftDownRudder[tailLeftDownRudder.Count - 1]);
            txt_horizRight_real.Text = Convert.ToString(rightHydroplane[rightHydroplane.Count - 1]);
            txt_horizLeft_real.Text = Convert.ToString(leftHydroplane[leftHydroplane.Count - 1]);
            txt_morotSpeed_real.Text = Convert.ToString(motorSpeed[motorSpeed.Count - 1]);

            txt_Depth_real.Text = Convert.ToString(depth[depth.Count - 1]);

            XAccTextBox.Text = Convert.ToString(MPU6050AccX);
            YAccTextBox.Text = Convert.ToString(MPU6050AccY);
            ZAccTextBox.Text = Convert.ToString(MPU6050AccZ);

            //rollTbx.Text = Convert.ToString(Form1.form1.roll);
            //pitchTbx.Text = Convert.ToString(Form1.form1.pitch);
            //yawTbx.Text = Convert.ToString(Form1.form1.yaw);
        }
        #endregion

        public void DcDraw()
		{
			DrawLines(curvePoint, pointX);
			DrawLegendAndDisplayData(curvePoint);
			XRudder3DAttitude();
		}

		public static bool DrawFlag = false;
		private void btn_DrawSwitch_Click(object sender, EventArgs e)
		{
			DrawFlag = !DrawFlag;
			btn_DrawSwitch.Text = DrawFlag ? "停止绘图" : "开始绘图";
        }

        #region  数据保存部分  保存至Excel
        Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
        Microsoft.Office.Interop.Excel.Workbook myWorkbook;            //工作薄实例声明
        Microsoft.Office.Interop.Excel.Worksheet myWorksheet;          //工作表实例声明
        /// <summary>
        /// 
        /// </summary>
        public void CreateExcel()
        {
            excel.Workbooks.Add(true);                    //不存在相同文件则创建一个新文件
            myWorkbook = excel.ActiveWorkbook;            //工作薄赋值为excel中的已激活工作薄
            myWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myWorkbook.ActiveSheet;    //工作表赋值为工作簿中已激活的工作表
            myWorksheet.Name = "xrudderDataSheet";         //给工作表取名字
            excel.DisplayAlerts = false;   //不提问任何提示，这样再关闭EXCEL对象时,就不会有保存提示,也就不会卡住了.这样就可以完美的关闭进程了。
            //myWorkbook.Names = "BOOKTest"; 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="value"></param>
        public void SetCellValue(Microsoft.Office.Interop.Excel.Worksheet ws, UInt64 x, UInt64 y, string value)     //ws:要设值的工作表; X:行; Y:列; value:值
        {
            ws.Cells[x, y] = value;
        }

        public void ExcelClose()        //关闭一个Microsoft.Office.Interop.Excel对象，销毁对象
        {
            //wb.Save();
            //myWorkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            excel.Quit();
            //myWorkbook = null;
            //excel = null;
            //GC.Collect();
            ExcelProgressKill(excel);
        }

        #region  结束Excel进程
        [DllImport("User32.dll", CharSet = CharSet.Auto)]  
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public void ExcelProgressKill(Microsoft.Office.Interop.Excel.Application excel)
        {
            IntPtr t = new IntPtr(excel.Hwnd);   //得到这个句柄，具体作用是得到这块内存入口 

            int k = 0;
            GetWindowThreadProcessId(t, out k);   //得到本进程唯一标志k
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);   //得到对进程k的引用
            p.Kill();     //关闭进程k
        }

       //保存文档
        public bool Save()
        {
            if (myWorksheet.Name == "")
            {
                return false;
            }
            else
            {
                try
                {
                    myWorkbook.Save();
                    return true;
                }

                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        //string fileName = @"C:\Users\sniper\Desktop\X-RudderUpper20170420全部功能实现-数据保存效率有问题\X-RudderUpper\xrudder.xls";
        //string fileName1 = "xrudder.xls";
        
        //文档另存为
        public bool SaveAs(object FileName)
        {
            try
            {
                myWorkbook.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion
        
        #region  数据先保存至DataTable中,此处不需要使用DataSet

        //DataTable xRudderData = new DataTable("xRudder");

        //public void CreatDataTableForm()
        //{
        //    DataColumn dc = null;
        //    dc = xRudderData.Columns.Add("tailRightUpRudder", System.Type.GetType("System.Single"));  //0
        //    dc.AutoIncrement = true;//自动增加
        //    dc.AutoIncrementSeed = 1;//起始为1
        //    dc.AutoIncrementStep = 1;//步长为1
        //    dc.AllowDBNull = false;//

        //    dc = xRudderData.Columns.Add("tailRightDownRudder", System.Type.GetType("System.Single"));
        //    dc = xRudderData.Columns.Add("tailLeftUpRudder", System.Type.GetType("System.Single"));
        //    dc = xRudderData.Columns.Add("tailLeftDownRudder", System.Type.GetType("System.Single"));
        //    dc = xRudderData.Columns.Add("rightHydroplane", System.Type.GetType("System.Single"));
        //    dc = xRudderData.Columns.Add("leftHydroplane", System.Type.GetType("System.Single"));
        //    dc = xRudderData.Columns.Add("motorSpeed", System.Type.GetType("System.Single"));
        //    dc = xRudderData.Columns.Add("depth", System.Type.GetType("System.Single"));
        //    dc = xRudderData.Columns.Add("Roll", System.Type.GetType("System.Single"));  //8
        //    dc = xRudderData.Columns.Add("Pitch", System.Type.GetType("System.Single")); //9
        //    dc = xRudderData.Columns.Add("Yaw", System.Type.GetType("System.Single"));   //10
        //    dc = xRudderData.Columns.Add("liluntailRightUpRudder", System.Type.GetType("System.Single"));       //11
        //    dc = xRudderData.Columns.Add("liluntailRightDownRudder", System.Type.GetType("System.Single"));  //12
        //    dc = xRudderData.Columns.Add("liluntailLeftUpRudder", System.Type.GetType("System.Single"));    //13   
        //    dc = xRudderData.Columns.Add("liluntailLeftDownRudder", System.Type.GetType("System.Single"));  //14
        //    dc = xRudderData.Columns.Add("lilunrightHydroplane", System.Type.GetType("System.Single"));     //15
        //    dc = xRudderData.Columns.Add("lilunleftHydroplane", System.Type.GetType("System.Single"));      //16
        //    dc = xRudderData.Columns.Add("lilunmotorSpeed", System.Type.GetType("System.Single"));      //17
        //}

        //public void AddDataToDataTable(System.Data.DataTable tmpDataTable)
        //{
        //    DataRow dr = tmpDataTable.NewRow();
        //    dr[0] = tailRightUpRudder[tailRightUpRudder.Count - 1];
        //    dr[1] = tailRightDownRudder[tailRightDownRudder.Count - 1];
        //    dr[2] = tailLeftUpRudder[tailLeftUpRudder.Count - 1];
        //    dr[3] = tailLeftDownRudder[tailLeftDownRudder.Count - 1];
        //    dr[4] = rightHydroplane[rightHydroplane.Count - 1];
        //    dr[5] = leftHydroplane[leftHydroplane.Count - 1];
        //    dr[6] = motorSpeed[motorSpeed.Count - 1];
        //    dr[7] = depth[depth.Count - 1];
        //    dr[8] = roll;
        //    dr[9] = pitch;
        //    dr[10] = yaw;
        //    dr[11] = tailRightUpRudderTheory;
        //    dr[12] = tailRightDownRudderTheory;
        //    dr[13] = tailLeftUpRudderTheory;
        //    dr[14] = tailLeftDownRudderTheory;
        //    dr[15] = rightHydroplaneTheory;
        //    dr[16] = leftHydroplaneTheory;
        //    dr[17] = motorSpeedTheory;
        //    tmpDataTable.Rows.Add(dr);
        //}

        private void DataTabletoExcel(System.Data.DataTable tmpDataTable,string savePath)
        {
            if(tmpDataTable == null)
                return;
            int rowNum = tmpDataTable.Rows.Count;//需要导出的数据的行数
            int columnNum = tmpDataTable.Columns.Count;//需要导出的数据的列数
            int rowIndex = 1;//起始行为第二行
            int columnIndex = 0;//起始列为第一列
            Microsoft.Office.Interop.Excel.Range range;//Excel的格式设置
            System.Reflection.Missing miss = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Excel.Application xlApp =new Microsoft.Office.Interop.Excel.Application();
            xlApp.DisplayAlerts = true;// 在程序执行过程中使出现的警告框显示
            xlApp.SheetsInNewWorkbook = 1;
            Microsoft.Office.Interop.Excel.Workbook xlBook = xlApp.Workbooks.Add(true);

            foreach(DataColumn dc in tmpDataTable.Columns)             //将datatable的列名导入excel表的第一行
            {
                columnIndex ++;
                xlApp.Cells[rowIndex,columnIndex] = dc.ColumnName;
            }
             //将数据写入到Excel表中
            for(int i = 0; i < rowNum; i++)
            {
                rowIndex++;
                columnIndex = 0;
                for(int j = 0; j < columnNum; j++)
                {//按行写入数据
                    columnIndex++;
                    range =(Microsoft.Office.Interop.Excel.Range)xlApp.Cells[rowIndex,columnIndex];
                    range.NumberFormatLocal = "@";//写入到表中的数据格式以文本形式存在
                    xlApp.Cells[rowIndex,columnIndex] = tmpDataTable.Rows[i][j].ToString();
                }
            }
            //数据保存
            xlBook.SaveAs(savePath, miss, miss, miss, miss, miss, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss, miss, miss);
            xlBook.Close(false,miss,miss);
            xlApp.Quit();
            xlApp = null;
            GC.Collect();
        }

        public void SaveToExcel(string addr, System.Data.DataTable dt)
        {
            //0.注意：
            // * Excel中形如Cells[x][y]的写法，前面的数字是列，后面的数字是行!
            // * Excel中的行、列都是从1开始的，而不是0
            //1.制作一个新的Excel文档实例
            Microsoft.Office.Interop.Excel.Application xlsApp = new Microsoft.Office.Interop.Excel.Application();
            xlsApp.Workbooks.Add(true);
            /* 示例输入：需要注意Excel里数组以1为起始（而不是0）
              * for (int i = 1; i < 10; i++)
              * {
              *   for (int j = 1; j < 10; j++)
              *   {
              *     xlsApp.Cells[i][j] = "-"; 
              *   }
              * }
              */
            //2.设置Excel分页卡标题
            xlsApp.ActiveSheet.Name = dt.TableName;
            //3.合并第一行的单元格
            string temp = "";
            if (dt.Columns.Count < 26)
            {
                temp = ((char)('A' + dt.Columns.Count)).ToString();
            }
            else if (dt.Columns.Count <= 26 + 26 * 26)
            {
                temp = ((char)('A' + (dt.Columns.Count - 26) / 26)).ToString()
                  + ((char)('A' + (dt.Columns.Count - 26) % 26)).ToString();
            }
            else throw new Exception("列数过多");
            Microsoft.Office.Interop.Excel.Range range = xlsApp.get_Range("A1", temp + "1");
            range.ClearContents(); //清空要合并的区域
            range.MergeCells = true; //合并单元格
            //4.填写第一行：表名，对应DataTable的TableName
            xlsApp.Cells[1][1] = dt.TableName;
            xlsApp.Cells[1][1].Font.Name = "黑体";
            xlsApp.Cells[1][1].Font.Size = 25;
            xlsApp.Cells[1][1].Font.Bold = true;
            xlsApp.Cells[1][1].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;//居中
            xlsApp.Rows[1].RowHeight = 60; //第一行行高为60（单位：磅）
            //5.合并第二行单元格，用于书写表格生成日期
            range = xlsApp.get_Range("A2", temp + "2");
            range.ClearContents(); //清空要合并的区域
            range.MergeCells = true; //合并单元格
            //6.填写第二行：生成时间
            xlsApp.Cells[1][2] = "报表生成于：" + DateTime.Now.ToString();
            xlsApp.Cells[1][2].Font.Name = "宋体";
            xlsApp.Cells[1][2].Font.Size = 15;
            //xlsApp.Cells[1][2].HorizontalAlignment = 4;//右对齐
            xlsApp.Cells[1][2].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;//居中
            xlsApp.Rows[2].RowHeight = 30; //第一行行高为60（单位：磅）
            //7.填写各列的标题行
            xlsApp.Cells[1][3] = "序号";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                xlsApp.Cells[i + 2][3] = dt.Columns[i].ColumnName;
            }
            xlsApp.Rows[3].Font.Name = "宋体";
            xlsApp.Rows[3].Font.Size = 15;
            xlsApp.Rows[3].Font.Bold = true;
            xlsApp.Rows[3].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;//居中
            //设置颜色
            range = xlsApp.get_Range("A3", temp + "3");
            range.Interior.ColorIndex = 33;
            //8.填写DataTable中的数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                xlsApp.Cells[1][i + 4] = i.ToString();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    xlsApp.Cells[j + 2][i + 4] = dt.Rows[i][j];
                }
            }
            range = xlsApp.get_Range("A4", temp + (dt.Rows.Count + 3).ToString());
            range.Interior.ColorIndex = 37;
            range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;
            //9.描绘边框
            range = xlsApp.get_Range("A1", temp + (dt.Rows.Count + 3).ToString());
            range.Borders.LineStyle = 1;
            range.Borders.Weight = 3;
            //10.打开制作完毕的表格
            //xlsApp.Visible = true;
            //11.保存表格到根目录下指定名称的文件中
            xlsApp.ActiveWorkbook.SaveAs(Application.StartupPath + "/" + addr);
            xlsApp.Quit();
            xlsApp = null;
            GC.Collect();
        }
        #endregion
        #region //最后一次性把数据保存  数据量大，耗费大量时间
        private void btnDataSave_Click(object sender, EventArgs e)
        {
            //if (XRudderSerialPort.IsOpen == true)
            //{
            //    MessageBox.Show("请关闭串口以保存数据！");
            //    return;
            //}
            //dataSaveFileDialog.Filter = " xls files(*.xls)|*.txt|All files(*.*)|*.*";
            ////设置文件名称：
            //dataSaveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + "X舵水下航行器数据表.xls";
            ////设置默认文件类型显示顺序  
            //dataSaveFileDialog.FilterIndex = 0;
            ////保存对话框是否记忆上次打开的目录  
            //dataSaveFileDialog.RestoreDirectory = true;
            //if (dataSaveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    string localFilePath = dataSaveFileDialog.FileName.ToString();
            //    //if(localFilePath == null)
            //    //string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
            //    //MessageBox.Show(FilePath);
            //    DataTabletoExcel(xRudderData, localFilePath);
            //}
        }
        #endregion
        #region //每来一个数据就保存一次，提高效率
        private void btn_dataSave_Click(object sender, EventArgs e)
        {
            if (XRudderSerialPort.IsOpen == true)
            {
                MessageBox.Show("请关闭串口以保存数据！");
                return;
            }
            dataSaveFileDialog.Filter = " xls files(*.xls)|*.txt|All files(*.*)|*.*";
            //设置文件名称：
            dataSaveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd") + "-" + "X舵水下航行器数据表.xls";
            //设置默认文件类型显示顺序  
            dataSaveFileDialog.FilterIndex = 0;
            //保存对话框是否记忆上次打开的目录  
            dataSaveFileDialog.RestoreDirectory = true;
            if (dataSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = dataSaveFileDialog.FileName.ToString();
                //if(localFilePath == null)
                //string FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                //MessageBox.Show(FilePath);
                SaveAs(localFilePath);
                ExcelClose();
            }  
        }

        //创建表头
        private void CreateWorkSheetHead()
        {
            try
            {
                SetCellValue(myWorksheet, 1, 1, "tailRightUpRudder");
                SetCellValue(myWorksheet, 1, 2, "tailRightDownRudder");
                SetCellValue(myWorksheet, 1, 3, "tailLeftUpRudder");
                SetCellValue(myWorksheet, 1, 4, "tailLeftDownRudder");
                SetCellValue(myWorksheet, 1, 5, "rightHydroplane");
                SetCellValue(myWorksheet, 1, 6, "leftHydroplane");
                SetCellValue(myWorksheet, 1, 7, "motorSpeed");
                SetCellValue(myWorksheet, 1, 8, "depth");
                SetCellValue(myWorksheet, 1, 9, "roll");
                SetCellValue(myWorksheet, 1, 10, "pitch");
                SetCellValue(myWorksheet, 1, 11, "yaw");
                SetCellValue(myWorksheet, 1, 12, "liluntailRightUpRudder");
                SetCellValue(myWorksheet, 1, 13, "liluntailRightDownRudder");
                SetCellValue(myWorksheet, 1, 14, "liluntailLeftUpRudder");
                SetCellValue(myWorksheet, 1, 15, "liluntailLeftDownRudder");
                SetCellValue(myWorksheet, 1, 16, "lilunrightHydroplane");
                SetCellValue(myWorksheet, 1, 17, "lilunleftHydroplane");
                SetCellValue(myWorksheet, 1, 18, "lilunmotorSpeed");

                //dc = xRudderData.Columns.Add("liluntailRightUpRudder", System.Type.GetType("System.Single"));       //11
                //dc = xRudderData.Columns.Add("liluntailRightDownRudder", System.Type.GetType("System.Single"));  //12
                //dc = xRudderData.Columns.Add("liluntailLeftUpRudder", System.Type.GetType("System.Single"));    //13   
                //dc = xRudderData.Columns.Add("liluntailLeftDownRudder", System.Type.GetType("System.Single"));  //14
                //dc = xRudderData.Columns.Add("lilunrightHydroplane", System.Type.GetType("System.Single"));     //15
                //dc = xRudderData.Columns.Add("lilunleftHydroplane", System.Type.GetType("System.Single"));      //16
                //dc = xRudderData.Columns.Add("lilunmotorSpeed", System.Type.GetType("System.Single"));      //17
            }
            //try
            //{
            //    myWorksheet.Cells[1][1] = "tailRightUpRudder";
            //}
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void SaveDataToExcel()
        {
            SetCellValue(myWorksheet, excelRow, 1, Convert.ToString(tailRightUpRudder[tailRightUpRudder.Count - 1]));
            SetCellValue(myWorksheet, excelRow, 2, Convert.ToString(tailRightDownRudder[tailRightDownRudder.Count - 1]));
            SetCellValue(myWorksheet, excelRow, 3, Convert.ToString(tailLeftUpRudder[tailLeftUpRudder.Count - 1]));
            SetCellValue(myWorksheet, excelRow, 4, Convert.ToString(tailLeftDownRudder[tailLeftDownRudder.Count - 1]));
            SetCellValue(myWorksheet, excelRow, 5, Convert.ToString(rightHydroplane[rightHydroplane.Count - 1]));
            SetCellValue(myWorksheet, excelRow, 6, Convert.ToString(leftHydroplane[leftHydroplane.Count - 1]));
            SetCellValue(myWorksheet, excelRow, 7, Convert.ToString(motorSpeed[motorSpeed.Count - 1]));
            SetCellValue(myWorksheet, excelRow, 8, Convert.ToString(depth[depth.Count - 1]));
            SetCellValue(myWorksheet, excelRow, 9, Convert.ToString(roll));
            SetCellValue(myWorksheet, excelRow, 10, Convert.ToString(pitch));
            SetCellValue(myWorksheet, excelRow, 11, Convert.ToString(yaw));

            SetCellValue(myWorksheet, excelRow, 12, Convert.ToString(tailRightUpRudderTheory));
            SetCellValue(myWorksheet, excelRow, 13, Convert.ToString(tailRightDownRudderTheory));
            SetCellValue(myWorksheet, excelRow, 14, Convert.ToString(tailLeftUpRudderTheory));
            SetCellValue(myWorksheet, excelRow, 15, Convert.ToString(tailLeftDownRudderTheory));
            SetCellValue(myWorksheet, excelRow, 16, Convert.ToString(rightHydroplaneTheory));
            SetCellValue(myWorksheet, excelRow, 17, Convert.ToString(leftHydroplaneTheory));
            SetCellValue(myWorksheet, excelRow, 18, Convert.ToString(motorSpeedTheory));

            excelRow ++;
        }
        #endregion
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (XRudderSerialPort.IsOpen == true)
            //{
            //    MessageBox.Show("请关闭串口！");
            //    return;
            //}
            ExcelClose();
        }

        #region 打开PID控制面板
        private void btn_control_Click(object sender, EventArgs e)
        {
            PID_change pid_change = new PID_change(this);
            pid_change.Show();
        }

        #endregion

        #region PID参数修改按钮
        private void btnChangePID_Click(object sender, EventArgs e)
        {
            if (txtCtrlKp.Text != "" && txtCtrlKi.Text != "" && txtCtrlKd.Text != "")
            {
                kp_input = Convert.ToSingle(txtCtrlKp.Text);    //输入的kp值
                ki_input = Convert.ToSingle(txtCtrlKi.Text);    //输入的ki值
                kd_input = Convert.ToSingle(txtCtrlKd.Text);    //输入的kd值
            }
            else
                MessageBox.Show("请输入PID控制器控制参数！");
        }
        #endregion

        #region 遥控器控制部分
        //static bool remotebtnfirstin = true;
        private void RemoteBtn_Click(object sender, EventArgs e)
        {
            //if (remotebtnfirstin == true)
            //{
            //    //串口初始化
            //    string[] ports = SerialPort.GetPortNames();
            //    for (int i = 0; i < ports.Length; i++)
            //    {
            //        if (ports[i] != XRudderSerialPort.PortName)
            //        {
            //            RemoteSerialPort.PortName = ports[i];
            //            RemoteSerialPort.BaudRate = 115200;
            //            RemoteSerialPort.DataBits = 8;
            //            RemoteSerialPort.StopBits = StopBits.One;
            //            RemoteSerialPort.Parity = Parity.None;
            //        }
            //    }
            //    remotebtnfirstin = false;
            //}
            //打开串口
            if (RemoteSerialPort.IsOpen)
            {
                RemoteSerialPort.Close();
            }
            else
            {
                //电脑只能外接两个串口，其中一个用于遥控器
                string[] ports = SerialPort.GetPortNames();
                for (int i = 0; i < ports.Length; i++)
                {
                    if (ports[i] != XRudderSerialPort.PortName)
                    {
                        RemoteSerialPort.PortName = ports[i];
                        RemoteSerialPort.BaudRate = 115200;
                        RemoteSerialPort.DataBits = 8;
                        RemoteSerialPort.StopBits = StopBits.One;
                        RemoteSerialPort.Parity = Parity.None;
                    }
                }
                RemoteSerialPort.Open();
            }
            RemoteBtn.Text = RemoteSerialPort.IsOpen ? "关闭遥控" : "开启遥控";
            //if(RemoteSerialPort.IsOpen)
            //    ctlData[33] = 0xff;     //快速控制、慢速控制标志位：0x00为慢速控制；0xff为快速控制
            //else if(!RemoteSerialPort.IsOpen)
            //    ctlData[33] = 0x00;   //快速控制、慢速控制标志位：0x00为慢速控制；0xff为快速控制
        }

        private void RemoteSerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int n = 0;
                while (n < 18)
                {
                    if (RemoteSerialPort.IsOpen == false)
                    {
                        return;
                    }
                    n = RemoteSerialPort.BytesToRead;
                }
                //开始接收数据
                byte[] byteRead = new byte[18];
                RemoteSerialPort.Read(byteRead, 0, byteRead.Length);
                RemoteSerialPort.DiscardInBuffer();
                if (byteRead[0] == 0x5A)
                {
                    SerialPortDataSend(XRudderSerialPort, byteRead);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                RemoteSerialPort.Close();
            }
        }

        void SerialPortDataRecv(SerialPort serialPortDataRecv, int recvByteNum)
        {
            try
            {
                int n = 0;
                while (n < recvByteNum)
                {
                    if (serialPortDataRecv.IsOpen == false)
                    {
                        return;
                    }
                    n = serialPortDataRecv.BytesToRead;
                }
                //开始接收数据
                byte[] byteRead = new byte[recvByteNum];
                serialPortDataRecv.Read(byteRead, 0, byteRead.Length);
                serialPortDataRecv.DiscardInBuffer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
                serialPortDataRecv.Close();
            } 
        }

        void SerialPortDataSend(SerialPort remoteDataSend,byte[] remoteData)
        {
            float tail_r_Angle = SingleFloatDataConvert(remoteData, 1);
            float tail_s_Angle = SingleFloatDataConvert(remoteData, 5);
            float horiz_Angle = SingleFloatDataConvert(remoteData, 9);
            float motorSpeed = SingleFloatDataConvert(remoteData, 13);

            if (dive_mode_flag == true)
            {
                ten2x_Rudders(tail_r_Angle, tail_s_Angle, horiz_Angle, motorSpeed);     //十字舵与X舵转换
                SerialPortDataSend();       //发送串口数据
                return;
            }

            standard_Yaw_change = tailAngle_yaw_converter * tail_r_Angle;
            if (motorSpeed > 500)
            {
                straight_naiv = true;
                //standard_Yaw_change = 180.0f / 35.0f * tail_r_Angle;
                init_straight_naiv();
            }
            else
            {
                straight_naiv = false;
                tail_r_Angle = Convert.ToSingle(0);
                tail_s_Angle = Convert.ToSingle(0);
                horiz_Angle = Convert.ToSingle(0);
                motor_Speed = Convert.ToSingle(0);  
            }
            ten2x_Rudders(tail_r_Angle, tail_s_Angle, horiz_Angle, motorSpeed);     //十字舵与X舵转换
            SerialPortDataSend();       //发送串口数据
        }
        #endregion

        #region 控制算法部分
        #region 直线航行
        private void btn_straight_Click(object sender, EventArgs e)
        {
            straight_naiv = !straight_naiv;
            btn_straight.Text = straight_naiv ? "停止直航" : "直线航行";

            if (straight_naiv)
            {
                init_straight_naiv();
            }  
            else 
            {
                tail_r_Angle = Convert.ToSingle(0);
                tail_s_Angle = Convert.ToSingle(0);
                horiz_Angle = Convert.ToSingle(0);
                motor_Speed = Convert.ToSingle(0);  
            }
            ten2x_Rudders(tail_r_Angle, tail_s_Angle, horiz_Angle, motor_Speed);    //十字舵与X舵转换
            SerialPortDataSend();       //发送串口数据
        }
        //直线航行 初始化配置
        public void init_straight_naiv()
        {
            e_k = 0.0f;      //当前误差
            e_k_1 = 0.0f;    //上一次误差
            e_k_2 = 0.0f;    //上两次误差
            last_tail_r_Angle = 0.0f;    //上次方向舵角度

            kp_input = Convert.ToSingle(txtCtrlKp.Text);    //输入的kp值
            ki_input = Convert.ToSingle(txtCtrlKi.Text);    //输入的ki值
            kd_input = Convert.ToSingle(txtCtrlKd.Text);    //输入的kd值
            kp = kp_input;
            ki = ki_input;
            kd = kd_input;
            A = kp + ki + kd; //pid控制器系数
            B = kp + 2 * kd;    //pid控制器系数
            C = kd;   //pid控制器系数

            standard_Yaw = yaw + standard_Yaw_change;//初始角度标定

            //初始控制指令
            tail_r_Angle = last_tail_r_Angle;
            tail_s_Angle = Convert.ToSingle(txt_Tail_s.Text);
            horiz_Angle = Convert.ToSingle(txt_HorizRudder.Text);
            motor_Speed = Convert.ToSingle(txt_MotorSpeed.Text);
        }
        #endregion

        #region   卡舵
        private void btnWorkLeftUp_Click(object sender, EventArgs e)
        {
            btnWorkLeftUp.Enabled = false;
            btnStopLeftUp.Enabled = true;
            leftUpRudder = true;
            ctlData[33] = 0x00;
            SerialPortDataSend();       //发送串口数据
        }
        private void btnStopLeftUp_Click(object sender, EventArgs e)
        {
            btnWorkLeftUp.Enabled = true;
            btnStopLeftUp.Enabled = false;
            leftUpRudder = false;
            ctlData[33] = 0xFF;
            SerialPortDataSend();       //发送串口数据
        }

        private void btnWorkRightUp_Click(object sender, EventArgs e)
        {
            btnWorkRightUp.Enabled = false;
            btnStopRightUp.Enabled = true;
            rightUpRudder = true;
            ctlData[34] = 0x00;
            SerialPortDataSend();       //发送串口数据
        }
        private void btnStopRightUp_Click(object sender, EventArgs e)
        {
            btnWorkRightUp.Enabled = true;
            btnStopRightUp.Enabled = false;
            rightUpRudder = false;
            ctlData[34] = 0xFF;
            SerialPortDataSend();       //发送串口数据
        }

        private void btnWorkLeftDown_Click(object sender, EventArgs e)
        {
            btnWorkLeftDown.Enabled = false;
            btnStopLeftDown.Enabled = true;
            leftDownRudder = true;
            ctlData[35] = 0x00;
            SerialPortDataSend();       //发送串口数据
        }
        private void btnStopLeftDown_Click(object sender, EventArgs e)
        {
            btnWorkLeftDown.Enabled = true;
            btnStopLeftDown.Enabled = false;
            leftDownRudder = false;
            ctlData[35] = 0xFF;
            SerialPortDataSend();       //发送串口数据
        }

        private void btnWorkRightDown_Click(object sender, EventArgs e)
        {
            btnWorkRightDown.Enabled = false;
            btnStopRightDown.Enabled = true;
            rightDownRudder = true;
            ctlData[36] = 0x00;
            SerialPortDataSend();       //发送串口数据
        }
        private void btnStopRightDown_Click(object sender, EventArgs e)
        {
            btnWorkRightDown.Enabled = true;
            btnStopRightDown.Enabled = false;
            rightDownRudder = false;
            ctlData[36] = 0xFF;
            SerialPortDataSend();       //发送串口数据
        }
        #endregion



        #endregion
    }
}
