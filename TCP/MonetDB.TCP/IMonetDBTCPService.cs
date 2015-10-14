using System;
using System.Collections.Generic;
using MonetDB.Models;
using MonetDB.TCP.Enums;
using MonetDB.TCP.Models;
using Hik.Communication.ScsServices.Service;

namespace MonetDB.TCP
{

    /// <summary>
    /// 
    /// </summary>
    [ScsService]
    public interface IMonetDbtcpService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param> 
        /// <returns></returns>
        bool CreateDatabase(string database);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        bool DropDatabase(string database);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        bool LoadDatabase(string database);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        bool KillDatabase(string database);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DatabaseStatus StatusDatabase(string database);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<MonetDatabaseInfo> GetDatabases();

        /// <summary>
        /// 
        /// </summary> 
        /// <returns></returns>
        bool BulkCopy(MonetBulkCopyModel model);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        int GetPort(string database);

    }
}
