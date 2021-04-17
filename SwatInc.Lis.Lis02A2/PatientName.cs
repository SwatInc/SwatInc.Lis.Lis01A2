using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class PatientName : AbstractLisSubRecord
	{
		[CompilerGenerated]
		private string @_LastName;

		[CompilerGenerated]
		private string @_FirstName;

		[CompilerGenerated]
		private string @_MiddleName;

		[CompilerGenerated]
		private string @_Suffix;

		[CompilerGenerated]
		private string @_Title;

		[LisRecordField(1)]
		public string LastName
		{
			get
			{
				return @_LastName;
			}
			set
			{
				@_LastName = value;
			}
		}

		[LisRecordField(2)]
		public string FirstName
		{
			get
			{
				return @_FirstName;
			}
			set
			{
				@_FirstName = value;
			}
		}

		[LisRecordField(3)]
		public string MiddleName
		{
			get
			{
				return @_MiddleName;
			}
			set
			{
				@_MiddleName = value;
			}
		}

		[LisRecordField(4)]
		public string Suffix
		{
			get
			{
				return @_Suffix;
			}
			set
			{
				@_Suffix = value;
			}
		}

		[LisRecordField(5)]
		public string Title
		{
			get
			{
				return @_Title;
			}
			set
			{
				@_Title = value;
			}
		}

		public PatientName(string aLisString)
			: base(aLisString)
		{
		}

		public PatientName()
		{
		}
	}
}
