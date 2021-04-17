using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	public class PatientRecord : AbstractLisRecord
	{
		[CompilerGenerated]
		private int @_SequenceNumber;

		[CompilerGenerated]
		private string @_PracticeAssignedPatientID;

		[CompilerGenerated]
		private string @_LaboratoryAssignedPatientID;

		[CompilerGenerated]
		private string @_PatientID3;

		[CompilerGenerated]
		private PatientName @_PatientName;

		[CompilerGenerated]
		private DateTime? @_Birthdate = null;

		[CompilerGenerated]
		private PatientSex? @_PatientSex = null;

		[CompilerGenerated]
		private string @_AttendingPhysicianID;

		[LisRecordField(2)]
		public int SequenceNumber
		{
			get
			{
				return @_SequenceNumber;
			}
			set
			{
				@_SequenceNumber = value;
			}
		}

		[LisRecordField(3)]
		public string PracticeAssignedPatientID
		{
			get
			{
				return @_PracticeAssignedPatientID;
			}
			set
			{
				@_PracticeAssignedPatientID = value;
			}
		}

		[LisRecordField(4)]
		public string LaboratoryAssignedPatientID
		{
			get
			{
				return @_LaboratoryAssignedPatientID;
			}
			set
			{
				@_LaboratoryAssignedPatientID = value;
			}
		}

		[LisRecordField(5)]
		public string PatientID3
		{
			get
			{
				return @_PatientID3;
			}
			set
			{
				@_PatientID3 = value;
			}
		}

		[LisRecordField(6)]
		public PatientName PatientName
		{
			get
			{
				return @_PatientName;
			}
			set
			{
				@_PatientName = value;
			}
		}

		[LisDateTimeUsage(LisDateTimeUsage.Date)]
		[LisRecordField(8)]
		public DateTime? Birthdate
		{
			get
			{
				return @_Birthdate;
			}
			set
			{
				@_Birthdate = value;
			}
		}

		[LisRecordField(9)]
		public PatientSex? PatientSex
		{
			get
			{
				return @_PatientSex;
			}
			set
			{
				@_PatientSex = value;
			}
		}

		[LisRecordField(14)]
		public string AttendingPhysicianID
		{
			get
			{
				return @_AttendingPhysicianID;
			}
			set
			{
				@_AttendingPhysicianID = value;
			}
		}

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
