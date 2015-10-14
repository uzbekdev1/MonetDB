using System;
using System.Windows.Forms;

namespace MonetDB.GUI.Tools
{
    public partial class ConnectForm : Form
    {
        private string _serverName;

        public ConnectForm()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbxServer.Text))
            {
                MessageBox.Show(@"Please enter server name", @"Server", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _serverName = tbxServer.Text;
        }


        public static string ChooseServerName
        {
            get
            {
                var dialog = new ConnectForm();

                return dialog.ShowDialog() != DialogResult.OK
                    ? string.Empty
                    : dialog._serverName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }
    }
}
