namespace CafeManagement.Models
{
    public class GiftPromotion: Promotion
    {
        public Guid ProductId {  get; set; }
        public int quantity {  get; set; }
        public Product Product { get; set; }
    }
}
