using CafeManagement.Models;

namespace CafeManagement.Interfaces.Promotion.Strategy
{
    public interface IPromotionStrategy
    {
        void ApplyPromotion(Order order);
    }
}
