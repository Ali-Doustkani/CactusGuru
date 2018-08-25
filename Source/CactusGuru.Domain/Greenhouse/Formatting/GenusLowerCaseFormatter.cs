using System;

namespace CactusGuru.Domain.Greenhouse.Formatting.Genera
{
    /// <summary>
    /// astrophytum
    /// </summary>
    internal class GenusLowerCaseFormatter : IFormatter<Genus>
    {
        /// <exception cref="NullReferenceException"/>
        public string Format(Genus Genera)
        {
            if (Genera == null)
                return string.Empty;
            return Genera.Title.ToLower();
        }
    }
}
