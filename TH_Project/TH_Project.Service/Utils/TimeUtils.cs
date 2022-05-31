using System;
using System.Globalization;

namespace Stump.Api.Data.Utils
{
    /// <summary>
    /// Các hàm liên quan đến DateTime hoặc Timespan
    /// </summary>
    public static class TimeUtils
    {
        private const string fullDateFormat = "dd/MM/yyyy HH:mm:ss";
        private const string shortDateFormat = "dd/MM/yyyy";

        private const string fullDateFormatRemoveSec = "dd/MM/yyyy HH:mm";
        private const string shortTimeFormat = @"hh\:mm";
        private const string americanDateFormat = "yyyy-MM-dd";

        /// <summary>
        /// Đổi DateTime sang chuỗi dd/MM/yyyy HH:mm:ss
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToFullDateString(this DateTime date)
        {
            return date.ToString(fullDateFormat);
        }
        /// <summary>
        /// Đổi DateTime sang chuỗi yyyy-mm-dd
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToAmericanDateString(this DateTime date)
        {
            return date.ToString(americanDateFormat);
        }
        /// <summary>
        /// Đổi ngày sang chuỗi dd/MM/yyyy
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string ToShortDateString(this DateTime date)
        {
            return date.ToString(shortDateFormat);
        }

        /// <summary>
        /// Đổi TimeSpan sang chuỗi hh\:mm
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToShortTimeString(this TimeSpan time)
        {
            return time.ToString(shortTimeFormat);
        }

        /// <summary>
        /// Đổi chuỗi dd/MM/yyyy sang DateTime
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static DateTime FromShortDateString(string src)
        {
            return DateTime.ParseExact(src, shortDateFormat, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Đổi chuỗi dd/MM/yyyy HH:mm:ss sang DateTime
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static DateTime FromFullDateString(string src)
        {
            return DateTime.ParseExact(src, fullDateFormat, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Đổi chuỗi hh\:mm sang Timespan
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static TimeSpan FromShortTimeString(string src)
        {
            return TimeSpan.ParseExact(src, shortTimeFormat, CultureInfo.CurrentCulture);
        }

        public static long ToMiliSeconds(this DateTime date)
        {
            var datestring = date.ToString(fullDateFormat);

            string[] subs = datestring.Split(' ');

            string[] subDate = subs[0].Split('/');
            var year = Int32.Parse(subDate[2]);
            var month = Int32.Parse(subDate[1]);
            var day = Int32.Parse(subDate[0]);

            string[] subTime = subs[1].Split(':');
            var hour = Int32.Parse(subTime[0]);
            var min = Int32.Parse(subTime[1]);
            var sec = Int32.Parse(subTime[2]);

            var dateconvert = new DateTime(year, month, day, hour, min, sec);
            var totalmilis = (long)(dateconvert - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            return totalmilis;
        }
        public static string ToFullDateStringRemoveSec(this DateTime date)
        {
            return date.ToString(fullDateFormatRemoveSec);
        }
        public static DateTime FormatDateTime(this string src)
        {
            string[] subs = src.Split(' ');

            string[] subDate = subs[0].Split('/');
            var year = Int32.Parse(subDate[2]);
            var month = Int32.Parse(subDate[1]);
            var day = Int32.Parse(subDate[0]);

            string[] subTime = subs[1].Split(':');
            var hour = Int32.Parse(subTime[0]);
            var min = Int32.Parse(subTime[1]);
            // var sec = Int32.Parse(subTime[2]);

            return new DateTime(year, month, day, hour, min, 0);
        }
        public static DateTime ToDateTime(this long milis)
        {
            long ticks = long.Parse(milis.ToString());
            TimeSpan time = TimeSpan.FromMilliseconds(ticks);
            DateTime startdate = new DateTime(1970, 1, 1, 0, 0, 0) + time;

            return startdate;
        }
        public static string ToId(this DateTime date)
        {
            var datestring = date.ToString(fullDateFormat);

            string[] subs = datestring.Split(' ');

            string[] subDate = subs[0].Split('/');
            var year = Int32.Parse(subDate[2]);
            var month = Int32.Parse(subDate[1]);
            var day = Int32.Parse(subDate[0]);

            string[] subTime = subs[1].Split(':');
            var hour = Int32.Parse(subTime[0]);
            var min = Int32.Parse(subTime[1]);
            var sec = Int32.Parse(subTime[2]);


            return day.ToString() + month.ToString() + year.ToString() + hour.ToString() + min.ToString() + sec.ToString();
        }
    }
}
