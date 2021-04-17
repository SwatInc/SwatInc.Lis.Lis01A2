using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class TerminatorRecord : AbstractLisRecord
	{
		[CompilerGenerated]
		private int @_SequenceNumber = 1;

		[CompilerGenerated]
		private TerminationCode @_TerminationCode;

		[LisRecordField(2)]
		public int SequenceNumber
		{
			get
			{
				return @_SequenceNumber;
			}
			set
			{
				@_SequenceNumber = value;
			}
		}

		[LisRecordField(3)]
		public TerminationCode TerminationCode
		{
			get
			{
				return @_TerminationCode;
			}
			set
			{
				@_TerminationCode = value;
			}
		}

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
