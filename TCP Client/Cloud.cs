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
    public partial class Cloud : UserControl
    {
        Form1 f = Form1.Instance;

        public Cloud()
        {
            InitializeComponent();

            Messages.sendMessage(Form1.Instance.connection, new string[] { "FILEALL" });
            string text = Messages.receiveMessage(Form1.Instance.connection);
            if (!text.Equals("File list is empty!"))
            {
                string[] t = text.Split(new char[] { ';' });
                t = t.Where(e => e != "").ToArray();
                listBoxFiles.Items.AddRange(t);
            }
            listBoxFiles.Update();
        }

        private void buttonDeleteFile_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem != null)
            {
                Messages.sendMessage(Form1.Instance.connection, new string[] { "FILEDELETE", listBoxFiles.SelectedItem.ToString() });
                listBoxFiles.Items.RemoveAt(listBoxFiles.SelectedIndex);
                MessageBox.Show(Messages.receiveMessage(Form1.Instance.connection));
            }
            else
                MessageBox.Show("Select file");
        }

        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem != null)
            {
                Messages.sendMessage(Form1.Instance.connection,
                    new string[] { "FILEUPDATE", listBoxFiles.SelectedItem.ToString(), textBoxFileText.Text });
                MessageBox.Show(Messages.receiveMessage(Form1.Instance.connection));
            }
            else
                MessageBox.Show("Select file");
        }

        private void buttonAddNewFile_Click(object sender, EventArgs e)
        {
            Messages.sendMessage(Form1.Instance.connection, new string[] { "FILEADD", textBoxNewFileName.Text.ToLower(), textBoxFileText.Text });
            string msg = Messages.receiveMessage(Form1.Instance.connection);
            MessageBox.Show(msg);
            if (!msg.Equals("FILE_EXISTS") && !msg.Equals("INV_FILE_NAME"))
                listBoxFiles.Items.Add(textBoxNewFileName.Text.ToLower());
            textBoxNewFileName.Text = "";
            textBoxFileText.Text = String.Empty;
        }


        private void buttonLogout_Click(object sender, EventArgs e)
        {
            listBoxFiles.Items.Clear();
            Messages.sendMessage(Form1.Instance.connection, new string[] { "EXIT" });
            Size size = new Size(416, 339);
            f.Size = size;

            ConnectScreen c = new ConnectScreen();
            c.Dock = DockStyle.Fill;
            Form1.Instance.panel.Controls.Add(c);
            Form1.Instance.panel.Controls["ConnectScreen"].BringToFront();
        }

        private void textBoxNewFileName_Enter(object sender, EventArgs e)
        {
            label1.Visible = false;
            textBoxFileText.Text = String.Empty;
        }

        private void textBoxNewFileName_Leave(object sender, EventArgs e)
        {
            if (textBoxNewFileName.Text.Equals(""))
                label1.Visible = true;
        }

        private void buttonChangePwd_Click(object sender, EventArgs e)
        {
            Size size = new Size(416, 339);
            f.Size = size;

            ChangePasswordScreen cps = new ChangePasswordScreen();
            cps.Dock = DockStyle.Fill;
            Form1.Instance.panel.Controls.Add(cps);
            Form1.Instance.panel.Controls["ChangePasswordScreen"].BringToFront();
        }

        private void buttonLogs_Click(object sender, EventArgs e)
        {
            Form1.Instance.panel.Controls.Remove(Form1.Instance.panel.Controls["LogsScreen"]);
            LogsScreen ls = new LogsScreen();
            Form1.Instance.panel.Controls.Add(ls);
            Form1.Instance.panel.Controls["LogsScreen"].BringToFront();
        }

        private void buttonUser_Click(object sender, EventArgs e)
        {
            Form1.Instance.panel.Controls.Remove(Form1.Instance.panel.Controls["UserScreen"]);
            UserScreen us = new UserScreen();
            Size size = new Size(235, 189);
            f.Size = size;
            Form1.Instance.panel.Controls.Add(us);
            Form1.Instance.panel.Controls["UserScreen"].BringToFront();
        }

        private void listBoxFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxFiles.SelectedItem != null)
            {
                try
                {
                    Messages.sendMessage(Form1.Instance.connection, new string[] { "FILEOPEN", listBoxFiles.SelectedItem.ToString().ToLower() });
                    string text = Messages.receiveMessage(Form1.Instance.connection);
                    textBoxFileText.Text = text;
                }
                catch (System.NullReferenceException)
                {

                }
            }
        }
    }
}
