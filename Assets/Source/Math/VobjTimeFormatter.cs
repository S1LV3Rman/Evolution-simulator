namespace Source
{
    public class VobjTimeFormatter : ITimeFormatter
    {
        private readonly bool[] leapYears =
        {
            false
        };
        private readonly int monthsPerYear = 10;
        private readonly int[] daysPerMonth =
        {
            100,
            100,
            100,
            100,
            100,
            100,
            100,
            100,
            100,
            100,
        };
        private readonly int[] daysPerMonthLeap =
        {
            100,
            100,
            100,
            100,
            100,
            100,
            100,
            100,
            100,
            100,
        };
        private readonly int hoursPerDay = 100;
        private readonly int minPerHour = 100;
        private readonly float secPerMin = 100f;
        private readonly float _realSecPerSec = 1f;

        public bool[] LeapYears => leapYears;
        public int MonthsPerYear => monthsPerYear;
        public int[] DaysPerMonth => daysPerMonth;
        public int[] DaysPerMonthLeap => daysPerMonthLeap;
        public int HoursPerDay => hoursPerDay;
        public int MinPerHour => minPerHour;
        public float SecPerMin => secPerMin;

        public float RealSecPerSec => _realSecPerSec;
    }
}