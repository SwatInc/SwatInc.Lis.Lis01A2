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

		//8.4.8 Specimen Collection Date and Time
		//This field shall represent the actual time the specimen was collected or obtained.
		[LisDateTimeUsage(LisDateTimeUsage.DateTime)]
		[LisRecordField(8)]
		public DateTime? SpecimenCollectionDateTime { get; set; }

		//8.4.9 Collection End Time
		//This field shall contain the end date and time of a timed specimen collection, such as 24-hour urine
		//collection.
		[LisDateTimeUsage(LisDateTimeUsage.DateTime)]
		[LisRecordField(9)]
		public DateTime? CollectionEndDateTime { get; set; }

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
