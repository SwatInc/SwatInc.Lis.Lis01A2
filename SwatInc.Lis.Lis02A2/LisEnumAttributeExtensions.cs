using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SwatInc.Lis.Lis02A2
{
    public static class LisEnumAttributeExtensions
    {
        public static string GetLisEnumValue<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if(!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a LisEnumAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if(memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(LisEnumAttribute), false);
                if(attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((LisEnumAttribute)attrs[0]).LisID;
                }
            }

            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }
    }
}
