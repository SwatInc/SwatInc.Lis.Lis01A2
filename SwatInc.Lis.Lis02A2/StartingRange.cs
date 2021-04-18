using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class StartingRange : AbstractLisSubRecord
	{


		[LisRecordField(1)]
		public string PatientID { get; set; }

		[LisRecordField(2)]
		public string SpecimenID { get; set; }

		[LisRecordField(3)]
		public string Reserved { get; set; }

		public StartingRange(string aLisString)
			: base(aLisString)
		{
		}

		public StartingRange()
		{
		}
	}
}
