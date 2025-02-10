using CafeManagement.Interfaces.Promotion.Strategy;
using System.Security.Cryptography;

namespace CafeManagement.Interfaces.Promotion.Promotion
{
    public interface IDiscountPromotion
    {
        void DiscountPercent(float percent);
        decimal DiscountAmount();
    }
}
