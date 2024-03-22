using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using slf4net;

namespace SwatInc.Lis.Lis02A2
{
	public abstract class AbstractLisRecord
	{
        private ILogger fLog;
		protected const char CR = '\r';

        private AbstractLisSubRecord fCreateSubrecord(string aString, Type aType)
		{
			return (AbstractLisSubRecord)Activator.CreateInstance(aType, aString);
		}

		private object fCreateLisEnum(string aString, Type aType)
		{
			object[] flagsAttribs = aType.GetCustomAttributes(typeof(FlagsAttribute), inherit: false);
			if ((int)flagsAttribs.LongLength > 0)
			{
				string inputString = string.Empty;
				string enumStringValue = null;
				int i = 0;
				FieldInfo[] fields = aType.GetFields();
				if (fields != null)
				{
					for (; i < (int)fields.LongLength; i++)
					{
						FieldInfo fi = fields[i];
						LisEnumAttribute[] attribs = fi.GetCustomAttributes(typeof(LisEnumAttribute), inherit: false) as LisEnumAttribute[];
						if ((int)attribs.LongLength > 0)
						{
							enumStringValue = attribs[0].LisID;
						}
						if (aString == null)
						{
							continue;
						}
						CharEnumerator enumerator = aString.GetEnumerator();
						if (enumerator == null)
						{
							continue;
						}
						try
						{
							while (enumerator.MoveNext())
							{
								char ch = enumerator.Current;
								if (string.Compare(enumStringValue, new string(ch, 1), ignoreCase: true) == 0)
								{
									inputString = inputString + fi.Name + ",";
								}
							}
						}
						finally
						{
							enumerator.Dispose();
						}
					}
				}
				if (inputString.Length > 0)
				{
					inputString = inputString.Remove(inputString.Length - 1, 1);
					return Enum.Parse(aType, inputString);
				}
			}
			else
			{
				string inputString = null;
				int i = 0;
				FieldInfo[] fields = aType.GetFields();
				if (fields != null)
				{
					for (; i < (int)fields.LongLength; i++)
					{
						FieldInfo fi = fields[i];
						LisEnumAttribute[] attribs = fi.GetCustomAttributes(typeof(LisEnumAttribute), inherit: false) as LisEnumAttribute[];
						if ((int)attribs.LongLength > 0)
						{
							inputString = attribs[0].LisID;
						}
						if (string.Compare(inputString, aString, ignoreCase: true) == 0)
						{
							return Enum.Parse(aType, fi.Name);
						}
					}
				}
			}
			object Result = default(object);
			return Result;
		}

		private string[] fCreateRemainingFieldsArray(RecordFields aRecordFields, int aStartIndex)
		{
			List<string> temp = new List<string>();
			int count = aRecordFields.Count;
			int i = aStartIndex;
			if (i <= count)
			{
				count++;
				do
				{
					temp.Add(aRecordFields.GetField(i));
					i++;
				}
				while (i != count);
			}
			return temp.ToArray();
		}

		private string fGetEnumLisString(object aEnum)
		{
			Type enumType = aEnum?.GetType();
			object[] flagsAttribs = enumType.GetCustomAttributes(typeof(FlagsAttribute), inherit: false);
			FieldInfo ev;
			LisEnumAttribute[] attribs;
			if ((int)flagsAttribs.LongLength > 0)
			{
				string Result = string.Empty;
				string[] inputVals = aEnum.ToString().Split(new char[1] { ',' });
				FieldInfo[] enumValues = enumType.GetFields();
				int i = 0;
				FieldInfo[] array = enumValues;
				if (array != null)
				{
					for (; i < (int)array.LongLength; i++)
					{
						ev = array[i];
						attribs = ev.GetCustomAttributes(typeof(LisEnumAttribute), inherit: false) as LisEnumAttribute[];
						if ((int)attribs.LongLength <= 0)
						{
							continue;
						}
						int j = 0;
						string[] array2 = inputVals;
						if (array2 == null)
						{
							continue;
						}
						for (; j < (int)array2.LongLength; j++)
						{
							string iv = array2[j];
							if (iv.Trim() == ev.Name)
							{
								Result += attribs[0].LisID;
							}
						}
					}
				}
				return Result;
			}
			ev = enumType.GetField(aEnum.ToString());
			attribs = ev.GetCustomAttributes(typeof(LisEnumAttribute), inherit: false) as LisEnumAttribute[];
			return ((int)attribs.LongLength <= 0) ? null : attribs[0].LisID;
		}

		private string fEscapeString(string aString, bool aSubrecord)
		{
			string Result = aString;
			Result = Result.Replace(new string(LISDelimiters.EscapeCharacter, 1), new string(LISDelimiters.EscapeCharacter, 1) + "E" + new string(LISDelimiters.EscapeCharacter, 1));
			Result = Result.Replace(new string(LISDelimiters.FieldDelimiter, 1), new string(LISDelimiters.EscapeCharacter, 1) + "F" + new string(LISDelimiters.EscapeCharacter, 1));
			if (aSubrecord)
			{
				Result = Result.Replace(new string(LISDelimiters.ComponentDelimiter, 1), new string(LISDelimiters.EscapeCharacter, 1) + "S" + new string(LISDelimiters.EscapeCharacter, 1));
			}
			return Result;
		}

		private string fRemoveOptionalSubFields(string aString)
		{
			if (aString.Contains(new string(LISDelimiters.ComponentDelimiter, 1)))
			{
				return aString.Split(new char[1] { LISDelimiters.ComponentDelimiter })[0];
			}
			return aString;
		}

		public virtual string ToLISString()
		{
			bool isSubRecord = this is AbstractLisSubRecord;
			char sepChar = ((!isSubRecord) ? LISDelimiters.FieldDelimiter : LISDelimiters.ComponentDelimiter);
			StringBuilder sb = new StringBuilder();
			Dictionary<int, string> fieldList = new Dictionary<int, string>();
			Type selfType = this?.GetType();
			PropertyInfo[] props = selfType.GetProperties();
			int maxFieldIndex = int.MinValue;
			int minFieldIndex = int.MaxValue;
			int i = 0;
			PropertyInfo[] array = props;
			string propString;
			if (array != null)
			{
				for (; i < (int)array.LongLength; i++)
				{
					PropertyInfo prop = array[i];
					object[] attribs = prop.GetCustomAttributes(typeof(LisRecordFieldAttribute), inherit: false);
					if ((int)attribs.LongLength <= 0)
					{
						continue;
					}
					LisRecordFieldAttribute attrib = (LisRecordFieldAttribute)attribs[0];
					Type propType = prop.PropertyType;
					Type nullablePropType = Nullable.GetUnderlyingType(propType);
					if ((object)nullablePropType != null)
					{
						propType = nullablePropType;
					}
					propString = null;
					object propVal = prop.GetValue(this, null);
					if (propVal != null)
					{
						if (propType == typeof(DateTime))
						{
							LisDateTimeUsage dateTimeUsage = LisDateTimeUsage.DateTime;
							attribs = prop.GetCustomAttributes(typeof(LisDateTimeUsageAttribute), inherit: false);
							if ((int)attribs.LongLength == 1)
							{
								LisDateTimeUsageAttribute dtAttrib = (LisDateTimeUsageAttribute)attribs[0];
								dateTimeUsage = dtAttrib.DateTimeUsage;
							}
							propString = ((DateTime)propVal).ToLISDate(dateTimeUsage);
						}
						else if (propType.IsEnum)
						{
							propString = fGetEnumLisString(propVal);
						}
						else if (propType.BaseType == typeof(AbstractLisSubRecord))
						{
							propString = (propVal as AbstractLisSubRecord).ToLISString();
						}
						else if (propType.IsArray)
						{
							if (attrib is LisRecordRemainingFieldsAttribute)
							{
								string[] ar = (string[])propVal;
								propString = string.Join(new string(sepChar, 1), ar);
							}
						}
						else
						{
							propString = propVal.ToString();
						}
					}
					if (!string.IsNullOrEmpty(propString))
					{
						fieldList.Add(attrib.FieldIndex, propString);
						if (attrib.FieldIndex > maxFieldIndex)
						{
							maxFieldIndex = attrib.FieldIndex;
						}
					}
					if (attrib.FieldIndex < minFieldIndex)
					{
						minFieldIndex = attrib.FieldIndex;
					}
				}
			}
			if (minFieldIndex <= maxFieldIndex)
			{
				int num = maxFieldIndex - 1;
				i = minFieldIndex;
				if (i <= num)
				{
					num++;
					do
					{
						fieldList.TryGetValue(i, out propString);
						if (!string.IsNullOrEmpty(propString))
						{
							sb.Append(fEscapeString(propString, isSubRecord));
						}
						sb.Append(sepChar);
						i++;
					}
					while (i != num);
				}
			}
			fieldList.TryGetValue(maxFieldIndex, out var field);
			if (!string.IsNullOrEmpty(field))
			{
				sb.Append(field);
			}
			if (!isSubRecord)
			{
				sb.Append('\r');
			}
			return sb.ToString();
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			Type selfType = this?.GetType();
			PropertyInfo[] props = selfType.GetProperties();
			int i = 0;
			PropertyInfo[] array = props;
			if (array != null)
			{
				for (; i < (int)array.LongLength; i++)
				{
					PropertyInfo prop = array[i];
					object[] attribs = prop.GetCustomAttributes(typeof(LisRecordFieldAttribute), inherit: false);
					if ((int)attribs.LongLength > 0)
					{
						object propVal = prop.GetValue(this, null);
						string propString = null;
						if (propVal != null)
						{
							propString = propVal.ToString();
						}
						if (!string.IsNullOrEmpty(propString))
						{
							sb.Append(prop.Name);
							sb.Append(": ");
							sb.AppendLine(propString);
						}
					}
				}
			}
			return sb.ToString();
		}

		public AbstractLisRecord(string aLisString) : base()
		{
            fLog = LoggerFactory.GetLogger(typeof(AbstractLisRecord));
            bool isSubRecord = this is AbstractLisSubRecord;
			char sepChar = ((!isSubRecord) ? LISDelimiters.FieldDelimiter : LISDelimiters.ComponentDelimiter);
			Type selfType = this?.GetType();
			PropertyInfo[] props = selfType.GetProperties();
			int limit = int.MaxValue;
			if (isSubRecord && (int)props.LongLength > 0 && !props[(int)props.LongLength - 1].PropertyType.IsArray)
			{
				limit = props.Length;
			}
			RecordFields rf = new RecordFields(aLisString, sepChar, limit);
			int i = 0;
			PropertyInfo[] array = props;
			if (array == null)
			{
				return;
			}
			for (; i < (int)array.LongLength; i++)
			{
				PropertyInfo prop = array[i];
				object[] attribs = prop.GetCustomAttributes(typeof(LisRecordFieldAttribute), inherit: false);
				if ((int)attribs.LongLength <= 0)
				{
					continue;
				}
				LisRecordFieldAttribute attrib = (LisRecordFieldAttribute)attribs[0];
				string field = rf.GetField(attrib.FieldIndex);
				if (string.IsNullOrEmpty(field))
				{
					continue;
				}
				Type propType = prop.PropertyType;
				Type nullablePropType = Nullable.GetUnderlyingType(propType);
				if ((object)nullablePropType != null)
				{
					propType = nullablePropType;
				}
				try
				{
					if(!(propType == typeof(int)))
					{
						if(!(propType == typeof(string)))
						{
							if(propType == typeof(DateTime))
							{
								LisDateTimeUsage dateTimeUsage = LisDateTimeUsage.DateTime;
								attribs = prop.GetCustomAttributes(typeof(LisDateTimeUsageAttribute), inherit: false);
								if((int)attribs.LongLength == 1)
								{
									LisDateTimeUsageAttribute dtAttrib = (LisDateTimeUsageAttribute)attribs[0];
									dateTimeUsage = dtAttrib.DateTimeUsage;
								}
								prop.SetValue(this, fRemoveOptionalSubFields(field).LisStringToDateTime(dateTimeUsage), null);
							}
							else if(propType.IsEnum)
							{
								prop.SetValue(this, fCreateLisEnum(fRemoveOptionalSubFields(field), propType), null);
							}
							else if(propType.BaseType == typeof(AbstractLisSubRecord))
							{
								prop.SetValue(this, fCreateSubrecord(field, propType), null);
							}
							else if(propType.IsArray)
							{
								if(!(attrib is LisRecordRemainingFieldsAttribute))
								{
									throw new FormatException("The LIS String was not of the correct format.");
								}
								prop.SetValue(this, fCreateRemainingFieldsArray(rf, attrib.FieldIndex), null);
							}
						}
						else
						{
							prop.SetValue(this, field, null);
						}
					}
					else
					{
						prop.SetValue(this, int.Parse(fRemoveOptionalSubFields(field)), null);
					}
				}
				catch(Exception ex)
				{

                    if(LisParserSettings.ThrowExceptionOnError)
                    {
                        throw ex;
                    }
					else
					{
                        fLog?.Warn($"{prop?.Name}: invalid field content ({field}) ");
					}
                }            
			}
		}

		public AbstractLisRecord()
		{
            fLog = LoggerFactory.GetLogger(typeof(AbstractLisRecord));
        }
	}
}
