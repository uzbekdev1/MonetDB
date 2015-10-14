using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using MonetDB.Enums.Logging;
using MonetDB.Helpers.Logging;
using MonetDB.Helpers.Win32;
// ReSharper disable All

namespace MonetDB.Helpers.Diagnostic
{
    /// <summary>
    /// 
    /// </summary>
    public static class DiagnosticHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <param name="outputFileName"></param>
        /// <param name="errorFileName"></param>
        /// <param name="inputFileName"></param>
        public static void ShowWindowApplicationEx(string path, string args, string outputFileName, string errorFileName, string inputFileName)
        {
            try
            {
                var process = new ExecutableHelper
                {
                    ProgramFileName = path,
                    Arguments = args
                };

                if (!string.IsNullOrWhiteSpace(outputFileName))
                    process.StandardOutputFileName = outputFileName;

                if (!string.IsNullOrWhiteSpace(inputFileName))
                    process.StandardInputFileName = inputFileName;

                if (!string.IsNullOrWhiteSpace(errorFileName))
                    process.StandardErrorFileName = errorFileName;

                process.Run();
            }
            catch (Exception e)
            {
                LoggerHelper.Write(LoggerOption.Error, e.Message);

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <param name="outputHandler"></param>
        /// <param name="errorHandler"></param>
        public static void ShowWindowApplicationWithCmd(string path, string args, Action<object, DataReceivedEventArgs> outputHandler, Action<object, DataReceivedEventArgs> errorHandler)
        {

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        UseShellExecute = false,
                        FileName = "cmd.exe",
                        CreateNoWindow = true,
                        LoadUserProfile = true,
                        WindowStyle = ProcessWindowStyle.Normal,
                        WorkingDirectory = Path.GetDirectoryName(path),
                        Arguments = String.Format("/C START /WAIT {0} {1}", path, args),
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };

                if (outputHandler != null)
                    process.OutputDataReceived += new DataReceivedEventHandler(outputHandler);

                if (errorHandler != null)
                    process.ErrorDataReceived += new DataReceivedEventHandler(errorHandler);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
                process.Close();
            }
            catch (Exception e)
            {
                LoggerHelper.Write(LoggerOption.Error, e.Message);

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        public static void ShowWindowApplicationWithCmd(string path, string args)
        {

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        UseShellExecute = true,
                        FileName = "cmd.exe",
                        CreateNoWindow = true,
                        LoadUserProfile = true,
                        WindowStyle = ProcessWindowStyle.Normal,
                        WorkingDirectory = Path.GetDirectoryName(path),
                        Arguments = String.Format("/C START {0} {1}", path, args),
                        ErrorDialog = true,
                    }
                };

                process.Start();
                process.WaitForExit();
            }
            catch (Exception e)
            {
                LoggerHelper.Write(LoggerOption.Error, e.Message);

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <param name="output"></param>
        /// <param name="error"></param>
        public static void ShowWindowApplication(string path, string args, out string output, out string error)
        {

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        UseShellExecute = false,
                        FileName = path,
                        CreateNoWindow = true,
                        LoadUserProfile = true,
                        WindowStyle = ProcessWindowStyle.Normal,
                        WorkingDirectory = Path.GetDirectoryName(path),
                        Arguments = args,
                        ErrorDialog = false,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };

                process.Start();

                output = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();

                process.WaitForExit();
            }
            catch (Exception e)
            {
                LoggerHelper.Write(LoggerOption.Error, e.Message);

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        public static void ShowWindowApplication(string path, string args)
        {

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo(path)
                        {
                            UseShellExecute = true,
                            FileName = path,
                            CreateNoWindow = true,
                            LoadUserProfile = true,
                            WindowStyle = ProcessWindowStyle.Normal,
                            WorkingDirectory = Path.GetDirectoryName(path),
                            Arguments = args
                        }
                };

                process.Start();

                process.WaitForExit(5000);

            }
            catch (Exception e)
            {
                LoggerHelper.Write(LoggerOption.Error, e.Message);

                throw;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <param name="timer"></param>
        /// <returns></returns>
        public static bool AutoLaunchApplicationToSleep(string path, string args, int timer = 5000)
        {

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo(path)
                    {
                        UseShellExecute = true,
                        FileName = path,
                        CreateNoWindow = true,
                        LoadUserProfile = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = Path.GetDirectoryName(path),
                        Arguments = args
                    }
                };

                process.Start();

                Thread.Sleep(timer);

                return true;

            }
            catch (Exception e)
            {
                LoggerHelper.Write(LoggerOption.Error, e.Message);

            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool AutoLaunchApplicationWaitForExit(string path, string args)
        {

            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo(path)
                    {
                        UseShellExecute = true,
                        FileName = path,
                        CreateNoWindow = true,
                        LoadUserProfile = true,
                        WindowStyle = ProcessWindowStyle.Hidden,
                        WorkingDirectory = Path.GetDirectoryName(path),
                        Arguments = args
                    }
                };

                process.Start();

                process.WaitForExit();

                return true;

            }
            catch (Exception e)
            {
                LoggerHelper.Write(LoggerOption.Error, e.Message);

            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="argName"></param>
        /// <returns></returns>
        public static bool KillProcessApplication(string processName, string argName)
        {
            try
            {

                var processes = Process.GetProcessesByName(processName);

                if (processes.Length == 0)
                    return false;

                foreach (var process in processes)
                {
                    var commandLine = ManagementObjectHelper.GetCommandLine(process);

                    if (commandLine.Contains(argName))
                    {
                        process.Kill();

                        Thread.Sleep(5000);

                        return true;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="argName"></param>
        /// <returns></returns>
        public static string GetProcessCommandLine(string processName, string argName)
        {
            try
            {

                var processes = Process.GetProcessesByName(processName);

                if (processes.Length == 0)
                    return String.Empty;

                foreach (var process in processes)
                {
                    var commandLine = ManagementObjectHelper.GetCommandLine(process);

                    if (commandLine.Contains(argName))
                    {
                        return commandLine;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return null;
        }

        /// <summary>
        /// </summary>
        /// <param name="processName"></param>
        /// <param name="argName"></param>
        /// <returns></returns>
        public static bool IsRunningApplication(string processName, string argName)
        {
            try
            {
                var processes = Process.GetProcessesByName(processName);

                if (processes.Length == 0)
                    return false;

                foreach (var process in processes)
                {
                    var commandLine = ManagementObjectHelper.GetCommandLine(process);

                    if (commandLine.Contains(argName))
                    {
                        return true;
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }

    }
}
