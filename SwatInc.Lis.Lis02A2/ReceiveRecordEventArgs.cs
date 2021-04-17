using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class ReceiveRecordEventArgs : EventArgs
	{
		[CompilerGenerated]
		private AbstractLisRecord @_ReceivedRecord;

		[CompilerGenerated]
		private LisRecordType @_RecordType;

		public AbstractLisRecord ReceivedRecord
		{
			get
			{
				return @_ReceivedRecord;
			}
			set
			{
				@_ReceivedRecord = value;
			}
		}

		public LisRecordType RecordType
		{
			get
			{
				return @_RecordType;
			}
			set
			{
				@_RecordType = value;
			}
		}

		public ReceiveRecordEventArgs(AbstractLisRecord aReceivedRecord, LisRecordType aLisRecordType)
		{
			@_ReceivedRecord = aReceivedRecord;
			@_RecordType = aLisRecordType;
		}

		public ReceiveRecordEventArgs()
		{
		}
	}
}
