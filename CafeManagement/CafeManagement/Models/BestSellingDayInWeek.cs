namespace CafeManagement.Models
{
    public class BestSellingDayInWeek
    {
        public Guid Id { get; set; } 
        public Guid ReportId { get; set; } 
        public string WeekDay { get; set; }
        public decimal Revenue { get; set; } 
    }
}
