using System;
using System.Drawing;
using System.Windows.Forms;
using Client;
using Exceptions;

namespace TCP_Client
{
    public partial class ChangePasswordScreen : UserControl
    {
        public ChangePasswordScreen()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form1 f = Form1.Instance;
            Size size = new Size(515, 540);
            f.Size = size;

            Form1.Instance.panel.Controls["Cloud"].BringToFront();
        }

        private void checkBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            textBoxNewPwd.PasswordChar = checkBoxShowPass.Checked ? '\0' : '*';
            textBoxOldPwd.PasswordChar = checkBoxShowPass.Checked ? '\0' : '*';
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 f = Form1.Instance;

                Connection con = f.connection;
                Password oldPwd = new Password(textBoxOldPwd.Text);
                Password newPwd = new Password(textBoxNewPwd.Text);
                
                Messages.sendMessage(con, new string[3] { "CHANGE_PWD", oldPwd._Password, newPwd._Password});

                string receivedMsg = Messages.receiveMessage(con);

                if (receivedMsg.Equals("CHANGE_PWD_ERROR"))
                    MessageBox.Show("Invalid password");
                else if (receivedMsg.Equals("CHANGE_PWD"))
                {
                    MessageBox.Show("Password changed successfully");
                    buttonBack_Click(sender, e);
                }
                else
                {
                    MessageBox.Show(receivedMsg);
                }
            }
            catch(LoginScreenExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}