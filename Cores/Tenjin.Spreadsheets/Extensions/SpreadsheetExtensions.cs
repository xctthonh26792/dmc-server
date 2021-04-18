using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Tenjin.Spreadsheets.Attributes;
using Tenjin.Spreadsheets.Models;

namespace Tenjin.Spreadsheets.Extensions
{
    public static class Extensiosns
    {
        public static Cell Cell(this Row row, int index)
        {
            if (row == null) return null;
            return row.Elements<Cell>().ElementAtOrDefault(index);
        }

        public static List<SpreadsheetSet> ToPairs(this Type value)
        {
            var sets = new List<SpreadsheetSet>();
            var properties = value.GetProperties();
            foreach (var property in properties)
            {
                var attribute = property.GetCustomAttribute<SpreadsheetColumn>();
                if (attribute == null)
                {
                    continue;
                }
                sets.Add(new SpreadsheetSet
                {
                    Column = attribute.Column,
                    Attribute = attribute,
                    Property = property
                });
            }
            return sets;
        }

        public static List<SpreadsheetSet> ToPairs<T>()
        {
            return ToPairs(typeof(T));
        }

        public static List<SpreadsheetSet> ToPairs(this Type value, Dictionary<string, SpreadsheetColumn> maps)
        {
            if (maps == null || maps.Count == 0)
            {
                return ToPairs(value);
            }
            var sets = new List<SpreadsheetSet>();
            var properties = value.GetProperties();
            foreach (var property in properties)
            {
                if (!maps.TryGetValue(property.Name, out var attribute))
                {
                    continue;
                }
                sets.Add(new SpreadsheetSet
                {
                    Column = attribute.Column,
                    Attribute = attribute,
                    Property = property
                });
            }
            return sets;
        }


        public static Dictionary<string, Cell> ToColumnDictionary(this Row row)
        {
            if (row == null) return null;
            return row.Descendants<Cell>().ToDictionary(cell => Regex.Replace(cell.CellReference, @"\d+", ""));
        }
    }
}
