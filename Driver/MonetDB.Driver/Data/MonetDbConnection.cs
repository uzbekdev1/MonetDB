using System;
using System.Data;
using MonetDB.DataProvider;
using MonetDB.Driver.Database;
using MonetDB.Driver.Mapi;
using MonetDB.Models;
using MonetDB.Driver.Exceptions;

namespace MonetDB.Driver.Data
{
    /// <summary>
    /// </summary>
    public class MonetDbConnection : IDbConnection
    {
        private MapiConnection _connection;
        private string _connectionString;
        private MonetDatabase _database;

        /// <summary>
        /// 
        /// </summary>
        public MonetDbConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            throw new MonetDbException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="il"></param>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IsolationLevel il)
        {
            throw new MonetDbException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseName"></param>
        public void ChangeDatabase(string databaseName)
        {
            throw new MonetDbException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateCommand()
        {
            return new MonetDbCommand(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Open()
        {
            if (State == ConnectionState.Open)
                throw new MonetDbException("Connection already open.");

            _connection = MapiFactory.GetConnectionhandle(_database.ConnectionInfo);

            State = ConnectionState.Open;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Close()
        {
            if (State == ConnectionState.Closed)
                return;

            MapiFactory.CloseConnectionHandle(_connection);

            State = ConnectionState.Closed;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ConnectionString
        {
            get { return _connectionString; }
            set
            {
                _connectionString = value;
                _database = new MonetDatabase(value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public MapiConnection GetConnectionHandle()
        {
            return _connection;
        }

        /// <summary>
        /// 
        /// </summary>
        public int ConnectionTimeout { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Database { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public ConnectionState State { get; private set; }

    }
}