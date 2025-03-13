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
        if (string.IsNullOrEmpty(password) || password.Length != 4)
            return false;

        return Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{4}$");
    }

    public bool IsValidUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
            return false;
        var regex = new Regex(@"^[a-zA-Z0-9_]{3,20}$");
        return regex.IsMatch(username);
    }
}