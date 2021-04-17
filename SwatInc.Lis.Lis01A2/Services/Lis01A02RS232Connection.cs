using SwatInc.Lis.Lis01A2.Interfaces;
using System;
using System.IO.Ports;

namespace SwatInc.Lis.Lis01A2.Services
{
    public class Lis01A02RS232Connection : ILis01A2Connection
    {
        #region Private Properties
        private SerialPort _comPort;

        #endregion
        public event EventHandler<LISConnectionReceivedDataEventArgs> OnReceiveString;

        #region Constructors
        public Lis01A02RS232Connection(SerialPort comPort)
        {
            ComPort = comPort;
        }
        #endregion

        #region Public Properties
        public SerialPort ComPort
        {
            get => _comPort; set
            {
                if (_comPort != null && _comPort != value)
                {
                    _comPort.DataReceived -= COMPortDataReceived;
                }
                _comPort = value;
                _comPort.DataReceived += COMPortDataReceived;
                _comPort.ReadTimeout = 15000;
            }
        }

        #region Private Methods
        private void COMPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string receivedData = ComPort.ReadExisting();
            OnReceiveString?.Invoke(this, new LISConnectionReceivedDataEventArgs(receivedData));
        }

        #endregion


        #endregion
        public void ClearBuffers()
        {
            _comPort.DiscardInBuffer();
            _comPort.DiscardOutBuffer();
        }

        public void Connect()
        {
            _comPort.Open();
        }

        public void DisConnect()
        {
            _comPort.Close();
        }

        public void WriteData(string value)
        {
            _comPort.Write(value);
        }
    }
}