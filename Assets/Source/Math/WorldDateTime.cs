using System;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

namespace Source
{
    public class WorldDateTime
    {
        public ITimeFormatter timeFormat;
        
        public int year;
        public int month;
        public int day;
        public int hour;
        public int min;
        public float sec;

        public WorldDateTime()
        {
            timeFormat = new GregorianTimeFormatter();
            
            year = 0;
            month = 0;
            day = 0;
            hour = 0;
            min = 0;
            sec = 0f;
        }

        public WorldDateTime(int year, int month, int day, int hour, int min, float sec)
        {
            timeFormat = new GregorianTimeFormatter();
            
            this.year = year;
            this.month = month;
            this.day = day;
            this.hour = hour;
            this.min = min;
            this.sec = sec;
        }

        public WorldDateTime(ITimeFormatter timeFormat)
        {
            this.timeFormat = timeFormat;
            
            year = 0;
            month = 0;
            day = 0;
            hour = 0;
            min = 0;
            sec = 0f;
        }

        public WorldDateTime(ITimeFormatter timeFormat,
            int year, int month, int day, int hour, int min, float sec)
        {
            this.timeFormat = timeFormat;
            
            this.year = year;
            this.month = month;
            this.day = day;
            this.hour = hour;
            this.min = min;
            this.sec = sec;
        }

        public WorldDateTime(WorldDateTime worldDateTime)
        {
            timeFormat = worldDateTime.timeFormat;
            
            year = worldDateTime.year;
            month = worldDateTime.month;
            day = worldDateTime.day;
            hour = worldDateTime.hour;
            min = worldDateTime.min;
            sec = worldDateTime.sec;
        }

        public WorldDateTime AddSecondsAndGetDelta(float seconds)
        {
            var old = new WorldDateTime(this);
            AddSeconds(seconds);
            return CalculateDelta(old, this);
        }

        public static WorldDateTime CalculateDelta(WorldDateTime lowerDate, WorldDateTime higherDate)
        {
            if (higherDate.timeFormat.GetType() != lowerDate.timeFormat.GetType())
                throw new NotImplementedException();

            var timeFormat = higherDate.timeFormat;

            var years = higherDate.year - lowerDate.year;
            
            var months = higherDate.month - lowerDate.month;
            if (months < 0)
            {
                years--;
                months = timeFormat.MonthsPerYear + months;
            }
            
            var days = higherDate.day - lowerDate.day;
            if (days < 0)
            {
                months--;
                if (timeFormat.LeapYears[lowerDate.year % timeFormat.LeapYears.Length])
                    days = timeFormat.DaysPerMonthLeap[lowerDate.month] + days;
                else
                    days = timeFormat.DaysPerMonth[lowerDate.month] + days;
            }
            
            var hours = higherDate.hour - lowerDate.hour;
            if (hours < 0)
            {
                days--;
                hours = timeFormat.HoursPerDay + hours;
            }
            
            var mins = higherDate.min - lowerDate.min;
            if (mins < 0)
            {
                hours--;
                mins = timeFormat.MinPerHour + mins;
            }
            
            var secs = higherDate.sec - lowerDate.sec;
            if (secs < 0)
            {
                mins--;
                secs = timeFormat.SecPerMin + secs;
            }

            return new WorldDateTime(timeFormat, years, months, days, hours, mins, secs);
        }

        public void AddSeconds(float seconds)
        {
            sec += seconds;
            
            min += Mathf.FloorToInt(sec / timeFormat.SecPerMin);
            sec %= timeFormat.SecPerMin;
            
            hour += min / timeFormat.MinPerHour;
            min %= timeFormat.MinPerHour;
            
            day += hour / timeFormat.HoursPerDay;
            hour %= timeFormat.HoursPerDay;

            if (timeFormat.LeapYears[year % timeFormat.LeapYears.Length])
            {
                if (day / timeFormat.DaysPerMonth[month] > 0)
                {
                    month++;
                    day = 0;
                }
            }
            else
            {
                if (day / timeFormat.DaysPerMonthLeap[month] > 0)
                {
                    month++;
                    day = 0;
                }
            }

            year += month / timeFormat.MonthsPerYear;
            month %= timeFormat.MonthsPerYear;
        }

        public string TimeToString()
        {
            return $"{hour:D2}:{min:D2}:{Mathf.FloorToInt(sec):D2}";
        }

        public string DateToString()
        {
            return $"{day + 1:D2}:{month + 1:D2}:{year + 1:D4}";
        }

        public override string ToString()
        {
            return $"{DateToString()} {TimeToString()}";
        }
    }
}