using System.Text.RegularExpressions;

namespace MJ_CAIS.Common.Validators
{
    public static class RegexValidator
    {
        public static bool IsValid(string text, string pattern)
        {
            if (string.IsNullOrEmpty(text)) return true;

            var regex = new Regex(pattern);
            return regex.IsMatch(text);
        }
    }
}
