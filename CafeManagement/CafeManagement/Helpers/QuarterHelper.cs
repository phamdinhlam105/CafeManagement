namespace CafeManagement.Helpers
{
    public static class QuarterHelper
    {
        public static (DateOnly Start, DateOnly End) GetQuarterDates(int year, int quarter)
        {
            return quarter switch
            {
                1 => (new DateOnly(year, 1, 1), new DateOnly(year, 3, 31)),
                2 => (new DateOnly(year, 4, 1), new DateOnly(year, 6, 30)),
                3 => (new DateOnly(year, 7, 1), new DateOnly(year, 9, 30)),
                4 => (new DateOnly(year, 10, 1), new DateOnly(year, 12, 31)),
                _ => throw new ArgumentException("Quarter must be between 1 and 4")
            };
        }
    }
}
