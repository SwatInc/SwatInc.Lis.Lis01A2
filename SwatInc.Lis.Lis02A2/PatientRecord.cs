using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class PatientRecord : AbstractLisRecord
	{

		[LisRecordField(2)]
		public int SequenceNumber { get; set; }

		[LisRecordField(3)]
		public string PracticeAssignedPatientId { get; set; }

		[LisRecordField(4)]
		public string LaboratoryAssignedPatientId { get; set; }

		[LisRecordField(5)]
		public string PatientID3 { get; set; }

		[LisRecordField(6)]
		public PatientName PatientName { get; set; } = new PatientName();

		[LisRecordField(7)]
		public PatientName MothersMaidenName { get; set; } = new PatientName();

		[LisDateTimeUsage(LisDateTimeUsage.Date)]
		[LisRecordField(8)]
		public DateTime? Birthdate { get; set; } = null;

		[LisRecordField(9)]
		public PatientSex? PatientSex { get; set; } = null;

		[LisRecordField(14)]
		public string AttendingPhysicianID { get; set; }

		public override string ToLISString()
		{
			return "P" + new string(LISDelimiters.FieldDelimiter, 1) + base.ToLISString();
		}

		public PatientRecord(string aLisString)
			: base(aLisString)
		{
		}

		public PatientRecord()
		{
		}
	}
}
