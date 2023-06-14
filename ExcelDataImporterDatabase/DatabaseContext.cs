using ExcelDataImporterDatabase.Models;
using Microsoft.EntityFrameworkCore;

namespace ExcelDataImporterDatabase;

public class DatabaseContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DatabaseContext(DbContextOptions options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseCamelCaseNamingConvention();
    }
}