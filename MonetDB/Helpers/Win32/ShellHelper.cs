using IWshRuntimeLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonetDB.Helpers.Win32
{
    public static class ShellHelper
    {
        private static readonly string StartupFolder = Environment.GetFolderPath(Environment.SpecialFolder.Startup);

        public static void AddToStartup(string title, string path, string args)
        {
            var shell = new WshShell();
            var shortcut = (IWshShortcut)shell.CreateShortcut($"{StartupFolder}\\{title}.lnk");

            shortcut.Description = title;
            shortcut.WindowStyle = 0;
            shortcut.WorkingDirectory = Path.GetDirectoryName(path);
            shortcut.TargetPath = path;
            shortcut.Arguments = args;
            shortcut.Save();

        }

        public static void RemoveFromStartup(string title)
        {
            IoHelper.RemoveFile($"{StartupFolder}\\{title}.lnk");
        }
    }
}
