namespace SwatInc.Lis.Lis02A2
{
	public enum OrderReportType
	{
		None,
		[LisEnum("O")]
		Order,
		[LisEnum("C")]
		Correction,
		[LisEnum("P")]
		Preliminary,
		[LisEnum("F")]
		Final,
		[LisEnum("X")]
		Cancelled,
		[LisEnum("I")]
		Pending,
		[LisEnum("Y")]
		NoOrder,
		[LisEnum("Z")]
		NoRecord,
		[LisEnum("Q")]
		Response
	}
}
