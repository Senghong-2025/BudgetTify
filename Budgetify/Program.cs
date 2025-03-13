using System.Data.Common;
using System.Text.Json;
using Budgetify.Data;
using Budgetify.Middleware;
using Budgetify.Repositories;
using Budgetify.Services;
using Budgetify.Validators;
using DbConnection = Budgetify.Data.DbConnection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddLogging();
builder.Logging.AddConsole(); // Logs to the console
builder.Logging.AddDebug(); // Logs to the debug output

// Example of a simple log message at startup
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDbConnection, DbConnection>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<AppValidator>();
builder.Services.AddSingleton<IJwtService, JwtService>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.Run();