using CafeManagement.Interfaces.Promotion.Strategy;
using CafeManagement.Models;

namespace CafeManagement.Interfaces.Promotion.Promotion
{
    public interface IGiftPromotion
    {
        void ProductGift(Product product, int amount);
    }
}
