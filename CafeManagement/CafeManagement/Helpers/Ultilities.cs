namespace CafeManagement.Helpers
{
    public static class Ultilities
    {
        public static DateTime GetToday()
        {
            return DateTime.UtcNow.Date;
        }
        public static DateTime GetYesterday()
        {
            return DateTime.UtcNow.AddDays(-1).Date;
        }
    }
}
