using MonetDB.Helpers;
using MonetDB.Helpers.Diagnostic;
using MonetDB.TCP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MonetDB.TCP.Startup
{
    class Program
    {

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please, re-run only with arguments!");
                Console.ResetColor();
                Console.ReadLine();
            }
            else
            {
                var serverOptions = args[0];

                if (String.IsNullOrWhiteSpace(serverOptions))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Server arguments not found!");
                    Console.ResetColor();
                    Console.ReadLine();
                }
                else
                {

                    var handle = GetConsoleWindow();
                    var model = BinaryFormatterHelper.Deserialize<MonetServerOptionsModel>(serverOptions);

                    // Hide
                    ShowWindow(handle, SW_HIDE);

                    //environment 
                    EnvironmentHelper.SetVariables("PATH", string.Format("{0}\\bin;{0}\\lib;{0}\\lib\\MonetDB5;", Directory.GetParent(Path.GetDirectoryName(model.ServerFileName)).FullName));

                    DiagnosticHelper.AutoLaunchApplicationToSleep(model.ServerFileName, model.ServerArguments);
                }
            }
        }
    }
}
