using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Reflection;
using Tenjin.Spreadsheets.Attributes;
using Tenjin.Spreadsheets.Interfaces;

namespace Tenjin.Spreadsheets.Extensions
{
    public class SpreadsheetCell
    {
        private readonly DateTime? _date;
        private readonly string _string;
        private readonly bool _boolean;
        private readonly double _double;
        private readonly int _int;
        private readonly long _long;

        public SpreadsheetCell(ISpreadsheetReader reader, Cell cell)
        {
            _date = reader.GetDate(cell);
            _string = reader.GetString(cell);
            _boolean = reader.GetBoolean(cell);
            _double = reader.GetDouble(cell);
            _int = reader.GetInt32(cell);
            _long = reader.GetInt64(cell);
        }

        public DateTime? GetDate()
        {
            if ((_date?.Year ?? 0) <= 1900)
            {
                return SpreadsheetConverts.GetDateTime(_string);
            }
            return _date;
        }

        public object GetDate(string pattern, string format)
        {
            if (!string.IsNullOrEmpty(pattern) && !string.IsNullOrEmpty(format))
            {
                return SpreadsheetConverts.GetDateTimeExact(_string, pattern).ToString(format);
            }
            if (string.IsNullOrEmpty(pattern) || !string.IsNullOrEmpty(format))
            {
                return GetDate()?.ToString(format) ?? string.Empty;
            }
            if (!string.IsNullOrEmpty(pattern) || string.IsNullOrEmpty(format))
            {
                return SpreadsheetConverts.GetDateTimeExact(_string, pattern);
            }
            return GetDate();
        }

        public object GetString()
        {
            return _string;
        }

        public object GetInt32()
        {
            return _int;
        }

        public object GetInt64()
        {
            return _long;
        }

        public object GetDouble()
        {
            return _double;
        }

        public object GetBoolean()
        {
            return _boolean;
        }

        public object Get(PropertyInfo property, SpreadsheetColumn attribute)
        {
            if (property.PropertyType == typeof(int))
            {
                return GetInt32();
            }
            if (property.PropertyType == typeof(long))
            {
                return GetInt64();
            }
            if (property.PropertyType == typeof(double))
            {
                return GetDouble();
            }
            if (property.PropertyType == typeof(bool))
            {
                return GetBoolean();
            }
            if (property.PropertyType == typeof(DateTime))
            {
                if (!string.IsNullOrEmpty(attribute.Pattern))
                {
                    return GetDate(attribute.Pattern, string.Empty);
                }
                return GetDate();
            }
            if (property.PropertyType == typeof(string))
            {
                if (!string.IsNullOrEmpty(attribute.Format) || !string.IsNullOrEmpty(attribute.Pattern))
                {
                    return GetDate(attribute.Pattern, attribute.Format);
                }
                return GetString();
            }
            throw new NotSupportedException();
        }
    }
}
