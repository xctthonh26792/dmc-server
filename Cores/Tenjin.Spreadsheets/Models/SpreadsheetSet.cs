using System.Reflection;
using Tenjin.Spreadsheets.Attributes;

namespace Tenjin.Spreadsheets.Models
{
    public class SpreadsheetSet
    {
        public string Column { get; set; }
        public SpreadsheetColumn Attribute { get; set; }
        public PropertyInfo Property { get; set; }
    }
}
