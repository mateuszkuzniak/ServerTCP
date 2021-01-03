using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCP_Client
{
    public partial class UserScreen : UserControl
    {
        public UserScreen()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form1.Instance.panel.Controls["Cloud"].BringToFront();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditUserScreen eus = new EditUserScreen();
            Form1.Instance.panel.Controls.Add(eus);
            Form1.Instance.panel.Controls["EditUserScreen"].BringToFront();
        }
    }
}
