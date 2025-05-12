using CafeManagement.Interfaces.Repositories.PromotionRepo;
using CafeManagement.Interfaces.Services.PromotionService;
using CafeManagement.Models.PromotionModel;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.PromotionService
{
    public class PromotionService : IPromotionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PromotionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        

        public async Task<Promotion> CreatePromotion(Promotion promotion)
        {
            if (promotion.Id == Guid.Empty)
            {
                promotion.Id = Guid.NewGuid();
            }
            await _unitOfWork.Promotion.Add(promotion);
            return promotion;
        }

        public async Task<PromotionSchedule> CreatePromotionSchedule(PromotionSchedule schedule)
        {
            if (schedule.Id == Guid.Empty)
            {
                schedule.Id = Guid.NewGuid();
            }
            await _unitOfWork.PromotionSchedule.Add(schedule);
            return schedule;
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
            var existPromotion = await _unitOfWork.Promotion.GetById(promotionId);
            if (existPromotion == null)
                throw new Exception("id Promotion not found");
            existPromotion.Name=promotionUpdate.Name;
            existPromotion.Description = promotionUpdate.Description;
            existPromotion.Discount = promotionUpdate.Discount;
            await _unitOfWork.Promotion.Update(existPromotion);
        }

        public async Task UpdatePromotionSchedule(Guid scheduleId, PromotionSchedule scheduleUpdate)
        {
            await _unitOfWork.PromotionSchedule.Update(scheduleUpdate);
        }

        public async Task<IEnumerable<PromotionSchedule>> GetScheduleByPromotionId(Guid promotionId)
        {
            return await _unitOfWork.PromotionSchedule.GetByPromotionId(promotionId);
        }
    }
}
