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
        public async Task ApplyPromotionToOrder(Guid orderId, Guid promotionId)
        {
            var order = await _unitOfWork.Order.GetById(orderId);
            var promotion = await _unitOfWork.Promotion.GetById(promotionId);
            if (order == null || promotion == null)
                throw new Exception("Order or Promotion not found.");
            order.PromotionId = promotionId;
            order.Promotion = promotion;
            await _unitOfWork.Order.Update(order);
        }

        public async Task CreatePromotion(Promotion promotion)
        {
            await _unitOfWork.Promotion.Add(promotion);
        }

        public async Task CreatePromotionSchedule(PromotionSchedule schedule)
        {
            await _unitOfWork.PromotionSchedule.Add(schedule);
        }

        public async Task<IEnumerable<Promotion>> GetActivePromotionByDate(DateOnly startDate, DateOnly endDate)
        {
            var schedules = (await _unitOfWork.PromotionSchedule.GetAll())
                .Where(p => p.startDate <= startDate && p.endDate <= endDate).ToList();

            return  schedules
                        .Select(ps => ps.Promotion)
                        .Where(p => p.isActive==1) 
                        .Distinct();
        }

        public async Task<IEnumerable<Promotion>> GetAllPromotions()
        {
            return await _unitOfWork.Promotion.GetAll();
        }

        public async Task<Promotion?> GetPromotionById(Guid promotionId)
        {
            return await _unitOfWork.Promotion.GetById(promotionId);
        }

        public async Task UpdatePromotion(Guid promotionId, Promotion promotionUpdate)
        {
            await _unitOfWork.Promotion.Update(promotionUpdate);
        }

        public async Task UpdatePromotionSchedule(Guid scheduleId, PromotionSchedule scheduleUpdate)
        {
            await _unitOfWork.PromotionSchedule.Update(scheduleUpdate);
        }
    }
}
