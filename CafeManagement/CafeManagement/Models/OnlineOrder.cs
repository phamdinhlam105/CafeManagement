namespace CafeManagement.Models
{
    public class OnlineOrder:Order
    {
        public DateTime DeliveryTime { get; set; }
        public decimal ShippingCost {  get; set; }
    }
}
