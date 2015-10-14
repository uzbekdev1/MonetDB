using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MonetDB.Driver.Database;
using MonetDB.Driver.Handlers;
using MonetDB.Driver.Helpers;
using MonetDB.Enums.Logging;
using MonetDB.Extensions;
using MonetDB.Helpers;
using MonetDB.Helpers.Logging;
using MonetDB.Models;
using MonetDB.TCP.Client;
using MonetDB.TCP.Models;

namespace MonetDB.Driver.BulkCopy
{
    /// <summary>
    /// </summary>
    public sealed class MonetBulkCopy : IDisposable
    {

        #region Disposable

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Events

        /// <summary>
        /// </summary>
        public event RowsCopiedEventHandler RowsCopied;

        #endregion

        #region Fields

        private readonly MonetDatabase _db;

        #endregion

        #region Ctors

        /// <summary>
        /// </summary>
        /// <param name="db"></param>
        public MonetBulkCopy(MonetDatabase db)
        {
            _db = db;
        }

        #endregion

        #region Props

        /// <summary>
        /// </summary>
        public int BatchSize { get; set; }

        /// <summary>
        /// </summary>
        public int NotifyAfter { get; set; }

        #endregion

        #region Members

        private void BulkCopy(string schemaName, string tableName, TableColumnInfo[] columns,
            IDataReader reader)
        {
            try
            {

                long sizeCounter = 0, rowIndex = 0;
                var secondValues = reader.GetMatrixInstance(BatchSize);

                while (reader.Read())
                {
                    reader.GetValues(secondValues[sizeCounter]);

                    sizeCounter++;

                    if (NotifyAfter > 0 && (rowIndex % NotifyAfter == 0 && rowIndex > 0))
                    {
                        RowsCopied?.Invoke(this, new RowsCopiedEventArgs(rowIndex));
                    }

                    if (sizeCounter == BatchSize)
                    {
                        _db.BulkCopy(schemaName, tableName, columns, secondValues, BatchSize);

                        sizeCounter = 0;
                        secondValues = reader.GetMatrixInstance(BatchSize);
                    }

                    rowIndex++;
                }

                var lastValues = new object[sizeCounter][];

                for (var lastIndex = 0; lastIndex < sizeCounter; lastIndex++)
                    lastValues[lastIndex] = secondValues[lastIndex];

                _db.BulkCopy(schemaName, tableName, columns, lastValues, BatchSize);

                RowsCopied?.Invoke(this, new RowsCopiedEventArgs(rowIndex));
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);

                RowsCopied?.Invoke(this, new RowsCopiedEventArgs
                {
                    Abort = true
                });
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="schemaName"></param>
        /// <param name="reader"></param>
        public void Upload(string schemaName, string tableName, IDataReader reader)
        {
            var columns = reader.GetColumns();

            _db.CreateTableIfNotExists(schemaName, tableName, columns);

            BulkCopy(schemaName, tableName, columns, reader);

        }

        #endregion

    }
}