using AgroOrganizer.Services.Excel.Interface;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;

namespace AgroOrganizer.Services.Excel
{
    public class ExcelService : IExcelService
    {
        public MemoryStream? GenerateExcel<T>(List<T> data, ExcelOptions excelOptions)
{
    if (data == null || !data.Any()) return null;

    try
    {
        IWorkbook workbook = new XSSFWorkbook();
        ISheet sheet = workbook.CreateSheet("Report");

        // 1. Дефиниране на стилове
        var headerStyle = CreateHeaderStyle(workbook);
        var dateStyle = CreateCellStyle(workbook, HorizontalAlignment.Center, "dd.MM.yyyy");
        var decimalStyle = CreateCellStyle(workbook, HorizontalAlignment.Right, "#,##0.00");
        var intStyle = CreateCellStyle(workbook, HorizontalAlignment.Center, "0");
        var normalStyle = CreateCellStyle(workbook, HorizontalAlignment.Left);

        int rowNum = 0;

        // Title
        if (excelOptions.ExcelTitle != null)
        {
            GenerateTitle(workbook, sheet, excelOptions.ExcelTitle, excelOptions);
            rowNum++;
        }

        // 2. Header
        IRow headerRow = sheet.CreateRow(rowNum++);
        headerRow.HeightInPoints = 25;
        int colIdx = 0;
        foreach (var col in excelOptions.Columns)
        {
            ICell cell = headerRow.CreateCell(colIdx++);
            cell.SetCellValue(col.Value.Label);
            cell.CellStyle = headerStyle;
        }

        // 3. Fill Data
        var properties = typeof(T).GetProperties();
        foreach (var item in data)
        {
            IRow row = sheet.CreateRow(rowNum++);
            colIdx = 0;

            foreach (var colKey in excelOptions.Columns.Keys)
            {
                var prop = Array.Find(properties, p => string.Equals(p.Name, colKey, StringComparison.OrdinalIgnoreCase));
                ICell cell = row.CreateCell(colIdx++);

                if (prop == null || prop.GetValue(item) == null)
                {
                    cell.CellStyle = normalStyle;
                    continue;
                }

                var value = prop.GetValue(item);
                Type type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                if (type == typeof(DateTime) || type == typeof(DateTimeOffset))
                {
                    cell.SetCellValue(type == typeof(DateTimeOffset) ? ((DateTimeOffset)value).DateTime : (DateTime)value);
                    cell.CellStyle = dateStyle;
                }
                else if (type == typeof(decimal) || type == typeof(double) || type == typeof(float))
                {
                    cell.SetCellValue(Convert.ToDouble(value));
                    cell.CellStyle = decimalStyle;
                }
                else if (type == typeof(int) || type == typeof(long))
                {
                    cell.SetCellValue(Convert.ToDouble(value));
                    cell.CellStyle = intStyle;
                }
                else
                {
                    cell.SetCellValue(value.ToString());
                    cell.CellStyle = normalStyle;
                }
            }
        }

        // 4. Auto-size колони (това маха "###")
        for (int i = 0; i < excelOptions.Columns.Count; i++)
        {
            sheet.AutoSizeColumn(i);
            // Добавяме малко "въздух", защото AutoSize е много впито
            sheet.SetColumnWidth(i, sheet.GetColumnWidth(i) + 1200); 
        }

        // Footer
        if (excelOptions.ExcelFooter != null)
        {
            GenerateFooter(workbook, sheet, excelOptions.ExcelFooter, excelOptions, rowNum);
        }

        // 5. Записване без грешки
        using (var ms = new MemoryStream())
        {
            workbook.Write(ms);
            return new MemoryStream(ms.ToArray()); // Връщаме копие, защото NPOI затваря оригиналния поток
        }
    }
    catch (Exception e)
    {
        Log.Error(e, "Excel generation failed");
        return null;
    }
}

// Помощен метод за красив Хедър
private ICellStyle CreateHeaderStyle(IWorkbook workbook)
{
    var style = workbook.CreateCellStyle();
    var font = workbook.CreateFont();
    font.IsBold = true;
    font.Color = IndexedColors.White.Index;
    font.FontHeightInPoints = 11;
    
    style.SetFont(font);
    style.FillForegroundColor = IndexedColors.RoyalBlue.Index;
    style.FillPattern = FillPattern.SolidForeground;
    style.Alignment = HorizontalAlignment.Center;
    style.VerticalAlignment = VerticalAlignment.Center;
    style.BorderBottom = BorderStyle.Medium;
    return style;
}
        private void GenerateFooter(IWorkbook workbook, ISheet sheet, ExcelFooter footer, ExcelOptions excelOptions, int lastRow)
        {
            IFont font = workbook.CreateFont();
            font.IsBold = true;
            font.FontHeightInPoints = 12;
            font.FontName = "Aptos Narrow";

            ICellStyle style = workbook.CreateCellStyle();
            style.SetFont(font);
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;

            IRow row = sheet.CreateRow(lastRow + footer.Offset);
            row.Height = (short)(footer.RowHeightInPoints * 20);

            ICell cell = row.CreateCell(0);
            cell.SetCellValue(footer.Content);
            cell.CellStyle = style;

            sheet.AddMergedRegion(new CellRangeAddress(lastRow + footer.Offset, lastRow + footer.Offset, 0, excelOptions.Columns.Count - 1));
        }

        private ICellStyle CreateCellStyle(IWorkbook workbook, HorizontalAlignment alignment, string? format = null)
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = alignment;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.WrapText = true;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderBottom = BorderStyle.Thin;
            if (format != null)
                style.DataFormat = workbook.CreateDataFormat().GetFormat(format);
            return style;
        }

        private void GenerateTitle(IWorkbook workbook, ISheet sheet, ExcelTitle excelTitle, ExcelOptions options)
        {
            IFont font = workbook.CreateFont();
            font.IsBold = true;
            font.FontHeightInPoints = 14;
            font.FontName = "Aptos Narrow";

            ICellStyle style = workbook.CreateCellStyle();
            style.SetFont(font);
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;

            IRow row = sheet.CreateRow(0);
            row.Height = (short)(excelTitle.RowHeightInPoints * 20);

            ICell cell = row.CreateCell(0);
            cell.SetCellValue(excelTitle.Content);
            cell.CellStyle = style;

            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, options.Columns.Count - 1));
        }

        private void GenerateHeader(IWorkbook workbook, ISheet sheet, ExcelOptions options, int rowNumber, Dictionary<int, int> maxChars)
        {
            IFont font = workbook.CreateFont();
            font.IsBold = true;
            font.FontHeightInPoints = 12;
            font.FontName = "Aptos Narrow";

            ICellStyle style = workbook.CreateCellStyle();
            style.SetFont(font);
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.WrapText = true;
            style.BorderTop = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;

            IRow row = sheet.CreateRow(rowNumber);
            int counter = 0;

            foreach (var col in options.Columns)
            {
                ICell cell = row.CreateCell(counter);
                cell.SetCellValue(col.Value.Label);
                cell.CellStyle = style;
                maxChars[counter] = col.Value.Label.Length;
                counter++;
            }
        }
    }
}