using System;
using MonetDB.Extensions;
using MonetDB.Helpers;

namespace MonetDB.DataProvider
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MonetDbConnectionInfo
    {
        private int _port;

        /// <summary>
        /// 
        /// </summary>
        public MonetDbConnectionInfo()
        {
            Host = "127.0.0.1";
            Database = "demo";
        }

        /// <exception cref="ArgumentException">A connection string must be provided.</exception>
        public MonetDbConnectionInfo(string connectionString)
        {
            Host = DnsHelper.GetIpResolver(connectionString.GetConnectionValue(MonetSettings.Connection.Host));
            Database = connectionString.GetConnectionValue(MonetSettings.Connection.Database);
        }

        /// <summary>
        /// 
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int GetPort()
        {
            return _port;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public void SetPort(int port)
        {
            _port = port;
        }

        /// <summary>
        /// 
        /// </summary>
        public string UserName => "monetdb";

        /// <summary>
        /// 
        /// </summary>
        public string Password => "monetdb";


        /// <summary>
        /// 
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"database={Database};host={Host};";
        }

    }
}