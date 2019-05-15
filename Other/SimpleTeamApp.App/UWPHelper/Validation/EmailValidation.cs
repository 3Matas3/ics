using System.Text.RegularExpressions;

namespace SimpleTeamApp.App.UWPHelper.Validation
{
    public static class EmailValidation
    {
        public static bool Check(string email)
        {
            return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, @"^[^@\.]+(\.[^@\.]+)*@[^@\.]+(\.[^@\.]+)+$");
        }
    }
}
