using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class SendProgressEventArgs : EventArgs
	{
		[CompilerGenerated]
		private double @_Progress;

		[CompilerGenerated]
		private Guid @_ThreadGuid;

		public double Progress
		{
			get
			{
				return @_Progress;
			}
			set
			{
				@_Progress = value;
			}
		}

		public Guid ThreadGuid
		{
			get
			{
				return @_ThreadGuid;
			}
			set
			{
				@_ThreadGuid = value;
			}
		}

		public SendProgressEventArgs(double aProgress, Guid aThreadGuid)
		{
			@_Progress = aProgress;
			@_ThreadGuid = aThreadGuid;
		}
	}
}
