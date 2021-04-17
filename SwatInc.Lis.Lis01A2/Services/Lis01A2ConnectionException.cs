using System;
using System.Runtime.Serialization;

namespace SwatInc.Lis.Lis01A2.Services
{
    public class Lis01A2ConnectionException : LisConnectionException
    {
        public Lis01A2ConnectionException()
        {
        }

        public Lis01A2ConnectionException(string message) : base(message)
        {
        }

        public Lis01A2ConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public Lis01A2ConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}