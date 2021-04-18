using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class TerminatorRecord : AbstractLisRecord
	{

		[LisRecordField(2)]
		public int SequenceNumber { get; set; } = 1;

		[LisRecordField(3)]
		public TerminationCode TerminationCode { get; set; }

		public override string ToLISString()
		{
			return "L" + new string(LISDelimiters.FieldDelimiter, 1) + base.ToLISString();
		}

		public TerminatorRecord(string aLisString)
			: base(aLisString)
		{
		}

		public TerminatorRecord()
		{
		}
	}
}
