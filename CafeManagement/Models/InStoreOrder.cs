namespace CafeManagement.Models
{
    public class InStoreOrder:Order
    {
        public enum Status
        {
            New = 0,
            Completed = 2,
            Cancelled = 3
        }

        public Status OderStatus { get; set; }
    }
}
