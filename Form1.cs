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
        Timer _testTimer = new Timer();

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
                    _testTimer.Interval = 200;
                    _testTimer.Enabled = true;
                    _testTimer.Tick += _testTimer_Tick;
                }
            }
        }

        void CommonConstructor()
        {
            InitializeComponent();

            this.TransparencyKey = Color.Black;

            _comPort = Settings.Default.comport;
            _mirrored = Settings.Default.mirror;

            if (_mirrored)
            {
                barStaticItemMirrorLabel.Caption = "Mirror On";
                barToggleSwitchItem1.Checked = true;
            }
            else
            {
                barStaticItemMirrorLabel.Caption = "Mirror Off";
                barToggleSwitchItem1.Checked = false;
            }

            //SetMirrorState();
            barEditItem4.EditValue = _comPort;
            serialPort2.DataReceived += new SerialDataReceivedEventHandler(serialPort2_DataReceived);
            serialPort2.DtrEnable = true;

            wpfGuageControl1.mirrorBtn.Click += MirrorBtn_Clicked;
            wpfGuageControl1.zeroBtn.Click += ZeroBtn_Click;
            wpfGuageControl1.btnClose.Click += BtnClose_Click;

            wpfGuageControl1.popupMenuItemMirror.ItemClick += MirrorBtn_Clicked;
            wpfGuageControl1.popupMenuItemZero.ItemClick += ZeroBtn_Click;
            wpfGuageControl1.popupMenuItemClose.ItemClick += BtnClose_Click;

            wpfGuageControl1.popupMenuItemShowMenu.ItemClick += PopupMenuItemShowMenu_ItemClick;

            _rotateTransform = new RotateTransform();
            wpfGuageControl1.circularGuageControl.RenderTransform = this._rotateTransform;

            this.ResizeRedraw = true;

        }

        private void PopupMenuItemShowMenu_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            ShowMenus(!bar2.Visible);
           
        }

        void ShowMenus(bool visible)
        {
            if(!visible)
            {
                bar2.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;
                wpfGuageControl1.popupMenuItemShowMenu.Content = "Show Menu";
                wpfGuageControl1.btnClose.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                bar2.Visible = true;
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                wpfGuageControl1.popupMenuItemShowMenu.Content = "Hide Menu";
                wpfGuageControl1.btnClose.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void BtnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.Close();
        }

        private void MirrorBtn_Clicked(object sender, System.Windows.RoutedEventArgs e)
        {
            SetMirrorState();
        }

        private void ZeroBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Zero();
        }

        private void _testTimer_Tick(object sender, EventArgs e)
        {
            _angle++;
            if(_angle > 360)
            {
                _angle = 0;
            }
            LineReceived(Angle.ToString() + " " + 1);
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
                wpfGuageControl1.arcScaleControl.StartAngle = -90;
                wpfGuageControl1.arcScaleControl.EndAngle = 270;
                _mirrored = false;
                LineReceived(_angle.ToString() + " " + 1);
            }
            else
            {
                wpfGuageControl1.arcScaleControl.StartAngle = 270;
                wpfGuageControl1.arcScaleControl.EndAngle = -90;
                _mirrored = true;
                LineReceived(_angle.ToString() + " " + 1);
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

            if (_rotateTransform == null)
                return;

            float needleAngle = 0.0f;


            if (_mirrored)
            {
                _rotateTransform.Angle = angle;
                needleAngle = 360 - angle;
            }
            else
            {
                _rotateTransform.Angle = -angle;
                needleAngle = 360-angle;
            }

            wpfGuageControl1.needle.Value = angle;

            string labelString = "";
            if (angle < 10)
                labelString = "00";
            else if (angle < 100)
                labelString = "0";

            wpfGuageControl1.lblAngle.Text = labelString + angle.ToString("N1") + " deg";

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
            _angle = 0;
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
            wpfGuageControl1.InvalidateVisual();
            elementHost1.Invalidate();
            elementHost1.Update();
            this.Update();
        }

        private void barCheckItem1_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (_testTimer.Enabled)
            {
                _testTimer.Enabled = false;
            }
            else
            {
                _testTimer.Enabled = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.FormBorderStyle == FormBorderStyle.None)
                Properties.Settings.Default.MenuVisible = false;
            else
                Properties.Settings.Default.MenuVisible = true;

            if (WindowState == FormWindowState.Maximized)
            {
                Properties.Settings.Default.Location = RestoreBounds.Location;
                Properties.Settings.Default.Size = RestoreBounds.Size;
            }
            else if (WindowState == FormWindowState.Normal)
            {
                Properties.Settings.Default.Location = Location;
                Properties.Settings.Default.Size = Size;
            }
            else
            {
                Properties.Settings.Default.Location = RestoreBounds.Location;
                Properties.Settings.Default.Size = RestoreBounds.Size;
            }
            Properties.Settings.Default.Save();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ShowMenus(Settings.Default.MenuVisible);

            // Set window location
            this.Location = Settings.Default.Location;
            
            // Set window size
            this.Size = Settings.Default.Size;  
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            wpfGuageControl1.InvalidateVisual();
            elementHost1.Invalidate();
            elementHost1.Update();
            this.Update();
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            wpfGuageControl1.InvalidateVisual();
            elementHost1.Invalidate();
            elementHost1.Update();
            this.Update();
        }
    }
}
