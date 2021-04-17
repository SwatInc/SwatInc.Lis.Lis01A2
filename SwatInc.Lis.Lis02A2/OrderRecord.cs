using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class OrderRecord : AbstractLisRecord
	{
		[CompilerGenerated]
		private int @_SequenceNumber;

		[CompilerGenerated]
		private string @_SpecimenID;

		[CompilerGenerated]
		private UniversalTestID @_TestID;

		[CompilerGenerated]
		private OrderPriority @_Priority;

		[CompilerGenerated]
		private DateTime? @_RequestedDateTime;

		[CompilerGenerated]
		private OrderActionCode @_ActionCode;

		[CompilerGenerated]
		private OrderReportType @_ReportType;

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
		public string SpecimenID
		{
			get
			{
				return @_SpecimenID;
			}
			set
			{
				@_SpecimenID = value;
			}
		}

		[LisRecordField(5)]
		public UniversalTestID TestID
		{
			get
			{
				return @_TestID;
			}
			set
			{
				@_TestID = value;
			}
		}

		[LisRecordField(6)]
		public OrderPriority Priority
		{
			get
			{
				return @_Priority;
			}
			set
			{
				@_Priority = value;
			}
		}

		[LisDateTimeUsage(LisDateTimeUsage.DateTime)]
		[LisRecordField(7)]
		public DateTime? RequestedDateTime
		{
			get
			{
				return @_RequestedDateTime;
			}
			set
			{
				@_RequestedDateTime = value;
			}
		}

		[LisRecordField(12)]
		public OrderActionCode ActionCode
		{
			get
			{
				return @_ActionCode;
			}
			set
			{
				@_ActionCode = value;
			}
		}

		[LisRecordField(26)]
		public OrderReportType ReportType
		{
			get
			{
				return @_ReportType;
			}
			set
			{
				@_ReportType = value;
			}
		}

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
