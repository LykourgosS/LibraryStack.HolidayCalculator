namespace LibraryStack.HolidayCalculator
{
    public class GreekPublicHolidays : IPublicHolidays
    {
        private GreekPublicHolidays() { }

        private static class GreekPublicHolidaysHolder
        {
            public static readonly GreekPublicHolidays greekPublicHolidays = new();
        }

        public static GreekPublicHolidays GetInstance()
        {
            return GreekPublicHolidaysHolder.greekPublicHolidays;
        }

        public DateTime GetOrthodoxEaster(int year)
        {
            var a = year % 19;
            var b = year % 7;
            var c = year % 4;

            var d = (19 * a + 16) % 30;
            var e = (2 * c + 4 * b + 6 * d) % 7;
            var f = (19 * a + 16) % 30;

            var key = f + e + 3;
            var month = (key > 30) ? 5 : 4;
            var day = (key > 30) ? key - 30 : key;

            return new DateTime(year, month, day);
        }

        public List<DateTime> GetHolidays(int year)
        {
            var easter = GetOrthodoxEaster(year);
            var holidays = new List<DateTime>
            {
                new DateTime(year, 1, 1),   //prwtoxronia
                new DateTime(year, 1, 6),   //Theofaneia
                easter.AddDays(-48),        //kathara deytera
                new DateTime(year, 3, 25),  //25i martioy
                new DateTime(year, 5, 1),   //prwtomagia
                easter.AddDays(-2),         //Megali paraskevi
                easter.AddDays(-1),         //Megalo savvato
                easter,                     //Pasxa
                easter.AddDays(1),          //deytera tou Pasxa
                easter.AddDays(50),         //agiou pnevmatos
                new DateTime(year, 8, 15),  //koimisi tis Theotokou
                new DateTime(year, 10, 28), //28i oktovriou
                new DateTime(year, 12, 25), //xristougenna
                new DateTime(year, 12, 26)  //synaksi tis Theotokou
            };

            return holidays;
        }

        public List<DateTime> GetHolidays(DateTime from, DateTime to)
        {
            var holidays = new List<DateTime>();

            var yearFrom = from.Year;
            var yearTo = to.Year;
            while(yearFrom <= yearTo)
            {
                holidays.AddRange(GetHolidays(yearTo));
                yearFrom++;
            }

            return holidays.Where(x => x >= from && x <= to).ToList();
        }

        public bool IsHoliday(DateTime date)
        {
            return GetHolidays(date.Year).Contains(date);
        }

        public bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public bool IsWorkDay(DateTime date)
        {
            return !(IsHoliday(date) || IsWeekend(date));
        }
    }
}