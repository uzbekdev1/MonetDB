using System;

namespace MonetDB.Driver.Handlers
{
    /// <summary>
    /// 
    /// </summary>
    public class RowsCopiedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Abort { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public long RowsCopied { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowsCopied"></param>
        public RowsCopiedEventArgs(long rowsCopied)
        {
            RowsCopied = rowsCopied;
        }

        /// <summary>
        /// 
        /// </summary>
        public RowsCopiedEventArgs()
            : this(0)
        {
        }
    }
}