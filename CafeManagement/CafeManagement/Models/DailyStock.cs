namespace CafeManagement.Models
{
    public class DailyStock
    {
        public Guid Id { get; set; }
        public DateTime createDate {  get; set; }
        public ICollection<DailyStockDetail> DailyStockDetails { get; set; }

        public DailyStock()
        {
            DailyStockDetails = new List<DailyStockDetail>();
        }
    }
}
