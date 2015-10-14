using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using MonetDB.DataProvider;
using MonetDB.Extensions;
using MonetDB.Helpers;
using MonetDB.Models;
using MonetDB.TCP.Enums;
using MonetDB.TCP.Models;
using Hik.Communication.ScsServices.Client;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;

namespace MonetDB.TCP.Client
{
    /// <summary>
    /// 
    /// </summary>
    public static class MonetDbtcpProxy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        private static IScsServiceClient<IMonetDbtcpService> GetFactory(string host)
        {
            var serviceClient = ScsServiceClientBuilder.CreateClient<IMonetDbtcpService>(new ScsTcpEndPoint(host, MonetSettings.Tcp.Port));

            serviceClient.Timeout = MonetDbtcpSettings.Tcp.TimeOut;
            serviceClient.ConnectTimeout = MonetDbtcpSettings.Tcp.TimeOut;

            return serviceClient;
        }

        /// <summary>
        /// </summary>
        /// <param name="host"></param>
        /// <param name="database"></param>
        public static bool DropDatabase(string host, string database)
        {
            var serviceClient = GetFactory(host);

            return serviceClient.ServiceProxy.DropDatabase(database);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public static bool KillDatabase(string host, string database)
        {
            var serviceClient = GetFactory(host);

            return serviceClient.ServiceProxy.KillDatabase(database);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public static DatabaseStatus StatusDatabase(string host, string database)
        {
            var serviceClient = GetFactory(host);

            return serviceClient.ServiceProxy.StatusDatabase(database);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="database"></param>
        public static bool CreateDatabase(string host, string database)
        {
            var serviceClient = GetFactory(host);

            return serviceClient.ServiceProxy.CreateDatabase(database);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="host"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        public static bool LoadDatabase(string host, string database)
        {
            var serviceClient = GetFactory(host);

            return serviceClient.ServiceProxy.LoadDatabase(database);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static List<MonetDatabaseInfo> GetDatabases(string host)
        {
            var serviceClient = GetFactory(host);

            return serviceClient.ServiceProxy.GetDatabases();
        }

        /// <summary>
        /// </summary>
        /// <param name="monetCredentialsModel"></param>
        /// <param name="schemaName"></param>
        /// <param name="tableName"></param>
        /// <param name="columns"></param>
        /// <param name="rows"></param>
        /// <param name="batchSize"></param>
        public static bool BulkCopy(MonetDbConnectionInfo monetCredentialsModel, string schemaName, string tableName, TableColumnInfo[] columns, string[] rows, int batchSize)
        {
            var serviceClient = GetFactory(monetCredentialsModel.Host);
            var model = new MonetBulkCopyModel(monetCredentialsModel)
            {
                SchemaName = schemaName,
                TableName = tableName,
                Columns = columns,
                BatchSize = batchSize
            };
            var rowsBytes = rows.ToBytes();

            model.Rows = rowsBytes.Lz4Compress();

            return serviceClient.ServiceProxy.BulkCopy(model);
        }


        public static int GetPort(string host, string database)
        {
            var serviceClient = GetFactory(host);

            return serviceClient.ServiceProxy.GetPort(database);
        }
    }
}
