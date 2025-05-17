namespace CafeManagement.Models.Employee
{
    public class WorkSchedule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }      
        public TimeSpan ShiftStart { get; set; } 
        public TimeSpan ShiftEnd { get; set; }  
        public string? Note { get; set; }
    }
}
