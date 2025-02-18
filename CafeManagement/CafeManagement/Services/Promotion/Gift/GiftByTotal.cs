using CafeManagement.Interfaces.Promotion.Condition;
using CafeManagement.Interfaces.Promotion.Promotion;
using CafeManagement.Interfaces.Promotion.Strategy;
using CafeManagement.Models;
using CafeManagement.Models.Order;

namespace CafeManagement.Services.Promotion.Gift
{
    public class GiftByTotal : IGiftPromotion, ITotalCondition, IPromotionStrategy
    {
        private decimal _minTotal;
        private Product _productGift;
        private int _giftAmount;
        private Order _order;
        public void ApplyPromotion(Order order)
        {
            _order = order;
        }

        public void GetTotalCondition(decimal total)
        {
            _minTotal = total;
        }

        public void ProductGift(Product product, int amount)
        {
            _productGift = product;
            _giftAmount = amount;
        }
    }
}
