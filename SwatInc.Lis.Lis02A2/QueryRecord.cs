using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class QueryRecord : AbstractLisRecord
	{

		[LisRecordField(2)]
		public int SequenceNumber { get; set; }

		[LisRecordField(3)]
		public StartingRange StartingRange { get; set; }

		[LisRecordField(13)]
		public string RequestInformationStatusCode { get; private set; } = "N";
        public override string ToLISString()
		{
			return "Q" + new string(LISDelimiters.FieldDelimiter, 1) + base.ToLISString();
		}

		public QueryRecord(string aLisString)
			: base(aLisString)
		{
		}

		public QueryRecord()
		{
		}
	}
}
