using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class UniversalTestID : AbstractLisSubRecord
	{
		[CompilerGenerated]
		private string @_TestID;

		[CompilerGenerated]
		private string @_TestName;

		[CompilerGenerated]
		private string @_TestType;

		[CompilerGenerated]
		private string @_ManufacturerCode;

		[CompilerGenerated]
		private string[] @_OptionalFields;

		[LisRecordField(1)]
		public string TestID
		{
			get
			{
				return @_TestID;
			}
			set
			{
				@_TestID = value;
			}
		}

		[LisRecordField(2)]
		public string TestName
		{
			get
			{
				return @_TestName;
			}
			set
			{
				@_TestName = value;
			}
		}

		[LisRecordField(3)]
		public string TestType
		{
			get
			{
				return @_TestType;
			}
			set
			{
				@_TestType = value;
			}
		}

		[LisRecordField(4)]
		public string ManufacturerCode
		{
			get
			{
				return @_ManufacturerCode;
			}
			set
			{
				@_ManufacturerCode = value;
			}
		}

		[LisRecordRemainingFields(5)]
		public string[] OptionalFields
		{
			get
			{
				return @_OptionalFields;
			}
			set
			{
				@_OptionalFields = value;
			}
		}

		public UniversalTestID(string aLisString)
			: base(aLisString)
		{
		}

		public UniversalTestID()
		{
		}
	}
}
