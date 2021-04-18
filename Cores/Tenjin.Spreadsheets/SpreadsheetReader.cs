using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tenjin.Spreadsheets.Attributes;
using Tenjin.Spreadsheets.Extensions;
using Tenjin.Spreadsheets.Interfaces;
using Tenjin.Spreadsheets.Models;

namespace Tenjin.Spreadsheets
{
    public abstract class SpreadsheetReader : ISpreadsheetReader, IDisposable
    {
        protected readonly SpreadsheetDocument _document;
        public SpreadsheetReader(Stream stream)
        {
            _document = SpreadsheetDocument.Open(stream, false);
        }

        public double GetDouble(Cell cell, double def = 0)
        {
            if (cell == null)
            {
                return def;
            }
            return SpreadsheetConverts.GetDouble(GetString(cell));
        }

        public long GetInt64(Cell cell, long def = 0)
        {
            if (cell == null)
            {
                return def;
            }
            return SpreadsheetConverts.GetInt64(GetString(cell));
        }

        public int GetInt32(Cell cell, int def = 0)
        {
            if (cell == null)
            {
                return def;
            }
            return SpreadsheetConverts.GetInt32(GetString(cell));
        }

        public bool GetBoolean(Cell cell, bool def = false)
        {
            if (cell == null)
            {
                return def;
            }
            var value = GetString(cell, "0").ToUpper();
            return value != "0" && value != "FALSE";
        }

        public string GetString(Cell cell, string def = "")
        {
            if (cell == null)
            {
                return def;
            }
            var shared = _document.WorkbookPart.SharedStringTablePart;
            var value = cell.CellValue?.InnerXml ?? string.Empty;
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                return shared.SharedStringTable.ChildElements[SpreadsheetConverts.GetInt32(value)].InnerText;
            }
            return value;
        }

        public DateTime? GetDate(Cell cell)
        {
            try
            {
                return DateTime.FromOADate(SpreadsheetConverts.GetDouble(GetString(cell))).Date;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public DateTime GetDate(Cell cell, string format)
        {
            return SpreadsheetConverts.GetDateTimeExact(GetString(cell), format);
        }

        public void Dispose()
        {
            _document.Dispose();
        }
    }

    public abstract class SpreadsheetReader<T> : SpreadsheetReader, ISpreadsheetReader<T>
        where T : new()
    {
        private readonly List<SpreadsheetSet> _sets;

        protected SpreadsheetReader(Stream stream) : base(stream)
        {
            _sets = typeof(T).ToPairs();
        }

        protected SpreadsheetReader(Stream stream, Dictionary<string, SpreadsheetColumn> maps) : base(stream)
        {
            _sets = typeof(T).ToPairs(maps);
        }

        protected T Convert(Row row)
        {
            var maps = row.ToColumnDictionary();
            var model = new T();
            foreach (var set in _sets)
            {
                if (!maps.TryGetValue(set.Column, out var cell))
                {
                    if (set.Attribute.Required)
                    {
                        return default;
                    }
                    continue;
                }
                var value = new SpreadsheetCell(this, cell).Get(set.Property, set.Attribute);
                set.Property.SetValue(model, value);
            }
            return Populate(maps, model);
        }

        protected virtual T Populate(Dictionary<string, Cell> maps, T model)
        {
            return model;
        }

        public List<T> Execute(int skip = 1)
        {
            var part = _document.WorkbookPart.WorksheetParts.FirstOrDefault();
            var sheet = part.Worksheet.GetFirstChild<SheetData>();
            var models = new List<T>();
            foreach (Row row in sheet.Descendants<Row>().Skip(skip))
            {
                var model = Convert(row);
                if (model == null)
                {
                    continue;
                }
                models.Add(model);
            }
            return models;
        }
    }
}
