namespace CafeManagement.Models.Order
{
    public class OnlineOrder : Order
    {
        public DateTime DeliveryTime { get; set; }
        public decimal ShippingCost { get; set; }
    }
}
