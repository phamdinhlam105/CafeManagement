using CafeManagement.Dtos.Respone.Stock;
using CafeManagement.Helpers;
using System.Text.Json.Serialization;

namespace CafeManagement.Dtos.Respone
{
    public class StockResponse
    {
        public Guid Id { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly createDate { get; set; }
        public ICollection<StockDetailResponse> Details { get; set; }
    }
}
