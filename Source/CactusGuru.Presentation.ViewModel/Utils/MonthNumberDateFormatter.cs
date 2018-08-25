using CactusGuru.Domain.Greenhouse.Formatting;
using System;
using System.Globalization;

namespace CactusGuru.Presentation.ViewModel.Utils
{
    public class MonthNumberDateFormatter : IFormatter<DateTime>
    {
        public MonthNumberDateFormatter()
        {
            _persianCalendar = new PersianCalendar();
        }

        private readonly PersianCalendar _persianCalendar;

        public string Format(DateTime date)
        {
            var month = _persianCalendar.GetMonth(date);
            var year = _persianCalendar.GetYear(date);
            return $"{year}-{month}";
        }
    }
}
