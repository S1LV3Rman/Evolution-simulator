namespace Source
{
    public interface ITimeFormatter
    {
        public bool[] LeapYears { get; }
        public int MonthsPerYear { get; }
        public int[] DaysPerMonth { get; }
        public int[] DaysPerMonthLeap { get; }
        public int HoursPerDay { get; }
        public int MinPerHour { get; }
        public float SecPerMin { get; }
        
        public float RealSecPerSec { get; }
    }
}