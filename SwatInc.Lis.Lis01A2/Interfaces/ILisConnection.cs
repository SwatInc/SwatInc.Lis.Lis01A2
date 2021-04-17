using System;

namespace SwatInc.Lis.Lis01A2.Interfaces
{
    public interface ILisConnection : IDisposable
    {
        LisConnectionStatus Status { get; set; }

        void Connect();

        void DisConnect();

        bool EstablishSendMode();

        void SendMessage(string aMessage);

        void StartReceiveTimeoutTimer();

        void StopSendMode();

        event EventHandler OnLISConnectionClosed;

        event EventHandler<LISConnectionReceivedDataEventArgs> OnReceiveString;

        event EventHandler OnReceiveTimeOut;
    }
}