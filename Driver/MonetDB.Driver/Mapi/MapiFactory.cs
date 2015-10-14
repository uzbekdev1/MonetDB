using System;
using MonetDB.DataProvider;
using MonetDB.Driver.Data;
using MonetDB.Driver.Exceptions;

namespace MonetDB.Driver.Mapi
{
    /// <summary>
    /// Handles the accounting for the connections to the database.  Handles the connection
    /// pooling of the connections.
    /// </summary>
    public static class MapiFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private const string Language = "sql";
        private const string UserName = "monetdb";
        private const string Password = "monetdb";


        /// <summary>
        /// 
        /// </summary>
        public static void DieQueryError(MapiConnection connectionHandle, MapiHdl queryHandle)
        {

            if (queryHandle.Ptr == IntPtr.Zero ||
                connectionHandle.Ptr == IntPtr.Zero ||
                MapiLib.MapiError(connectionHandle).Ptr != IntPtr.Zero)
            {
                throw new MonetDbException(MapiLib.MapiErrorString(connectionHandle));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionHandle"></param>
        /// <exception cref="MonetDbException"></exception>
        public static void DieConnectionError(MapiConnection connectionHandle)
        {

            if (connectionHandle.Ptr == IntPtr.Zero ||
                MapiLib.MapiError(connectionHandle).Ptr != IntPtr.Zero)
            {
                throw new MonetDbException(MapiLib.MapiErrorString(connectionHandle));
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <exception cref="MonetDbException"></exception>
        public static MapiHdl GetQueryHandle(MapiConnection connection, string query)
        {
            var queryHandle = MapiLib.MapiQuery(connection, query);

            DieQueryError(connection, queryHandle);

            return queryHandle;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryHandle"></param>
        public static void CloseQueryHandle(MapiHdl queryHandle)
        {
            MapiLib.MapiFinish(queryHandle);
            MapiLib.MapiCloseHandle(queryHandle);
        }

        /// <summary>
        /// Returns a connection from the connection pool.
        /// </summary> 
        /// <param></param>
        /// <param name="connectionInfo"></param>
        /// <returns></returns>
        public static MapiConnection GetConnectionhandle(MonetDbConnectionInfo connectionInfo)
        {
            var connection = MapiLib.MapiConnect(connectionInfo.Host, connectionInfo.GetPort(), UserName, Password, Language, connectionInfo.Database);

            DieConnectionError(connection);

            return connection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        public static void CloseConnectionHandle(MapiConnection connection)
        {
            MapiLib.MapiDisconnect(connection);
            MapiLib.MapiDestroy(connection);
        }
    }
}
