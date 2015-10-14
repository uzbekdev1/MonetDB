using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using MonetDB.Enums.Logging;
using MonetDB.Extensions;

namespace MonetDB.Helpers.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public static class LoggerHelper
    {
        private static LoggerMode _mode;

        static LoggerHelper()
        {
            _mode = LoggerMode.Console;
        }

        /// <summary>
        /// 
        /// </summary>
        public static LoggerMode Mode
        {
            get { return _mode; }
            set { _mode = value; }
        }

        /// <summary>
        /// </summary>
        /// <param name="option"></param>
        /// <param name="msg"></param>
        public static void Write(LoggerOption option, object msg)
        {
            Write(option, msg.To<string>());
        }

        /// <summary>
        /// </summary>
        /// <param name="option"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public static void Write(LoggerOption option, string format, params object[] args)
        {
            switch (_mode)
            {
                case LoggerMode.Console:
                    {
                        #region Console

                        Console.ResetColor();

                        switch (option)
                        {
                            case LoggerOption.None:
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                                break;
                            case LoggerOption.Error:
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                }
                                break;
                            case LoggerOption.Warning:
                                {
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                }
                                break;
                            case LoggerOption.Info:
                                {
                                    Console.ForegroundColor = ConsoleColor.Green;
                                }
                                break;
                        }

                        if (args.Length > 0)
                        {
                            Console.WriteLine(format, args);
                        }
                        else
                        {
                            Console.WriteLine(format);
                        }

                        Console.ResetColor();

                        #endregion
                    }
                    break;
                case LoggerMode.Trace:
                    {
                        #region Trace

                        switch (option)
                        {
                            case LoggerOption.None:
                                {
                                    if (args.Length > 0)
                                    {
                                        Trace.WriteLine(string.Format(format, args));
                                    }
                                    else
                                    {
                                        Trace.WriteLine(format);
                                    }
                                }
                                break;
                            case LoggerOption.Error:
                                {
                                    if (args.Length > 0)
                                    {
                                        Trace.TraceError(format, args);
                                    }
                                    else
                                    {
                                        Trace.TraceError(format);
                                    }
                                }
                                break;
                            case LoggerOption.Warning:
                                {
                                    if (args.Length > 0)
                                    {
                                        Trace.TraceWarning(format, args);
                                    }
                                    else
                                    {
                                        Trace.TraceWarning(format);
                                    }
                                }
                                break;
                            case LoggerOption.Info:
                                {
                                    if (args.Length > 0)
                                    {
                                        Trace.TraceInformation(format, args);
                                    }
                                    else
                                    {
                                        Trace.TraceInformation(format);
                                    }
                                }
                                break;
                        }


                        #endregion
                    }
                    break;
                case LoggerMode.Event:
                    {
                        #region Event

                        switch (option)
                        {
                            case LoggerOption.None:
                                {

                                }
                                break;
                            case LoggerOption.Error:
                                {

                                }
                                break;
                            case LoggerOption.Warning:
                                {

                                }
                                break;
                            case LoggerOption.Info:
                                {

                                }
                                break;
                        }


                        #endregion
                    }
                    break;
            }
        }


    }
}
