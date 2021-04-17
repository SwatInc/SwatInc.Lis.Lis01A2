using System;

namespace SwatInc.Lis.Lis02A2
{
	[Flags]
	public enum ResultNatureOfAbnormalityTestingSet
	{
		None = 0x0,
		[LisEnum("A")]
		Age = 0x1,
		[LisEnum("S")]
		Sex = 0x2,
		[LisEnum("R")]
		Race = 0x4,
		[LisEnum("N")]
		Normal = 0x8
	}
}
