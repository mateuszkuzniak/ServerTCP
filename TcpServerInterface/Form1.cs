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
           
            if (startButton.Text == @"Start")
            {
                Logs.Text = null;
                server = new ServerTAP<LoginServerProtocol>(IPAddress.Parse(IpBox.Text), int.Parse(PortBox.Text), Logs, this.usersList);
                server.Start();
                Debug.WriteLine(IpBox);
                Debug.WriteLine(PortBox);
                label4.Text = $@"{IpBox.Text} : {PortBox.Text}";
                label2.Text = @"On";
                startButton.Text = @"Stop";

                Task.Run(() => server.GetAllLoggedUsers());
            }
            else if (startButton.Text == @"Stop")
            {
                server.Stop();
                label2.Text = @"Closed";
                label4.Text = @"";
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
    }
}