using System;
using System.Globalization;

namespace CactusGuru.Infrastructure.Utils
{
    public static class DateUtil
    {
        private static readonly string[] _months = { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };

        public static string ToPersianDate(DateTime dateTime, string separator = "/")
        {
            var cal = new PersianCalendar();
            var year = cal.GetYear(dateTime);
            var month = cal.GetMonth(dateTime);
            var day = cal.GetDayOfMonth(dateTime);
            return $"{year}{separator}{month}{separator}{day}";
        }

        public static int GetPersianYear(DateTime dateTime)
        {
            var cal = new PersianCalendar();
            return cal.GetYear(dateTime);
        }

        public static string ToLongPersianDate(DateTime dateTime)
        {
            var cal = new PersianCalendar();
            var year = cal.GetYear(dateTime);
            var month = _months[cal.GetMonth(dateTime) - 1];
            var day = cal.GetDayOfMonth(dateTime);
            return string.Format("{0} {1} {2}", day, month, year);
        }

        public static DateTime FromPersianDate(string date)
        {
            var cal = new PersianCalendar();
            var parts = date.Split(new[] { '/' });
            var year = Convert.ToInt32(parts[0]);
            var month = Convert.ToInt32(parts[1]);
            var day = Convert.ToInt32(parts[2]);
            return cal.ToDateTime(year, month, day, 0, 0, 0, 0);
        }

        public const string NOT_VALID_PERSIAN_DATE = "تاریخ به درستی وارد نشده است.";

        public static bool IsValid(string date)
        {
            try
            {
                FromPersianDate(date);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
