using AngleReaderWF.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using DevExpress.XtraEditors;
using DevExpress.XtraGauges.Core.Drawing;
using DevExpress.XtraBars;
using Timer = System.Windows.Forms.Timer;
using System.Windows.Media;
using Color = System.Drawing.Color;
using DevExpress.Xpf.Gauges;

namespace AngleReaderWF
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {

        enum DisplayMode
        {
            Needle = 0,
            Dial = 1,
        }

        public float Angle
        {
            get { return _angle; }
        }

        private bool _mouseDown;
        private Point _lastLocation;
        private String _comPort = "None";
        public float _angle = 0.0f;
        bool _mirrored = false;
        bool _testMode = false;
        bool _wpfMode = false;
        Timer _testTimer;
        DisplayMode _displayMode = DisplayMode.Needle;
        WPFGuageControl _wpfGuageControl;
        RotateTransform _rotateTransform;

        public Form1()
        {
            CommonConstructor();
        }

        public Form1(string[] args)
        {
            CommonConstructor();

            if (args.Length > 0)
            {
                if (args[0] == "TEST")
                {
                    _testMode = true;
                    _testTimer.Interval = 20;
                    _testTimer.Enabled = true;
                }
                else if (args[0] == "WPFTEST")
                {
                    _testMode = true;
                    _testTimer.Interval = 20;
                    _testTimer.Enabled = true;
                    _wpfMode = true; 
                }

            }

        }

        void CommonConstructor()
        {
            InitializeComponent();

            ComplexShader needleShader = this.arcScaleNeedleComponent1.Shader as ComplexShader;
            needleShader.StyleColor1 = Color.FromArgb(0, 103, 192);

            this.TransparencyKey = Color.Black;

            _comPort = Settings.Default.comport;
            _mirrored = Settings.Default.mirror;
            _displayMode = (DisplayMode)Settings.Default.mode;

            //override
            _displayMode = DisplayMode.Dial;

            SetMirrorState();
            barEditItem4.EditValue = _comPort;
            serialPort2.DataReceived += new SerialDataReceivedEventHandler(serialPort2_DataReceived);
            serialPort2.DtrEnable = true;

            _testTimer = new Timer();
            _testTimer.Tick += _testTimer_Tick;

            //_wpfMode = true;

            if(_wpfMode)
            {
                _rotateTransform = new RotateTransform();
                wpfGuageControl1.circularGuageControl.RenderTransform = this._rotateTransform;            
            }
            else
            {
                elementHost1.Hide();
                gaugeControl1.Show();
            }
        }

        private void _testTimer_Tick(object sender, EventArgs e)
        {
            _angle++;
            if(_angle > 360)
            {
                _angle = 0;
            }
            SetAngle(_angle);
        }

        void OpenComPort()
        {
            if (!_testMode)
            {
                if (serialPort2 != null)
                {
                    serialPort2.Close();
                    serialPort2.BaudRate = 115200;
                    if (!_comPort.StartsWith("COM"))
                    {
                        MessageBox.Show("No COM Port selected.  Please plug in the device and select the correct port.", "No COM Port");
                    }
                    else
                    {
                        serialPort2.PortName = _comPort;

                        try
                        {
                            serialPort2.Open();
                            Zero();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "COM Port error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        void SetMirrorState()
        {
            if (_mirrored)
            {
                _mirrored = false;
                barStaticItemMirrorLabel.Caption = "Mirror On";
                arcScaleComponent1.StartAngle =  270;
                arcScaleComponent1.EndAngle = -90;
            }
            else
            {
                _mirrored = true;
                barStaticItemMirrorLabel.Caption = "Mirror Off";
                arcScaleComponent1.StartAngle = -90;
                arcScaleComponent1.EndAngle = 270;
            }

            Settings.Default.mirror = _mirrored;
            Settings.Default.Save();
        }

        private void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs args)
        {
            string line = serialPort2.ReadLine();
            this.BeginInvoke(new LineReceivedEvent(LineReceived), line);
        }

        private delegate void LineReceivedEvent(string line);
        private void LineReceived(string line)
        {
            string[] strings = line.Split(new char[] { '\r', ' ' });
            float.TryParse(strings[0], out _angle);

            double angleForModulus = _angle * 10;
            long longAngleForModuls = ((long)angleForModulus) % 3600;
            float finalAngle = ((float)longAngleForModuls) / 10.0f;
            if (finalAngle < 0)
            {
                finalAngle = 360 + finalAngle;
            }

            Console.WriteLine(longAngleForModuls);

            SetAngle(finalAngle);
        }


        void SetAngle(float angle)
        {
            if (_displayMode == DisplayMode.Needle)
            {
                if (_wpfMode)
                {
                    
                }
                else
                {

                    arcScaleNeedleComponent1.Value = angle + 90;
                    labelComponent1.Text = angle.ToString("N1") + " deg";
                }
            }
            else
            {
                if (_wpfMode)
                {
                    wpfGuageControl1.arcScaleControl.EndAngle = angle;
                    wpfGuageControl1.arcScaleControl.StartAngle = angle + 360;
                    //_rotateTransform.Angle = (double)angle;
                    float needleAngle = angle + 90;
                    if(needleAngle > 360)
                    {
                        needleAngle -= 360;
                    }
                    wpfGuageControl1.needle.Value = (needleAngle);

                }
                else
                {
                    arcScaleComponent1.EndAngle = angle;
                    arcScaleComponent1.StartAngle = angle + 360;
                    arcScaleNeedleComponent1.Value = angle + 90;
                }
            }
        }
        
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDown = true;
            _lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - _lastLocation.X) + e.X, (this.Location.Y - _lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDown = false;
        }

        private void barEditItem4_ShowingEditor(object sender, DevExpress.XtraBars.ItemCancelEventArgs e)
        {
            repositoryItemComboBox2.Items.Clear();


            foreach (string s in SerialPort.GetPortNames())
            {
                repositoryItemComboBox2.Items.Add(s);
            }
            if (repositoryItemComboBox2.Items.Count > 0)
            {
                barEditItem4.EditValue = repositoryItemComboBox2.Items[0].ToString();
            }
            else
            {
                barEditItem4.EditValue = "No COM Ports";
            }
        }

        private void barEditItem4_EditValueChanged(object sender, EventArgs e)
        {
            _comPort = barEditItem4.EditValue.ToString();
            Settings.Default.comport = _comPort;
            Settings.Default.Save();

            OpenComPort();
        }

        void Zero()
        {
            if (serialPort2 != null && serialPort2.IsOpen)
            {
                serialPort2.WriteLine("R");
            }
        }
        private void barToggleSwitchItem1_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetMirrorState();
        }

        private void barEditItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Zero();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //tidy up the serial port

            serialPort2.Close();
            serialPort2.Dispose();
        }

        private void barEditItem4_HiddenEditor(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_testMode)
            {
                _comPort = (sender as BarEditItem).EditValue.ToString();
                if (serialPort2.IsOpen)
                {
                    serialPort2.Close();
                    serialPort2.PortName = _comPort;
                    OpenComPort();

                    Settings.Default.comport = _comPort;
                    Settings.Default.Save();
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }
    }
}
