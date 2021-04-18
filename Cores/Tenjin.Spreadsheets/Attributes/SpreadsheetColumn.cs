using System;

namespace Tenjin.Spreadsheets.Attributes
{
    public class SpreadsheetColumn : Attribute
    {
        public string Column { get; private set; }
        public string Pattern { get; set; }
        public string Format { get; set; }
        public bool Required { get; set; }
        public SpreadsheetColumn(string column)
        {
            Column = column;
        }
    }
}
