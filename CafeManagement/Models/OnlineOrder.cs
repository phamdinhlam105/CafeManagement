namespace CafeManagement.Models
{
    public class OnlineOrder:Order
    {
        public enum Status
        {
            New = 0,
            Confirmed = 1,
            Completed = 2,
            Cancelled = 3
        }

        public Status OderStatus { get; set; }
        public Guid CustomerId {  get; set; }
        public Customer Customer { get; set; }
    }
}
