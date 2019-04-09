// Jeremy j'ai essayer de creer une connexion puis j'ai tenté le liste et après j'ai vite fait crée une echange 
// de message entre le client et le serveur.
// Sa function sauf le binding avec le liste 
// J'ai mis le devoir sur Git aussi mais pour que tu le voit je me rappelle pas 
// Sur github mon identifiant c'est JohnSchulman et mon de pass c'est j7o7s7ephschulman123 

using System;
using System.Text;
using System.Windows;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.IO.Ports;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WpfApp6.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Xml.Linq;

namespace WpfApplication1
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<IPPORT> info { get; set; } = new ObservableCollection<IPPORT>();
        public IPEndPoint IPAdresse { get; private set; }

        Socket  clientSocket;

        // Receiving byte array  
        byte[] bytes = new byte[1024];
        private object listener;
        private string port;

        public event PropertyChangedEventHandler PropertyChanged;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Console.Write("ip: ");
            var address = IPAddress.Parse(AdresseIP.Text);
            string[] ports = SerialPort.GetPortNames();
            Console.Write("message: ", ports);

            clientSocket.Connect(address, 4510);
            tbStatus.Text = "Connected";

        }

        private static IPAddress GetIPAddress()
        {
            return Dns.GetHostAddresses(Dns.GetHostName())
                .First(x => x.AddressFamily == AddressFamily.InterNetwork);
        }
        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Console.Write("message: ");
            var data = Encoding.UTF8.GetBytes(tbMsg.Text);
            clientSocket.Send(data);
            receive();
        }

    
        // une function qui essaye de binder et de le mettre l'organiser dans une liste
    
        private async Task<List<IPPORT>> GetIPPORT(int IP, int Port)
        {
            TcpListener listener = new TcpListener(Port);
            TcpListener listener2 = new TcpListener(IP);
            listener.Start();
          
            Console.WriteLine(" on port " + this.port);
            Console.WriteLine("Hit <enter> to stop service\n");
            while (true)
            {
                try
                {
                    TcpClient tcpClient = await listener.AcceptTcpClientAsync();
                    Task t = Process(tcpClient);
                    await t;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private Task Process(TcpClient tcpClient)
        {
            throw new NotImplementedException();
        }

        void receive()
        {
            var buffer = new byte[clientSocket.SendBufferSize];
            var readBytes = clientSocket.Receive(buffer);
            var msg = Encoding.UTF8.GetString(buffer, 0, readBytes);
            tbReceivedMsg.Text = msg;
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            clientSocket.Close();
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
