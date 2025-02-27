using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.PromotionRepo;
using CafeManagement.Models.PromotionModel;

namespace CafeManagement.Repositories.PromotionRepo
{
    public class PromotionScheduleRepository:BaseRepository<PromotionSchedule>,IPromotionSchedule
    {
        public PromotionScheduleRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
