namespace SwatInc.Lis.Lis02A2
{
	public enum OrderActionCode
	{
		None,
		[LisEnum("C")]
		Cancel,
		[LisEnum("A")]
		Add,
		[LisEnum("N")]
		New,
		[LisEnum("P")]
		Pending,
		[LisEnum("L")]
		Reserved,
		[LisEnum("X")]
		InProcess,
		[LisEnum("Q")]
		QC
	}
}
