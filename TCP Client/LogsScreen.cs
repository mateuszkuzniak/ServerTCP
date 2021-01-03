using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client;

namespace TCP_Client
{
    public partial class LogsScreen : UserControl
    {
        Form1 f = Form1.Instance;

        public LogsScreen()
        {
            InitializeComponent();
            Messages.sendMessage(Form1.Instance.connection, new string[] { "GETLOGS" });
            string logs = Messages.receiveMessage(Form1.Instance.connection);
            textBoxLogs.Text = logs;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Messages.sendMessage(Form1.Instance.connection, new string[] { "GETLOGS" });
            string logs = Messages.receiveMessage(Form1.Instance.connection);
            textBoxLogs.Text = logs;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form1.Instance.panel.Controls["Cloud"].BringToFront();
        }
    }
}
