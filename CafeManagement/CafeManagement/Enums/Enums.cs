namespace CafeManagement.Enums
{
    public enum UserRole
    {
        Admin = 1,
        Manager = 2,
        Employee = 3,
        Customer = 4,
    }
    public enum OrderStatus
    {
        New,
        Completed,
        Cancelled
    }

    public enum AssignmentStatus
    {
        Scheduled,
        CheckedIn,
        Absent,
        Late,
        LeftEarly,
        Completed,
        Cancelled
    }
}
