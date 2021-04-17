using System;
using System.Runtime.Serialization;

namespace SwatInc.Lis.Lis01A2
{
    public class Lis01A02TCPConnectionException : LisConnectionException
    {
        public Lis01A02TCPConnectionException()
        {
        }

        public Lis01A02TCPConnectionException(string message) : base(message)
        {
        }

        public Lis01A02TCPConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public Lis01A02TCPConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}