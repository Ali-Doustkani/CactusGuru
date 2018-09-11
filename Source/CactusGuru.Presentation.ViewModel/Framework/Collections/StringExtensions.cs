using System;

namespace CactusGuru.Presentation.ViewModel.Framework.Collections
{
    public static class StringExtensions
    {
        public static bool Has(this string source, string str)
        {
            return source.IndexOf(str, StringComparison.OrdinalIgnoreCase) != -1;
        }
    }
}
