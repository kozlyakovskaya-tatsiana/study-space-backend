using System.Text.RegularExpressions;

namespace Domain.Guards;

public static class UserGuards
{
    public static void EnsureEmailIsValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, RegularExpressionsForValidation.EmailRegex))
            throw new ArgumentException("Email is invalid", nameof(email));
    }
}


