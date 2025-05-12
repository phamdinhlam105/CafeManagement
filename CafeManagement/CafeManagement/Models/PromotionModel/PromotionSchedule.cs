using CafeManagement.Helpers;
using System.Text.Json.Serialization;

namespace CafeManagement.Models.PromotionModel
{
    public class PromotionSchedule
    {
        public Guid Id { get; set; }
        public Guid PromotionId { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly startDate {  get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly endDate { get; set; }
        public string Note {  get; set; }
        [JsonIgnore]
        public Promotion? Promotion { get; set; }
    }
}
