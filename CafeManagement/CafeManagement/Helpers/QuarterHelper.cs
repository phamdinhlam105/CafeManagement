namespace CafeManagement.Helpers
{
    public static class QuarterHelper
    {
        public static (DateTime Start, DateTime End) GetQuarterDates(int year, int quarter)
        {
            return quarter switch
            {
                1 => (new DateTime(year, 1, 1, 0, 0, 0), new DateTime(year, 3, 31, 23, 59, 59)),
                2 => (new DateTime(year, 4, 1, 0, 0, 0), new DateTime(year, 6, 30, 23, 59, 59)),
                3 => (new DateTime(year, 7, 1, 0, 0, 0), new DateTime(year, 9, 30, 23, 59, 59)),
                4 => (new DateTime(year, 10, 1, 0, 0, 0), new DateTime(year, 12, 31, 23, 59, 59)),
                _ => throw new ArgumentException("Quarter must be between 1 and 4")
            };
        }
    }
}
