using System;
using System.Windows.Forms;
using Client;
using System.Text.RegularExpressions;

namespace TCP_Client
{
    public partial class EditUserScreen : UserControl
    {
        Form1 f = Form1.Instance;

        public EditUserScreen()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form1.Instance.panel.Controls.Remove(Form1.Instance.panel.Controls["UserScreen"]);
            UserScreen us = new UserScreen();
            Form1.Instance.panel.Controls.Add(us);
            Form1.Instance.panel.Controls["UserScreen"].BringToFront();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (ValidUserData())
            {
                string data = $"{textBoxEmail.Text} {textBoxFirstName.Text} {textBoxLastName.Text} {textBoxPhone.Text}";
                Messages.sendMessage(Form1.Instance.connection, new string[] { "UPDATEUSERDATA", data });
                Messages.receiveMessage(Form1.Instance.connection);
                MessageBox.Show("User profile saved");
                buttonBack_Click(sender, e);
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
    }
}
