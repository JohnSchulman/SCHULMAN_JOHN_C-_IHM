using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerSocketWpfApp
{
    class ClientManager
    {

        private Socket _socket;
        private Thread _listenerThread;
        private bool _initReceived;

        public ClientManager(Socket socket)
        {
            _socket = socket;
            _listenerThread = new Thread(Run);
            _listenerThread.Start();
        }


        private void Run()
        {
            var buffer = new byte[_socket.SendBufferSize];
            while (true)
            {
                var readBytes = _socket.Receive(buffer);
                
            }
        }
    }
}
