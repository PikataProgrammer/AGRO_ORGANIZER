using AgroOrganizer.Context;
using AgroOrganizer.Models.PasswordHasher;
using AgroOrganizer.Models.PasswordHasher.Interface;
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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();
const string baseRoute = "/api";
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
