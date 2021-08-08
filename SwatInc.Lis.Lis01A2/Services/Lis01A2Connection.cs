using slf4net;
using SwatInc.Lis.Lis01A2.Interfaces;
using System;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Timers;

namespace SwatInc.Lis.Lis01A2.Services
{
    public class Lis01A2Connection : ILisConnection
    {
        #region Private Fields
        private const char STX = '\u0002';
        private const char ETX = '\u0003';
        private const char ETB = '\u0017';
        private const char ENQ = '\u0005';
        private const char ACK = '\u0006';
        private const char NAK = '\u0015';
        private const char EOT = '\u0004';
        private const char NUL = '\0';
        private const char CR = '\r';
        private const char LF = '\n';

        private const int _maxFrameSize = 63993;
        private readonly ILogger _logger;

        private ILis01A2Connection _connection;
        private readonly StringBuilder _tempReceiveBuffer = new StringBuilder();
        private string _tempIntermediateFrameBuffer;

        private readonly EventWaitHandle _enqWaitObject = new EventWaitHandle(true, EventResetMode.ManualReset);
        private readonly EventWaitHandle _ackWaitObject = new EventWaitHandle(true, EventResetMode.ManualReset);
        private readonly EventWaitHandle _eotWaitObject = new EventWaitHandle(true, EventResetMode.ManualReset);
        private bool _ackReceived;
        private readonly System.Timers.Timer _receiveTimeOutTimer = new System.Timers.Timer();
        private int _frameNumber;
        private bool _lastFrameWasIntermediate;
        protected internal bool _isDisposed;

        #endregion

        #region Events
        public event EventHandler<LISConnectionReceivedDataEventArgs> OnReceiveString;
        public event EventHandler OnLISConnectionClosed;
        public event EventHandler OnReceiveTimeOut;
        #endregion

        #region Constructors
        public Lis01A2Connection(ILis01A2Connection connection) : this(connection, 30)
        {
        }

        public Lis01A2Connection(ILis01A2Connection connection, int timeOut)
        {
            Connection = connection;
            _receiveTimeOutTimer.Interval = timeOut * 1000;
            _receiveTimeOutTimer.Enabled = false;
            _receiveTimeOutTimer.Elapsed += ReceiveTimeOutTimer_Elapsed;
            _logger = LoggerFactory.GetLogger(typeof(Lis01A2Connection));
        }
        #endregion

        #region Public Properties
        public LisConnectionStatus Status { get; set; }
        public ILis01A2Connection Connection
        {
            get => _connection; set
            {
                if (_connection != null && _connection != value)
                {
                    _connection.OnReceiveString -= ConnectionDataReceived;
                }
                _connection = value;
                _connection.OnReceiveString += ConnectionDataReceived;
            }
        }

        #endregion

        #region Private Methods
        private void ConnectionDataReceived(object sender, LISConnectionReceivedDataEventArgs e)
        {

            string buffer = e.ReceivedData;
            if (buffer != null)
            {
                CharEnumerator enumerator = buffer.GetEnumerator();
                if (enumerator != null)
                {
                    try
                    {
                        while (enumerator.MoveNext())
                        {
                            char currentCharacter = enumerator.Current;
                            switch (Status)
                            {
                                case LisConnectionStatus.Idle:
                                    {
                                        if (currentCharacter != ENQ)
                                        {
                                            _logger.Debug("send <NAK>");
                                            Connection.WriteData($"{NAK}");
                                        }
                                        else
                                        {
                                            _logger.Debug("received <ENQ>");
                                            Connection.WriteData($"{ACK}");
                                            _logger.Debug("send <ACK>");
                                            Status = LisConnectionStatus.Receiving;
                                            _eotWaitObject.Reset();
                                            _tempReceiveBuffer.Clear();
                                            _tempIntermediateFrameBuffer = string.Empty;
                                            _receiveTimeOutTimer.Enabled = true;
                                        }
                                        continue;
                                    }
                                case LisConnectionStatus.Sending:
                                    {
                                        _ackReceived = currentCharacter == ACK;
                                        _ackWaitObject.Set();
                                        continue;
                                    }
                                case LisConnectionStatus.Receiving:
                                    {
                                        _receiveTimeOutTimer.Stop();
                                        _receiveTimeOutTimer.Start();
                                        if (currentCharacter == ENQ)
                                        {
                                            _logger.Debug("received <ENQ>");
                                            Connection.WriteData($"{NAK}");
                                            _logger.Debug("send <NAK>");
                                            return;
                                        }
                                        else if (currentCharacter != EOT)
                                        {
                                            if (currentCharacter != NUL)
                                            {
                                                _tempReceiveBuffer.Append(currentCharacter);
                                            }
                                            if (currentCharacter == LF)
                                            {
                                                string tempReceiveBuffer = _tempReceiveBuffer.ToString();
                                                if (!CheckChecksum(tempReceiveBuffer))
                                                {
                                                    _logger.Error($"CHECKSUM INVALID. FRAME: {_tempIntermediateFrameBuffer}");
                                                    _logger.Debug("send <NAK>");
                                                    Connection.WriteData($"{NAK}");
                                                    _tempReceiveBuffer.Clear();
                                                }
                                                else
                                                {
                                                    _logger.Debug("send <ACK>");
                                                    Connection.WriteData($"{ACK}");
                                                    string cleanReceiveBuffer = tempReceiveBuffer.Substring(2, tempReceiveBuffer.Length - 7);
                                                    if (_lastFrameWasIntermediate)
                                                    {
                                                        _tempIntermediateFrameBuffer = $"{_tempIntermediateFrameBuffer}{cleanReceiveBuffer}";
                                                    }
                                                    else if (OnReceiveString != null)
                                                    {
                                                        string frame = string.Concat(_tempIntermediateFrameBuffer, cleanReceiveBuffer);
                                                        OnReceiveString?.Invoke(this, new LISConnectionReceivedDataEventArgs(frame));
                                                        _logger.Info(string.Concat("received: ", frame));
                                                    }
                                                    _tempReceiveBuffer.Clear();
                                                    _tempIntermediateFrameBuffer = string.Empty;
                                                }
                                            }
                                            continue;
                                        }
                                        else
                                        {
                                            _receiveTimeOutTimer.Stop();
                                            _logger.Debug("received <EOT>");
                                            Status = LisConnectionStatus.Idle;
                                            _eotWaitObject.Set();
                                            return;
                                        }
                                    }
                                case LisConnectionStatus.Establishing:
                                    {
                                        if (currentCharacter == ACK)
                                        {
                                            _logger.Debug("received <ACK>");
                                            Status = LisConnectionStatus.Sending;
                                            _enqWaitObject.Set();
                                            return;
                                        }
                                        else if (currentCharacter != ENQ)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            _logger.Debug("received <ENQ>");
                                            Thread.Sleep(1000);
                                            _logger.Debug("send <ENQ>");
                                            Connection.WriteData($"{ENQ}");
                                            return;
                                        }
                                    }
                                default:
                                    {
                                        if (Status == LisConnectionStatus.Idle)
                                        {
                                            goto case LisConnectionStatus.Idle;
                                        }
                                        if (Status == LisConnectionStatus.Establishing)
                                        {
                                            goto case LisConnectionStatus.Establishing;
                                        }
                                        if (Status == LisConnectionStatus.Sending)
                                        {
                                            goto case LisConnectionStatus.Sending;
                                        }
                                        if (Status == LisConnectionStatus.Receiving)
                                        {
                                            goto case LisConnectionStatus.Receiving;
                                        }
                                        continue;
                                    }
                            }
                        }
                    }
                    finally
                    {
                        enumerator.Dispose();
                    }
                }
            }
        }

        private void ReceiveTimeOutTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Status = LisConnectionStatus.Idle;
            _receiveTimeOutTimer.Stop();
            OnReceiveTimeOut?.Invoke(this, new EventArgs());
        }

        private string CalculateChecksum(string frame)
        {
            int total = 0;
            if (frame != null)
            {
                CharEnumerator enumerator = frame.GetEnumerator();
                if (enumerator != null)
                {
                    try
                    {
                        while (enumerator.MoveNext())
                        {
                            total += enumerator.Current;
                        }
                    }
                    finally
                    {
                        enumerator.Dispose();
                    }
                }
            }
            return ((byte)(total % 256)).ToString("X2", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// determine whether to calculate checksum and verifies checksum if required.
        /// </summary>
        /// <param name="frame">The ASTM frame to determine to and verify checksum</param>
        /// <returns>true when checksum is required and verified. False otherwise</returns>
        private bool CheckChecksum(string frame)
        {
            bool result = false;
            int lineLength = frame.Length;
            if (lineLength < 5)
            {
                return result;
            }
            if (frame[0] != STX)
            {
                return result;
            }
            if (frame[lineLength - 1] != LF)
            {
                return result;
            }
            if (frame[lineLength - 2] != CR)
            {
                return result;
            }


            char etxOrEtb = frame[lineLength - 5];
            if ((etxOrEtb == ETX ? false : etxOrEtb != ETB))
            {
                return result;
            }
            _lastFrameWasIntermediate = etxOrEtb == ETB;

            if (frame[lineLength - 6] != CR)
            {
                return result;
            }

            var calculatedCheckSum = CalculateChecksum(frame.Substring(1, lineLength - 5));
            var receivedCheckSum = frame.Substring(lineLength - 4, 2);
            if (receivedCheckSum != calculatedCheckSum)
            {
                _logger.Error($"CHECKSUM MISMATCH: Received:{receivedCheckSum} | Calculated: {calculatedCheckSum}");
                return result;
            }
            result = true;
            return result;
        }

        private void SendEndFrame(int frameNumber, string frame)
        {
            _logger.Info($"{frameNumber}{frame}");
            _logger.Debug("send <ETX>");
            SendString($"{frameNumber}{frame}{ETX}");
        }

        private void SendIntermediateFrame(int frameNumber, string frame)
        {
            _logger.Info($"{frameNumber}{frame}");
            _logger.Debug("send <ETB>");
            SendString($"{frameNumber}{frame}{ETB}");
        }

        private void SendString(string frame)
        {
            if (Status != LisConnectionStatus.Sending)
            {
                _logger.Error("Connection not in Send mode when trying to send data.");
                throw new Lis01A2ConnectionException("Connection not in Send mode when trying to send data.");
            }
            Connection.ClearBuffers();
            int tryCounter = 0;
            string tempSendString = $"{STX}{frame}{CalculateChecksum(frame)}{CR}{LF}";
            Connection.WriteData(tempSendString);
            while (!WaitForACK())
            {
                tryCounter++;
                if (tryCounter > 5)
                {
                    StopSendMode();
                    _logger.Error("Max number of send retries reached.");
                    throw new Lis01A2ConnectionException("Max number of send retries reached.");
                }
                Connection.WriteData(tempSendString);
            }
        }
        private bool WaitForACK()
        {
            _ackWaitObject.Reset();
            if (!_ackWaitObject.WaitOne(15000))
            {
                StopSendMode();
                _logger.Error("No response from LIS within timeout period.");
                throw new Lis01A2ConnectionException("No response from LIS within timeout period.");
            }
            _logger.Debug("Received <ACK>");
            return _ackReceived;
        }
        #endregion

        public void Connect()
        {
            try
            {
                _logger.Info("\nConnecting To LIS...");
                Connection.Connect();
                _logger.Info("Connected");
            }
            catch (Exception ex)
            {
                _logger.Error($"Error Opening Connection\n{ex.Message}");
                throw new Lis01A2ConnectionException("Error opening Connection.", ex);
            }
        }

        public void DisConnect()
        {
            if (Status != LisConnectionStatus.Idle && !_eotWaitObject.WaitOne(15000))
            {
                Status = LisConnectionStatus.Idle;
                throw new Lis01A2ConnectionException("Error closing Connection, no EOT received.");
            }
            try
            {
                _logger.Info("Disconnecting...");
                Connection.DisConnect();
                Status = LisConnectionStatus.Idle;
                _logger.Info("Disconnected");
                OnLISConnectionClosed?.Invoke(this, new EventArgs());
            }
            catch (Exception ex)
            {
                _logger.Error($"Error Closing Connection\n{ex.Message}");
                throw new Lis01A2ConnectionException("Error closing Connection.", ex);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                IDisposable connection = Connection as IDisposable;
                if (connection != null)
                {
                    connection.Dispose();
                }
                else
                {
                }
                _isDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool EstablishSendMode()
        {
            bool result = true;
            _frameNumber = 1;
            if (Status != LisConnectionStatus.Idle)
            {
                _logger.Error("Connection not idle when trying to establish send mode");
                throw new Lis01A2ConnectionException("Connection not idle when trying to establish send mode");
            }
            Status = LisConnectionStatus.Establishing;
            _logger.Info("Establishing send mode");
            Connection.ClearBuffers();
            _enqWaitObject.Reset();
            try
            {
                Connection.WriteData($"{ENQ}");
            }
            catch (Exception ex)
            {
                _logger.Error("Cannot establish send mode.");
                _logger.Error($"{ex.Message}\n{ex.StackTrace}");

            }
            _logger.Debug("send <ENQ>");
            _enqWaitObject.WaitOne(15000, false);
            if (Status != LisConnectionStatus.Sending)
            {
                return result;
                try
                {
                    StopSendMode();
                    _logger.Info("<ENQ> timed out. Set connection to idle.");
                }
                catch (Exception)
                {
                    if (Status != LisConnectionStatus.Sending) { Status = LisConnectionStatus.Idle; }
                }
            }
            if (Status == LisConnectionStatus.Sending) { return result; }
            return false;
        }

        public void SendMessage(string aMessage)
        {
            while (aMessage.Length > _maxFrameSize)
            {
                string intermediateFrame = aMessage.Substring(0, _maxFrameSize);
                SendIntermediateFrame(_frameNumber, intermediateFrame);
                aMessage = aMessage.Remove(0, _maxFrameSize);
                _frameNumber++;
                if (_frameNumber <= 7)
                {
                    continue;
                }
                _frameNumber = 0;
            }
            SendEndFrame(_frameNumber, aMessage);
            _frameNumber++;
            if (_frameNumber > 7)
            {
                _frameNumber = 0;
            }
        }

        public void StartReceiveTimeoutTimer()
        {
            _receiveTimeOutTimer.Start();
        }

        public void StopSendMode()
        {
            Connection.WriteData($"{EOT}");
            _logger.Debug("send <EOT>");
            Status = LisConnectionStatus.Idle;
        }
    }
}