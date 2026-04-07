using AgroOrganizer.Models.Dtos.ExpenseDto;
using AgroOrganizer.Models.Dtos.Reports;
using AgroOrganizer.Services.Excel;
using AgroOrganizer.Services.Excel.Interface;
using AgroOrganizer.Services.Interfaces;
using MathNet.Numerics;
using Serilog;

namespace AgroOrganizer.Services.Reports;

public class ReportExpensesService
{
    private readonly IExpenseService _expenseService;
    private readonly IExcelService _excelService;

    public ReportExpensesService(IExpenseService expenseService, IExcelService excelService)
    {
        _expenseService = expenseService;
        _excelService = excelService;
    }

    public async Task<MemoryStream?> GenerateExpenseExcel()
    {
        var expenses = await _expenseService.GetAllExcelAsync();
        

        if (expenses == null || !expenses.Any())
        {
            Log.Warning("No expenses found in database.");  
            return null;
        }
        decimal totalExpenses = expenses.Sum(e => e.Amount);

        foreach (var expense in expenses)
        {
            if (expense.Amount < 0)
            {
                Log.Information("Expense has no amount: {Amount}", expense.Amount);
            }

            if (expense.Type == null)
            {
                Log.Information("Expense has no type: {Type}", expense.Type);   
            }
        }
        
        Log.Information("Total expenses fetched: {Count}", expenses.Count);

        var reportData = FlattenExpenses(expenses);
        
        if (reportData == null || !reportData.Any())
        {
            Log.Warning("After flattening, no report data was generated.");
            return null;
        }

        var excelOptions = GetExcelOptions(totalExpenses);
        return _excelService.GenerateExcel(reportData, excelOptions);
    }

    private ExcelOptions GetExcelOptions(decimal totalExpenses)
    {
        var currentYear = DateTime.Now.Year;

        return new ExcelOptions
        {
            ExcelTitle = new ExcelTitle { Content = $"Справка разходи - {currentYear}" },
            ExcelFooter = new ExcelFooter { Content = $"ОБЩО РАЗХОДИ: {totalExpenses:N2} евро." + "\nГенерирана от Агро Дерменджиеви" },
            Columns =
            {
                { "Type", new ExcelColumn { Label = "Тип разход" } },
                { "Amount", new ExcelColumn { Label = "Стойност на разхода" } },
            }
        };
    }

    private List<ExpenseReportDto> FlattenExpenses(List<ExpenseDto> expenses)
    {
        var result = new List<ExpenseReportDto>();

        
        foreach (var expense in expenses)
        {
            result.Add(new ExpenseReportDto
            {
                Amount = expense.Amount,
                Type = expense.Type,
            });
        }
        
        return result;
    }
}