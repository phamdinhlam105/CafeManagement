using CafeManagement.Models.PromotionModel;

namespace CafeManagement.Interfaces.Repositories.PromotionRepo
{
    public interface IPromotionSchedule:IRepository<PromotionSchedule>
    {
        Task<IEnumerable<PromotionSchedule>> GetByPromotionId(Guid id);
    }
}
