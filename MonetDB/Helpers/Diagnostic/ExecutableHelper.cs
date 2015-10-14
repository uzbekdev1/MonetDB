using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MonetDB.Helpers.Diagnostic
{
    /// <summary>
    ///     Encapsulates an executable program.
    ///     This class makes it easy to run a console app and have that app's output appear
    ///     in the parent console's window, and to redirect input and output from/to files.
    /// </summary>
    /// <remarks>
    ///     To use this class:
    ///     (1) Create an instance.
    ///     (2) Set the ProgramFileName property if a filename wasn't specified in the constructor.
    ///     (3) Set other properties if required.
    ///     (4) Call Run().
    /// </remarks>
    public class ExecutableHelper
    {
        #region Private Fields

        private StreamReader _standardError;
        private StreamReader _standardOutput;
        private StreamWriter _standardInput;

        private string _standardInputFileName;
        private string _standardOutputFileName;
        private string _standardErrorFileName;

        private readonly ProcessStartInfo _processStartInfo = new ProcessStartInfo();

        #endregion  // Private Fields

        #region Constructor

        /// <summary>Runs the specified program file name.</summary>
        /// <param name="programFileName">Name of the program file to run.</param>
        public ExecutableHelper(string programFileName)
        {
            ProgramFileName = programFileName;

            _processStartInfo.ErrorDialog = false;
            _processStartInfo.CreateNoWindow = false;
            _processStartInfo.UseShellExecute = false;
            _processStartInfo.RedirectStandardOutput = false;
            _processStartInfo.RedirectStandardError = false;
            _processStartInfo.RedirectStandardInput = false;
            _processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _processStartInfo.Arguments = "";
        }

        /// <summary>Constructor.</summary>
        public ExecutableHelper()
            : this(string.Empty)
        {
        }

        #endregion  // Constructor

        #region Public Properties

        /// <summary>The filename (full pathname) of the executable.</summary>
        public string ProgramFileName
        {
            get { return _processStartInfo.FileName; }

            set { _processStartInfo.FileName = value; }
        }

        /// <summary>The command-line arguments passed to the executable when run. </summary>
        public string Arguments
        {
            get { return _processStartInfo.Arguments; }

            set { _processStartInfo.Arguments = value; }
        }

        /// <summary>The working directory set for the executable when run.</summary>
        public string WorkingDirectory
        {
            get { return _processStartInfo.WorkingDirectory; }

            set { _processStartInfo.WorkingDirectory = value; }
        }

        /// <summary>
        ///     The file to be used if standard input is redirected,
        ///     or null or string.Empty to not redirect standard input.
        /// </summary>
        public string StandardInputFileName
        {
            set
            {
                _standardInputFileName = value;
                _processStartInfo.RedirectStandardInput = !string.IsNullOrEmpty(value);
            }

            get { return _standardInputFileName; }
        }

        /// <summary>
        ///     The file to be used if standard output is redirected,
        ///     or null or string.Empty to not redirect standard output.
        /// </summary>
        public string StandardOutputFileName
        {
            set
            {
                _standardOutputFileName = value;
                _processStartInfo.RedirectStandardOutput = !string.IsNullOrEmpty(value);
            }

            get { return _standardOutputFileName; }
        }

        /// <summary>
        ///     The file to be used if standard error is redirected,
        ///     or null or string.Empty to not redirect standard error.
        /// </summary>
        public string StandardErrorFileName
        {
            set
            {
                _standardErrorFileName = value;
                _processStartInfo.RedirectStandardError = !string.IsNullOrEmpty(value);
            }

            get { return _standardErrorFileName; }
        }

        #endregion  // Public Properties

        #region Public Methods

        /// <summary>Add a set of name-value pairs into the set of environment variables available to the executable.</summary>
        /// <param name="variables">The name-value pairs to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="variables" /> is <see langword="null" />.</exception>
        public void AddEnvironmentVariables(StringDictionary variables)
        {
            if (variables == null)
                throw new ArgumentNullException(nameof(variables));

            var environmentVariables = _processStartInfo.EnvironmentVariables;

            foreach (DictionaryEntry e in variables)
                environmentVariables[(string) e.Key] = (string) e.Value;
        }

        /// <summary>Run the executable and wait until the it has terminated.</summary>
        /// <returns>The exit code returned from the executable.</returns>
        /// <exception cref="InvalidOperationException">
        ///     No file name was specified in the
        ///     <see cref="T:System.Diagnostics.Process" /> component's <see cref="P:System.Diagnostics.Process.StartInfo" />.-or-
        ///     The <see cref="P:System.Diagnostics.ProcessStartInfo.UseShellExecute" /> member of the
        ///     <see cref="P:System.Diagnostics.Process.StartInfo" /> property is true while
        ///     <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardInput" />,
        ///     <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardOutput" />, or
        ///     <see cref="P:System.Diagnostics.ProcessStartInfo.RedirectStandardError" /> is true.
        /// </exception>
        /// <exception cref="ObjectDisposedException">The process object has already been disposed. </exception>
        /// <exception cref="Win32Exception">There was an error in opening the associated file. </exception>
        /// <exception cref="SystemException">
        ///     No process <see cref="P:System.Diagnostics.Process.Id" /> has been set, and a
        ///     <see cref="P:System.Diagnostics.Process.Handle" /> from which the <see cref="P:System.Diagnostics.Process.Id" />
        ///     property can be determined does not exist.-or- There is no process associated with this
        ///     <see cref="T:System.Diagnostics.Process" /> object.-or- You are attempting to call
        ///     <see cref="M:System.Diagnostics.Process.WaitForExit" /> for a process that is running on a remote computer. This
        ///     method is available only for processes that are running on the local computer.
        /// </exception>
        /// <exception cref="ThreadStateException">
        ///     The caller attempted to join a thread that is in the
        ///     <see cref="F:System.Threading.ThreadState.Unstarted" /> state.
        /// </exception>
        /// <exception cref="ThreadInterruptedException">The thread is interrupted while waiting. </exception>
        /// <exception cref="ArgumentNullException">
        ///     The value that specifies the
        ///     <see cref="P:System.Diagnostics.Process.StartInfo" /> is null.
        /// </exception>
        public int Run()
        {
            Thread standardInputThread = null;
            Thread standardOutputThread = null;
            Thread standardErrorThread = null;

            _standardInput = null;
            _standardError = null;
            _standardOutput = null;

            var exitCode = -1;

            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = _processStartInfo;
                    process.Start();


                    if (process.StartInfo.RedirectStandardInput)
                    {
                        _standardInput = process.StandardInput;
                        standardInputThread = StartThread(SupplyStandardInput, "StandardInput");
                    }

                    if (process.StartInfo.RedirectStandardError)
                    {
                        _standardError = process.StandardError;
                        standardErrorThread = StartThread(WriteStandardError, "StandardError");
                    }

                    if (process.StartInfo.RedirectStandardOutput)
                    {
                        _standardOutput = process.StandardOutput;
                        standardOutputThread = StartThread(WriteStandardOutput, "StandardOutput");
                    }

                    process.WaitForExit();
                    exitCode = process.ExitCode;
                }
            }

            finally // Ensure that the threads do not persist beyond the process being run
            {
                if (standardInputThread != null)
                    standardInputThread.Join();

                if (standardOutputThread != null)
                    standardOutputThread.Join();

                if (standardErrorThread != null)
                    standardErrorThread.Join();
            }

            return exitCode;
        }

        #endregion  // Public Methods

        #region Private Methods

        /// <summary>Start a thread.</summary>
        /// <param name="startInfo">start information for this thread</param>
        /// <param name="name">name of the thread</param>
        /// <returns>thread object</returns>
        private static Thread StartThread(ThreadStart startInfo, string name)
        {
            var t = new Thread(startInfo)
            {
                IsBackground = true,
                Name = name
            };
            t.Start();

            return t;
        }

        /// <summary>Thread which supplies standard input from the appropriate file to the running executable.</summary>
        private void SupplyStandardInput()
        {
            // feed text from the file a line at a time into the standard input stream

            using (var reader = File.OpenText(_standardInputFileName))
            using (var writer = _standardInput)
            {
                writer.AutoFlush = true;

                for (;;)
                {
                    var textLine = reader.ReadLine();

                    if (textLine == null)
                        break;

                    writer.WriteLine(textLine);
                }
            }
        }

        /// <summary>Thread which outputs standard output from the running executable to the appropriate file.</summary>
        private void WriteStandardOutput()
        {
            using (var writer = File.CreateText(_standardOutputFileName))
            using (var reader = _standardOutput)
            {
                writer.AutoFlush = true;

                for (;;)
                {
                    var textLine = reader.ReadLine();

                    if (textLine == null)
                        break;

                    writer.WriteLine(textLine);
                }
            }

            if (File.Exists(_standardOutputFileName))
            {
                var info = new FileInfo(_standardOutputFileName);

                // if the error info is empty or just contains eof etc.

                if (info.Length < 4)
                    info.Delete();
            }
        }

        /// <summary>Thread which outputs standard error output from the running executable to the appropriate file.</summary>
        private void WriteStandardError()
        {
            using (var writer = File.CreateText(_standardErrorFileName))
            using (var reader = _standardError)
            {
                writer.AutoFlush = true;

                for (;;)
                {
                    var textLine = reader.ReadLine();

                    if (textLine == null)
                        break;

                    writer.WriteLine(textLine);
                }
            }

            if (File.Exists(_standardErrorFileName))
            {
                var info = new FileInfo(_standardErrorFileName);

                // if the error info is empty or just contains eof etc.

                if (info.Length < 4)
                    info.Delete();
            }
        }

        #endregion  // Private Methods
    }
}