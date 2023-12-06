namespace LibraryStack.HolidayCalculator
{
    internal interface IPublicHolidays
    {
        public List<DateTime> GetHolidays(int year);
        public List<DateTime> GetHolidays(DateTime from, DateTime to);
        public bool IsHoliday(DateTime date);
        public bool IsWeekend(DateTime date);
        public bool IsWorkDay(DateTime date);
    }
}
