using ServerLibrary;
using DatabaseLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpServerInterface
{
    public partial class Form1 : Form
    {
        String IpAdress;
        String Port;
        Server<LoginServerProtocol> server;
        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            server = new ServerTAP<LoginServerProtocol>(IPAddress.Parse(IpBox.Text), int.Parse(PortBox.Text));
            if (startButton.Text == @"Start")
            {         
                server.Start();
                Debug.WriteLine(IpBox);
                Debug.WriteLine(PortBox);
                label4.Text = $@"{IpAdress} : {int.Parse(Port)}";
                label2.Text = @"On";
                startButton.Text = @"Stop";
                usersButton.Enabled = true;
            }
            else if (startButton.Text == @"Stop")
            {
                //server.Stop();
                label2.Text = @"Closed";
                label4.Text = @"";
                startButton.Text = @"Start";
                usersButton.Enabled = false;
            }
        }

        private void IpBox_TextChanged(object sender, EventArgs e)
        {
            IpAdress = IpBox.Text.ToString();
        }

        private void PortBox_TextChanged(object sender, EventArgs e)
        {
            Port = PortBox.Text.ToString();
        }

        private void usersButton_Click(object sender, EventArgs e)
        {
            String users = server.GetAllLoggedUsers();
            usersList.Clear();
            usersList.AppendText("Active Users:" + Environment.NewLine);
            foreach (var u in users.Split(';'))
            {
                usersList.AppendText(u + Environment.NewLine);
            }
        }
    }
}