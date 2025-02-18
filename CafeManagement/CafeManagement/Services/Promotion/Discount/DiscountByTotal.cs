using CafeManagement.Interfaces.Promotion.Condition;
using CafeManagement.Interfaces.Promotion.Promotion;
using CafeManagement.Interfaces.Promotion.Strategy;
using CafeManagement.Models.Order;

namespace CafeManagement.Services.Promotion.Discount
{
    public class DiscountByTotal : IPromotionStrategy, ITotalCondition, IDiscountPromotion
    {
        private decimal _minTotal;
        private Order _order;
        private float _discountPercent;
        public void ApplyPromotion(Order order)
        {
            _order = order;
        }

        public decimal DiscountAmount()
        {
            if (_order.Price > _minTotal)
                return _order.Price * (decimal)_discountPercent / 100;
            else
                return 0;
        }

        public void DiscountPercent(float percent)
        {
            _discountPercent = percent;
        }

        public void GetTotalCondition(decimal total)
        {
            _minTotal = total;
        }
    }
}
