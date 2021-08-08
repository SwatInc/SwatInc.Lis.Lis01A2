using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class OrderRecord : AbstractLisRecord
	{

		[LisRecordField(2)]
		public int SequenceNumber { get; set; }
		[LisRecordField(3)]
		public string SpecimenID { get; set; }

		[LisRecordField(5)]
		public UniversalTestID TestID { get; set; }

		[LisRecordField(6)]
		public OrderPriority Priority { get; set; }

		[LisDateTimeUsage(LisDateTimeUsage.DateTime)]
		[LisRecordField(7)]
		public DateTime? RequestedDateTime { get; set; }

		[LisRecordField(26)]
		public OrderReportType ReportType { get; set; }

        public override string ToLISString()
		{
			return "O" + new string(LISDelimiters.FieldDelimiter, 1) + base.ToLISString();
		}

		public OrderRecord(string aLisString)
			: base(aLisString)
		{
		}

		public OrderRecord()
		{
		}
	}
}
