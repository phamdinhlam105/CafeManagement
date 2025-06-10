using CafeManagement.Dtos.Respone.StockRes;
using CafeManagement.Helpers;
using System.Text.Json.Serialization;

namespace CafeManagement.Dtos.Respone.StockRes
{
    public class StockResponse
    {
        public Guid Id { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly createDate { get; set; }
        public ICollection<StockDetailResponse> Details { get; set; }
    }
}
