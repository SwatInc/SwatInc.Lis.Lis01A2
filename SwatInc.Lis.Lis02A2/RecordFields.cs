using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	internal class RecordFields
	{
		private string[] fItems;

		[CompilerGenerated]
		private string @_Value;

		public string Value
		{
			get
			{
				return @_Value;
			}
			set
			{
				@_Value = value;
			}
		}

		public int Count => fItems.Length;

		public RecordFields(string lisString, char aSeporatorChar, int aNumberOfFields)
		{
			@_Value = lisString;
			fItems = lisString.Split(new char[1] { aSeporatorChar }, aNumberOfFields);
		}

		public string GetField(int indx)
		{
			if ((int)fItems.LongLength < indx)
			{
				return string.Empty;
			}
			string Result = fItems[indx - 1];
			Result = Result.Replace(new string(LISDelimiters.EscapeCharacter, 1) + "F" + new string(LISDelimiters.EscapeCharacter, 1), new string(LISDelimiters.FieldDelimiter, 1));
			Result = Result.Replace(new string(LISDelimiters.EscapeCharacter, 1) + "S" + new string(LISDelimiters.EscapeCharacter, 1), new string(LISDelimiters.ComponentDelimiter, 1));
			Result = Result.Replace(new string(LISDelimiters.EscapeCharacter, 1) + "R" + new string(LISDelimiters.EscapeCharacter, 1), new string(LISDelimiters.RepeatDelimiter, 1));
			return Result.Replace(new string(LISDelimiters.EscapeCharacter, 1) + "E" + new string(LISDelimiters.EscapeCharacter, 1), new string(LISDelimiters.EscapeCharacter, 1));
		}
	}
}
