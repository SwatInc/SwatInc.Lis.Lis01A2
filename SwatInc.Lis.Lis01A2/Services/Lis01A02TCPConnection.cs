using Microsoft.Extensions.Logging;
using SwatInc.Lis.Lis01A2.Interfaces;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SwatInc.Lis.Lis01A2.Services
{
    public class Lis01A02TCPConnection : ILis01A2Connection
    {
        #region Private Properties
        private Socket _socket;
        private ILogger _logger;
        private bool _isInServerMode = false;
        private TcpClient _client;
        #endregion

        public event EventHandler<LISConnectionReceivedDataEventArgs> OnReceiveString;

        #region Constructors
        public Lis01A02TCPConnection(string aNetWorkAddress, ushort aNetWorkPort)
        {
            NetWorkAddress = aNetWorkAddress;
            NetWorkPort = aNetWorkPort;
            _logger = new LoggerFactory().CreateLogger<Lis01A02TCPConnection>();
        }
        #endregion

        public string NetWorkAddress { get; set; }
        public ushort NetWorkPort { get; set; }
        public void ClearBuffers()
        {
            throw new NotImplementedException();
        }

        public void Connect()
        {
            if (_isInServerMode)
            {
                return;
            }
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (_socket.Connected)
            {
                throw new Lis01A02TCPConnectionException("Could not connect to server because the connection is already open");
            }
            _socket.Connect(NetWorkAddress, NetWorkPort);
            if (_socket.Connected)
            {
                ReceiveLoop();
            }
        }

        private void ReceiveLoop()
        {
            try
            {
                byte[] array = new byte[1024]; //1KByte buffer
                while (_socket != null && _socket.Connected)
                {
                    int available = _socket.Available;
                    if (available > 0)
                    {
                        int count = _socket.Receive(array);
                        string dataReceived = Encoding.UTF8.GetString(array, 0, count);
                        OnReceiveString?.Invoke(this, new LISConnectionReceivedDataEventArgs(dataReceived));
                    }
                    else
                    {
                        Thread.Sleep(5);  //sleep 5 ms
                    }
                }
            }
            catch (SocketException ex) when (ex.ErrorCode == 10057)
            {
                // Log the exception and exit to stop receiving.
                _logger.LogError(ex.Message);
            }
        }

        public async Task StartListeningAsync()
        {
            await Task.Run(() =>
            {
                _isInServerMode = true;
                TcpListener server = new (IPAddress.Parse(NetWorkAddress), NetWorkPort);
                server.Start();
                byte[] bytes = new Byte[1024];
                string data = null;
                while (true)
                {
                    _client = server.AcceptTcpClient();
                    _socket = _client.Client;
                    data = null;
                    NetworkStream stream = _client.GetStream();
                    for (int i = stream.Read(bytes, 0, bytes.Length); i != 0; i = stream.Read(bytes, 0, bytes.Length))
                    {
                        data = Encoding.UTF8.GetString(bytes, 0, i);
                        OnReceiveString?.Invoke(this, new LISConnectionReceivedDataEventArgs(data));
                    }
                    _client.Close();
                }
            });
        }
        public void DisConnect()
        {
            if (_socket.Connected)
            {
                _socket.Close();
            }
            ref Socket socketPointer = ref _socket;
            IDisposable disposable = socketPointer;
            if (disposable == null)
            {
            }
            else
            {   
                disposable.Dispose();
            }
            socketPointer = null;
        }

        public void WriteData(string value)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(value);
            _socket.Send(buffer);
        }
    }
}