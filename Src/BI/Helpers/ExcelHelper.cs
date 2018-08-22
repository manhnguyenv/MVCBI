using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace BI.Helpers
{
  public static class ExcelHelper
  {
    /// <summary>
    ///
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static DataTable GetDataTableFromExcel(string path)
    {
      using (var pck = new ExcelPackage())
      {
        using (var stream = File.OpenRead(path))
        {
          pck.Load(stream);
        }
        var ws = pck.Workbook.Worksheets.First();
        DataTable tbl = new DataTable();
        bool hasHeader = true; // adjust it accordingly( i've mentioned that this is a simple approach)
        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        {
          tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
        }
        var startRow = hasHeader ? 2 : 1;
        for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        {
          var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
          var row = tbl.NewRow();
          foreach (var cell in wsRow)
          {
            row[cell.Start.Column - 1] = cell.Text;
          }
          tbl.Rows.Add(row);
        }
        return tbl;
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="path"></param>
    /// <param name="hasHeader"></param>
    /// <returns></returns>
    public static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
    {
      using (var pck = new ExcelPackage())
      {
        using (var stream = File.OpenRead(path))
        {
          pck.Load(stream);
        }
        var ws = pck.Workbook.Worksheets.First();
        DataTable tbl = new DataTable();
        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
        {
          tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
        }
        var startRow = hasHeader ? 2 : 1;
        for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        {
          var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
          DataRow row = tbl.Rows.Add();
          foreach (var cell in wsRow)
          {
            row[cell.Start.Column - 1] = cell.Text;
          }
        }
        return tbl;
      }
    }

    /// <summary>
    /// Gets the excel header and creates a dictionary object based on column name in order to get the index.
    /// Assumes that each column name is unique.
    /// </summary>
    /// <param name="workSheet"></param>
    /// <returns></returns>
    public static Dictionary<string, int> GetExcelHeader(ExcelWorksheet workSheet, int rowIndex)
    {
      Dictionary<string, int> header = new Dictionary<string, int>();

      if (workSheet != null)
      {
        for (int columnIndex = workSheet.Dimension.Start.Column; columnIndex <= workSheet.Dimension.End.Column; columnIndex++)
        {
          if (workSheet.Cells[rowIndex, columnIndex].Value != null)
          {
            string columnName = workSheet.Cells[rowIndex, columnIndex].Value.ToString();

            if (!header.ContainsKey(columnName) && !string.IsNullOrEmpty(columnName))
            {
              header.Add(columnName, columnIndex);
            }
          }
        }
      }

      return header;
    }

    /// <summary>
    /// Parse worksheet values based on the information given.
    /// </summary>
    /// <param name="workSheet"></param>
    /// <param name="rowIndex"></param>
    /// <param name="columnName"></param>
    /// <returns></returns>
    public static string ParseWorksheetValue(ExcelWorksheet workSheet, Dictionary<string, int> header, int rowIndex, string columnName)
    {
      string value = string.Empty;
      int? columnIndex = header.ContainsKey(columnName) ? header[columnName] : (int?)null;

      if (workSheet != null && columnIndex != null && workSheet.Cells[rowIndex, columnIndex.Value].Value != null)
      {
        value = workSheet.Cells[rowIndex, columnIndex.Value].Value.ToString();
      }

      return value;
    }
  }
}
