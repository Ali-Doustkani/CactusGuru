namespace CactusGuru.Domain.Greenhouse.Formatting
{
    /// <summary>
    /// Astrophytum asterias
    /// </summary>
    public class GenusFirstLetterCapitalFormatter : IFormatter<Genus>
    {
        public string Format(Genus genus)
        {
            if (string.IsNullOrEmpty(genus?.Title))
                return string.Empty;
            return CapitalFirstChar(genus.Title);
        }

        public static string CapitalFirstChar(string name)
        {
            var loweredGenera = name.ToLower();
            var firstChar = char.ToUpper(loweredGenera[0]);
            string restChars = string.Empty;
            if (loweredGenera.Length > 1)
                restChars = loweredGenera.Substring(1, loweredGenera.Length - 1);
            return string.Concat(firstChar.ToString(), restChars);
        }
    }
}
