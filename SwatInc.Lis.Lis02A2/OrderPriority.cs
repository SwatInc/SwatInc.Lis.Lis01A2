namespace SwatInc.Lis.Lis02A2
{
	public enum OrderPriority
	{
		None,
		[LisEnum("S")]
		Stat,
		[LisEnum("A")]
		ASAP,
		[LisEnum("R")]
		Routine,
		[LisEnum("C")]
		CallBack,
		[LisEnum("P")]
		PreOperative
	}
}
