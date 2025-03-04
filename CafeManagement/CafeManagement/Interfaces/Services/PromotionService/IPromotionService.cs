using CafeManagement.Models.PromotionModel;

namespace CafeManagement.Interfaces.Services.PromotionService
{
    public interface IPromotionService
    {
        Task CreatePromotion(Promotion promotion);
        Task UpdatePromotion(Guid promotionId, Promotion promotionUpdate);
        Task<Promotion?> GetPromotionById(Guid promotionId);
        Task<IEnumerable<Promotion>> GetAllPromotions();
        Task<IEnumerable<Promotion>> GetActivePromotionByDate(DateOnly startDate, DateOnly endDate);
        Task ApplyPromotionToOrder(Guid orderId, Guid promotionId);
        Task CreatePromotionSchedule(PromotionSchedule schedule);
        Task UpdatePromotionSchedule(Guid scheduleId, PromotionSchedule scheduleUpdate);

    }
}
