namespace CafeManagement.Models
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid OderId {  get; set; }
        public Guid ProductId { get; set; }
        public int Quantity {  get; set; }
        public string Note {  get; set; }
        public Product Product {  get; set; }
        public Order Order {  get; set; }
    }
}
