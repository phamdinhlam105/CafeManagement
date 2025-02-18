using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Promotion.Strategy
{
    public interface IPromotionStrategy
    {
        void ApplyPromotion(Order order);
    }
}
