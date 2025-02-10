namespace CafeManagement.Models
{
    public class DailyRevenue
    {
        public Guid Id { get; set; }
        public decimal TotalRevenue { get; set; }
        public DateOnly Day { get; set; }
    }
}
    