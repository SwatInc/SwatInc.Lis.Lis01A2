using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	[AttributeUsage(AttributeTargets.Property)]
	public class LisDateTimeUsageAttribute : Attribute
	{
		public LisDateTimeUsage DateTimeUsage { get; set; }

		public LisDateTimeUsageAttribute(LisDateTimeUsage dateTimeUsage)
		{
			DateTimeUsage = dateTimeUsage;
		}
	}
}
