using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using MonetDB.Extensions;

namespace MonetDB.Helpers.Win32
{
    /// <summary>
    /// 
    /// </summary>
    public static class ManagementObjectHelper
    {
        /// <summary>
        /// Can I get command line arguments of other processes -  http://stackoverflow.com/questions/2633628/can-i-get-command-line-arguments-of-other-processes-from-net-c
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        /// <exception cref="PlatformNotSupportedException">The platform is Windows 98 or Windows Millennium Edition (Windows Me); set <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> to false to access this property on Windows 98 and Windows Me.</exception>
        /// <exception cref="InvalidOperationException">The process <see cref="P:System.Diagnostics.Process.Id" /> is not available.-or- The process has exited. </exception>
        /// <exception cref="ArgumentOutOfRangeException">Enlarging the value of this instance would exceed <see cref="P:System.Text.StringBuilder.MaxCapacity" />. </exception>
        /// <exception cref="ArgumentNullException"><paramref /> is null. </exception>
        /// <exception cref="FormatException">The format item in <paramref /> is invalid.-or- The index of a format item is not zero. </exception>
        public static string GetCommandLine(Process process)
        {
            var commandLineAppender = new StringBuilder();
            var managementQuery = $"SELECT * FROM Win32_Process WHERE ProcessId = {process.Id}";

            using (var searcher = new ManagementObjectSearcher(managementQuery))
            {
                foreach (var item in searcher.Get())
                {
                    commandLineAppender.AppendFormat("{0} ", item["CommandLine"]);
                }
            }

            return commandLineAppender.ToString();
        }

        /// <summary>
        /// http://stackoverflow.com/questions/1542213/how-to-find-the-number-of-cpu-cores-via-net-c
        /// </summary>
        /// <returns></returns>
        public static int GetNumberOfCores()
        {
            var managementQuery = "SELECT * FROM Win32_Processor";

            using (var searcher = new ManagementObjectSearcher(managementQuery))
            {
                return searcher.Get().Cast<ManagementBaseObject>().Sum(item => item["NumberOfCores"].To<int>());
            }
        }
    }
}
