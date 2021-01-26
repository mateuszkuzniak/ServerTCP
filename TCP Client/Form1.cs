using Client;
using System;
using System.Windows.Forms;

namespace TCP_Client
{
    public partial class Form1 : Form
    {
        static Form1 _obj;
        public Connection connection;

        public static Form1 Instance
        {
            get
            {
                if(_obj == null)
                {
                    _obj = new Form1();
                }
                return _obj;
            }
        }

        public Panel panel
        {
            get { return panelMain; }
            set { panelMain = value; }
        }

       

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _obj = this;
            ConnectScreen cs = new ConnectScreen();
            panelMain.Controls.Add(cs);
            
        }
    }
}
