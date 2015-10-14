using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using MonetDB.Connector.Data;
using MonetDB.Connector.Data.Helpers;
using MonetDB.DataProvider;
using MonetDB.Driver.BulkCopy;
using MonetDB.Driver.Database;
using MonetDB.Driver.Handlers;
using MonetDB.Enums.Logging;
using MonetDB.Extensions;
using MonetDB.Helpers.Logging;
using MonetDB.Models;
using MonetDB.Connector.Data.Enums;

namespace MonetDB.GUI.Tools
{
    internal partial class BulkLoaderForm : Form
    {
        #region Fields

        private SchemaTableInfo _tableInfo;
        private BackgroundWorker _worker;
        private IDataConnector _dataConnector;
        private readonly MonetDbConnectionInfo _connectionInfo;
         
        #endregion

        #region Ctors

        public BulkLoaderForm(MonetDbConnectionInfo monetDbConnectionInfo)
        {
            InitializeComponent();

            _connectionInfo = monetDbConnectionInfo;

            _worker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            _dataConnector = DataConnectorFactoryHelper.GetDataFactory(DataSourceType.SqlServer);
        }

        #endregion

        #region Members

        private void BulkLoaderForm_Load(object sender, EventArgs e)
        {

            rtbxSQLConnectionString.Text = $@"Server={_connectionInfo.Host};Database={_connectionInfo.Database};User Id=sa;Password=;";

        }

        private void btnSQLConnect_Click(object sender, EventArgs e)
        {
            lsvSQLCols.Items.Clear();
            cbxSQLSchema.DataSource = null;

            try
            {
                btnSQLConnect.Enabled = false;

                var sqlConnectionString = rtbxSQLConnectionString.Text;

                if (string.IsNullOrWhiteSpace(sqlConnectionString))
                    return;

                _dataConnector.ConnectionString = sqlConnectionString;

                var tables = _dataConnector.GetSchemaTables();

                if (tables.Count == 0)
                    return;

                MessageBox.Show(@"Connected", @"Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoggerHelper.Write(LoggerOption.Info, "SqlServer connected : {0}", sqlConnectionString);

                cbxSQLSchema.DataSource = tables;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoggerHelper.Write(LoggerOption.Error, "Error: {0}", exp.Message);
            }
            finally
            {
                btnSQLConnect.Enabled = true;
            }
        }

        private void cbxSQLSchema_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var sqlConnectionString = rtbxSQLConnectionString.Text;

                if (string.IsNullOrWhiteSpace(sqlConnectionString))
                    return;

                _dataConnector.ConnectionString = sqlConnectionString;

                if (cbxSQLSchema.SelectedIndex == -1)
                    return;

                _tableInfo = (SchemaTableInfo)cbxSQLSchema.SelectedItem;

                if (_tableInfo == null)
                    return;

                tbxMonetSchema.Text = string.IsNullOrWhiteSpace(_tableInfo.Schema)
                    ? $"\"{_tableInfo.Table}\""
                    : $"\"{_tableInfo.Schema}\".\"{_tableInfo.Table}\"";

                var items = _dataConnector.GetTableColumns(_tableInfo.Table, _tableInfo.Schema);

                if (items.Count == 0)
                    return;

                lsvSQLCols.Items.Clear();
                lsvSQLCols.Items.AddRange(items.Select(s => new ListViewItem(new[]
                {
                    s.Column,
                    s.DataType,
                    s.Nullable.To<string>()
                })).ToArray());

                LoggerHelper.Write(LoggerOption.Info, "Getting {0} tables from SqlServer", items.Count);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoggerHelper.Write(LoggerOption.Error, "Error: {0}", exp.Message);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (cbxSQLSchema.SelectedIndex == -1)
                return;

            var db = new MonetDatabase(_connectionInfo.ToString());
            var cols = lsvSQLCols.CheckedItems.Count > 0
                ? string.Join(",",
                    lsvSQLCols.CheckedItems.Cast<ListViewItem>()
                        .Select(
                            s => string.Format("{1}{0}{2}", s.Text, _dataConnector.OpenScope, _dataConnector.CloseScope)))
                : "*";
            var query = string.IsNullOrWhiteSpace(_tableInfo.Schema)
                ? string.Format("SELECT {0} FROM {2}{1}{3} ", cols, _tableInfo.Table, _dataConnector.OpenScope,
                    _dataConnector.CloseScope)
                : string.Format("SELECT {0} FROM {3}{1}{4}.{3}{2}{4} ", cols, _tableInfo.Schema, _tableInfo.Table,
                    _dataConnector.OpenScope, _dataConnector.CloseScope);
            var reader = _dataConnector.ExecuteReader(query);
            var batchSize = Convert.ToInt32(nudBatchSize.Value);

            var prog = TaskbarManager.Instance;
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            _worker.DoWork += delegate (object o, DoWorkEventArgs args)
            {
                prog.SetProgressState(TaskbarProgressBarState.Normal);

                var bulkCopy = new MonetBulkCopy(db)
                {
                    BatchSize = batchSize,
                    NotifyAfter = batchSize
                };

                bulkCopy.RowsCopied += delegate (object o1, RowsCopiedEventArgs args1)
                {
                    if (args1.Abort)
                        return;

                    if (_worker.CancellationPending)
                    {
                        args.Cancel = true;

                        throw new OperationCanceledException();
                    }
                    var percent = Math.Round(100 * (decimal)args1.RowsCopied / _tableInfo.RowsCount, 3,
                        MidpointRounding.AwayFromZero);
                    var userState = new KeyValuePair<decimal, long>(percent, args1.RowsCopied);

                    _worker.ReportProgress(0, userState);
                };

                bulkCopy.Upload(_tableInfo.Schema, _tableInfo.Table, reader);
            };
            _worker.ProgressChanged += delegate (object o, ProgressChangedEventArgs args)
            {
                var userState = (KeyValuePair<decimal, long>)args.UserState;

                lblCopyElapsed.Text = $"{stopWatch.Elapsed:g}";
                pbrCopyStatus.Value = (int)userState.Key;


                prog.SetProgressValue((int)userState.Key, pbrCopyStatus.Maximum);

                Thread.Sleep(100);
            };
            _worker.RunWorkerCompleted += delegate (object o, RunWorkerCompletedEventArgs args)
            {
                if (args.Cancelled)
                {
                    MessageBox.Show(@"Bulk operation canceled!", @"Canceled", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    LoggerHelper.Write(LoggerOption.Warning, @"Bulk operation canceled!");
                }
                else if (args.Error != null)
                {
                    MessageBox.Show(args.Error.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    LoggerHelper.Write(LoggerOption.Error, "Error: {0}", args.Error.Message);
                }
                else
                {
                    MessageBox.Show(@"Bulk operation completed", @"Completed", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    LoggerHelper.Write(LoggerOption.Info, @"Bulk operation completed");
                }

                stopWatch.Stop();
                prog.SetProgressState(TaskbarProgressBarState.NoProgress);

                pbrCopyStatus.Value = 0;
            };

            _worker.RunWorkerAsync();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!_worker.IsBusy)
                return;

            if (!_worker.CancellationPending)
                _worker.CancelAsync();
        }

        #endregion
    }
}