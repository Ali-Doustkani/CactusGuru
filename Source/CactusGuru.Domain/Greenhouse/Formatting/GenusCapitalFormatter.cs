using System;

namespace CactusGuru.Domain.Greenhouse.Formatting
{
    /// <summary>
    /// ASTROPHYTUM
    /// </summary>
    public class GenusCapitalFormatter : IFormatter<Genus>
    {
        /// <exception cref="NullReferenceException"/> 
        public string Format(Genus Genera)
        {
            if (string.IsNullOrEmpty(Genera?.Title))
                return string.Empty;
            return Genera.Title.ToUpper();
        }
    }
}
