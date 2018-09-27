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

//using FTD2XX_NET;

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

        UInt32 ftdiDeviceCount = 0;
        //FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

        // Create new instance of the FTDI device class
        //FTDI myFtdiDevice = new FTDI();

        int nEvents = 0;


        public Form1()
        {
            InitializeComponent();

            // get the version object for this assembly
            Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            // write out the whole version number
            labelVersion.Text = "version " + v.ToString();

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
            listView1.Columns.Add("Com Port", 60);
            listView1.Columns.Add("Active", 160);
        }

        private void buttonCom_Click(object sender, EventArgs e)
        {
            //var usbDevices = GetUSBDevices();
        }


        private void buttonDevices_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Click Devices");

            // listview löschen
            listView1.Items.Clear();
            ftdiDeviceCount = 0;

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
                    ftdiDeviceCount ++;
                }
            }

            if (ftdiDeviceCount > 0)
                labelDeviceCount.BackColor = Color.FromName("blanchedalmond");
            else labelDeviceCount.BackColor = Color.FromName("OrangeRed");
            labelDeviceCount.Text = ftdiDeviceCount.ToString();

            coll.Dispose();

#if false
            // Determine the number of FTDI devices connected to the machine
            ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);

            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                if (ftdiDeviceCount > 0)
                    labelDeviceCount.BackColor = Color.FromName("blanchedalmond");
                else labelDeviceCount.BackColor = Color.FromName("OrangeRed");
                labelDeviceCount.Text = ftdiDeviceCount.ToString();
            }
            else
            {
                labelDeviceCount.BackColor = Color.FromName("OrangeRed");
                labelDeviceCount.Text = "ftdi Error ";
            }

            if (ftdiDeviceCount == 0) return;

            // listview löschen
            listView1.Items.Clear();

            // Device Liste anzeigen
            // Allocate storage for device info list
            FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

            // Populate our device list
            ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);

            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                string[] arr = new string[5];
                ListViewItem itm;
                for (UInt32 i = 0; i < ftdiDeviceCount; i++)
                {
                    arr[0] = ftdiDeviceList[i].SerialNumber.ToString();
                    arr[1] = ftdiDeviceList[i].LocId.ToString();
                    arr[2] = ftdiDeviceList[i].Description.ToString();
                    itm = new ListViewItem(arr);
                    listView1.Items.Add(itm);
                }
            }
#endif
        }

        private void buttonOpen1_Click(object sender, EventArgs e)
        {
            for (int zeile = 0; zeile < listView1.Items.Count; zeile++)
            {
                for (int spalte = 0; spalte < listView1.Columns.Count; spalte++)
                {
                    textBox1.Text += listView1.Items[zeile].SubItems[spalte].ToString() + "\n";
                }
            }

            // open first device in list
            //ftStatus = myFtdiDevice.OpenBySerialNumber(ftdiDeviceList[0].SerialNumber);
            /*ftStatus = myFtdiDevice.OpenBySerialNumber(sensors[0]);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Console.WriteLine("Failed to open device (error " + ftStatus.ToString() + ")");
                Console.ReadKey();
                return;
            }

            UInt32 numBytesAvailable = 0;

            for(int i=0; i<10; i++)
            {
                ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);
                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    Console.WriteLine("Failed to get number of bytes available to read (error " + ftStatus.ToString() + ")");
                    Console.ReadKey();
                    //break;
                }

                if (numBytesAvailable > 0)
                {
                    string readData = "";
                    UInt32 numBytesRead = 0;
                    byte[] dataBuffer = new byte[1024];

                    // TODO: check so you don't over your buffer.
                    ftStatus = myFtdiDevice.Read(out readData, numBytesAvailable, ref numBytesRead);

                    //ProcessData(readData);
                    // show ts
                    DateTime now = DateTime.Now;
                    //String timeStamp = GetTimestamp(DateTime.Now);
                    textBox1.Text = now + "     ";
                    // show sensor_id
                    textBox1.Text += sensors[0];
                    // show dta
                    textBox1.Text += "     " + readData;
                }

                System.Threading.Thread.Sleep(100); // Sleep 1 seconds.
            }
            ftStatus = myFtdiDevice.Close();
            */
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

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
                    System.Threading.Thread.Sleep(500);

                    DateTime now = DateTime.Now;
                    worker.ReportProgress(0, now + "  STRING MIT ID UND CID");
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

        private void startAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void cancelAsyncButton_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.WorkerSupportsCancellation == true)
            {
                // Cancel the asynchronous operation.
                backgroundWorker1.CancelAsync();
            }
        }


    }

    /*class USBDeviceInfo
    {
        public USBDeviceInfo(string deviceID, string pnpDeviceID, string description, string name)
        {
            this.DeviceID = deviceID;
            this.PnpDeviceID = pnpDeviceID;
            this.Description = description;
            this.Description = name;
        }
        public string DeviceID { get; private set; }
        public string PnpDeviceID { get; private set; }
        public string Description { get; private set; }
        public string Caption { get; private set; }
    }*/


}
