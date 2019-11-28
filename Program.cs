using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

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
            string sPath = "rf_trail_";
            DateTime now = DateTime.Now;
            string sTime = String.Format("{0:yyyy_MM_dd_HHmmss}", now);
            filePath = sPath + sTime + ".txt";
        }
    }

    public class FtdiDevice
    {
        public string s_id    { get; set; }
        public string s_com   { get; set; }
        public bool   bActive { get; set; }
        public bool   bGroupA { get; set; }

        public FtdiDevice(string devid, string com, bool bAct)
        {
            s_id = devid;
            s_com = com;
            bActive = bAct;
            bGroupA = true;
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
