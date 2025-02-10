using CafeManagement.Enums;
using CafeManagement.Models;

namespace CafeManagement.Dtos.Respone
{
    public class NewOrderResponse
    {
        public Guid Id {  get; set; }
        public string? Note {  get; set; }
        public decimal? ShippingCost {  get; set; }
        public decimal Total {  get; set; }
        public int Amount {  get; set; }
        public DateTime? DeliveryTime { get; set; }
        public OrderType OrderType { get; set; }
        public OrderStatus Status { get; set; }
    }
}
