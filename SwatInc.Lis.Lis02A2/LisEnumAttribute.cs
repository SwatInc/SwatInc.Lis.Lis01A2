using System;
using System.Runtime.CompilerServices;

namespace SwatInc.Lis.Lis02A2
{
	[AttributeUsage(AttributeTargets.Field)]
	public class LisEnumAttribute : Attribute
	{

		public string LisID { get; set; }

		public LisEnumAttribute(string aLisID)
		{
			LisID = aLisID;
		}
	}
}
