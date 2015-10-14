using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MonetDB.DataProvider; 
using MonetDB.Driver.Database;
using MonetDB.Enums.Logging;
using MonetDB.Extensions;
using MonetDB.GUI.Tools;
using MonetDB.Helpers;
using MonetDB.Helpers.Diagnostic;
using MonetDB.Helpers.Logging;
using MonetDB.Helpers.Win32;
using MonetDB.Models;
using MonetDB.TCP.Client;
using MonetDB.TCP.Enums;
using MonetDB.TCP.Models;

namespace MonetDB.GUI
{
    internal partial class MainForm : Form
    {

        #region Fields

        private string _serverName;
        private MonetDatabase _db;
        private Timer _timer;
        private BackgroundWorker _worker;

        #endregion

        #region Ctors

        private void Init()
        {
            _worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true,
                WorkerReportsProgress = true
            };
            _worker.DoWork += delegate (object sender, DoWorkEventArgs args)
            {
                _db = new MonetDatabase(ConnectionInfo.ToString());

                args.Result = _db.GetDatabases();
            };
            _worker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
            {
                if (dgwDbList.Columns.Count == 0)
                    return;

                var databases = (List<MonetDatabaseInfo>)args.Result;

                dgwDbList.Rows.Clear();

                foreach (var database in databases)
                {
                    dgwDbList.Rows.Add(new object[]
                        {
                            database.Database, 
                            IoHelper.ToFileSize(database.Size),
                            database.Status
                        });
                }

                dgwDbList.ClearSelection();

            };
            _timer = new Timer
            {
                Enabled = true,
                Interval = 10000
            };
            _timer.Tick += delegate (object sender, EventArgs args)
            {
                if (_worker.IsBusy)
                    return;

                _worker.RunWorkerAsync();
            };
        }

        public MainForm()
        {
            InitializeComponent();

            KernelHelper.AttachConsole();
        }

        #endregion

        #region Members

        private MonetDbConnectionInfo ConnectionInfo
        {
            get
            {
                var info = new MonetDatabaseInfo();

                if (dgwDbList.SelectedRows.Count == 0)
                    info.Host = _serverName;
                else
                {
                    var rowIndex = dgwDbList.SelectedRows[0].Index;

                    info.Database = dgwDbList[0, rowIndex].Value.To<string>(); 
                }

                return info;
            }
        }

        private string GetConnectionString()
        { 
            return ConnectionInfo.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            _serverName = ConnectForm.ChooseServerName;

            Text = $"Connected to '{_serverName}' server";

            Init();

            _worker.RunWorkerAsync(_serverName);
            _timer.Start();

        }

        private void dgwDbList_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (var b = new SolidBrush(dgwDbList.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(CultureInfo.InvariantCulture), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (_worker.IsBusy)
                return;

            _worker.RunWorkerAsync(_serverName);
        }

        private void btnDropDb_Click(object sender, EventArgs e)
        {
            try
            {
                var connectionString = GetConnectionString();

                if (string.IsNullOrWhiteSpace(connectionString))
                    return;

                _db = new MonetDatabase(connectionString);

                if (_db.StatusDatabase() == DatabaseStatus.None)
                {
                    MessageBox.Show(@"Database is not exists", @"Database", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return;
                }

                if (
                    MessageBox.Show(@"Do you really want to drop this database?(y/n)", @"Drop", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                _db.DropDatabase();

                LoggerHelper.Write(LoggerOption.Warning, "Database {0} droped", _db.ConnectionInfo.Database);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoggerHelper.Write(LoggerOption.Error, "Error: {0}", exp.Message);
            }
        }

        private void btnKillDb_Click(object sender, EventArgs e)
        {
            try
            {
                var connectionString = GetConnectionString();

                if (string.IsNullOrWhiteSpace(connectionString))
                    return;

                _db = new MonetDatabase(connectionString);

                if (_db.StatusDatabase() == DatabaseStatus.None)
                {
                    MessageBox.Show(@"Database is not exists", @"Database", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return;
                }

                _db.KillDatabase();

                LoggerHelper.Write(LoggerOption.Warning, "Database {0} killed", _db.ConnectionInfo.Database);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoggerHelper.Write(LoggerOption.Error, "Error: {0}", exp.Message);
            }
        }

        private void btnLoadDb_Click(object sender, EventArgs e)
        {

            try
            {

                var connectionInfo = GetConnectionString();

                if (string.IsNullOrWhiteSpace(connectionInfo))
                    return;

                _db = new MonetDatabase(connectionInfo);

                if (_db.StatusDatabase() == DatabaseStatus.Started)
                {
                    MessageBox.Show(@"Database already loaded", @"Database", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return;
                }

                _db.LoadDatabase();

                LoggerHelper.Write(LoggerOption.Info, "Database {0} loaded", _db.ConnectionInfo.Database);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoggerHelper.Write(LoggerOption.Error, "Error: {0}", exp.Message);
            }
        }

        private void btnCreateDb_Click(object sender, EventArgs e)
        {
            try
            {

                var monetConnection = DatabaseForm.GetDatabaseInfo(_serverName);

                if (monetConnection == null)
                    return;

                _db = new MonetDatabase(monetConnection.ToString());

                if (_db.StatusDatabase() == DatabaseStatus.Started)
                {
                    MessageBox.Show(@"Database already created", @"Database", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return;
                }

                _db.CreateDatabase();

                LoggerHelper.Write(LoggerOption.Info, "Database {0} created", _db.ConnectionInfo.Database);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoggerHelper.Write(LoggerOption.Error, "Error: {0}", exp.Message);
            }
        }

        private void btnExplore_Click(object sender, EventArgs e)
        {
            var connectionInfo = ConnectionInfo;

            if (connectionInfo == null)
                return;

            var exploreForm = new ExploreForm(connectionInfo);

            exploreForm.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _worker.CancelAsync();
            _timer.Stop();

            Environment.Exit(Environment.ExitCode);
        }

        private void dgwDbList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var connectionInfo = ConnectionInfo;

            if (connectionInfo == null)
                return;

            var exploreForm = new ExploreForm(connectionInfo);

            exploreForm.Show();
        }

        #endregion


    }
}
