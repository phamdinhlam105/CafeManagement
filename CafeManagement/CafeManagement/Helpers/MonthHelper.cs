namespace CafeManagement.Helpers
{
    public static class MonthHelper
    {
        public static (DateOnly Start, DateOnly End) GetMonthHelper(int month, int year)
        {
            DateOnly start = new DateOnly(year, month, 1);
            DateOnly end = new DateOnly(year, month, DateTime.DaysInMonth(year, month));
            return (start, end);
        }

    }
}
