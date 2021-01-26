using System;
using System.IO;
using System.Windows.Forms;

namespace TCP_Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool turnOff = false;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            while (turnOff == false)
            {
                try
                {
                    Application.Run(new Form1());
                    turnOff = true;
                }
                catch (IOException)
                {
                    MessageBox.Show("Lost connection with server");
                }
                
            }
        }
    }
}
