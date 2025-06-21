namespace CafeManagement.Dtos.Response.OrderRes
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public int NumberOfOrders { get; set; }
    }
}
