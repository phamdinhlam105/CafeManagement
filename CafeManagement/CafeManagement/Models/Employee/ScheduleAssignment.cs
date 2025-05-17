using CafeManagement.Enums;

namespace CafeManagement.Models.Employee
{
    public class ScheduleAssignment
    {
        public Guid Id {  get; set; }
        public Guid WorkScheduleId { get; set; }
        public Guid UserId { get; set; }
        public string? RoleInShift { get; set; } 
        public bool IsCheckedIn { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        public string? Note { get; set; }
        public User User { get; set; }
        public WorkSchedule WorkSchedule { get; set; }
        public AssignmentStatus Status {  get; set; }
    }
}
