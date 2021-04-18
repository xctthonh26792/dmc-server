using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;

namespace Tenjin.Spreadsheets.Interfaces
{
    public interface ISpreadsheetReader
    {
        double GetDouble(Cell cell, double def = 0);

        long GetInt64(Cell cell, long def = 0);

        int GetInt32(Cell cell, int def = 0);

        bool GetBoolean(Cell cell, bool def = false);

        string GetString(Cell cell, string def = "");

        DateTime? GetDate(Cell cell);
    }

    public interface ISpreadsheetReader<T>
        where T : new()
    {
        List<T> Execute(int skip = 1);
    }
}
