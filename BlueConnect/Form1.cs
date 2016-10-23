using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Ports;
using InTheHand.Net.Sockets;
using System.Threading;
using System.IO;


namespace BlueConnect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(serverStarted)
            {
                updateUI("Server already Started");
                return;
            }
            if(rbClient.Checked)
            {
                connectAsClient();
            }
            else
            {
                connectAsServer();
            }
        }
        public void connectAsClient()
        {

        }
        public void connectAsServer()
        {
            Thread BluetoothServerThread = new Thread(new ThreadStart(ServerConnectThread));
            BluetoothServerThread.Start();
        }

        Guid mUUID = new Guid("00001101-0000-1000-8000-00805F9B34F8");
        bool serverStarted = false;
        public void ServerConnectThread()
        {
            serverStarted = true;
            updateUI("Server Started !! Waiting for client .....");
            BluetoothListener blueListen = new BluetoothListener(mUUID);
            blueListen.Start();
            BluetoothClient conn = blueListen.AcceptBluetoothClient();
            String name = conn.RemoteMachineName;

            updateUI("Client has Connected");

            Stream mStream = conn.GetStream();
            while (true)
            {
                try
                {
                    //handle Server Connection
                    byte[] received = new byte[1024];
                    mStream.Read(received, 0, received.Length);
                    updateUI("Received : " + Encoding.ASCII.GetString(received));
                    byte[] sent = Encoding.ASCII.GetBytes("Hello World");
                    mStream.Write(sent, 0, sent.Length);
                }
                catch(Exception e)
                {
                    updateUI("Client has disconnected !!");
                }
            }

        }
        private void updateUI(String message)
        {
            Func<int> del = delegate ()
            {
                tbOut.AppendText(message + System.Environment.NewLine);
                return 0;
            };
            Invoke(del);
        }

        private void tbOut_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
