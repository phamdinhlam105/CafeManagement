using CafeManagement.Models.PromotionModel;

namespace CafeManagement.Interfaces.Services.PromotionService
{
    public interface IPromotionService
    {
        void CreatePromotion(Promotion promotion);
        void UpdatePromotion(Guid promotionId, Promotion promotionUpdate);
        Promotion? GetPromotionById(Guid promotionId);
        IEnumerable<Promotion> GetAllPromotions();
        IEnumerable<Promotion> GetActivePromotionByDate(DateOnly startDate, DateOnly endDate);
        void ApplyPromotionToOrder(Guid orderId, Guid promotionId);
        void CreatePromotionSchedule(PromotionSchedule schedule);
        void UpdatePromotionSchedule(Guid scheduleId, PromotionSchedule scheduleUpdate);

    }
}
