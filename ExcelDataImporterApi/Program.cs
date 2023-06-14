using ExcelDataImporterDatabase;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//! Only required if client not using proxy
// Enable CORS
//builder.Services.AddCors();

// Database connection string
var connectionString = builder.Configuration.GetConnectionString("DBSettings");
// Add Database contexts
builder.Services
    .AddDbContext<DatabaseContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)))
    .AddControllersWithViews();

// Magic to use this as a Windows Service
builder.Host.UseWindowsService();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
//! Only for forcing recreation
//dbContext.Database.EnsureDeleted();
dbContext.Database.EnsureCreated();

//! Only required if client not using proxy
// Enable CORS
//app.UseCors(builder => builder.WithOrigins("http://localhost:4200"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.Urls.Add("http://*:5000");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
