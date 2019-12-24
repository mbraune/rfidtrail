using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;
using IniParser;
using IniParser.Model;


namespace trail01
{
    // static class to hold globals
    static class Globals
    {
        public static int instance;  // default 00

        // config settings
        public static string sConfigFile;
        public static IniData iniData;
    }

    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Trace.TraceInformation("Program Main");
            Trace.Flush();

            // set the instance ID, default is 00
            Globals.instance = 0;
            if (args.Length == 1) {
                Int32 i = 0;
                Int32.TryParse(args[0], out i);
                Globals.instance = i;
            }

            Globals.sConfigFile = "rfidtrail_" + (Globals.instance).ToString("D2") + ".ini";
            Trace.TraceInformation("config file name : " + Globals.sConfigFile);

            if (Globals.iniData == null) Globals.iniData = new IniData();

            FileIniDataParser parser;
            parser = new FileIniDataParser();
            // create config file if not exist
            if (!File.Exists(Globals.sConfigFile))
            {
                Trace.TraceInformation("file " + Globals.sConfigFile + " does not exist");
                // create 
                createDefaultConfig();
                parser.WriteFile(Globals.sConfigFile, Globals.iniData);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }


        // read data from config file
        //

        // helper 
        // create default config data
        private static void createDefaultConfig()
        {
            //Add a new section and some keys
            Globals.iniData.Sections.AddSection("general");
            Globals.iniData["general"].AddKey("twoGroups", "true");
            Globals.iniData["general"].AddKey("timer", "160");
            //
            Globals.iniData.Sections.AddSection("devices");
            Globals.iniData["devices"].AddKey("device0", "");
            Globals.iniData["devices"].AddKey("status0", "0");
            Globals.iniData["devices"].AddKey("device1", "");
            Globals.iniData["devices"].AddKey("status1", "0");
            Globals.iniData["devices"].AddKey("device2", "");
            Globals.iniData["devices"].AddKey("status2", "0");
            Globals.iniData["devices"].AddKey("device3", "");
            Globals.iniData["devices"].AddKey("status3", "0");
            Globals.iniData["devices"].AddKey("device4", "");
            Globals.iniData["devices"].AddKey("status4", "0");
            Globals.iniData["devices"].AddKey("device5", "");
            Globals.iniData["devices"].AddKey("status5", "0");
            Globals.iniData["devices"].AddKey("device6", "");
            Globals.iniData["devices"].AddKey("status6", "0");
            Globals.iniData["devices"].AddKey("device7", "");
            Globals.iniData["devices"].AddKey("status7", "0");
            Globals.iniData["devices"].AddKey("device8", "");
            Globals.iniData["devices"].AddKey("status8", "0");
            Globals.iniData["devices"].AddKey("device9", "");
            Globals.iniData["devices"].AddKey("status9", "0");
        }
    }

    public static class FileLogger
    {
        public static string filePath; // = "D:\\work\\RFTrail_Log.txt";
        static ReaderWriterLock rwl = new ReaderWriterLock();

        public static void Log(string message, bool bAddTime)
        {
            rwl.AcquireWriterLock(Timeout.Infinite);
            if (bAddTime)
            {
                DateTime now = DateTime.Now;
                string sTime = String.Format("{0:yyyy.MM.dd HH:mm:ss}", now);
                message = sTime + " ; " + message;
            }
            using (StreamWriter streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
            rwl.ReleaseWriterLock();
        }

        public static void Reset()
        {
            string sPath = "rftrail_" + (Globals.instance).ToString("D2") + "_";
            DateTime now = DateTime.Now;
            string sTime = String.Format("{0:yyyyMMdd_HHmmss}", now);
            filePath = sPath + sTime + ".txt";
        }
    }

    public class FtdiDevice
    {
        public string s_id    { get; set; }
        public string s_com   { get; set; }
        public bool   bActive { get; set; }

        public FtdiDevice(string devid, string com, bool bAct)
        {
            s_id = devid;
            s_com = com;
            bActive = bAct;
        }
    }

    /// <summary>
    /// Contains global variables for project.
    /// </summary>
    static class DeviceList
    {
        static List<FtdiDevice> _devList;     // static list instance

        static DeviceList()
        {
            // Allocate the list
            _devList    = new List<FtdiDevice>();
        }

        public static void Clear()
        {
            _devList.Clear();
        }

        public static void Record(FtdiDevice value)
        {
            // Record this value in the list.
            _devList.Add(value);
        }

        public static List<FtdiDevice> GetContent()
        {
            return _devList;
        }

        public static int Count()
        {
            return _devList.Count;
        }

        public static string GetDevId(string ComPort)
        {
            FtdiDevice ftdev = _devList.Find(x => x.s_com == ComPort);
            return ftdev.s_id;
        }

        public static void SetStatus(string ComPort, bool stat)
        {
            FtdiDevice ftdev = _devList.Find(x => x.s_com == ComPort);
            int idx = _devList.IndexOf(ftdev);
            _devList[idx].bActive = stat;
        }
    }
}
