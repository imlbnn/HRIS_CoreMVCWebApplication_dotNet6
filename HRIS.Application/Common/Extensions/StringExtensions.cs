using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRIS.Application.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Splits string, trims, and removed null and empty values
        /// </summary>
        /// <param name="value"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string[] SplitCleaned(this string value, char delimiter = ',')
        {
            if (value == null)
                return null;

            var _values = value.Split(delimiter);
            _values = _values.Where(x => !string.IsNullOrEmpty(x.Trim())).Select(x => x.Trim()).ToArray();
            return _values;
        }
        public static string[] SplitSearchKeys(this string value, char delimiter = ' ')
        {
            if (value == null)
                return null;

            var _values = value.Split('"')
                     .Select((element, index) => index % 2 == 0  // If even index
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                                           : new string[] { element })  // Keep the entire item
                     .SelectMany(element => element).ToArray();

            return _values;
        }

        public static string NullToEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;
            return value;
        }
    }
}
