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
                if (!Password.isValid(textBoxNewPwd.Text))
                    throw new LoginScreenExceptions("Password is not safe!\nPassword must contain:\n" +
                                                    "\t-at least one uppercase letter\n" +
                                                    "\t-at least one lowercase letter\n" +
                                                    "\t-at least one number\n" +
                                                    "\t-at least one symblol(*, &, #, !, @, %)");
                
                Messages.sendMessage(con, new string[3] { "CHANGE_PWD", oldPwd._Password, newPwd._Password});

                string receivedMsg = Messages.receiveMessage(con);

                if (receivedMsg.Equals("CHANGE_PWD_ERROR"))
                    MessageBox.Show(@"Invalid password");
                else if (receivedMsg.Equals("CHANGE_PWD"))
                {
                    MessageBox.Show(@"Password changed successfully");
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

        private void textBoxOldPwd_Enter(object sender, EventArgs e)
        {
            if (textBoxOldPwd.Text.Equals("Password"))
            {
                textBoxOldPwd.Text = "";
                textBoxOldPwd.ForeColor = Color.Black;
            }
        }

        private void textBoxOldPwd_Leave(object sender, EventArgs e)
        {
            if (textBoxOldPwd.Text.Equals(""))
            {
                textBoxOldPwd.Text = "Password";
                textBoxOldPwd.ForeColor = Color.Silver;
            }
        }

        private void textBoxNewPwd_Enter(object sender, EventArgs e)
        {
            if (textBoxNewPwd.Text.Equals("Password"))
            {
                textBoxNewPwd.Text = "";
                textBoxNewPwd.ForeColor = Color.Black;
            }
        }

        private void textBoxNewPwd_Leave(object sender, EventArgs e)
        {
            if (textBoxNewPwd.Text.Equals(""))
            {
                textBoxNewPwd.Text = "Password";
                textBoxNewPwd.ForeColor = Color.Silver;
            }
        }
    }
}