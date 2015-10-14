using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonetDB.TCP
{
    /// <summary>
    /// 
    /// </summary>
    public static class MonetDbtcpSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public struct Tcp
        {

            /// <summary>
            /// connection  command time out
            /// </summary>
            public const int TimeOut = 24 * 60 * 60 * 1000;

        }

        public const string MonetConfigurationFile = "MonetServerConfiguration.xml";

    }
}
