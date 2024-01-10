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

namespace AngleReaderWF
{
    public partial class Form1 : Form
    {

        private bool mouseDown;
        private Point lastLocation;
        private String comPort = "None";
        float angle = 0.0f;
        bool mirrored = false;

        public Form1()
        {
            InitializeComponent();
            this.TransparencyKey = Color.Black;

            comPort = Settings.Default.comport;
            mirrored = Settings.Default.mirror;

            SetMirrorState();
            barEditItem4.EditValue = comPort;
            serialPort2.DataReceived += new SerialDataReceivedEventHandler(serialPort2_DataReceived);
            serialPort2.DtrEnable = true;
        }


        void SetMirrorState()
        {
            if (mirrored)
            {
                mirrored = false;
                barStaticItemMirrorLabel.Caption = "Mirror On";
                arcScaleComponent1.StartAngle =  270;
                arcScaleComponent1.EndAngle = -90;
            }
            else
            {
                mirrored = true;
                barStaticItemMirrorLabel.Caption = "Mirror Off";
                arcScaleComponent1.StartAngle = -90;
                arcScaleComponent1.EndAngle = 270;
            }

            Settings.Default.mirror = mirrored;
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
            string[] strings = line.Split(new char[] {'\r', ' '});
            float.TryParse(strings[0], out angle);

            double angleForModulus = angle * 10;
            long longAngleForModuls = ((long)angleForModulus) % 3600;
            float finalAngle = ((float)longAngleForModuls) / 10.0f;

            Console.WriteLine(longAngleForModuls);

            arcScaleNeedleComponent1.Value = finalAngle;
            labelComponent1.Text = finalAngle.ToString("N1") + " deg";
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
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
            comPort = barEditItem4.EditValue.ToString();
            Settings.Default.comport = comPort;
            Settings.Default.Save();

            if (serialPort2 != null)
            {
                serialPort2.Close();
                serialPort2.PortName = comPort;
                try
                {
                    serialPort2.Open();
                    Zero();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("COM Port already in use or not available.", "COM Port error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
    }
}