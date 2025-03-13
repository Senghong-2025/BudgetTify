using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Budgetify.Validators;

public class AppValidator
{
    public bool IsValidEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }

    public bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;
    
        var regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d.*\d.*\d.*\d).+$");
        return regex.IsMatch(password);
    }

    public bool IsValidUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
            return false;
        var regex = new Regex(@"^[a-zA-Z0-9_]{3,20}$");
        return regex.IsMatch(username);
    }
}