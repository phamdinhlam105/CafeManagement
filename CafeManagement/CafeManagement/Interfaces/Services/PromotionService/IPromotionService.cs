using CafeManagement.Models.PromotionModel;

namespace CafeManagement.Interfaces.Services.PromotionService
{
    public interface IPromotionService
    {
        Task<Promotion> CreatePromotion(Promotion promotion);
        Task UpdatePromotion(Guid promotionId, Promotion promotionUpdate);
        Task<Promotion?> GetPromotionById(Guid promotionId);
        Task<IEnumerable<Promotion>> GetAllPromotions();
        Task<IEnumerable<PromotionSchedule>> GetScheduleByPromotionId(Guid promotionId);
        Task<IEnumerable<Promotion>> GetActivePromotionByDate(DateOnly startDate, DateOnly endDate);
        Task<PromotionSchedule> CreatePromotionSchedule(PromotionSchedule schedule);
        Task UpdatePromotionSchedule(Guid scheduleId, PromotionSchedule scheduleUpdate);

    }
}
