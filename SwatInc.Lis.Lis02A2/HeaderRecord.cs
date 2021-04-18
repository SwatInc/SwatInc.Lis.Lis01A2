using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class HeaderRecord : AbstractLisRecord
	{

		[LisRecordField(5)]
		public string SenderID { get; set; }

		[LisRecordField(14)]
		public DateTime MessageDateTime { get; set; } = DateTime.Now;

		public override string ToLISString()
		{
			return "H" + new string(LISDelimiters.FieldDelimiter, 1) + new string(LISDelimiters.RepeatDelimiter, 1) + new string(LISDelimiters.ComponentDelimiter, 1) + new string(LISDelimiters.EscapeCharacter, 1) + new string(LISDelimiters.FieldDelimiter, 1) + base.ToLISString();
		}

		public HeaderRecord(string aLisString): base(aLisString)
		{
			LISDelimiters.FieldDelimiter = aLisString[1];
			LISDelimiters.RepeatDelimiter = aLisString[2];
			LISDelimiters.ComponentDelimiter = aLisString[3];
			LISDelimiters.EscapeCharacter = aLisString[4];
		}

		public HeaderRecord()
		{
		}
	}
}
