using System;
using System.Windows.Forms;
using MonetDB.DataProvider;
using MonetDB.Extensions;

namespace MonetDB.GUI.Tools
{
    public partial class DatabaseForm : Form
    {
        private readonly string _host;
        private readonly MonetDbConnectionInfo _connectionInfo;

        public DatabaseForm()
        {
            InitializeComponent();
        }
        public DatabaseForm(string host)
            : this()
        {
            _host = host;
        }

        public DatabaseForm(MonetDbConnectionInfo connectionInfo)
            : this()
        {
            _connectionInfo = connectionInfo;
        }

        private void DatabaseForm_Load(object sender, EventArgs e)
        {
            Text = $"Create databe in '{_host}' host";

            if (_connectionInfo != null)
            { 
                tbxDb.Text = _connectionInfo.Database; 
            }
        }

        public static MonetDbConnectionInfo GetDatabaseInfo(string host)
        {
            var form = new DatabaseForm(host);

            return form.ShowDialog() != DialogResult.OK
                ? null
                : new MonetDbConnectionInfo
                {
                    Host = host,
                    Database = form.tbxDb.Text
                };
        }

        public static MonetDbConnectionInfo GetDatabaseInfo(MonetDbConnectionInfo connectionInfo)
        {
            var form = new DatabaseForm(connectionInfo);

            return form.ShowDialog() != DialogResult.OK
                ? null
                : new MonetDbConnectionInfo
                {
                    Host = connectionInfo.Host,
                    Database = form.tbxDb.Text
                };
        }
    }
}
