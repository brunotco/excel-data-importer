using ExcelDataImporterDatabase.Models;
using ExcelDataImporterDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExcelDataImporterTest;

public class Manager
{
    public Manager() { }
    public IConfiguration Configuration { get; }
    public Manager(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public string GetConnection()
    {
        return Configuration.GetConnectionString("DBSettings");
    }
    public static string connectionString = "server=localhost; user=root; password=root; port=3306; database=importer";
    public static async void RecreateDb()
    {
        var contextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
        using var context = new DatabaseContext(contextOptions);
        var database = context.Database;
        // Recreate DB
        database.EnsureDeleted();
        database.EnsureCreated();


    }
    public static async void PutOnDb(User user)
    {
        var contextOptions = new DbContextOptionsBuilder<DatabaseContext>()
            .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            .Options;
        using var context = new DatabaseContext(contextOptions);
        context.User.Add(user);
        await context.SaveChangesAsync();
    }
}