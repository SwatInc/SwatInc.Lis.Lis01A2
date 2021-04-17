namespace SwatInc.Lis.Lis02A2
{
	public enum TerminationCode
	{
		[LisEnum("N")]
		Normal,
		[LisEnum("T")]
		SenderAborted,
		[LisEnum("R")]
		ReceiverRequestedAbort,
		[LisEnum("E")]
		UnknownSystemError,
		[LisEnum("Q")]
		ErrorInLastRequestForInformation,
		[LisEnum("I")]
		NoInformationAvailableFromLastQuery,
		[LisEnum("F")]
		LastRequestForInformationProcessed
	}
}
