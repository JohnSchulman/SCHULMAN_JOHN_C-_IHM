// Houssem Dellai    
// houssem.dellai@ieee.org    
// +216 95 325 964    
// Studying Software Engineering    
// in the National Engineering School of Sfax (ENIS)   

using System;
using System.Text;
using System.Windows;
using System.Net;
using System.Net.Sockets;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;
using System.Linq;

namespace ServerSocketWpfApp
{
    public partial class MainWindow : Window
    {


        static Socket _serverSocket;
        static Socket clientSocket;
        static Thread _listenerThread;

        private TextBox tbAux = new TextBox();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

        }


        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var address = GetIPAddress();
            var endpoint = new IPEndPoint(address, 4510);
            _serverSocket.Bind(endpoint);
            tbStatus.Text = "Serveur started ip adresse address";
            AdresseIPServeur.Content = address;
            Console.WriteLine("Serveur started ip adresse address");


        }

        private void Listen_Click(object sender, RoutedEventArgs e)
        {
            _serverSocket.Listen(0);
            clientSocket = _serverSocket.Accept();
            // serverSocket.Close();
            tbStatus.Text = "Listenning started";
            receive(clientSocket);

            /* _listenerThread = new Thread(Listen);
             _listenerThread.Start();
             Console.WriteLine("press enter to exit...");
             Console.ReadLine();*/

        }

        // Thread pour le serveur
        /*private static void Listen()
        {
            while (true)
            {
                _serverSocket.Listen(0);
                clientSocket = _serverSocket.Accept();
                
                receive(clientSocket);
                // new ClientManager(clientSocket);
            }
        }*/


        void receive(Socket clientSocket)
        {
            var buffer = new byte[clientSocket.SendBufferSize];
            var readBytes = clientSocket.Receive(buffer);
            var msg = Encoding.UTF8.GetString(buffer, 0, readBytes);
            Console.WriteLine("Listenning started");
            Console.WriteLine(msg);

            tbStatus.Text = "Listenning started";
            tbMsgReceived.Text = msg;
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Console.Write("message: ");
            var data = Encoding.UTF8.GetBytes(tbMsgToSend.Text);
            clientSocket.Send(data);
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            clientSocket.Close();
            _serverSocket.Close();
        }
        private static IPAddress GetIPAddress()
        {
            return Dns.GetHostAddresses(Dns.GetHostName())
                .First(x => x.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
