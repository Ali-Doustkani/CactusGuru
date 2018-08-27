using System;
using System.Globalization;

namespace CactusGuru.Presentation.ViewModel.Utils
{
    public class MonthNameDateFormatter  
    {
        public MonthNameDateFormatter()
        {
            _persianCalendar = new PersianCalendar();
        }

        private readonly PersianCalendar _persianCalendar;

        public string Format(DateTime date)
        {
            var month = _persianCalendar.GetMonth(date);
            var year = _persianCalendar.GetYear(date);
            return $"{GetMonthName(month)} {year}";
        }

        private string GetMonthName(int month)
        {
            switch (month)
            {
                case 1: return "فروردین";
                case 2: return "اردیبهشت";
                case 3: return "خرداد";
                case 4: return "تیر";
                case 5: return "مرداد";
                case 6: return "شهریور";
                case 7: return "مهر";
                case 8: return "آبان";
                case 9: return "آذر";
                case 10: return "دی";
                case 11: return "بهمن";
                case 12: return "اسفند";
            }
            throw new InvalidOperationException("Invalid month index.");
        }
    }
}
