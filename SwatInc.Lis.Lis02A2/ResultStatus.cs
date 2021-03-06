namespace SwatInc.Lis.Lis02A2
{
	public enum ResultStatus
	{
		None,
		[LisEnum("C")]
		Correction,
		[LisEnum("P")]
		PreliminaryResults,
		[LisEnum("F")]
		FinalResults,
		[LisEnum("X")]
		CannotBeDone,
		[LisEnum("I")]
		ResultsPending,
		[LisEnum("S")]
		PartialResults,
		[LisEnum("M")]
		MICLevel,
		[LisEnum("R")]
		PreviouslyTransmitted,
		[LisEnum("N")]
		NecessaryInformation,
		[LisEnum("Q")]
		ResponseToOutstandingQuery,
		[LisEnum("V")]
		ApprovedResult,
		[LisEnum("W")]
		Warning
	}
}
