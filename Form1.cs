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
using System.IO;
using IniParser;
using IniParser.Model;


namespace trail01
{
    /*public struct EventLog
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
    };*/

    enum MachineState
    {
        PowerOff    = 0,
        Initialized = 5,
        PortsOpen   = 10,
        Running     = 15,
    }


    public partial class Form1 : Form
    {
        public const int MaxDev = 10;

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

        private bool bGroupA;                // groupA active

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
                    // scan ports
                    // get strings
                    buttonDevices.Enabled = false;
                    buttonOpen.Enabled = true;
                    break;
                case MachineState.PortsOpen:
                    disableCheckboxes();
                    openAvailablePorts();
                    activateAvailablePorts();
                    setBackColorLabelOpened();
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

        private CheckBox indexedCheckBox(int i)
        {
            switch (i)
            {
                case 0: return checkBox0;
                case 1: return checkBox1;
                case 2: return checkBox2;
                case 3: return checkBox3;
                case 4: return checkBox4;
                case 5: return checkBox5;
                case 6: return checkBox6;
                case 7: return checkBox7;
                case 8: return checkBox8;
                case 9: return checkBox9;
                default: return null;    
            }
        }

        // indexed access to labelNum0 .. labelNum9
        private Label indexedLabelNum(int i)
        {
            switch (i)
            {
                case 0: return labelNum0;
                case 1: return labelNum1;
                case 2: return labelNum2;
                case 3: return labelNum3;
                case 4: return labelNum4;
                case 5: return labelNum5;
                case 6: return labelNum6;
                case 7: return labelNum7;
                case 8: return labelNum8;
                case 9: return labelNum9;
                default: return null;
            }
        }

        // indexed access to labelPort0 .. labelPort9
        private Label indexedLabelPort(int i)
        {
            switch (i)
            {
                case 0: return labelPort0;
                case 1: return labelPort1;
                case 2: return labelPort2;
                case 3: return labelPort3;
                case 4: return labelPort4;
                case 5: return labelPort5;
                case 6: return labelPort6;
                case 7: return labelPort7;
                case 8: return labelPort8;
                case 9: return labelPort9;
                default: return null;
            }
        }

        // indexed access to label0 .. label9
        private Label indexedLabel(int i)
        {
            switch (i)
            {
                case 0: return label0;
                case 1: return label1;
                case 2: return label2;
                case 3: return label3;
                case 4: return label4;
                case 5: return label5;
                case 6: return label6;
                case 7: return label7;
                case 8: return label8;
                case 9: return label9;
                default: return null;
            }
        }

        //###############################################################################
        //
        //  section config (ini) file read, write
        //

        // read data from ini file
        private void loadConfig()
        {
            FileIniDataParser parser = new FileIniDataParser();
            Globals.iniData = parser.ReadFile(Globals.sConfigFile);

            // set 2 vars read from section [general]
            string sTwo = Globals.iniData["general"]["twoGroups"];
            checkBoxMux.Checked = bool.Parse(sTwo);
            // timer
            textBox1.Text = Globals.iniData["general"]["timer"];
        }

        // search for s_device in iniData, if found read matching status
        private int getStoredCheckState(string s_device)
        {
            int ret = 0;
            bool found = false;
            string s1 = "";

            KeyDataCollection keyCol = Globals.iniData["devices"];
            foreach (KeyData key in keyCol)
                if (key.Value.Equals(s_device)) {
                    found = true;
                    s1 = key.KeyName;
                    break;
                }
            if (found) {
                string si = Regex.Replace(s1, @"\D","");
                string stat = "status" + si;
                Int32.TryParse(Globals.iniData["devices"][stat], out ret);
            }
            return ret;
        }

        // write data to ini file
        private void saveConfig()
        {
            // [general]
            if (checkBoxMux.Checked == false)
                Globals.iniData["general"]["twoGroups"] = "false";
            else
                Globals.iniData["general"]["twoGroups"] = "true";
            Globals.iniData["general"]["timer"] = textBox1.Text;

            // [devices]
            for (var i = 0; i < DeviceList.Count(); i++)
            {
                if (DeviceList.GetContent()[i].bActive != true) continue;
                switch (i)
                {
                    case 0:
                        Globals.iniData["devices"]["device0"] = labelNum0.Text;
                        Globals.iniData["devices"]["status0"] = checkBox0.CheckState.GetHashCode().ToString();
                        break;
                    case 1:
                        Globals.iniData["devices"]["device1"] = labelNum1.Text;
                        Globals.iniData["devices"]["status1"] = checkBox1.CheckState.GetHashCode().ToString();
                        break;
                    case 2:
                        Globals.iniData["devices"]["device2"] = labelNum2.Text;
                        Globals.iniData["devices"]["status2"] = checkBox2.CheckState.GetHashCode().ToString();
                        break;
                    case 3:
                        Globals.iniData["devices"]["device3"] = labelNum3.Text;
                        Globals.iniData["devices"]["status3"] = checkBox3.CheckState.GetHashCode().ToString();
                        break;
                    case 4:
                        Globals.iniData["devices"]["device4"] = labelNum4.Text;
                        Globals.iniData["devices"]["status4"] = checkBox4.CheckState.GetHashCode().ToString();
                        break;
                    case 5:
                        Globals.iniData["devices"]["device5"] = labelNum5.Text;
                        Globals.iniData["devices"]["status5"] = checkBox5.CheckState.GetHashCode().ToString();
                        break;
                    case 6:
                        Globals.iniData["devices"]["device6"] = labelNum6.Text;
                        Globals.iniData["devices"]["status6"] = checkBox6.CheckState.GetHashCode().ToString();
                        break;
                    case 7:
                        Globals.iniData["devices"]["device7"] = labelNum7.Text;
                        Globals.iniData["devices"]["status7"] = checkBox7.CheckState.GetHashCode().ToString();
                        break;
                    case 8:
                        Globals.iniData["devices"]["device8"] = labelNum8.Text;
                        Globals.iniData["devices"]["status8"] = checkBox8.CheckState.GetHashCode().ToString();
                        break;
                    case 9:
                        Globals.iniData["devices"]["device9"] = labelNum9.Text;
                        Globals.iniData["devices"]["status9"] = checkBox9.CheckState.GetHashCode().ToString();
                        break;
                    default:
                        break;
                }
            }
            FileIniDataParser parser = new FileIniDataParser();
            parser.WriteFile(Globals.sConfigFile, Globals.iniData);
        }
        //
        //  end section config (ini) file read, write
        //
        //###############################################################################


        public Form1()
        {
            InitializeComponent();
            
            // write instance to title bar
            string title = "rftrail_" + (Globals.instance).ToString("D2");
            this.Text = title;

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

            bGroupA = true;     // status groupA or groupB active
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

            this.timer1.Start();
        }

        private void buttonDevices_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Click Devices");
            loadConfig();   // read ini file 

            var searcher = new ManagementObjectSearcher("Select * from Win32_PnPEntity");
            var coll = searcher.Get();

            foreach (ManagementObject o in coll)
            {
                Console.WriteLine((string)o["Description"]);
                if (((string)o["DeviceID"]).Contains("FTDIBUS"))
                {
                    if (DeviceList.Count() < MaxDev) {
                        Console.WriteLine((string)o["Name"]);
                        // extract COM Port
                        string device = (string)o["DeviceID"];
                        Trace.WriteLine("found " + device);
                        string pattern = "(.*PID_6001\\+)";
                        string sTmp = Regex.Replace(device, pattern, String.Empty);
                        string devid = sTmp.Substring(0, 4);  // serial number is S001, S002, ..
                        string port = GetVCP_COMPort(o);
                        Trace.WriteLine("found devid " + devid + " at " + port);
                        //Console.WriteLine((string)o["DeviceID"]);
                        //Console.WriteLine((string)o["PNPDeviceID"]);
                        //Console.WriteLine("VCP ComPort " + GetVCP_COMPort(o));

                        FtdiDevice ftdev = new FtdiDevice(devid, port, false);
                        DeviceList.Record(ftdev);
                    }
                }
            }
            coll.Dispose();

            // check if device in config --> yes set CheckState 


            // fill Labels with serial port names
            for (var i = 0; i < DeviceList.Count(); i++) {
                indexedLabelNum(i).Text = DeviceList.GetContent()[i].s_id;
                indexedLabelPort(i).Text = DeviceList.GetContent()[i].s_com;
                indexedCheckBox(i).Enabled = true;
                indexedCheckBox(i).CheckState = (CheckState)getStoredCheckState(DeviceList.GetContent()[i].s_id);//CheckState.Checked; // getStoredCheckState( devicename);
            }
            checkBoxMux.Enabled = true;
            textBox1.Enabled = true;

            SetState(MachineState.Initialized);
        }

        private void buttonOpen1_Click(object sender, EventArgs e)
        {
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
            saveConfig();             // store config to ini file
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

        private void openAvailablePorts()
        {
            for (var i = 0; i < DeviceList.Count(); i++)
            {
                switch (i)
                {
                    case 0:     openPort(i, _serialPort0);       break;
                    case 1:     openPort(i, _serialPort1);       break;
                    case 2:     openPort(i, _serialPort2);       break;
                    case 3:     openPort(i, _serialPort3);       break;
                    case 4:     openPort(i, _serialPort4);       break;
                    case 5:     openPort(i, _serialPort5);       break;
                    case 6:     openPort(i, _serialPort6);       break;
                    case 7:     openPort(i, _serialPort7);       break;
                    case 8:     openPort(i, _serialPort8);       break;
                    case 9:     openPort(i, _serialPort9);       break;
                    default:
                        break;
                }
            }
        }

        private void activateAvailablePorts()
        {
            for (var i = 0; i < DeviceList.Count(); i++)
            {
                if (DeviceList.GetContent()[i].bActive != true) continue;
                switch (i)
                {
                    case 0: if (checkBox0.CheckState == CheckState.Unchecked)
                            {    _serialPort0.WriteLine("SRD"); closePort(0); }
                            else _serialPort0.WriteLine("SRA");
                        break;
                    case 1: if (checkBox1.CheckState == CheckState.Unchecked)
                            {    _serialPort1.WriteLine("SRD"); closePort(1); }
                            else _serialPort1.WriteLine("SRA");
                        break;
                    case 2: if (checkBox2.CheckState == CheckState.Unchecked)
                            {    _serialPort2.WriteLine("SRD"); closePort(2); }
                            else _serialPort2.WriteLine("SRA");
                        break;
                    case 3: if (checkBox3.CheckState == CheckState.Unchecked)
                            {    _serialPort3.WriteLine("SRD"); closePort(3); }
                            else _serialPort3.WriteLine("SRA");
                        break;
                    case 4: if (checkBox4.CheckState == CheckState.Unchecked)
                            {    _serialPort4.WriteLine("SRD"); closePort(4); }
                            else _serialPort4.WriteLine("SRA");
                        break;
                    case 5: if (checkBox5.CheckState == CheckState.Unchecked)
                            {    _serialPort5.WriteLine("SRD"); closePort(5); }
                            else _serialPort5.WriteLine("SRA");
                        break;
                    case 6: if (checkBox6.CheckState == CheckState.Unchecked)
                            {    _serialPort6.WriteLine("SRD"); closePort(6); }
                            else _serialPort6.WriteLine("SRA");
                        break;
                    case 7: if (checkBox7.CheckState == CheckState.Unchecked)
                            {    _serialPort7.WriteLine("SRD"); closePort(7); }
                            else _serialPort7.WriteLine("SRA");
                        break;
                    case 8: if (checkBox8.CheckState == CheckState.Unchecked)
                            {    _serialPort8.WriteLine("SRD"); closePort(8); }
                            else _serialPort8.WriteLine("SRA");
                        break;
                    case 9: if (checkBox9.CheckState == CheckState.Unchecked)
                            {    _serialPort9.WriteLine("SRD"); closePort(9); }
                            else _serialPort9.WriteLine("SRA");
                        break;
                    default:
                        break;
                }
            }
        }

        private void toggleAvailablePorts(bool bA)
        {
            string sWrite = "";
            for (var i = 0; i < DeviceList.Count(); i++)
            {
                if (DeviceList.GetContent()[i].bActive != true) continue;

                if (indexedCheckBox(i).CheckState == CheckState.Checked)
                     sWrite = (bA ? "SRD" : "SRA");
                else sWrite = (bA ? "SRA" : "SRD");
                switch (i)
                {
                    case 0: _serialPort0.WriteLine(sWrite); break;
                    case 1: _serialPort1.WriteLine(sWrite); break;
                    case 2: _serialPort2.WriteLine(sWrite); break;
                    case 3: _serialPort3.WriteLine(sWrite); break;
                    case 4: _serialPort4.WriteLine(sWrite); break;
                    case 5: _serialPort5.WriteLine(sWrite); break;
                    case 6: _serialPort6.WriteLine(sWrite); break;
                    case 7: _serialPort7.WriteLine(sWrite); break;
                    case 8: _serialPort8.WriteLine(sWrite); break;
                    case 9: _serialPort9.WriteLine(sWrite); break;
                    default:
                        break;
                }
            }
        }

        private void setBackColorLabelOpened()
        {
            for (var i = 0; i<DeviceList.Count(); i++)
            {
                if (DeviceList.GetContent()[i].bActive != true) continue;

                if (indexedCheckBox(i).CheckState != CheckState.Unchecked)
                    indexedLabel(i).BackColor = Color.FromName("LightSteelBlue");
            }
        }

        private bool openPort(int idx, SerialPort port)
        {
            if (! port.IsOpen)
            {
                string scom = (DeviceList.GetContent()[idx].s_com);
                port.PortName = scom;
                port.BaudRate = 9600;
                port.ReadTimeout = 200;
                port.NewLine = "\r";
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

        private void closePort(int idx)
        {
            switch (idx)
            {
                case 0:     _serialPort0.Close();   break;
                case 1:     _serialPort1.Close();   break;
                case 2:     _serialPort2.Close();   break;
                case 3:     _serialPort3.Close();   break;
                case 4:     _serialPort0.Close();   break;
                case 5:     _serialPort0.Close();   break;
                case 6:     _serialPort0.Close();   break;
                case 7:     _serialPort0.Close();   break;
                case 8:     _serialPort0.Close();   break;
                case 9:     _serialPort0.Close();   break;
                default:
                    break;
            }
            DeviceList.SetStatus(DeviceList.GetContent()[idx].s_com, false);
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
                indexedLabelNum(i).Text  = "";
                indexedLabelPort(i).Text = "";
                indexedCheckBox(i).CheckState = CheckState.Unchecked;
            }
            checkBoxMux.CheckState = CheckState.Unchecked;
        }

        private void disableCheckboxes()
        {
            for (var i = 0; i < MaxDev; i++)
                indexedCheckBox(i).Enabled = false;
            checkBoxMux.Enabled = false;
            textBox1.Enabled = false;
        }

        private void backgroundWorker0_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            string sRaw = string.Empty;
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

                    try {
                        sRaw = _serialPort0.ReadExisting();
                    }
                    catch (TimeoutException) { }
                    string sTmp = Regex.Replace(sRaw, "[okOK\r]", "");

                    if (sTmp.Length > 0)
                    {
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
            string sRaw = string.Empty;
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

                    try
                    {
                        sRaw = _serialPort1.ReadExisting();
                    }
                    catch (TimeoutException) { }
                    string sTmp = Regex.Replace(sRaw, "[okOK\r]", "");

                    if (sTmp.Length > 0)
                    {
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
            string sRaw = string.Empty;
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

                    try
                    {
                        sRaw = _serialPort2.ReadExisting();
                    }
                    catch (TimeoutException) { }
                    string sTmp = Regex.Replace(sRaw, "[okOK\r]", "");

                    if (sTmp.Length > 0)
                    {
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
            string sRaw = string.Empty;
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

                    try
                    {
                        sRaw = _serialPort3.ReadExisting();
                    }
                    catch (TimeoutException) { }
                    string sTmp = Regex.Replace(sRaw, "[okOK\r]", "");

                    if (sTmp.Length > 0)
                    {
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
            string sRaw = string.Empty;
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

                    try
                    {
                        sRaw = _serialPort4.ReadExisting();
                    }
                    catch (TimeoutException) { }
                    string sTmp = Regex.Replace(sRaw, "[okOK\r]", "");

                    if (sTmp.Length > 0)
                    {
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
            string sRaw = string.Empty;
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

                    try
                    {
                        sRaw = _serialPort5.ReadExisting();
                    }
                    catch (TimeoutException) { }
                    string sTmp = Regex.Replace(sRaw, "[okOK\r]", "");

                    if (sTmp.Length > 0)
                    {
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
            string sRaw = string.Empty;
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

                    try
                    {
                        sRaw = _serialPort6.ReadExisting();
                    }
                    catch (TimeoutException) { }
                    string sTmp = Regex.Replace(sRaw, "[okOK\r]", "");

                    if (sTmp.Length > 0)
                    {
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
            string sRaw = string.Empty;
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

                    try
                    {
                        sRaw = _serialPort7.ReadExisting();
                    }
                    catch (TimeoutException) { }
                    string sTmp = Regex.Replace(sRaw, "[okOK\r]", "");

                    if (sTmp.Length > 0)
                    {
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
            string sRaw = string.Empty;
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

                    try
                    {
                        sRaw = _serialPort8.ReadExisting();
                    }
                    catch (TimeoutException) { }
                    string sTmp = Regex.Replace(sRaw, "[okOK\r]", "");

                    if (sTmp.Length > 0)
                    {
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
            string sRaw = string.Empty;
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

                    try
                    {
                        sRaw = _serialPort9.ReadExisting();
                    }
                    catch (TimeoutException) { }
                    string sTmp = Regex.Replace(sRaw, "[okOK\r]", "");

                    if (sTmp.Length > 0)
                    {
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

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (this.checkBoxMux.Checked)
            {
                labelTimer.Text = (bGroupA ? "" : "X");
                if (bGroupA) labelTimer.BackColor = Color.FromName("LightSteelBlue");
                else labelTimer.BackColor = Color.FromName("IndianRed");
            }
            bGroupA = !bGroupA;
            toggleAvailablePorts(bGroupA);
        }


        private void checkBox0_CheckStateChanged(object sender, EventArgs e)
        {
            switch (checkBox0.CheckState)
            {
                case CheckState.Unchecked:      labelNum0.BackColor = Color.FromName("AthensGrey"); break;
                case CheckState.Checked:        labelNum0.BackColor = Color.FromName("LightSteelBlue"); break;
                case CheckState.Indeterminate:  labelNum0.BackColor = Color.FromName("IndianRed"); break;
            }
        }


        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            switch (checkBox1.CheckState)
            {
                case CheckState.Unchecked:      labelNum1.BackColor = Color.FromName("AthensGrey"); break;
                case CheckState.Checked:        labelNum1.BackColor = Color.FromName("LightSteelBlue"); break;
                case CheckState.Indeterminate:  labelNum1.BackColor = Color.FromName("IndianRed"); break;
            }
        }


        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            switch (checkBox2.CheckState)
            {
                case CheckState.Unchecked:      labelNum2.BackColor = Color.FromName("AthensGrey"); break;
                case CheckState.Checked:        labelNum2.BackColor = Color.FromName("LightSteelBlue"); break;
                case CheckState.Indeterminate:  labelNum2.BackColor = Color.FromName("IndianRed"); break;
            }
        }


        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            switch (checkBox3.CheckState)
            {
                case CheckState.Unchecked: labelNum3.BackColor = Color.FromName("AthensGrey"); break;
                case CheckState.Checked: labelNum3.BackColor = Color.FromName("LightSteelBlue"); break;
                case CheckState.Indeterminate: labelNum3.BackColor = Color.FromName("IndianRed"); break;
            }
        }


        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            switch (checkBox4.CheckState)
            {
                case CheckState.Unchecked: labelNum4.BackColor = Color.FromName("AthensGrey"); break;
                case CheckState.Checked: labelNum4.BackColor = Color.FromName("LightSteelBlue"); break;
                case CheckState.Indeterminate: labelNum4.BackColor = Color.FromName("IndianRed"); break;
            }
        }


        private void checkBox5_CheckStateChanged(object sender, EventArgs e)
        {
            switch (checkBox5.CheckState)
            {
                case CheckState.Unchecked: labelNum5.BackColor = Color.FromName("AthensGrey"); break;
                case CheckState.Checked: labelNum5.BackColor = Color.FromName("LightSteelBlue"); break;
                case CheckState.Indeterminate: labelNum5.BackColor = Color.FromName("IndianRed"); break;
            }
        }


        private void checkBox6_CheckStateChanged(object sender, EventArgs e)
        {
            switch (checkBox6.CheckState)
            {
                case CheckState.Unchecked: labelNum6.BackColor = Color.FromName("AthensGrey"); break;
                case CheckState.Checked: labelNum6.BackColor = Color.FromName("LightSteelBlue"); break;
                case CheckState.Indeterminate: labelNum6.BackColor = Color.FromName("IndianRed"); break;
            }
        }


        private void checkBox7_CheckStateChanged(object sender, EventArgs e)
        {
            switch (checkBox7.CheckState)
            {
                case CheckState.Unchecked: labelNum7.BackColor = Color.FromName("AthensGrey"); break;
                case CheckState.Checked: labelNum7.BackColor = Color.FromName("LightSteelBlue"); break;
                case CheckState.Indeterminate: labelNum7.BackColor = Color.FromName("IndianRed"); break;
            }
        }


        private void checkBox8_CheckStateChanged(object sender, EventArgs e)
        {
            switch (checkBox8.CheckState)
            {
                case CheckState.Unchecked: labelNum8.BackColor = Color.FromName("AthensGrey"); break;
                case CheckState.Checked: labelNum8.BackColor = Color.FromName("LightSteelBlue"); break;
                case CheckState.Indeterminate: labelNum8.BackColor = Color.FromName("IndianRed"); break;
            }
        }


        private void checkBox9_CheckStateChanged(object sender, EventArgs e)
        {
            switch (checkBox9.CheckState)
            {
                case CheckState.Unchecked: labelNum9.BackColor = Color.FromName("AthensGrey"); break;
                case CheckState.Checked: labelNum9.BackColor = Color.FromName("LightSteelBlue"); break;
                case CheckState.Indeterminate: labelNum9.BackColor = Color.FromName("IndianRed"); break;
            }
        }


        private void checkBoxMux_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxMux.Checked == false)
            {
                checkBox0.ThreeState = false;
                checkBox1.ThreeState = false;
                checkBox2.ThreeState = false;
                checkBox3.ThreeState = false;
                checkBox4.ThreeState = false;
                checkBox5.ThreeState = false;
                checkBox6.ThreeState = false;
                checkBox7.ThreeState = false;
                checkBox8.ThreeState = false;
                checkBox9.ThreeState = false;
                labelStatic1.BackColor = Color.FromName("AthensGrey");
                labelStatic1.Text = "";
            }
            else
            {
                checkBox0.ThreeState = true;
                checkBox1.ThreeState = true;
                checkBox2.ThreeState = true;
                checkBox3.ThreeState = true;
                checkBox4.ThreeState = true;
                checkBox5.ThreeState = true;
                checkBox6.ThreeState = true;
                checkBox7.ThreeState = true;
                checkBox8.ThreeState = true;
                checkBox9.ThreeState = true;
                labelStatic1.BackColor = Color.FromName("IndianRed");
                labelStatic1.Text = "group B";
            }

            this.checkBox0.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkBox3.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkBox4.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkBox6.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkBox7.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkBox8.CheckState = System.Windows.Forms.CheckState.Unchecked;
            this.checkBox9.CheckState = System.Windows.Forms.CheckState.Unchecked;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int interval = Int32.Parse(textBox1.Text);
                // allow only numbers > 50
                if (interval < 50) {
                    textBox1.Text = "50";
                    interval = 50;
                }
                this.timer1.Interval = interval;
            }
            catch (FormatException)
            {
                Console.WriteLine("Unable to parse '{input}'");
            }
        }

    }
}
