using System;
using System.Runtime.Serialization;

namespace SwatInc.Lis.Lis02A2
{
	public class LISParserException : Exception
	{
		public LISParserException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public LISParserException(string message)
			: base(message)
		{
		}

		public LISParserException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public LISParserException()
		{
		}
	}
}
