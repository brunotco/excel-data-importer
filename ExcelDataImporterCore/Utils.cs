using System.Text.RegularExpressions;
using System.Text;
using ExcelDataImporterDatabase.Models;

namespace ExcelDataImporterCore;

public class Utils
{
    // A regex that matches any space.
    private static readonly Regex WhiteSpace = new Regex(@"\s+");
    // A regex that matches any diacritic.
    private static readonly Regex Diacritics = new Regex(@"\p{M}");
    // Takes a string and returns a copy of that string without any spaces. 
    public static string RemoveSpaces(string text)
    {
        return WhiteSpace.Replace(text, "");
    }
    // Takes a string and returns a copy of that string without any diacritics. 
    public static string RemoveDiacritics(string text)
    {
        return Diacritics.Replace(text, string.Empty);
    }
    // Takes a string and returns a copy of that string after running all validations.
    public static string ValidateString(string text)
    {
        if (text == null)
            return text;
        string result = text.Normalize(NormalizationForm.FormD);
        result = RemoveSpaces(result);
        result = RemoveDiacritics(result);
        return result;
    }
    // Takes a User and return a copy of that User after running validate on props.
    public static User ValidateUser(User user)
    {
        User validated = new User
        {
            Fullname = user.Fullname,
            Username = Utils.ValidateString(user.Username),
            Password = Utils.ValidateString(user.Password),
            Email = Utils.ValidateString(user.Email),
        };
        if (user.Id != null) validated.Id = user.Id;
        if (user.Active != null) validated.Active = user.Active;
        return validated;
    }
}