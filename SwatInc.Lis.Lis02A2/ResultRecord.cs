using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class ResultRecord : AbstractLisRecord
	{

		[LisRecordField(2)]
		public int SequenceNumber { get; set; }

		[LisRecordField(3)]
		public UniversalTestID UniversalTestID { get; set; }

		[LisRecordField(4)]
		public string Data { get; set; }

		[LisRecordField(5)]
		public string Units { get; set; }

		[LisRecordField(6)]
		public string ReferenceRanges { get; set; }

		[LisRecordField(7)]
		public ResultAbnormalFlags ResultAbnormalFlag { get; set; }

		[LisRecordField(8)]
		public ResultNatureOfAbnormalityTestingSet NatureOfAbnormalityTesting { get; set; }

		[LisRecordField(9)]
		public ResultStatus ResultStatus { get; set; }

		[LisRecordField(11)]
        public string OperatorIdentification { get; set; }

		[LisDateTimeUsage(LisDateTimeUsage.DateTime)]
		[LisRecordField(12)]
        public DateTime? TestStartedDateTime{ get; set; }

		[LisDateTimeUsage(LisDateTimeUsage.DateTime)]
		[LisRecordField(13)]
		public DateTime? TestCompletedDateTime { get; set; }

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
