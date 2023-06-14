using ExcelDataImporterDatabase.Models;
using ExcelDataImporterTest;
using OfficeOpenXml;
using System.Reflection.PortableExecutable;
using System.Text.Json;

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
using var package = new ExcelPackage(new FileInfo(@"import.xlsx"));
var sheet = package.Workbook.Worksheets[0];
var list = new List<User>();
int rowCount = sheet.Dimension.Rows;
int colCount = sheet.Dimension.Columns;

List<Header> Headers = new List<Header>();
var columnInfo = Enumerable.Range(1, colCount).ToList()
.Where(n => sheet.Cells[1, n].Value != null)
    .Select(n => new Header { Index = n, ColumnName = sheet.Cells[1, n].Value.ToString() });

int colIndex(string ColumnName)
{
    return columnInfo.ToList().Find(info => info.ColumnName == ColumnName).Index;
}

for (int c = 1; c <= rowCount; c++)
{
    if (sheet.Cells[1, c].Value != null)
        Headers.Add(new Header { Index = c, ColumnName = sheet.Cells[1, c].Value.ToString() });
}

for (int r = 2; r <= rowCount; r++)
{
    var user = new User
    {
        Fullname = sheet.Cells[r, colIndex("Fullname")].GetValue<string>(),
        Username = sheet.Cells[r, colIndex("Username")].GetValue<string>(),
        Password = sheet.Cells[r, colIndex("Password")].GetValue<string>(),
        Email = sheet.Cells[r, colIndex("Email")].GetValue<string>(),
        Active = sheet.Cells[r, colIndex("Active")].GetValue<bool>(),
    };
    list.Add(user);
}

Manager.RecreateDb();
list.ForEach(row =>
{
    Console.WriteLine(row.Username);
    Manager.PutOnDb(row);
});