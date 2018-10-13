using System;
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


    public partial class Form1 : Form
    {
        // list of known sensors --> move to config/device.ini
        public readonly string[] sensors = { "A107BOUE", "A107BOUF", "gamma", "delta", "echo" };

        public SerialPort _serialPort0;
        public SerialPort _serialPort1; 
        public SerialPort _serialPort2; 
        public SerialPort _serialPort3; 
        public SerialPort _serialPort4; 

        int nEvents = 0;

        public Form1()
        {
            InitializeComponent();

            // get the version object for this assembly
            Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            // write out the whole version number
            labelVersion.Text = "version " + v.ToString();

            _serialPort0 = new SerialPort();
            _serialPort1 = new SerialPort();
            _serialPort2 = new SerialPort();
            _serialPort3 = new SerialPort();
            _serialPort4 = new SerialPort();

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
                return string.Empty;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Trace.TraceInformation("Form1_Load");

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //Add column header
            listView1.Columns.Add("SerialNumber", 100);
            listView1.Columns.Add("Com Port", 80);
        }

        private void buttonDevices_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Click Devices");

            // reset all
            listView1.Items.Clear();
            DeviceList.Clear();
            closeAllPorts();

            var searcher = new ManagementObjectSearcher("Select * from Win32_PnPEntity");
            var coll = searcher.Get();

            foreach (ManagementObject o in coll)
            {
                Console.WriteLine((string)o["Description"]);
                if (((string)o["DeviceID"]).Contains("FTDIBUS"))
                {
                    Console.WriteLine((string)o["Name"]);
                    //Console.WriteLine((string)o["Description"]);

                    // extract COM Port
                    string device = (string)o["DeviceID"];
                    Trace.WriteLine("found" + device);
                    string pattern = "(.*PID_6001\\+)";
                    string sTmp = Regex.Replace(device, pattern, String.Empty);
                    string devid = sTmp.Substring(0, 8);
                    string port = GetVCP_COMPort(o);
                    Trace.WriteLine("found devid " + devid + "  at " + port);
                    //Console.WriteLine((string)o["DeviceID"]);
                    //Console.WriteLine((string)o["PNPDeviceID"]);
                    //Console.WriteLine("VCP ComPort " + GetVCP_COMPort(o));

                    // append to list 
                    string[] arr = new string[4];
                    ListViewItem itm;
                    arr[0] = devid;
                    arr[1] = port;
                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                    FtdiDevice ftdev = new FtdiDevice(devid, port);
                    DeviceList.Record(ftdev);
                }
            }

            if (DeviceList.Count() > 0)
                labelDeviceCount.BackColor = Color.FromName("blanchedalmond");
            else labelDeviceCount.BackColor = Color.FromName("OrangeRed");
            labelDeviceCount.Text = DeviceList.Count().ToString();

            coll.Dispose();
        }

        private void buttonOpen1_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < DeviceList.Count(); i++)
            {
                switch (i)
                {
                    case 0:
                        if (openPort(0, _serialPort0)) {
                            label0.Text = "open";
                            label0.BackColor = Color.FromName("PaleGreen");
                        }
                        break;
                    case 1:
                        if (openPort(1, _serialPort1)) {
                            label1.Text = "open";
                            label1.BackColor = Color.FromName("PaleGreen");
                        }
                        break;
                    case 2:
                        if (openPort(2, _serialPort2)) {
                            label2.Text = "open";
                            label2.BackColor = Color.FromName("PaleGreen");
                        }
                        break;
                    case 3:
                        if (openPort(3, _serialPort3)) {
                            label3.Text = "open";
                            label3.BackColor = Color.FromName("PaleGreen");
                        }
                        break;
                    case 4:
                        if (openPort(4, _serialPort4))
                        {
                            label4.Text = "open";
                            label4.BackColor = Color.FromName("PaleGreen");
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private bool openPort(int idx, SerialPort port)
        {
            if (! port.IsOpen)
            {
                port.PortName = (DeviceList.GetContent()[idx].s_com);
                port.BaudRate = 9600;
                port.ReadTimeout = 200;
                port.Open();
                return true;
            }
                return false;
        }

        private void closeAllPorts()
        {
            _serialPort0.Close();   label0.Text = "--"; label0.BackColor = Color.FromName("LightSteelBlue");
            _serialPort1.Close();   label1.Text = "--"; label1.BackColor = Color.FromName("LightSteelBlue");
            _serialPort2.Close();   label2.Text = "--"; label2.BackColor = Color.FromName("LightSteelBlue");
            _serialPort3.Close();   label3.Text = "--"; label3.BackColor = Color.FromName("LightSteelBlue");
            _serialPort4.Close();   label4.Text = "--"; label4.BackColor = Color.FromName("LightSteelBlue");
        }

        private void backgroundWorker0_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sMsg = string.Empty;

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
                        sTmp = _serialPort0.ReadExisting();
                    }
                    catch (TimeoutException) { }

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
                        }
                    }
                }
            }
        }

        private void backgroundWorker0_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            resultLabel.Text = (e.UserState + "USER STATE   evnts " + nEvents++);
            // add event to EventLog
        }

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
                        }
                    }
                }
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ///resultLabel.Text = (e.ProgressPercentage.ToString() + "%");
            resultLabel.Text = (e.UserState + "USER STATE   evnts " + nEvents++);

            // add event to EventLog
        }

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
                        }
                    }
                }
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            resultLabel.Text = (e.UserState + "USER STATE   evnts " + nEvents++);
            // add event to EventLog
        }

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

        private void startAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker0.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker0.RunWorkerAsync();
            }
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
            if (backgroundWorker2.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void cancelAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker0.WorkerSupportsCancellation == true)
            {
                backgroundWorker0.CancelAsync();
            }
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                backgroundWorker1.CancelAsync();
            }
            if (backgroundWorker2.WorkerSupportsCancellation == true)
            {
                backgroundWorker2.CancelAsync();
            }
        }


    }

}
