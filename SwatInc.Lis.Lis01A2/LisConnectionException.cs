using System;
using System.Runtime.Serialization;

namespace SwatInc.Lis.Lis01A2
{
    public class LisConnectionException : Exception
    {
        public LisConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public LisConnectionException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public LisConnectionException(string message) : base(message)
        {
        }

        public LisConnectionException()
        {
        }
    }
}