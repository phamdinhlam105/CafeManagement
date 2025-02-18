namespace CafeManagement.Models.Stock
{
    public class DailyStock
    {
        public Guid Id { get; set; }
        public DateOnly createDate { get; set; }
        public ICollection<DailyStockDetail> DailyStockDetails { get; set; }

        public DailyStock()
        {
            DailyStockDetails = new List<DailyStockDetail>();
        }
    }
}
