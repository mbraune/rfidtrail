﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using System.Text.RegularExpressions;
using System.IO.Ports;


namespace trail01
{
    public struct EventLog
    {
        public DateTime ts;
        public int devTyp, devId, channel, value;

        public EventLog(DateTime time, int p1, int p2, int p3, int p4)
        {
            ts = time;
            devTyp = p1;
            devId = p2;
            channel = p3;
            value = p4;
        }
    };

    enum MachineState
    {
        PowerOff    = 0,
        Initialized = 5,
        PortsOpen   = 10,
        Running     = 15,
    }


    public partial class Form1 : Form
    {
        public SerialPort _serialPort0;
        public SerialPort _serialPort1;
        public SerialPort _serialPort2;
        public SerialPort _serialPort3;
        public SerialPort _serialPort4;
        public SerialPort _serialPort5;
        public SerialPort _serialPort6;
        public SerialPort _serialPort7;
        public SerialPort _serialPort8;
        public SerialPort _serialPort9;

        //MachineState state = MachineState.PowerOff;

        private void SetState(MachineState _state)
        {
            switch (_state)
            {
                case MachineState.PowerOff:
                    // reset all
                    emptyAllDevicesLabel();
                    DeviceList.Clear();
                    closeAllPorts();
                    startAsyncButton.BackColor = Color.FromName("Gray");
                    cancelAsyncButton.Enabled = false;
                    buttonDevices.Enabled = true;
                    break;
                case MachineState.Initialized:
                    buttonDevices.Enabled = false;
                    buttonOpen.Enabled = true;
                    break;
                case MachineState.PortsOpen:
                    buttonOpen.Enabled = false;
                    startAsyncButton.Enabled = true;
                    break;
                case MachineState.Running:
                    startAsyncButton.BackColor = Color.FromName("PaleGreen");
                    startAsyncButton.Enabled = false;
                    cancelAsyncButton.Enabled = true;
                    break;
            }
        }

        public Form1()
        {
            InitializeComponent();

            // get the version object for this assembly
            Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            labelVersion.Text = "version " + v.ToString();

            _serialPort0 = new SerialPort();
            _serialPort1 = new SerialPort();
            _serialPort2 = new SerialPort();
            _serialPort3 = new SerialPort();
            _serialPort4 = new SerialPort();
            _serialPort5 = new SerialPort();
            _serialPort6 = new SerialPort();
            _serialPort7 = new SerialPort();
            _serialPort8 = new SerialPort();
            _serialPort9 = new SerialPort();

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
        }

        private string GetVCP_COMPort(ManagementObject Device)
        {
            try
            {
                return new Regex(@".*\((COM[0-9]+)\).*").Match((string)Device["Name"]).Groups[1].Value;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return string.Empty;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Trace.TraceInformation("Form1_Load");
            SetState(MachineState.PowerOff);
        }

        private void buttonDevices_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Click Devices");
            var searcher = new ManagementObjectSearcher("Select * from Win32_PnPEntity");
            var coll = searcher.Get();

            foreach (ManagementObject o in coll)
            {
                Console.WriteLine((string)o["Description"]);
                if (((string)o["DeviceID"]).Contains("FTDIBUS"))
                {
                    Console.WriteLine((string)o["Name"]);
                    // extract COM Port
                    string device = (string)o["DeviceID"];
                    Trace.WriteLine("found" + device);
                    string pattern = "(.*PID_6001\\+)";
                    string sTmp = Regex.Replace(device, pattern, String.Empty);
                    string devid = sTmp.Substring(0, 4);  // serial number is S001, S002, ..
                    string port = GetVCP_COMPort(o);
                    Trace.WriteLine("found devid " + devid + "  at " + port);
                    //Console.WriteLine((string)o["DeviceID"]);
                    //Console.WriteLine((string)o["PNPDeviceID"]);
                    //Console.WriteLine("VCP ComPort " + GetVCP_COMPort(o));

                    FtdiDevice ftdev = new FtdiDevice(devid, port, false);
                    DeviceList.Record(ftdev);
                }
            }

            for (var i = 0; i < DeviceList.Count(); i++)
            {
                switch (i)
                {
                    case 0:
                        labelNum0.Text = DeviceList.GetContent()[i].s_id;
                        labelPort0.Text = DeviceList.GetContent()[i].s_com;
                        break;
                    case 1:
                        labelNum1.Text = DeviceList.GetContent()[i].s_id;
                        labelPort1.Text = DeviceList.GetContent()[i].s_com;
                        break;
                    case 2:
                        labelNum2.Text = DeviceList.GetContent()[i].s_id;
                        labelPort2.Text = DeviceList.GetContent()[i].s_com;
                        break;
                    case 3:
                        labelNum3.Text = DeviceList.GetContent()[i].s_id;
                        labelPort3.Text = DeviceList.GetContent()[i].s_com;
                        break;
                    case 4:
                        labelNum4.Text = DeviceList.GetContent()[i].s_id;
                        labelPort4.Text = DeviceList.GetContent()[i].s_com;
                        break;
                    case 5:
                        labelNum5.Text = DeviceList.GetContent()[i].s_id;
                        labelPort5.Text = DeviceList.GetContent()[i].s_com;
                        break;
                    case 6:
                        labelNum6.Text = DeviceList.GetContent()[i].s_id;
                        labelPort6.Text = DeviceList.GetContent()[i].s_com;
                        break;
                    case 7:
                        labelNum7.Text = DeviceList.GetContent()[i].s_id;
                        labelPort7.Text = DeviceList.GetContent()[i].s_com;
                        break;
                    case 8:
                        labelNum8.Text = DeviceList.GetContent()[i].s_id;
                        labelPort8.Text = DeviceList.GetContent()[i].s_com;
                        break;
                    case 9:
                        labelNum9.Text = DeviceList.GetContent()[i].s_id;
                        labelPort9.Text = DeviceList.GetContent()[i].s_com;
                        break;
                    default:
                        break;
                }
            }
            coll.Dispose();
            SetState(MachineState.Initialized);
        }

        private void buttonOpen1_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < DeviceList.Count(); i++)
            {
                switch (i)
                {
                    case 0:
                        if (openPort(0, _serialPort0)) {
                            label0.BackColor = Color.FromName("LightSteelBlue");
                        }
                        break;
                    case 1:
                        if (openPort(1, _serialPort1)) {
                            label1.BackColor = Color.FromName("LightSteelBlue");
                        }
                        break;
                    case 2:
                        if (openPort(2, _serialPort2)) {
                            label2.BackColor = Color.FromName("LightSteelBlue");
                        }
                        break;
                    case 3:
                        if (openPort(3, _serialPort3)) {
                            label3.BackColor = Color.FromName("LightSteelBlue");
                        }
                        break;
                    case 4:
                        if (openPort(4, _serialPort4)) {
                            label4.BackColor = Color.FromName("LightSteelBlue");
                        }
                        break;
                    case 5:
                        if (openPort(5, _serialPort5)) {
                            label5.BackColor = Color.FromName("LightSteelBlue");
                        }
                        break;
                    case 6:
                        if (openPort(6, _serialPort6)) {
                            label6.BackColor = Color.FromName("LightSteelBlue");
                        }
                        break;
                    case 7:
                        if (openPort(7, _serialPort7)) {
                            label7.BackColor = Color.FromName("LightSteelBlue");
                        }
                        break;
                    case 8:
                        if (openPort(8, _serialPort8)) {
                            label8.BackColor = Color.FromName("LightSteelBlue");
                        }
                        break;
                    case 9:
                        if (openPort(9, _serialPort9)) {
                            label9.BackColor = Color.FromName("LightSteelBlue");
                        }
                        break;

                    default:
                        break;
                }
            }
            SetState(MachineState.PortsOpen);
        }

        private void startAsyncButton_Click(object sender, EventArgs e)
        {
            FileLogger.Reset();
            resultLabel.Text = FileLogger.filePath;

            if (DeviceList.Count() > 0)
                if ((DeviceList.GetContent()[0].bActive == true) && (backgroundWorker0.IsBusy != true)) {
                    label0.BackColor = Color.FromName("LightGreen");
                    backgroundWorker0.RunWorkerAsync();
                }
            if (DeviceList.Count() > 1)
                if ((DeviceList.GetContent()[1].bActive == true) && (backgroundWorker1.IsBusy != true)) {
                    label1.BackColor = Color.FromName("LightGreen");
                    backgroundWorker1.RunWorkerAsync();
                }
            if (DeviceList.Count() > 2)
                if ((DeviceList.GetContent()[2].bActive == true) && (backgroundWorker2.IsBusy != true)) {
                    label2.BackColor = Color.FromName("LightGreen");
                    backgroundWorker2.RunWorkerAsync();
                }
            if (DeviceList.Count() > 3)
                if ((DeviceList.GetContent()[3].bActive == true) && (backgroundWorker3.IsBusy != true)) {
                    label3.BackColor = Color.FromName("LightGreen");
                    backgroundWorker3.RunWorkerAsync();
                }
            if (DeviceList.Count() > 4)
                if ((DeviceList.GetContent()[4].bActive == true) && (backgroundWorker4.IsBusy != true)) {
                    label4.BackColor = Color.FromName("LightGreen");
                    backgroundWorker4.RunWorkerAsync();
                }
            if (DeviceList.Count() > 5)
                if ((DeviceList.GetContent()[5].bActive == true) && (backgroundWorker5.IsBusy != true)) {
                    label5.BackColor = Color.FromName("LightGreen");
                    backgroundWorker5.RunWorkerAsync();
                }
            if (DeviceList.Count() > 6)
                if ((DeviceList.GetContent()[6].bActive == true) && (backgroundWorker6.IsBusy != true)) {
                    label6.BackColor = Color.FromName("LightGreen");
                    backgroundWorker6.RunWorkerAsync();
                }
            if (DeviceList.Count() > 7)
                if ((DeviceList.GetContent()[7].bActive == true) && (backgroundWorker7.IsBusy != true)) {
                    label7.BackColor = Color.FromName("LightGreen");
                    backgroundWorker7.RunWorkerAsync();
                }
            if (DeviceList.Count() > 8)
                if ((DeviceList.GetContent()[8].bActive == true) && (backgroundWorker8.IsBusy != true)) {
                    label8.BackColor = Color.FromName("LightGreen");
                    backgroundWorker8.RunWorkerAsync();
                }
            if (DeviceList.Count() > 9)
                if ((DeviceList.GetContent()[9].bActive == true) && (backgroundWorker9.IsBusy != true)) {
                    label9.BackColor = Color.FromName("LightGreen");
                    backgroundWorker9.RunWorkerAsync();
                }

            SetState(MachineState.Running);
        }

        private void cancelAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker0.WorkerSupportsCancellation == true) {
                backgroundWorker0.CancelAsync();
            }
            if (backgroundWorker1.WorkerSupportsCancellation == true) {
                backgroundWorker1.CancelAsync();
            }
            if (backgroundWorker2.WorkerSupportsCancellation == true) {
                backgroundWorker2.CancelAsync();
            }
            if (backgroundWorker3.WorkerSupportsCancellation == true) {
                backgroundWorker3.CancelAsync();
            }
            if (backgroundWorker4.WorkerSupportsCancellation == true) {
                backgroundWorker4.CancelAsync();
            }
            if (backgroundWorker5.WorkerSupportsCancellation == true) {
                backgroundWorker5.CancelAsync();
            }
            if (backgroundWorker6.WorkerSupportsCancellation == true) {
                backgroundWorker6.CancelAsync();
            }
            if (backgroundWorker7.WorkerSupportsCancellation == true) {
                backgroundWorker7.CancelAsync();
            }
            if (backgroundWorker8.WorkerSupportsCancellation == true) {
                backgroundWorker8.CancelAsync();
            }
            if (backgroundWorker9.WorkerSupportsCancellation == true) {
                backgroundWorker9.CancelAsync();
            }

            SetState(MachineState.PowerOff);
        }

        // end buttons
        //#########################################################################

        private bool openPort(int idx, SerialPort port)
        {
            if (! port.IsOpen)
            {
                string scom = (DeviceList.GetContent()[idx].s_com);
                port.PortName = scom;
                port.BaudRate = 9600;
                port.ReadTimeout = 200;
                try
                {
                    port.Open();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Error opening my port: {0}", ex.Message);
                    return false;
                }
                DeviceList.SetStatus(scom, true);
                return true;
            }
                return false;
        }

        private void closeAllPorts()
        {
            _serialPort0.Close(); label0.Text = "--"; label0.BackColor = Color.FromName("Gray");
            _serialPort1.Close(); label1.Text = "--"; label1.BackColor = Color.FromName("Gray");
            _serialPort2.Close(); label2.Text = "--"; label2.BackColor = Color.FromName("Gray");
            _serialPort3.Close(); label3.Text = "--"; label3.BackColor = Color.FromName("Gray");
            _serialPort4.Close(); label4.Text = "--"; label4.BackColor = Color.FromName("Gray");
            _serialPort5.Close(); label5.Text = "--"; label5.BackColor = Color.FromName("Gray");
            _serialPort6.Close(); label6.Text = "--"; label6.BackColor = Color.FromName("Gray");
            _serialPort7.Close(); label7.Text = "--"; label7.BackColor = Color.FromName("Gray");
            _serialPort8.Close(); label8.Text = "--"; label8.BackColor = Color.FromName("Gray");
            _serialPort9.Close(); label9.Text = "--"; label9.BackColor = Color.FromName("Gray");
            for (var i = 0; i < DeviceList.Count(); i++)
                DeviceList.SetStatus(DeviceList.GetContent()[i].s_com, false);
        }

        private void emptyAllDevicesLabel()
        {
            for (var i = 0; i < DeviceList.Count(); i++)
            {
                switch (i)
                {
                    case 0:
                        labelNum0.Text = "";
                        labelPort0.Text = "";
                        break;
                    case 1:
                        labelNum1.Text = "";
                        labelPort1.Text = "";
                        break;
                    case 2:
                        labelNum2.Text = "";
                        labelPort2.Text = "";
                        break;
                    case 3:
                        labelNum3.Text = "";
                        labelPort3.Text = "";
                        break;
                    case 4:
                        labelNum4.Text = "";
                        labelPort4.Text = "";
                        break;
                    case 5:
                        labelNum5.Text = "";
                        labelPort5.Text = "";
                        break;
                    case 6:
                        labelNum6.Text = "";
                        labelPort6.Text = "";
                        break;
                    case 7:
                        labelNum7.Text = "";
                        labelPort7.Text = "";
                        break;
                    case 8:
                        labelNum8.Text = "";
                        labelPort8.Text = "";
                        break;
                    case 9:
                        labelNum9.Text = "";
                        labelPort9.Text = "";
                        break;
                    default:
                        break;
                }
            }
        }

        private void backgroundWorker0_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sMsg = string.Empty;
            int    nCnt = 0;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(200);
                    string sTmp = string.Empty;

                    try {
                        sTmp = _serialPort0.ReadExisting();
                    }
                    catch (TimeoutException) { }

                    // debug
                    //sTmp = "serialPort0_debug";
                    // end debug

                    if (sTmp.Length > 0)
                    {
                        sTmp = sTmp.TrimEnd('\r');
                        sMsg += sTmp;

                        if (sMsg.Length > 15)
                        {
                            string log;
                            log = DeviceList.GetDevId(_serialPort0.PortName);
                            log += " ; " + sMsg;
                            FileLogger.Log(log, true);
                            sMsg = string.Empty;
                            label0.Text = (++nCnt).ToString();
                        }
                    }
                }
            }
        }

        private void backgroundWorker0_ProgressChanged(object sender, ProgressChangedEventArgs e) {}

        private void backgroundWorker0_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sMsg = string.Empty;
            int nCnt = 0;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(200);
                    string sTmp = string.Empty; ;

                    try
                    {
                        sTmp = _serialPort1.ReadExisting();
                    }
                    catch (TimeoutException) { }

                    // debug
                    //System.Threading.Thread.Sleep(13);
                    //sTmp = "serialPort1_debug";
                    // end debug

                    if (sTmp.Length > 0)
                    {
                        sTmp = sTmp.TrimEnd('\r');
                        sMsg += sTmp;

                        if (sMsg.Length > 15)
                        {
                            string log;
                            log = DeviceList.GetDevId(_serialPort1.PortName);
                            log += " ; " + sMsg;
                            FileLogger.Log(log, true);
                            sMsg = string.Empty;
                            label1.Text = (++nCnt).ToString();
                        }
                    }
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {}

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sMsg = string.Empty;
            int nCnt = 0;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(200);
                    string sTmp = string.Empty; ;

                    try
                    {
                        sTmp = _serialPort2.ReadExisting();
                    }
                    catch (TimeoutException) { }

                    // debug
                    //System.Threading.Thread.Sleep(37);
                    //sTmp = "serialPort2_debug";
                    // end debug


                    if (sTmp.Length > 0)
                    {
                        sTmp = sTmp.TrimEnd('\r');
                        sMsg += sTmp;

                        if (sMsg.Length > 15)
                        {
                            string log;
                            log = DeviceList.GetDevId(_serialPort2.PortName);
                            log += " ; " + sMsg;
                            FileLogger.Log(log, true);
                            sMsg = string.Empty;
                            label2.Text = (++nCnt).ToString();
                        }
                    }
                }
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e) {}

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sMsg = string.Empty;
            int nCnt = 0;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(200);
                    string sTmp = string.Empty; ;

                    try
                    {
                        sTmp = _serialPort3.ReadExisting();
                    }
                    catch (TimeoutException) { }

                    // debug
                    //System.Threading.Thread.Sleep(51);
                    //sTmp = "serialPort3_debug";
                    // end debug

                    if (sTmp.Length > 0)
                    {
                        sTmp = sTmp.TrimEnd('\r');
                        sMsg += sTmp;

                        if (sMsg.Length > 15)
                        {
                            string log;
                            log = DeviceList.GetDevId(_serialPort3.PortName);
                            log += " ; " + sMsg;
                            FileLogger.Log(log, true);
                            sMsg = string.Empty;
                            label3.Text = (++nCnt).ToString();
                        }
                    }
                }
            }
        }

        private void backgroundWorker3_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
        }

        private void backgroundWorker4_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sMsg = string.Empty;
            int nCnt = 0;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(200);
                    string sTmp = string.Empty; ;

                    try
                    {
                        sTmp = _serialPort4.ReadExisting();
                    }
                    catch (TimeoutException) { }

                    // debug
                    //System.Threading.Thread.Sleep(29);
                    //sTmp = "serialPort4_debug";
                    // end debug

                    if (sTmp.Length > 0)
                    {
                        sTmp = sTmp.TrimEnd('\r');
                        sMsg += sTmp;

                        if (sMsg.Length > 15)
                        {
                            string log;
                            log = DeviceList.GetDevId(_serialPort4.PortName);
                            log += " ; " + sMsg;
                            FileLogger.Log(log, true);
                            sMsg = string.Empty;
                            label4.Text = (++nCnt).ToString();
                        }
                    }
                }
            }
        }

        private void backgroundWorker4_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void backgroundWorker4_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
        }

        private void backgroundWorker5_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sMsg = string.Empty;
            int nCnt = 0;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(200);
                    string sTmp = string.Empty; ;

                    try
                    {
                        sTmp = _serialPort5.ReadExisting();
                    }
                    catch (TimeoutException) { }

                    if (sTmp.Length > 0)
                    {
                        sTmp = sTmp.TrimEnd('\r');
                        sMsg += sTmp;

                        if (sMsg.Length > 15)
                        {
                            string log;
                            log = DeviceList.GetDevId(_serialPort5.PortName);
                            log += " ; " + sMsg;
                            FileLogger.Log(log, true);
                            sMsg = string.Empty;
                            label5.Text = (++nCnt).ToString();
                        }
                    }
                }
            }
        }

        private void backgroundWorker5_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void backgroundWorker5_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
        }

        private void backgroundWorker6_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sMsg = string.Empty;
            int nCnt = 0;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(200);
                    string sTmp = string.Empty; ;

                    try
                    {
                        sTmp = _serialPort6.ReadExisting();
                    }
                    catch (TimeoutException) { }

                    if (sTmp.Length > 0)
                    {
                        sTmp = sTmp.TrimEnd('\r');
                        sMsg += sTmp;

                        if (sMsg.Length > 15)
                        {
                            string log;
                            log = DeviceList.GetDevId(_serialPort6.PortName);
                            log += " ; " + sMsg;
                            FileLogger.Log(log, true);
                            sMsg = string.Empty;
                            label6.Text = (++nCnt).ToString();
                        }
                    }
                }
            }
        }

        private void backgroundWorker6_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void backgroundWorker6_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
        }

        private void backgroundWorker7_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sMsg = string.Empty;
            int nCnt = 0;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(200);
                    string sTmp = string.Empty; ;

                    try
                    {
                        sTmp = _serialPort7.ReadExisting();
                    }
                    catch (TimeoutException) { }

                    if (sTmp.Length > 0)
                    {
                        sTmp = sTmp.TrimEnd('\r');
                        sMsg += sTmp;

                        if (sMsg.Length > 15)
                        {
                            string log;
                            log = DeviceList.GetDevId(_serialPort7.PortName);
                            log += " ; " + sMsg;
                            FileLogger.Log(log, true);
                            sMsg = string.Empty;
                            label7.Text = (++nCnt).ToString();
                        }
                    }
                }
            }
        }

        private void backgroundWorker7_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void backgroundWorker7_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
        }

        private void backgroundWorker8_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sMsg = string.Empty;
            int nCnt = 0;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(200);
                    string sTmp = string.Empty; ;

                    try
                    {
                        sTmp = _serialPort8.ReadExisting();
                    }
                    catch (TimeoutException) { }

                    if (sTmp.Length > 0)
                    {
                        sTmp = sTmp.TrimEnd('\r');
                        sMsg += sTmp;

                        if (sMsg.Length > 15)
                        {
                            string log;
                            log = DeviceList.GetDevId(_serialPort8.PortName);
                            log += " ; " + sMsg;
                            FileLogger.Log(log, true);
                            sMsg = string.Empty;
                            label8.Text = (++nCnt).ToString();
                        }
                    }
                }
            }
        }

        private void backgroundWorker8_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void backgroundWorker8_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
        }

        private void backgroundWorker9_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sMsg = string.Empty;
            int nCnt = 0;

            while (true)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    // Perform a time consuming operation and report progress.
                    System.Threading.Thread.Sleep(200);
                    string sTmp = string.Empty; ;

                    try
                    {
                        sTmp = _serialPort9.ReadExisting();
                    }
                    catch (TimeoutException) { }

                    if (sTmp.Length > 0)
                    {
                        sTmp = sTmp.TrimEnd('\r');
                        sMsg += sTmp;

                        if (sMsg.Length > 15)
                        {
                            string log;
                            log = DeviceList.GetDevId(_serialPort9.PortName);
                            log += " ; " + sMsg;
                            FileLogger.Log(log, true);
                            sMsg = string.Empty;
                            label9.Text = (++nCnt).ToString();
                        }
                    }
                }
            }
        }

        private void backgroundWorker9_ProgressChanged(object sender, ProgressChangedEventArgs e) { }

        private void backgroundWorker9_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                resultLabel.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                resultLabel.Text = "Error: " + e.Error.Message;
            }
            else
            {
                resultLabel.Text = "Done!";
            }
        }
    }
}
