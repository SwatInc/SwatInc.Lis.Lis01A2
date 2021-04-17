using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class QueryRecord : AbstractLisRecord
	{
		[CompilerGenerated]
		private int @_SequenceNumber;

		[CompilerGenerated]
		private StartingRange @_StartingRange = new StartingRange();

		[CompilerGenerated]
		private StartingRange @_EndingRange = new StartingRange();

		[CompilerGenerated]
		private UniversalTestID @_UniversalTestID = new UniversalTestID();

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
		public StartingRange StartingRange
		{
			get
			{
				return @_StartingRange;
			}
			set
			{
				@_StartingRange = value;
			}
		}

		[LisRecordField(4)]
		public StartingRange EndingRange
		{
			get
			{
				return @_EndingRange;
			}
			set
			{
				@_EndingRange = value;
			}
		}

		[LisRecordField(5)]
		public UniversalTestID UniversalTestID
		{
			get
			{
				return @_UniversalTestID;
			}
			set
			{
				@_UniversalTestID = value;
			}
		}

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
