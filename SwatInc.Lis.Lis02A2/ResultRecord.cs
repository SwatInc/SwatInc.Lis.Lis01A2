using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class ResultRecord : AbstractLisRecord
	{
		[CompilerGenerated]
		private int @_SequenceNumber;

		[CompilerGenerated]
		private UniversalTestID @_UniversalTestID;

		[CompilerGenerated]
		private string @_Data;

		[CompilerGenerated]
		private string @_Units;

		[CompilerGenerated]
		private string @_ReferenceRanges;

		[CompilerGenerated]
		private ResultAbnormalFlags @_ResultAbnormalFlag;

		[CompilerGenerated]
		private ResultNatureOfAbnormalityTestingSet @_NatureOfAbnormalityTesting;

		[CompilerGenerated]
		private ResultStatus @_ResultStatus;

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

		[LisRecordField(4)]
		public string Data
		{
			get
			{
				return @_Data;
			}
			set
			{
				@_Data = value;
			}
		}

		[LisRecordField(5)]
		public string Units
		{
			get
			{
				return @_Units;
			}
			set
			{
				@_Units = value;
			}
		}

		[LisRecordField(6)]
		public string ReferenceRanges
		{
			get
			{
				return @_ReferenceRanges;
			}
			set
			{
				@_ReferenceRanges = value;
			}
		}

		[LisRecordField(7)]
		public ResultAbnormalFlags ResultAbnormalFlag
		{
			get
			{
				return @_ResultAbnormalFlag;
			}
			set
			{
				@_ResultAbnormalFlag = value;
			}
		}

		[LisRecordField(8)]
		public ResultNatureOfAbnormalityTestingSet NatureOfAbnormalityTesting
		{
			get
			{
				return @_NatureOfAbnormalityTesting;
			}
			set
			{
				@_NatureOfAbnormalityTesting = value;
			}
		}

		[LisRecordField(9)]
		public ResultStatus ResultStatus
		{
			get
			{
				return @_ResultStatus;
			}
			set
			{
				@_ResultStatus = value;
			}
		}

		public override string ToLISString()
		{
			return "R" + new string(LISDelimiters.FieldDelimiter, 1) + base.ToLISString();
		}

		public ResultRecord(string aLisString)
			: base(aLisString)
		{
		}

		public ResultRecord()
		{
		}
	}
}
