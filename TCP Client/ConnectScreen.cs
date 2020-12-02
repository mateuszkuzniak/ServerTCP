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
using System.Threading;
using System.Net.NetworkInformation;

namespace TCP_Client
{
    public partial class ConnectScreen : UserControl
    {
        bool isConnected;
        public ConnectScreen()
        {
            InitializeComponent();
            isConnected = false;
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {

            try
            {
                Form1 f = Form1.Instance;
                if (f.connection != null)
                    f.connection = null;
                f.connection = new Connection(textBoxIp.Text, textBoxPort.Text);
                isConnected = true;
            }
            catch (System.FormatException ex)
            {
                MessageBox.Show(ex.Message);
                isConnected = false;
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Could not connect");
            }

            if (isConnected == true)
            {
                LoginScreen lc = new LoginScreen();
                lc.Dock = DockStyle.Fill;
                Form1.Instance.panel.Controls.Add(lc);
                Form1.Instance.panel.Controls["LoginScreen"].BringToFront();               
            }

        }


        void isStillConnected()
        {
            while (Form1.Instance.connection != null)
            {
                IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] tcpConnections = ipProperties.GetActiveTcpConnections().Where(x => x.LocalEndPoint.Equals(Form1.Instance.connection.Client.Client.LocalEndPoint) && x.RemoteEndPoint.Equals(Form1.Instance.connection.Client.Client.RemoteEndPoint)).ToArray();

                if (tcpConnections != null && tcpConnections.Length > 0)
                {
                    TcpState stateOfConnection = tcpConnections.First().State;
                    if (stateOfConnection == TcpState.Established)
                    {
                        // Connection is OK
                    }
                    else
                    {
                        Form1.Instance.connection = null;
                        //ConnectScreen cs = new ConnectScreen();
                        //cs.Dock = DockStyle.Fill;
                        //Form1.Instance.panel.Controls.Add(cs);
                        //Form1.Instance.panel.Controls["ConnectionScreen"].BringToFront();

                        //MessageBox.Show("Lost connection with server!");
                    }

                }
                else
                {
                    Form1.Instance.connection = null;
                    //ConnectScreen cs = new ConnectScreen();
                    //cs.Dock = DockStyle.Fill;
                    //Form1.Instance.panel.Controls.Add(cs);
                    //Form1.Instance.panel.Controls["ConnectionScreen"].BringToFront();

                    //MessageBox.Show("Lost connection with server!");
                }
            }
            throw new NullReferenceException();
        }


        private void textBoxIp_Enter(object sender, EventArgs e)
        {
            if (textBoxIp.Text.Equals("IP Address"))
            {
                textBoxIp.Text = "";
                textBoxIp.ForeColor = Color.Black;
            }
        }

        private void textBoxIp_Leave(object sender, EventArgs e)
        {
            if (textBoxIp.Text.Equals(""))
            {
                textBoxIp.Text = "IP Address";
                textBoxIp.ForeColor = Color.Silver;
            }
        }

        private void textBoxPort_Enter(object sender, EventArgs e)
        {
            if (textBoxPort.Text.Equals("Port"))
            {
                textBoxPort.Text = "";
                textBoxPort.ForeColor = Color.Black;
            }
        }

        private void textBoxPort_Leave(object sender, EventArgs e)
        {
            if (textBoxPort.Text.Equals(""))
            {
                textBoxPort.Text = "Port";
                textBoxPort.ForeColor = Color.Silver;
            }
        }
    }
}