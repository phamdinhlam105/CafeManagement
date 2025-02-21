namespace CafeManagement.Models.PromotionModel
{
    public class PromotionSchedule
    {
        public Guid Id { get; set; }
        public Guid PromotionId { get; set; }
        public DateOnly startDate {  get; set; }
        public DateOnly endDate { get; set; }
        public string Note {  get; set; }
        public Promotion Promotion { get; set; }
    }
}
