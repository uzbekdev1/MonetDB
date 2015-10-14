using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonetDB.Helpers.Win32;

namespace MonetDB
{
    /// <summary>
    /// 
    /// </summary>
    public static class MonetSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public struct Tcp
        {

            /// <summary>
            /// 
            /// </summary>
            public const int Port = 54321;

        } 

        /// <summary>
        /// 
        /// </summary>
        public struct Connection
        {  
            /// <summary>
            /// 
            /// </summary>
            public const string Host = "host";

            /// <summary>
            /// 
            /// </summary>
            public const string Database = "database"; 
        }

        /// <summary>
        /// 
        /// </summary>
        public const string MonetConfigurationFile = "MonetConfiguration.xml";
    }
}
