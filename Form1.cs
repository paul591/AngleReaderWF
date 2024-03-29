﻿using AngleReaderWF.Properties;
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
using System.Collections.ObjectModel;
using DevExpress.Xpf.Editors.Themes;

namespace AngleReaderWF
{

    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public float Angle
        {
            get { return _angle; }
        }

        public float RPM
        {
            get { return _rpm; }
        }

        private bool _mouseDown;
        private Point _lastLocation;
        public float _angle = 0.0f;
        public float _rpm = 0.0f;
        bool _mirrored = false;
        bool _testMode = false;
        Timer _testTimer = new Timer();
        RotateTransform _rotateTransform;
        SettingsDialog _settingsDialog;
        public Preferences _preferences;

        MessageQueue _messageQueue;
        Log _log = new Log();

        Timer _rpmTimeoutTimer;

        public Form1()
        {
            CommonConstructor();
        }

        public Form1(string[] args)
        {
            CommonConstructor();
        }

        void CommonConstructor()
        {
            InitializeComponent();

            this.TransparencyKey = Color.Black;

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

            serialPort2.DataReceived += new SerialDataReceivedEventHandler(serialPort2_DataReceived);
            serialPort2.DtrEnable = true;

            wpfGuageControl1.mirrorBtn.Click += MirrorBtn_Clicked;
            wpfGuageControl1.zeroBtn.Click += ZeroBtn_Click;
            wpfGuageControl1.btnClose.Click += BtnClose_Click;

            wpfGuageControl1.popupMenuItemMirror.ItemClick += MirrorBtn_Clicked;
            wpfGuageControl1.popupMenuItemZero.ItemClick += ZeroBtn_Click;
            wpfGuageControl1.popupMenuItemClose.ItemClick += BtnClose_Click;
            wpfGuageControl1.popupMenuItemSettings.ItemClick += PopupMenuItemSettings_ItemClick;

            wpfGuageControl1.popupMenuItemShowMenu.ItemClick += PopupMenuItemShowMenu_ItemClick;

            _rotateTransform = new RotateTransform();
            wpfGuageControl1.circularGuageControl.RenderTransform = this._rotateTransform;

            _preferences = new Preferences();
            _preferences.ZeroValue = Settings.Default.ZeroValue;
            _preferences.COMPort = Settings.Default.comport;

            _messageQueue = new MessageQueue();
            _messageQueue.PropertyChanged += _messageQueue_MessageAdded;

            _rpmTimeoutTimer = new Timer();
            _rpmTimeoutTimer.Tick += _rpmTimeoutTimer_Tick;
            _rpmTimeoutTimer.Interval = 2000;
            _rpmTimeoutTimer.Enabled = true;
            _rpmTimeoutTimer.Stop();
        }

        private void _rpmTimeoutTimer_Tick(object sender, EventArgs e)
        {
            //RPM Timeout of 1000ms has elapsed, so we'll just reset the 
            //rpm label to 0, so as not to leave it set at a weird number
            _rpm = 0;
            wpfGuageControl1.lblRPM.Text = "0000";
            _rpmTimeoutTimer.Stop();
        }

        private void _messageQueue_MessageAdded(object sender, PropertyChangedEventArgs e)
        {
            while (_messageQueue._messageQueue.Count > 0)
            {
                if (serialPort2 != null && serialPort2.IsOpen)
                {
                    serialPort2.Write(_messageQueue._messageQueue[0].MessageText);
                }
                _messageQueue._messageQueue.RemoveAt(0);
            }
        }

        private void PopupMenuItemSettings_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            this.TopMost = false;
            _settingsDialog = new SettingsDialog(_preferences, _log, _messageQueue);
            if(_settingsDialog.ShowDialog() == DialogResult.OK)
            {
                Settings.Default.ZeroValue = _preferences.ZeroValue;
                Settings.Default.comport = _preferences.COMPort;
                Settings.Default.Save();

                OpenComPort();
            }
            this.TopMost = true;
        }

        private void PopupMenuItemShowMenu_ItemClick(object sender, DevExpress.Xpf.Bars.ItemClickEventArgs e)
        {
            ShowMenus(this.FormBorderStyle == FormBorderStyle.None);
        }

        void ShowMenus(bool visible)
        {
            if (!visible)
            {
                this.FormBorderStyle = FormBorderStyle.None;
                wpfGuageControl1.popupMenuItemShowMenu.Content = "Show Title Bar";
                wpfGuageControl1.btnClose.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                wpfGuageControl1.popupMenuItemShowMenu.Content = "Hide Title Bar";
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

        void OpenComPort()
        {
            if (!_testMode)
            {
                if (serialPort2 != null)
                {
                    serialPort2.Close();
                    serialPort2.BaudRate = 115200;
                    if (!_preferences.COMPort.StartsWith("COM"))
                    {
                        MessageBox.Show("No COM Port selected.  Please plug in the device and select the correct port.", "No COM Port");
                    }
                    else
                    {
                        serialPort2.PortName = _preferences.COMPort;

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
            _log.AddLogEntry(line);

            // Split the incoming line.  If this is data, then the first character will be a 'D' 
            // followed by the parameteres in the format:
            // D ang cnt rpm
            string[] strings = line.Split(new char[] { '\r', ' ' });

            //Check if its a data packet.
            if (strings[0] == "D")
            {
                //Yup.  So, lets get the angle from the parameters
                float.TryParse(strings[1], out _angle);

                double angleForModulus = _angle * 10;
                long longAngleForModuls = ((long)angleForModulus) % 3600;
                float finalAngle = ((float)longAngleForModuls) / 10.0f;
                if (finalAngle < 0)
                {
                    finalAngle = 360 + finalAngle;
                }

                float rpm = 0.0f;
                //We dont really care abou the actual count at the moment so we will
                //skip over that one, and now go and get the RPM...
                float.TryParse(strings[3], out rpm);

                SetAngleAndRpm(finalAngle, (int)rpm);
            }
            else if (strings[0] == "S")
            {
                if (strings.Length == 5)
                {
                    int result = 0;
                    if (int.TryParse(strings[1], out result))
                    {
                        _preferences.PulsePerRev = result;
                    }

                    if (int.TryParse(strings[2], out result))
                    {
                        _preferences.FilterDepth = result;
                    }

                    if (int.TryParse(strings[3], out result))
                    {
                        _preferences.LoopInterval = result;
                    }
                }

            }
        }


        void SetAngleAndRpm(float angle, int rpm)
        {
            _rpmTimeoutTimer.Stop();
            _rpmTimeoutTimer.Start();

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
                needleAngle = 360 - angle;
            }

            wpfGuageControl1.needle.Value = angle;

            string labelString = "";
            if (angle < 10)
                labelString = "00";
            else if (angle < 100)
                labelString = "0";

            wpfGuageControl1.lblAngle.Text = labelString + angle.ToString("N1") + "°";

            if (rpm < 10)
                labelString = "000";
            else if (rpm < 100)
                labelString = "00";
            else if (rpm < 1000)
                labelString = "0";
               
            wpfGuageControl1.lblRPM.Text = labelString + rpm.ToString("N0");

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

        void Zero()
        {
            if (serialPort2 != null && serialPort2.IsOpen)
            {
                if (_preferences != null)
                {
                    serialPort2.WriteLine("R" + _preferences.ZeroValue);
                }
                else
                {
                    serialPort2.WriteLine("R");
                }
            }
            if (_preferences != null)
            {
                _angle = _preferences.ZeroValue;
            }
            _rpm = 0;
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


        private void Form1_Shown(object sender, EventArgs e)
        {

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

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
            this.elementHost1.Invalidate();
            wpfGuageControl1.InvalidateVisual();
        }
    }
}
