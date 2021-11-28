namespace Source
{
    public class GregorianTimeFormatter : ITimeFormatter
    {
        private readonly bool[] leapYears =
        {
            true,
            false,
            false,
            false
        };
        private readonly int monthsPerYear = 12;
        private readonly int[] daysPerMonth =
        {
            31,
            28,
            31,
            30,
            31,
            30,
            31,
            31,
            30,
            31,
            30,
            31
        };
        private readonly int[] daysPerMonthLeap =
        {
            31,
            29,
            31,
            30,
            31,
            30,
            31,
            31,
            30,
            31,
            30,
            31
        };
        private readonly int hoursPerDay = 24;
        private readonly int minPerHour = 60;
        private readonly float secPerMin = 60f;

        public bool[] LeapYears => leapYears;
        public int MonthsPerYear => monthsPerYear;
        public int[] DaysPerMonth => daysPerMonth;
        public int[] DaysPerMonthLeap => daysPerMonthLeap;
        public int HoursPerDay => hoursPerDay;
        public int MinPerHour => minPerHour;
        public float SecPerMin => secPerMin;
    }
}