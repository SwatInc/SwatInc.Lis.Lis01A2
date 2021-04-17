using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class StartingRange : AbstractLisSubRecord
	{
		[CompilerGenerated]
		private string @_PatientID;

		[CompilerGenerated]
		private string @_SpecimenID;

		[CompilerGenerated]
		private string @_Reserved;

		[LisRecordField(1)]
		public string PatientID
		{
			get
			{
				return @_PatientID;
			}
			set
			{
				@_PatientID = value;
			}
		}

		[LisRecordField(2)]
		public string SpecimenID
		{
			get
			{
				return @_SpecimenID;
			}
			set
			{
				@_SpecimenID = value;
			}
		}

		[LisRecordField(3)]
		public string Reserved
		{
			get
			{
				return @_Reserved;
			}
			set
			{
				@_Reserved = value;
			}
		}

		public StartingRange(string aLisString)
			: base(aLisString)
		{
		}

		public StartingRange()
		{
		}
	}
}
