using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonetDB.GUI
{
    internal static class Program
    {
        private static readonly Mutex Mutex = new Mutex(true, @"MonetDB.GUI");
         
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            if (Mutex.WaitOne(TimeSpan.Zero, true))
            { 
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());

                Mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show(@"Only one instance at a time", @"Single instance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
