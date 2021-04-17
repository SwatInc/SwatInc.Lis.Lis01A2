using System;
using System.Runtime.Serialization;

namespace SwatInc.Lis.Lis02A2
{
	public class LISParserEstablishmentFailedException : LISParserException
	{
		public LISParserEstablishmentFailedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public LISParserEstablishmentFailedException(string message)
			: base(message)
		{
		}

		public LISParserEstablishmentFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public LISParserEstablishmentFailedException()
		{
		}
	}
}
