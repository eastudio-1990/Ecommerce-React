using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce_API.Control
{
    public class Date
    {
        public static string GeorgianToPersian()
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            int year = p.GetYear(DateTime.Now);
            int month = p.GetMonth(DateTime.Now);
            int day = p.GetDayOfMonth(DateTime.Now);
            return (year.ToString() + "/" + (month > 9 ? month.ToString() : "0" + month.ToString()) + "/" + (day > 9 ? day.ToString() : "0" + day.ToString()));
        }

        public static string GeorgianToPersian(DateTime date)
        {
            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            int year = p.GetYear(date);
            int month = p.GetMonth(date);
            int day = p.GetDayOfMonth(date);
            return (year.ToString() + "/" + (month > 9 ? month.ToString() : "0" + month.ToString()) + "/" + (day > 9 ? day.ToString() : "0" + day.ToString()));
        }

        public static DateTime PersianToGeorgian(string persianDate)
        {

            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            return p.ToDateTime(
                Convert.ToInt32(persianDate.Substring(0, 4)),
                Convert.ToInt32(persianDate.Substring(5, 2)),
                Convert.ToInt32(persianDate.Substring(8, 2)), 0, 0, 0, 0);

        }

        public static string GetDay(string Date)
        {
            DayOfWeek week = PersianToGeorgian(Date).DayOfWeek;
            if (week == DayOfWeek.Friday)
                return "جمعه";
            else if (week == DayOfWeek.Saturday)
                return "شنبه";
            else if (week == DayOfWeek.Sunday)
                return "یکشنبه";
            else if (week == DayOfWeek.Monday)
                return "دوشنبه";
            else if (week == DayOfWeek.Tuesday)
                return "سه شنبه";
            else if (week == DayOfWeek.Wednesday)
                return "چهارشنبه";
            
            else
                return "پنجشنبه";


        }
        public static string GetMonth(string Date)
        {
            string month = Date.Substring(5,2);
            if (month == "01")
                return "فروردین";
            else if(month == "02")
                return "اردیبهشت";
            else if (month == "03")
                return "خرداد";
            else if (month == "04")
                return "تیر";
            else if (month == "05")
                return "مرداد";
            else if (month == "06")
                return "شهریور";
            else if (month == "07")
                return "مهر";
            else if (month == "08")
                return "آبان";
            else if (month == "09")
                return "آذر";
            else if (month == "10")
                return "دی";
            else if (month == "11")
                return "بهمن";
            else
                return "اسفند";


        }

        public static string CorrectTime(string _TimeVal)
        {
            if (string.IsNullOrEmpty(_TimeVal))
                return "00:00";

            string[] _timeArr = _TimeVal.Split(':');
            int h = 0;
            int m = 0;
            try
            {
                h = int.Parse(_timeArr[1]);
                m = int.Parse(_timeArr[0]);

                string hStr = h.ToString();
                string mStr = m.ToString();

                if (hStr.Length == 1)
                    hStr = "0" + hStr;

                if (mStr.Length == 1)
                    mStr = "0" + mStr;

                return hStr + ":" + mStr;


            }
            catch { return "00:00"; }

        }

        public static string CurrentTime()
        {
            string Hour = "0";
            string Minute = "0";
            string Seconds = "0";
            DateTime Now = System.DateTime.Now;
            if (Now.Hour < 10)
                Hour = "0" + Now.Hour.ToString();
            else
                Hour = Now.Hour.ToString();

            if (Now.Minute < 10)
                Minute = "0" + Now.Minute.ToString();
            else
                Minute = Now.Minute.ToString();

            if (Now.Second < 10)
                Seconds = "0" + Now.Second.ToString();
            else
                Seconds = Now.Second.ToString();

            return Hour + ":" + Minute + ":" + Seconds;

        }

        public static DateTime CreateDate(string persianDate, int hour, int min)
        {
            DateTime _tmpDate = PersianToGeorgian(persianDate);
            _tmpDate = new DateTime(_tmpDate.Year, _tmpDate.Month, _tmpDate.Day, hour, min, 0);
            return _tmpDate;
        }

        public void test()
        {

        }

        public static IList<string> GetFridaysForYearFromPoint(DateTime startDate)
        {

            DateTime currentFriday = startDate;
            string Year = Control.Date.GeorgianToPersian(startDate).Substring(0, 4);
            List<string> results = new List<string>();

            //Find the nearest Friday forward of the start date
            while (currentFriday.DayOfWeek != DayOfWeek.Friday)
            {

                currentFriday = currentFriday.AddDays(1);
            }

            //Find all the fridays!
            string Friday = Control.Date.GeorgianToPersian(currentFriday).Substring(0, 4);

            while (Friday == Year)
            {

                results.Add(GeorgianToPersian(currentFriday));
                currentFriday = currentFriday.AddDays(7);
                Friday = Control.Date.GeorgianToPersian(currentFriday).Substring(0, 4);

            }

            return results;
        }
        public static IList<string> GetFridaysForMonthFromPoint(DateTime startDate, int Day)
        {

            DateTime currentFriday = startDate;
            string Year = Control.Date.GeorgianToPersian(startDate).Substring(0, 4);
            int numberOfDays = 0;
            List<string> results = new List<string>();

            //Find the nearest Friday forward of the start date
            while (currentFriday.DayOfWeek != DayOfWeek.Friday)
            {
                currentFriday = currentFriday.AddDays(1);
                numberOfDays += 1;
            }

            int Index = Convert.ToInt32((Day - numberOfDays) / 7);
            int i;
            //Find all the fridays!
            for (i = 0; i <= Index; i++)
            {

                results.Add(GeorgianToPersian(currentFriday));
                currentFriday = currentFriday.AddDays(7);
            }

            return results;
        }

        public static double CalDuration(string BeginTime, string EndTime)
        {
            string[] beginElement = BeginTime.Split(':');
            string[] endElement = EndTime.Split(':');
            int beginmin = Convert.ToInt32(beginElement[0]) * 60 + Convert.ToInt32(beginElement[1]);
            int endmin = Convert.ToInt32(endElement[0]) * 60 + Convert.ToInt32(endElement[1]);
            return endmin - beginmin;

        }

        public static string GetTime(DateTime MyDateTime)
        {
            string Hour = MyDateTime.Hour.ToString();
            string Minute = MyDateTime.Minute.ToString();
            if (MyDateTime.Hour < 10)
                Hour = "0" + MyDateTime.Hour.ToString();
            if (MyDateTime.Minute < 10)
                Minute = "0" + MyDateTime.Minute.ToString();
            return Hour + ":" + Minute;
        }



    }
}
