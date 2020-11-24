using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Sude.Mvc.UI
{


    public static class DateTimeExtention
    {
        private static readonly PersianCalendar persianCalendar = new PersianCalendar();



        public static string ToLocalizationDateTime(this DateTime value)
        {
            return value.ToLocalizationDateTime("G");
        }

        public static DateTime ToGerogianDateTime(this DateTime value)
        {
            return DateTimeExtention.persianCalendar.ToDateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, value.Millisecond);
        }

        public static string ToLocalizationDateTime(this DateTime value, string format)
        {

            int persianYear = value.ToPersianYear();
            int persianMonth = value.ToPersianMonth();
            int persianDay = value.ToPersianDay();

            switch (format)
            {
                case "D":
                    return string.Format("{0} - {1} {2} {3}", (object)value.ToPersianWeekDayName(), (object)persianDay, (object)value.ToPersianMonthName(), (object)persianYear);
                case "F":
                    return string.Format("{0} - {1} {2} {3}, {4}", (object)value.ToPersianWeekDayName(), (object)persianDay, (object)value.ToPersianMonthName(), (object)persianYear, (object)value.ToString("HH:mm"));
                //return string.Format("{0} - {1} {2} {3}, {4}", (object) value.ToPersianWeekDayName(), (object) persianDay, (object) value.ToPersianMonthName(), (object) persianYear, (object) value.ToString("HH:mm:ss"));
                case "G":
                    return string.Format("{0} - {1} {2} {3}, {4}", (object)value.ToPersianWeekDayName(), (object)persianDay, (object)value.ToPersianMonthName(), (object)persianYear, (object)value.ToString("HH:mm"));
                //return string.Format("{0}-{1}-{2} {3}", (object) persianYear, (object) persianMonth, (object) persianDay, (object) value.ToString("HH:mm:ss"));
                case "MMMM":
                    return string.Format("{0}", (object)value.ToPersianMonthName());
                case "d":
                    return string.Format("{0} - {1} {2} {3}, {4}", (object)value.ToPersianWeekDayName(), (object)persianDay, (object)value.ToPersianMonthName(), (object)persianYear, (object)value.ToString("HH:mm"));
                //return string.Format("{0}-{1}-{2}", (object) persianYear, (object) persianMonth, (object) persianDay);
                case "f":
                    return string.Format("{0} - {1} {2} {3}, {4}", (object)value.ToPersianWeekDayName(), (object)persianDay, (object)value.ToPersianMonthName(), (object)persianYear, (object)value.ToString("HH:mm"));
                case "g":                  
                return string.Format("{0}/{1}/{2} {3}", (object) persianDay, (object) persianMonth, (object) persianYear, (object) value.ToString("HH:mm"));
                case "m":
                    return string.Format("{0} {1}", (object)value.ToPersianMonthName(), (object)persianYear);
                default:
                        return String.Format("{0}/{1}/{2}", persianYear, persianMonth, persianDay); ;
            }
        }

        public static string ToLocalizationDateTime(this string value, string format)
        {
            if (!value.IsTimeString())
                throw new Exception("This is Not Valid Date Time String");
            return Convert.ToDateTime(value).ToLocalizationDateTime(format);
        }

        

        private static bool IsTimeString(this string value)
        {
            try
            {
                Convert.ToDateTime(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int ToPersianYear(this DateTime value)
        {
            return DateTimeExtention.persianCalendar.GetYear(value);
        }

        public static int ToPersianMonth(this DateTime value)
        {
            return DateTimeExtention.persianCalendar.GetMonth(value);
        }

        public static int ToPersianDay(this DateTime value)
        {
            return DateTimeExtention.persianCalendar.GetDayOfMonth(value);
        }

        public static int ToPersianDayOfWeek(this DateTime value)
        {
            return (int)DateTimeExtention.persianCalendar.GetDayOfWeek(value);
        }

        public static string ToPersianMonthName(this DateTime value)
        {
            switch (value.ToPersianMonth())
            {
                case 1:
                    return "فروردین";
                case 2:
                    return "اردیبهشت";
                case 3:
                    return "خرداد";
                case 4:
                    return "تیر";
                case 5:
                    return "مرداد";
                case 6:
                    return "شهریور";
                case 7:
                    return "مهر";
                case 8:
                    return "آبان";
                case 9:
                    return "آذر";
                case 10:
                    return "دی";
                case 11:
                    return "بهمن";
                case 12:
                    return "اسفند";
                default:
                    return string.Empty;
            }
        }

        public static string ToPersianWeekDayName(this DateTime value)
        {
            switch (value.ToPersianDayOfWeek())
            {
                case 0:
                    return "یکشنبه";
                case 1:
                    return "دوشنبه";
                case 2:
                    return "سه شنبه";
                case 3:
                    return "چهارشنبه";
                case 4:
                    return "پنج شنبه";
                case 5:
                    return "جمعه";
                case 6:
                    return "شنبه";
                default:
                    return string.Empty;
            }
        }

       
    }
}

 