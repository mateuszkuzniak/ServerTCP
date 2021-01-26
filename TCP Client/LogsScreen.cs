using System;
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
            string logs = Messages.receiveLongMessage(Form1.Instance.connection);
            textBoxLogs.Text = logs;
            textBoxLogs.Select(textBoxLogs.TextLength + 1, 0);
            textBoxLogs.ScrollToCaret();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form1.Instance.panel.Controls["Cloud"].BringToFront();
        }
    }
}
