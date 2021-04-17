using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	[AttributeUsage(AttributeTargets.Property)]
	internal class LisRecordFieldAttribute : Attribute
	{
		[CompilerGenerated]
		private int @_FieldIndex;

		public int FieldIndex
		{
			get
			{
				return @_FieldIndex;
			}
			set
			{
				@_FieldIndex = value;
			}
		}

		public LisRecordFieldAttribute(int aFieldIndex)
		{
			@_FieldIndex = aFieldIndex;
		}
	}
}
