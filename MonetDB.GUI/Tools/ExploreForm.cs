using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using MonetDB.DataProvider;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using MonetDB.Driver.Database;
using MonetDB.Driver.Helpers;
using MonetDB.Enums.Logging;
using MonetDB.GUI.UI;
using MonetDB.Helpers;
using MonetDB.Helpers.Diagnostic;
using MonetDB.Helpers.Logging; 
using System.Drawing;
using MonetDB.Extensions;
using MonetDB.TCP.Client;
using MonetDB.TCP.Models;

namespace MonetDB.GUI.Tools
{
    public partial class ExploreForm : Form
    {
        private const string GridResultKey = "dgw";
        private readonly MonetDbConnectionInfo _connectionInfo;
        private MonetDatabase _db;

        private void TreeInit()
        {
            trwSchema.Nodes.Clear();
            //db
            var dbNode = new TreeNode(_connectionInfo.Database);

            //schemas
            var schemas =
                MonetDbHelper.ExecuteDataSet(_connectionInfo.ToString(), "SELECT * FROM schemas WHERE system=0;").Tables[0];

            foreach (DataRow schemaRow in schemas.Rows)
            {
                var schemaNode = new TreeNode(schemaRow["name"].To<string>());

                //tables
                var tables =
                    MonetDbHelper.ExecuteDataSet(_connectionInfo.ToString(),
                        $"SELECT * FROM tables WHERE schema_id={schemaRow["id"]} and system=0;").Tables[0];

                foreach (DataRow tableRow in tables.Rows)
                {
                    var tableNode = new TreeNode(tableRow["name"].To<string>());

                    //columns
                    var colums =
                        MonetDbHelper.ExecuteDataSet(_connectionInfo.ToString(),
                            $"SELECT * FROM columns WHERE table_id={tableRow["id"]};").Tables[0];

                    foreach (DataRow columnRow in colums.Rows)
                    {
                        var txt =
                            $"{columnRow["name"]}({columnRow["type"]},{(columnRow["null"].ToBool() ? "null" : "not null")})";

                        tableNode.Nodes.Add(txt);
                    }

                    schemaNode.Nodes.Add(tableNode);
                }

                dbNode.Nodes.Add(schemaNode);
            }

            trwSchema.Nodes.Add(dbNode);

        }

        public ExploreForm(MonetDbConnectionInfo connectionInfo)
        {
            InitializeComponent();

            _connectionInfo = connectionInfo;
        }

        private void ExploreForm_Load(object sender, EventArgs e)
        {
            TreeInit();
        }

        private void bulkLoaderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var bulkLoaderForm = new BulkLoaderForm(_connectionInfo);

            bulkLoaderForm.Show();
        }

        private void executeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var query = tbxQuery.SelectionLength > 0
                    ? tbxQuery.SelectedText
                    : tbxQuery.Text;

                if (query.IsNull())
                    return;

                var stopWatch = new Stopwatch();

                stopWatch.Start();

                var ds = MonetDbHelper.ExecuteDataSet(_connectionInfo.ToString(), query);

                stopWatch.Stop();

                tbcResult.TabPages.Clear();

                for (var i = 0; i < ds.Tables.Count; i++)
                {
                    var tabPage = new TabPage(ds.Tables[i].TableName);
                    var dgw = new CustomDatagridView($"{GridResultKey}_{i}")
                    {
                        DataSource = ds.Tables[i].DefaultView
                    };

                    tabPage.Controls.Add(dgw);
                    tbcResult.TabPages.Add(tabPage);
                }

                lblElapsed.Text = $"{stopWatch.Elapsed:g}";

                //single smt's
                if (ds.Tables.Count == 1)
                {
                    lblRows.Text = $"{ds.Tables[0].Rows.Count} rows";
                }

                TreeInit();
            }
            catch (Exception exception)
            {
                LoggerHelper.Write(LoggerOption.Error, exception.Message);

                MessageBox.Show(exception.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tbcResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tbcResult.SelectedIndex == -1)
                return;

            var key = $"{GridResultKey}_{tbcResult.SelectedIndex}";
            var dgw = (DataGridView)tbcResult.Controls.Find(key, true)[0];

            lblRows.Text = $"{dgw.RowCount} rows";
        }

        private void trwSchema_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item == null)
                return;

            var node = (TreeNode)e.Item;

            //database
            if (node.Parent == null)
                return;

            var startIndex = node.Text.IndexOf("(", StringComparison.InvariantCultureIgnoreCase);
            var endIndex = node.Text.LastIndexOf(")", StringComparison.InvariantCultureIgnoreCase) - startIndex + 1;
            var txt = String.Empty;

            if (startIndex != -1 && endIndex != -1 && startIndex < endIndex)
            {
                //column
                txt = $"\"{node.Text.Remove(startIndex, endIndex)}\"";
            }
            else
            {
                if (node.Parent.Parent == null)
                {
                    //schema
                    txt = $"\"{node.Text}\"";
                }
                else
                {
                    //table
                    txt = $"\"{node.Parent.Text}\".\"{node.Text}\"";
                }
            }

            trwSchema.DoDragDrop(txt, DragDropEffects.Copy);
        }

        private void tbxQuery_DragDrop(object sender, DragEventArgs e)
        {
            var i = tbxQuery.SelectionStart;
            var s = tbxQuery.Text.Substring(i);

            tbxQuery.Text = tbxQuery.Text.Substring(0, i) + e.Data.GetData(DataFormats.Text).To<string>() + s;
        }

        private void tbxQuery_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.StringFormat)
                   ? DragDropEffects.Copy
                   : DragDropEffects.None;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _db = new MonetDatabase(_connectionInfo.ToString());

            try
            {
                var fileName = AskOpenFile();

                if (string.IsNullOrWhiteSpace(fileName))
                    return;

                var model = MonetConfigurationHelper.GetConfiguration(Path.Combine(IoHelper.CurrentRoot, MonetSettings.MonetConfigurationFile));
                var file = Path.Combine(model.DbInstallerDir, "bin\\mclient.exe");
                var args =
                    $"-h \"{_db.ConnectionInfo.Host}\" -p {_db.ConnectionInfo.GetPort()} -d \"{_db.ConnectionInfo.Database}\" -u \"{_db.ConnectionInfo.UserName}\" -L \"{Path.Combine(model.TempDir)}\"";

                LoggerHelper.Write(LoggerOption.Info, "Database restoring started...");

                DiagnosticHelper.ShowWindowApplicationEx(file, args, null, null, fileName);

                LoggerHelper.Write(LoggerOption.Info, "Database restored");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoggerHelper.Write(LoggerOption.Error, "Error: {0}", exp.Message);
            }
        }

        private string AskSaveFile()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    AskSaveFile();
                }));
            }
            else
            {
                using (var dumpDialog = new SaveFileDialog
                {
                    OverwritePrompt = true,
                    DefaultExt = "monetdb",
                    Filter = @"Dump files|*.monetdb",
                    RestoreDirectory = true,
                    InitialDirectory = IoHelper.CurrentRoot,
                    ValidateNames = true,
                    CheckPathExists = true,
                    SupportMultiDottedExtensions = true,
                    FileName = _db.ConnectionInfo.Database,
                    Title = @"Dumping the MonetDB"
                })
                {
                    if (dumpDialog.ShowDialog() == DialogResult.OK)
                    {
                        return dumpDialog.FileName;
                    }
                }
            }

            return null;
        }

        private string AskOpenFile()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate
                {
                    AskSaveFile();
                }));
            }
            else
            {
                using (var restoreDialog = new OpenFileDialog
                {
                    DefaultExt = "monetdb",
                    Filter = @"Restore files|*.monetdb",
                    RestoreDirectory = true,
                    InitialDirectory = IoHelper.CurrentRoot,
                    ValidateNames = true,
                    CheckPathExists = true,
                    SupportMultiDottedExtensions = true,
                    FileName = _db.ConnectionInfo.Database,
                    Title = @"Restoring the MonetDB"
                })
                {
                    if (restoreDialog.ShowDialog() == DialogResult.OK)
                    {
                        return restoreDialog.FileName;
                    }
                }
            }

            return null;
        }

        private void sqlConsoleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _db = new MonetDatabase(_connectionInfo.ToString());

            try
            {
                var model = MonetConfigurationHelper.GetConfiguration(Path.Combine(IoHelper.CurrentRoot, MonetSettings.MonetConfigurationFile));
                var file = Path.Combine(model.DbInstallerDir, "bin\\mclient.exe");
                var args =
                    $"-h \"{_db.ConnectionInfo.Host}\" -p {_db.ConnectionInfo.GetPort()} -d \"{_db.ConnectionInfo.Database}\" -u \"{_db.ConnectionInfo.UserName}\" -L \"{Path.Combine(model.TempDir)}\"";

                LoggerHelper.Write(LoggerOption.Info, "Starting MonetDB client session...");

                DiagnosticHelper.ShowWindowApplication(file, args);

                LoggerHelper.Write(LoggerOption.Info, "MonetDB client session closed");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoggerHelper.Write(LoggerOption.Error, "Error: {0}", exp.Message);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _db = new MonetDatabase(_connectionInfo.ToString());


            try
            {
                var fileName = AskSaveFile();

                if (string.IsNullOrWhiteSpace(fileName))
                    return;

                var model = MonetConfigurationHelper.GetConfiguration(Path.Combine(IoHelper.CurrentRoot, MonetSettings.MonetConfigurationFile));
                var file = Path.Combine(model.DbInstallerDir, "bin\\msqldump.exe");
                var args =
                    $"-h \"{_db.ConnectionInfo.Host}\" -p {_db.ConnectionInfo.GetPort()} -d \"{_db.ConnectionInfo.Database}\" -u \"{_db.ConnectionInfo.UserName}\"";

                LoggerHelper.Write(LoggerOption.Info, "Database dumping started...");

                DiagnosticHelper.ShowWindowApplicationEx(file, args, fileName, null, null);

                LoggerHelper.Write(LoggerOption.Info, "Database dumped");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, @"Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                LoggerHelper.Write(LoggerOption.Error, "Error: {0}", exp.Message);
            }
        }

    }
}
