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
using System.Text.RegularExpressions;

namespace TCP_Client
{
    public partial class UserScreen : UserControl
    {
        Form1 f = Form1.Instance;

        public UserScreen()
        {
            InitializeComponent();
            updateUserProfile();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Size size = new Size(515, 540);
            f.Size = size;
            f.panel.Controls["Cloud"].BringToFront();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (ValidUserData())
            {
                string data = $"{textBoxEmail.Text} {textBoxFirstName.Text} {textBoxLastName.Text} {textBoxPhone.Text}";
                Messages.sendMessage(Form1.Instance.connection, new string[] { "UPDATEUSERDATA", data });
                Messages.receiveMessage(Form1.Instance.connection);
                MessageBox.Show("User profile saved");
            }
            else
            {
                MessageBox.Show("Failed to save profile");
            }
        }

        bool ValidUserData()
        {
            if (textBoxEmail.TextLength > 0 && (!Regex.IsMatch(textBoxEmail.Text, @"[@]") || !Regex.IsMatch(textBoxEmail.Text, @"[.]")))
                MessageBox.Show("Invalid Email");
            else if (textBoxFirstName.TextLength > 0 && !Regex.IsMatch(textBoxFirstName.Text, @"^[a-zA-Z]+$"))
                MessageBox.Show("Invalid first name");
            else if (textBoxLastName.TextLength > 0 && !Regex.IsMatch(textBoxLastName.Text, @"^[a-zA-Z]+(-[a-zA-Z]+)?$"))
                MessageBox.Show("Invalid last name");
            else if (textBoxPhone.TextLength > 0 && (!Regex.IsMatch(textBoxPhone.Text, @"^[0-9]+$") || textBoxPhone.Text.Length < 9 || textBoxPhone.Text.Length > 9))
                MessageBox.Show("Invalid phone number");
            else
            {
                return true;
            }
            return false;
        }

        void updateUserProfile()
        {
            Messages.sendMessage(f.connection, new string[] { "GETUSER" });
            string text = Messages.receiveMessage(f.connection);
            string[] data = text.Split(';');
            if (data[1] != "")
                textBoxFirstName.Text = data[1];
            if (data[2] != "")
                textBoxLastName.Text = data[2];
            if (data[0] != "")
                textBoxEmail.Text = data[0];
            if (data[3] != "")
                textBoxPhone.Text = data[3];
        }

    }
}
