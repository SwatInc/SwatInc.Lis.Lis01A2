namespace SwatInc.Lis.Lis02A2
{
	public static class LISDelimiters
	{
		public static char EscapeCharacter;

		public static char FieldDelimiter;

		public static char ComponentDelimiter;

		public static char RepeatDelimiter;

		static LISDelimiters()
		{
			FieldDelimiter = '|';
			RepeatDelimiter = '\\';
			ComponentDelimiter = '^';
			EscapeCharacter = '&';
		}

		public static string AddFieldDelimiters(int numberOfDelimiters)
		{
			return new string(FieldDelimiter, numberOfDelimiters);
		}
	}
}
