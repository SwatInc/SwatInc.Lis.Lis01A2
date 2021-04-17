using System;

namespace SwatInc.Lis.Lis02A2
{
	[AttributeUsage(AttributeTargets.Property)]
	internal class LisRecordRemainingFieldsAttribute : LisRecordFieldAttribute
	{
		public LisRecordRemainingFieldsAttribute(int aFieldIndex)
			: base(aFieldIndex)
		{
		}
	}
}
