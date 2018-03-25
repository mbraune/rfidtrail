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

using FTD2XX_NET;

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
        FTDI.FT_STATUS ftStatus = FTDI.FT_STATUS.FT_OK;

        // Create new instance of the FTDI device class
        FTDI myFtdiDevice = new FTDI();

        public Form1()
        {
            InitializeComponent();

            // get the version object for this assembly
            Version v = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            // write out the whole version number
            labelVersion.Text = "version " + v.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Trace.TraceInformation("Form1_Load");

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            //Add column header
            listView1.Columns.Add("SerialNumber", 100);
            listView1.Columns.Add("Loc ID", 60);
            listView1.Columns.Add("Description", 160);
        }

        private void buttonDevices_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Click Devices");
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
                string[] arr = new string[4];
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
        }
    }
}
