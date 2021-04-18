using System;
using System.Globalization;

namespace Tenjin.Spreadsheets.Extensions
{
    public class SpreadsheetConverts
    {
        public static string GetString(object value, string defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value)) return defaultValue;
            if (value is string resp) return resp.Trim();
            return value.ToString().Trim();
        }

        public static int GetInt32(object value, int defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value)) return defaultValue;
            if (value is int resp) return resp;
            return int.TryParse(GetString(value), out int r) ? r : defaultValue;
        }

        public static int? GetNullableInt32(object value, int? defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value)) return defaultValue;
            if (value is int resp) return resp;
            return int.TryParse(GetString(value), out int r) ? r : defaultValue;
        }

        public static long GetInt64(object value, long defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value)) return defaultValue;
            if (value is long resp) return resp;
            return long.TryParse(GetString(value), out long r) ? r : defaultValue;
        }

        public static long? GetNullableInt64(object value, long? defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value)) return defaultValue;
            if (value is long resp) return resp;
            return long.TryParse(GetString(value), out long r) ? r : defaultValue;
        }

        public static double GetDouble(object value, double defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value)) return defaultValue;
            if (value is double resp) return resp;
            return double.TryParse(GetString(value), out double r) ? r : defaultValue;
        }

        public static decimal GetDecimal(object value, decimal defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value)) return defaultValue;
            if (value is decimal resp) return resp;
            return decimal.TryParse(GetString(value), out decimal r) ? r : defaultValue;
        }

        public static double? GetNullableDouble(object value, double? defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value)) return defaultValue;
            if (value is double resp) return resp;
            return double.TryParse(GetString(value), out double r) ? r : defaultValue;
        }

        public static decimal? GetNullableDecimal(object value, decimal? defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value)) return defaultValue;
            if (value is decimal resp) return resp;
            return decimal.TryParse(GetString(value), out decimal r) ? r : defaultValue;
        }

        public static DateTime GetDateTimeExact(object value, string format = "dd/MM/yyyy", DateTime defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value) || SpreadsheetUtils.IsStringEmpty(format)) return defaultValue;
            if (value is DateTime resp) return resp;
            return DateTime.TryParseExact(GetString(value), format, null, DateTimeStyles.None, out DateTime r) ? r : defaultValue;
        }

        public static DateTime? GetNullableDateTimeExact(object value, string format = "dd/MM/yyyy", DateTime? defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value) || SpreadsheetUtils.IsStringEmpty(format)) return defaultValue;
            if (value is DateTime resp) return resp;
            return DateTime.TryParseExact(GetString(value), format, null, DateTimeStyles.None, out DateTime r) ? r : defaultValue;
        }

        public static DateTime GetDateTime(object value, DateTime defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value)) return defaultValue;
            if (value is DateTime resp) return resp;
            return DateTime.TryParse(GetString(value), out DateTime r) ? r : defaultValue;
        }

        public static DateTime? GetNullableDateTime(object value, DateTime? defaultValue = default)
        {
            if (SpreadsheetUtils.IsNull(value)) return defaultValue;
            if (value is DateTime resp) return resp;
            return DateTime.TryParse(GetString(value), out DateTime r) ? r : defaultValue;
        }
    }
}
