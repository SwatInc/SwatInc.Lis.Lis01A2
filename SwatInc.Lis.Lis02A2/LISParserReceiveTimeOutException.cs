using System;
using System.Runtime.Serialization;

namespace SwatInc.Lis.Lis02A2
{
	public class LISParserReceiveTimeOutException : LISParserException
	{
		public LISParserReceiveTimeOutException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public LISParserReceiveTimeOutException(string message)
			: base(message)
		{
		}

		public LISParserReceiveTimeOutException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public LISParserReceiveTimeOutException()
		{
		}
	}
}
