namespace CafeManagement.Helpers
{
    public static class Ultilities
    {
        public static DateOnly GetToday()
        {
            return DateOnly.FromDateTime(DateTime.UtcNow);
        }

        public static DateOnly GetYesterday()
        {
            return DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1));
        }
    }
}
