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
using System.Net.Sockets;
using Exceptions;

namespace TCP_Client
{
    public partial class LoginScreen : UserControl
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {

                Account acc = new Account(textBoxLogin.Text, textBoxPassword.Text);
                Form1 f = Form1.Instance;

                Connection con = f.connection;

                Messages.sendMessage(con, new string[3] { "LOGIN", acc.Login.ToLower(), acc.Password._Password });

                string receivedMsg = Messages.receiveMessage(con);

                if (receivedMsg.Equals("INV_USER"))
                    MessageBox.Show("User does not exist");
                else if (receivedMsg.Equals("USER IS CURRENTLY LOGGED IN "))
                    MessageBox.Show("User is currently logged in");
                else if (receivedMsg.Equals("INV_PWD"))
                    MessageBox.Show("Invalid password");
                else if (receivedMsg.Equals("LOGGED"))
                    MessageBox.Show("Logged in successfully");


                if (receivedMsg.Equals("LOGGED"))
                {
                    openCloudScreen(f);
                }
            }
            catch(LoginScreenExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            try
            {
                Account acc = new Account(textBoxLogin.Text, textBoxPassword.Text);
                Form1 f = Form1.Instance;

                Connection con = f.connection;

                Messages.sendMessage(con, new string[3] { "REGISTER", acc.Login.ToLower(), acc.Password._Password });

                string receivedMsg = Messages.receiveMessage(con);


                if (receivedMsg.Equals("USER_EXISTS"))
                    MessageBox.Show("Login is already used");
                else if (receivedMsg.Equals("REG_OK"))
                    MessageBox.Show("Signed up successfully");
                else if (receivedMsg.Equals("INV_USER"))
                    MessageBox.Show("Login can't be null");
            }
            catch (LoginScreenExceptions ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void checkBoxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            textBoxPassword.PasswordChar = checkBoxShowPass.Checked ? '\0' : '*';
        }

        private void openCloudScreen(Form1 f)
        {
            Size size = new Size(515, 540);
            f.Size = size;

            Cloud cs = new Cloud();
            cs.Dock = DockStyle.Fill;
            Form1.Instance.panel.Controls.Clear();
            Form1.Instance.panel.Controls.Add(cs);
            Form1.Instance.panel.Controls["Cloud"].BringToFront();
            
        }

        private void textBoxPassword_Enter(object sender, EventArgs e)
        {
            if (textBoxPassword.Text.Equals("Password"))
            {
                textBoxPassword.Text = "";
                textBoxPassword.ForeColor = Color.Black;
            }
        }

        private void textBoxLogin_Enter(object sender, EventArgs e)
        {
            if (textBoxLogin.Text.Equals("Login"))
            {
                textBoxLogin.Text = "";
                textBoxLogin.ForeColor = Color.Black;
            }
        }

        private void textBoxLogin_Leave(object sender, EventArgs e)
        {
            if (textBoxLogin.Text.Equals(""))
            {
                textBoxLogin.Text = "Login";
                textBoxLogin.ForeColor = Color.Silver;
            }
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text.Equals(""))
            {
                textBoxPassword.Text = "Password";
                textBoxPassword.ForeColor = Color.Silver;
            }
        }
    }
}
