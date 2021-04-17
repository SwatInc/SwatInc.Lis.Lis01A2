using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class HeaderRecord : AbstractLisRecord
	{

		[LisRecordField(3)]
		public string MessageControlID { get; set; }

		[LisRecordField(4)]
		public string AccessPassword { get; set; }

		[LisRecordField(5)]
		public string SenderID { get; set; }

		[LisRecordField(6)]
		public string SenderStreetAddress { get; set; }

		[LisRecordField(8)]
		public string SenderTelephoneNumber { get; set; }

		[LisRecordField(9)]
		public string CharacteristicsOfSender { get; set; }

		[LisRecordField(10)]
		public string ReceiverID { get; set; }

		[LisRecordField(11)]
		public string Comment { get; set; }

		[LisRecordField(12)]
		public HeaderProcessingID ProcessingID { get; set; }

		[LisRecordField(13)]
		public string Version { get; set; } = "LIS2-A2";

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
