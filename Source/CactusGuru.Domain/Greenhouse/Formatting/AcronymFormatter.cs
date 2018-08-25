namespace CactusGuru.Domain.Greenhouse.Formatting
{
    internal static class AcronymFormatter
    {
        public static string Format(string fullname, string acronym)
        {
            string formatted;
            if (string.IsNullOrEmpty(acronym))
                formatted = fullname;
            else if (string.IsNullOrEmpty(fullname))
                formatted = acronym;
            else
                formatted = $"{fullname} ({acronym})";
            if (formatted == null)
                return string.Empty;
            return formatted.Trim();
        }
    }
}
