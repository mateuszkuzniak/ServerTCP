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
    public partial class EditUserScreen : UserControl
    {
        public EditUserScreen()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            Form1.Instance.panel.Controls["UserScreen"].BringToFront();
        }
    }
}
