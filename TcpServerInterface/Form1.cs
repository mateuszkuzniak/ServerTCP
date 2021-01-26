using ServerLibrary;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpServerInterface
{
    public partial class Form1 : Form
    {
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
                startButton.Text = @"Start";
            }
        }

    }
}