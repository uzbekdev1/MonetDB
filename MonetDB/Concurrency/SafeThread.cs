using System;
using System.Globalization;
using System.Threading;

namespace MonetDB.Concurrency
{
    /// <summary>
    /// simple no-arg delegate type; can use this for anonymous methods, e.g.
    /// <code>
    ///     SafeThread safeThrd = new SafeThread((SimpleDelegate) delegate { dosomething(); });
    /// </code>
    /// </summary>
    public delegate void SimpleDelegate();

    /// <summary>
    /// delegate for thread-threw-exception event
    /// </summary>
    /// <param name="thrd">the SafeThread that threw the exception</param>
    /// <param name="ex">the exception throws</param>
    public delegate void ThreadThrewExceptionHandler(SafeThread thrd, Exception ex);

    /// <summary>
    /// delegate for thread-completed event
    /// </summary>
    /// <param name="thrd">the SafeThread that completed processing</param>
    /// <param name="hadException">true if the thread terminated due to an exception</param>
    /// <param name="ex">the exception that terminated the thread, or null if completed successfully</param>
    public delegate void ThreadCompletedHandler(SafeThread thrd, bool hadException, Exception ex);

    /// <summary>
    /// This class implements a Thread wrapper to trap unhandled exceptions 
    /// thrown by the thread-start delegate. Add ThreadException event 
    /// handlers to be notified of such exceptions and take custom actions
    /// (such as restart, clean-up, et al, depending on what the SafeThread was
    /// doing in your application). Add ThreadCompleted event handlers to be
    /// notified when the thread has completed processing.
    /// </summary>
    public class SafeThread : MarshalByRefObject
    {
          
        /// <summary>
        /// gets the internal thread being used
        /// </summary>
        public Thread ThreadObject { get; private set; }

        /// <summary>
        /// the thread-start object, if any
        /// </summary>
        private readonly ThreadStart _ts;

        /// <summary>
        /// the parameterized thread-start object, if any
        /// </summary>
        private readonly ParameterizedThreadStart _pts;

        /// <summary>
        /// the SimpleDelegate target, if any
        /// </summary>
        private readonly SimpleDelegate _dlg;

        /// <summary>
        /// gets the thread-start argument, if any
        /// </summary>
        public object ThreadStartArg { get; private set; }

        /// <summary>
        /// gets the last exception thrown
        /// </summary>
        public Exception LastException { get; private set; }

        /// <summary>
        /// the name of the internal thread
        /// </summary>
        private string _name;

        /// <summary>
        /// gets/sets the name of the internal thread
        /// </summary>
        public string Name
        {
            get { return _name ?? "Thread#" + GetHashCode(); }
            set { _name = value; }
        }

        /// <summary>
        /// object tag - use to hold extra info about the SafeThread
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// default constructor for SafeThread
        /// </summary>
        public SafeThread()
        {
            ShouldReportThreadAbort = false;
        }

        /// <summary>
        /// SafeThread constructor using ThreadStart object
        /// </summary>
        /// <param name="ts">ThreadStart object to use</param>
        public SafeThread(ThreadStart ts)
            : this()
        {
            _ts = ts;
            ThreadObject = new Thread(ts);
        }

        /// <summary>
        /// SafeThread constructor using ParameterizedThreadStart object
        /// </summary>
        /// <param name="pts">ParameterizedThreadStart to use</param>
        public SafeThread(ParameterizedThreadStart pts)
            : this()
        {
            _pts = pts;
            ThreadObject = new Thread(pts);
        }

        /// <summary>
        /// SafeThread constructor using SimpleDelegate object for anonymous methods, e.g.
        /// <code>
        ///     SafeThread safeThrd = new SafeThread((SimpleDelegate) delegate { dosomething(); });
        /// </code>
        /// </summary>
        /// <param name="sd"></param>
        public SafeThread(SimpleDelegate sd)
            : this()
        {
            _dlg = sd;
            _pts = new ParameterizedThreadStart(this.CallDelegate);
            ThreadObject = new Thread(_pts);
        }

        /// <summary>
        /// thread-threw-exception event
        /// </summary>
        public event ThreadThrewExceptionHandler ThreadException;

        /// <summary>
        /// called when a thread throws an exception
        /// </summary>
        /// <param name="ex">Exception thrown</param>
        public void OnThreadException(Exception ex)
        {
            try
            {
                if (ex is ThreadAbortException && !ShouldReportThreadAbort)
                {
                    return;
                }

                if (ThreadException != null)
                {
                    ThreadException.Invoke(this, ex);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// thread-completed event
        /// </summary>
        public event ThreadCompletedHandler ThreadCompleted;

        /// <summary>
        /// called when a thread completes processing
        /// </summary>
        private void OnThreadCompleted(bool bHadException, Exception ex)
        {
            try
            {
                if (ThreadCompleted != null)
                {
                    ThreadCompleted.Invoke(this, bHadException, ex);
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// starts thread with target if any
        /// </summary>
        private void StartTarget()
        {
            Exception exceptn = null;
            var bHadException = false;
            try
            { 
                if (_ts != null)
                {
                    _ts.Invoke();
                }
                else if (_pts != null)
                {
                    _pts.Invoke(ThreadStartArg);
                }
            }
            catch (Exception ex)
            {
                bHadException = true;
                exceptn = ex;
                this.LastException = ex;
                OnThreadException(ex);
            }
            finally
            {
                OnThreadCompleted(bHadException, exceptn);
            }
        }

        /// <summary>
        /// thread-start internal method for SimpleDelegate target
        /// </summary>
        /// <param name="arg">unused</param>
        private void CallDelegate(object arg)
        {
            this._dlg.Invoke();
        }

        /// <summary>
        /// starts thread execution
        /// </summary>
        public void Start()
        {
            ThreadObject = new Thread(new ThreadStart(StartTarget))
            {
                Name = this.Name
            };

            if (_aptState != null)
            {
                ThreadObject.TrySetApartmentState((ApartmentState) _aptState);
            }

            ThreadObject.Start();
        }

        /// <summary>
        /// starts thread execution with parameter
        /// </summary>
        /// <param name="val">parameter object</param>
        public void Start(object val)
        {
            ThreadStartArg = val;
            Start();
        }

        /// <summary>
        /// gets/sets a flag to control whether thread-abort exception is reported or not
        /// </summary>
        public bool ShouldReportThreadAbort { get; set; }


        /// <summary>
        /// abort the thread execution
        /// </summary>
        public void Abort()
        { 
            ThreadObject.Abort();
        }

        /// <summary>
        /// gets or sets the Culture for the current thread.
        /// </summary>
        public CultureInfo CurrentCulture => ThreadObject != null ? ThreadObject.CurrentCulture : null;

        /// <summary>
        /// gets or sets the current culture used by the Resource Manager
        /// to look up culture-specific resources at run time.
        /// </summary>
        public CultureInfo CurrentUiCulture => ThreadObject != null ? ThreadObject.CurrentUICulture : null;

        /// <summary>
        /// gets an System.Threading.ExecutionContext object that contains information
        /// about the various contexts of the current thread.
        /// </summary>
        public ExecutionContext ExecutionContext => ThreadObject != null ? ThreadObject.ExecutionContext : null;

        /// <summary>
        /// Returns an System.Threading.ApartmentState value indicating the apparent state.
        /// </summary>
        /// <returns></returns>
        public ApartmentState GetApartmentState()
        {
            return ThreadObject != null ? ThreadObject.GetApartmentState() : ApartmentState.Unknown;
        }

        /// <summary>
        /// Interrupts a thread that is in the WaitSleepJoin thread state.
        /// </summary>
        public void Interrupt()
        {
            if (ThreadObject != null)
            {
                ThreadObject.Interrupt();
            }
        }

        /// <summary>
        /// gets a value indicating the execution status of the thread
        /// </summary>
        public bool IsAlive => ThreadObject != null && ThreadObject.IsAlive;

        /// <summary>
        /// Gets or sets a value indicating whether or not a thread is a background thread
        /// </summary>
        public bool IsBackground
        {
            get { return ThreadObject != null && ThreadObject.IsBackground; }
            set
            {
                if (ThreadObject != null)
                {
                    ThreadObject.IsBackground = value;
                }
            }
        }

        /// <summary>
        /// gets a value indicating whether or not a thread belongs to the managed thread pool
        /// </summary>
        public bool IsThreadPoolThread => ThreadObject != null && ThreadObject.IsThreadPoolThread;

        /// <summary>
        /// Blocks the calling thread until a thread terminates,
        /// while continuing to perform standard COM and SendMessage pumping.
        /// </summary>
        public void Join()
        {
            if (ThreadObject != null)
            {
                ThreadObject.Join();
            }
        }

        /// <summary>
        /// Blocks the calling thread until a thread terminates or the specified time elapses,
        /// while continuing to perform standard COM and SendMessage pumping.
        /// </summary>
        /// <param name="millisecondsTimeout">the number of milliseconds to wait for the
        /// thread to terminate</param>
        public bool Join(int millisecondsTimeout)
        {
            return ThreadObject != null && ThreadObject.Join(millisecondsTimeout);
        }

        /// <summary>
        /// Blocks the calling thread until a thread terminates or the specified time elapses,
        /// while continuing to perform standard COM and SendMessage pumping.
        /// </summary>
        /// <param name="timeout">a System.TimeSpan set to the amount of time to wait
        /// for the thread to terminate </param>
        public bool Join(TimeSpan timeout)
        {
            return ThreadObject != null && ThreadObject.Join(timeout);
        }

        /// <summary>
        /// Gets a unique identifier for the current managed thread
        /// </summary>
        public int ManagedThreadId => ThreadObject != null ? ThreadObject.ManagedThreadId : 0;

        /// <summary>
        /// gets or sets a value indicating the scheduling priority of a thread
        /// </summary>
        public ThreadPriority Priority
        {
            get { return ThreadObject != null ? ThreadObject.Priority : ThreadPriority.Lowest; }
            set
            {
                if (ThreadObject != null)
                {
                    ThreadObject.Priority = value;
                }
            }
        }

        private object _aptState;

        /// <summary>
        /// sets the ApartmentState of a thread before it is started
        /// </summary>
        /// <param name="state">ApartmentState</param>
        public void SetApartmentState(ApartmentState state)
        {
            if (ThreadObject == null)
            {
                _aptState = state;
            }
            else
            {
                ThreadObject.SetApartmentState(state);
            }
        }

        /// <summary>
        /// gets a value containing the states of the current thread
        /// </summary>
        public ThreadState ThreadState => ThreadObject != null ? ThreadObject.ThreadState : ThreadState.Unstarted;

        /// <summary>
        /// returns a System.String that represents the current System.Object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ThreadObject != null ? ThreadObject.ToString() : base.ToString();
        }

        /// <summary>
        /// sets the ApartmentState of a thread before it is started
        /// </summary>
        /// <param name="state">ApartmentState</param>
        public bool TrySetApartmentState(ApartmentState state)
        {
            if (ThreadObject != null)
            {
                return ThreadObject.TrySetApartmentState(state);
            }
            _aptState = state;
            return false;
        }
    }
}