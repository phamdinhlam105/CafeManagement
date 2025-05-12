using CafeManagement.Helpers;
using System.Text.Json.Serialization;

namespace CafeManagement.Models.Stock
{
    public class DailyStock
    {
        public Guid Id { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly createDate { get; set; }
        public ICollection<DailyStockDetail> DailyStockDetails { get; set; }

        public DailyStock()
        {
            DailyStockDetails = new List<DailyStockDetail>();
        }
    }
}
