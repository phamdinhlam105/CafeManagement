using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.PromotionRepo;
using CafeManagement.Models.PromotionModel;
using iText.Commons.Bouncycastle.Asn1.Pkcs;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.PromotionRepo
{
    public class PromotionScheduleRepository:BaseRepository<PromotionSchedule>,IPromotionSchedule
    {
        public PromotionScheduleRepository(CafeManagementDbContext _context) : base(_context) { }

        public async Task<IEnumerable<PromotionSchedule>> GetByPromotionId(Guid id)
        {
            return await _context.PromotionSchedules
                .Where(ps => ps.PromotionId == id)
                .ToListAsync();
        }
    }
}
