using System.ComponentModel.DataAnnotations;

namespace ExcelDataImporterDatabase.Models;

public class User
{
    [Key]
    public int? Id { get; set; }
    public string? Fullname { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Email { get; set; }
    public bool? Active { get; set; } = true;
}