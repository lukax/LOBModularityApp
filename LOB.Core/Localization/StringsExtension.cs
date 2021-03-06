﻿#region Usings

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#endregion

namespace LOB.Core.Localization {
    public static class StringsExtension {
        public static string ToLocalizedString(this string s) {
            PropertyInfo[] properties = typeof(Strings).GetProperties();
            string result = null;
            foreach(PropertyInfo propertyInfo in properties.Where(propertyInfo => propertyInfo.Name == "Common_" + s)) result = propertyInfo.GetValue(properties).ToString();

            return result;
        }

        public static string ToLocalizedString(this string s, string customPrefix) {
            PropertyInfo[] properties = typeof(Strings).GetProperties();
            string result = "";
            foreach(PropertyInfo propertyInfo in properties)
                if(propertyInfo.Name == customPrefix + "_" + s) {
                    result = propertyInfo.GetValue(properties).ToString();
                    break;
                }
            return result;
        }

        public static IEnumerable<string> ToLocalizedString(this IEnumerable<string> list) {
            IList<string> enumerable = list as IList<string> ?? list.ToList();
            var results = new List<string>(enumerable.Count());
            foreach(string item in enumerable) {
                //results.
            }
            return results;
        }

        public static IEnumerable<string> GetProperties<T>() where T : class {
            PropertyInfo[] properties = typeof(T).GetProperties();
            return properties.Select(propertyInfo => propertyInfo.GetValue(properties) as string).Where(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}