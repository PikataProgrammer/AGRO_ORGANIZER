using AgroOrganizer.Context;
using AgroOrganizer.Controllers;
using AgroOrganizer.Models.PasswordHasher;
using AgroOrganizer.Models.PasswordHasher.Interface;
using AgroOrganizer.Repositories;
using AgroOrganizer.Repositories.Interfaces;
using AgroOrganizer.Services;
using AgroOrganizer.Services.Activity;
using AgroOrganizer.Services.Auth;
using AgroOrganizer.Services.Auth.Interfaces;
using AgroOrganizer.Services.Contract;
using AgroOrganizer.Services.Driver;
using AgroOrganizer.Services.Excel;
using AgroOrganizer.Services.Excel.Interface;
using AgroOrganizer.Services.Expense;
using AgroOrganizer.Services.Field;
using AgroOrganizer.Services.FieldSeason;
using AgroOrganizer.Services.Interfaces;
using AgroOrganizer.Services.Mail;
using AgroOrganizer.Services.Mail.Interfaces;
using AgroOrganizer.Services.Reports;
using AgroOrganizer.Services.Sales;
using AgroOrganizer.Services.Vehicles;
using AgroOrganizer.Services.Vehicles.VehicleMaintenanceService;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", Serilog.Events.LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:5174") 
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddMemoryCache();
//Services
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IActivityService, ActivityService>();
builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IDriverService, DriverService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IFieldService, FieldService>();
builder.Services.AddScoped<IFieldSeasonService, FieldSeasonDtoService>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IVehiclesService, VehiclesService>();
builder.Services.AddScoped<IVehicleMaintenanceService, VehicleMaintenanceService>();

builder.Services.AddHostedService<AgroOrganizer.Services.Market.MarketPriceScraperService>();
builder.Services.AddScoped<ReportFieldService>();
builder.Services.AddScoped<ReportExpensesService>();
builder.Services.AddScoped<IExcelService, ExcelService>();


//Repositories
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IFieldRepository, FieldRepository>();
builder.Services.AddScoped<IFieldSeasonRepository, FieldSeasonRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleServiceRepository, VehicleServiceRepository>();

builder.Services.AddScoped<IJwtUtils, JwtUtils>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseCors("FrontendPolicy");
app.UseStaticFiles();
app.UseMiddleware<JwtMiddleware>();


//Controllers
ActivityController.SetUpActivityRoutes(app, "/api/activity");
AuthController.SetUpAuthRoutes(app, "/api/auth");
ContractController.SetUpContractRoutes(app, "/api/contract");
DriverController.SetUpDriverRoutes(app, "/api/driver");
ExpenseController.SetUpExpenseRoutes(app, "/api/expense");
FieldController.SetUpFieldRoutes(app, "/api/field");
FieldSeasonController.SetUpFieldSeasonRoutes(app, "/api/fieldseason");
SaleController.SetUpSaleRoutes(app, "/api/sale");
UserController.SetUpUserRoutes(app, "/api/user");
ReportController.SetUpReportRoutes(app, "/api/reports");
VehicleController.SetUpVehiclesRoutes(app, "/api/vehicles");
VehicleServiceController.SetUpVehicleServiceRoutes(app, "/api/vehicleservices");
MarketController.SetUpMarketRoutes(app, "/api/market");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Console.WriteLine("Now listening on:");
Console.WriteLine("http://localhost:5236");
app.Run();
