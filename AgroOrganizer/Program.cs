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
using AgroOrganizer.Services.Expense;
using AgroOrganizer.Services.Field;
using AgroOrganizer.Services.FieldSeason;
using AgroOrganizer.Services.Interfaces;
using AgroOrganizer.Services.Mail;
using AgroOrganizer.Services.Mail.Interfaces;
using AgroOrganizer.Services.Sales;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"))
);

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

//Repositories
builder.Services.AddScoped<IActivityRepository, ActivityRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IFieldRepository, FieldRepository>();
builder.Services.AddScoped<IFieldSeasonRepository, FieldSeasonRepository>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IJwtUtils, JwtUtils>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

//Controllers
ActivityController.SetUpActivityRoutes(app, "api/activity");
AuthController.SetUpAuthRoutes(app, "/api/auth");
ContractController.SetUpContractRoutes(app, "api/contract");
DriverController.SetUpDriverRoutes(app, "api/driver");
ExpenseController.SetUpExpenseRoutes(app, "api/expense");
FieldController.SetUpFieldRoutes(app, "api/field");
FieldSeasonController.SetUpFieldSeasonRoutes(app, "api/fieldseason");
SaleController.SetUpSaleRoutes(app, "api/sale");
UserController.SetUpUserRoutes(app, "api/user");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
