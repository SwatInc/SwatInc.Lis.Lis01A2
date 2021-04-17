using System;
using System.Globalization;

namespace SwatInc.Lis.Lis02A2
{
	internal static class LISExtensions
	{
		public static string ToLISDate(this DateTime dateTime, LisDateTimeUsage lisDateTimeUsage)
		{
			switch (lisDateTimeUsage)
			{
			default:
			{
				if (lisDateTimeUsage == LisDateTimeUsage.Date)
				{
					goto case LisDateTimeUsage.Date;
				}
				if (lisDateTimeUsage == LisDateTimeUsage.DateTime)
				{
					goto case LisDateTimeUsage.DateTime;
				}
				string Result = default(string);
				if (lisDateTimeUsage != LisDateTimeUsage.Time)
				{
					return Result;
				}
				goto case LisDateTimeUsage.Time;
			}
			case LisDateTimeUsage.Date:
				return dateTime.Year.ToString("D4") + dateTime.Month.ToString("D2") + dateTime.Day.ToString("D2");
			case LisDateTimeUsage.DateTime:
				return dateTime.Year.ToString("D4") + dateTime.Month.ToString("D2") + dateTime.Day.ToString("D2") + dateTime.Hour.ToString("D2") + dateTime.Minute.ToString("D2") + dateTime.Second.ToString("D2");
			case LisDateTimeUsage.Time:
				return dateTime.Hour.ToString("D2") + dateTime.Minute.ToString("D2") + dateTime.Second.ToString("D2");
			}
		}

		public static DateTime LisStringToDateTime(this string lisString, LisDateTimeUsage lisDateTimeUsage)
		{
			switch (lisDateTimeUsage)
			{
			default:
			{
				if (lisDateTimeUsage == LisDateTimeUsage.Date)
				{
					goto case LisDateTimeUsage.Date;
				}
				if (lisDateTimeUsage == LisDateTimeUsage.DateTime)
				{
					goto case LisDateTimeUsage.DateTime;
				}
				DateTime Result = default(DateTime);
				if (lisDateTimeUsage != LisDateTimeUsage.Time)
				{
					return Result;
				}
				goto case LisDateTimeUsage.Time;
			}
			case LisDateTimeUsage.Date:
				return DateTime.ParseExact(lisString, "yyyyMMdd", CultureInfo.InvariantCulture);
			case LisDateTimeUsage.DateTime:
				return DateTime.ParseExact(lisString, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
			case LisDateTimeUsage.Time:
				return DateTime.ParseExact(lisString, "HHmmss", CultureInfo.InvariantCulture);
			}
		}
	}
}
