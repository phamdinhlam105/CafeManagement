using CafeManagement.Interfaces.Promotion.Condition;
using CafeManagement.Interfaces.Promotion.Promotion;
using CafeManagement.Interfaces.Promotion.Strategy;
using CafeManagement.Models.Order;

namespace CafeManagement.Services.Promotion.Discount
{
    public class DiscountByAmount : IDiscountPromotion, IProductAmountCondition,IPromotionStrategy
    {
        private int _minAmount;
        private float _discountPercent;
        private Order _order;

        public void ApplyPromotion(Order order)
        {
            _order = order;
        }

        public decimal DiscountAmount()
        {
            if (_order.Quantity >= _minAmount)
                return _order.Price * (decimal)_discountPercent / 100;
            else
                return 0;
        }

        public void DiscountPercent(float percent)
        {
            _discountPercent = percent;
        }

        public void GetProductAmountCondition(int amount)
        {
            _minAmount = amount;
        }
    }
}
