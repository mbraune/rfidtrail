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


    /// <summary>
    /// Contains global variables for project.
    /// </summary>
    static class PortList
    {
        static List<string> _portList;  // static list instance

        static PortList()
        {
            //
            // Allocate the list.
            //
            _portList = new List<string>();
        }

        public static void Record(string value)
        {
            //
            // Record this value in the list.
            //
            _portList.Add(value);
        }

        public static List<string> GetContent()
        {
            return _portList;
        }
    }

}
