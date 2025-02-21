using CafeManagement.Interfaces.Repositories.PromotionRepo;
using CafeManagement.Interfaces.Services.PromotionService;
using CafeManagement.Models.PromotionModel;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.PromotionService
{
    public class PromotionService : IPromotionService
    {
        private IUnitOfWork _unitOfWork;
        public PromotionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void ApplyPromotionToOrder(Guid orderId, Guid promotionId)
        {
            var order = _unitOfWork.Order.GetById(orderId);
            var promotion = _unitOfWork.Promotion.GetById(promotionId);
            if (order == null || promotion == null)
                throw new Exception("Order or Promotion not found.");
            order.PromotionId = promotionId;
            order.Promotion = promotion;
            _unitOfWork.Order.Update(order);
        }

        public void CreatePromotion(Promotion promotion)
        {
            _unitOfWork.Promotion.Add(promotion);
        }

        public void CreatePromotionSchedule(PromotionSchedule schedule)
        {
            _unitOfWork.PromotionSchedule.Add(schedule);
        }

        public IEnumerable<Promotion> GetActivePromotionByDate(DateOnly startDate, DateOnly endDate)
        {
            var schedules = _unitOfWork.PromotionSchedule.GetAll()
                .Where(p => p.startDate <= startDate && p.endDate <= endDate).ToList();

            return  schedules
                        .Select(ps => ps.Promotion)
                        .Where(p => p.isActive==1) 
                        .Distinct();
        }

        public IEnumerable<Promotion> GetAllPromotions()
        {
            return _unitOfWork.Promotion.GetAll();
        }

        public Promotion? GetPromotionById(Guid promotionId)
        {
            return _unitOfWork.Promotion.GetById(promotionId);
        }

        public void UpdatePromotion(Guid promotionId, Promotion promotionUpdate)
        {
            _unitOfWork.Promotion.Update(promotionUpdate);
        }

        public void UpdatePromotionSchedule(Guid scheduleId, PromotionSchedule scheduleUpdate)
        {
            _unitOfWork.PromotionSchedule.Update(scheduleUpdate);
        }
    }
}
