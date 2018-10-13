using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace trail01
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Trace.TraceInformation("Program Main");
            Trace.Flush();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public static class FileLogger
    {
        static string filePath = "D:\\work\\RFTrail_Log.txt";
        public static void Log(string message, bool bAddTime)
        {
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
        }
    }

    public struct FtdiDevice
    {
        public string s_id { get; private set; }
        public string s_com { get; private set; }

        public FtdiDevice(string devid, string com) : this()
        {
            this.s_id  = devid;
            this.s_com = com;
        }
    }

    /// <summary>
    /// Contains global variables for project.
    /// </summary>
    static class DeviceList
    {
        static List<FtdiDevice> _devList;     // static list instance
        static List<bool>       _statusList;  // static list instance

        static DeviceList()
        {
            // Allocate the lists
            _devList    = new List<FtdiDevice>();
            _statusList = new List<bool>();
        }

        public static void Clear()
        {
            _devList.Clear();
            _statusList.Clear();
        }

        public static void Record(FtdiDevice value)
        {
            // Record this value in the list.
            _devList.Add(value);
            _statusList.Add(false);
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

        public static List<bool> GetStatus()
        {
            return _statusList;
        }

        public static void SetStatus(string ComPort, bool stat)
        {
            FtdiDevice ftdev = _devList.Find(x => x.s_com == ComPort);
            int idx = _devList.IndexOf(ftdev);
            _statusList[idx] = stat;
        }
    }

}
