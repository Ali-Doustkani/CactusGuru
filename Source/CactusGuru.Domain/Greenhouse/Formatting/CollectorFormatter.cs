using System;
using CactusGuru.Infrastructure.Utils;

namespace CactusGuru.Domain.Greenhouse.Formatting
{
    public class CollectorFormatter : IFormatter<Collector>
    {
        /// <exception cref="ArgumentNullException"/>
        public string Format(Collector collector)
        {
            ArgumentChecker.CheckNull(collector);
            return AcronymFormatter.Format(collector.FullName, collector.Acronym);
        }
    }
}
