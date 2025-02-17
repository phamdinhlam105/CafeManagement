namespace CafeManagement.Models
{
    public class QuarterReport:ReportBase
    {
        public int Quarter {  get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        
    }
}
