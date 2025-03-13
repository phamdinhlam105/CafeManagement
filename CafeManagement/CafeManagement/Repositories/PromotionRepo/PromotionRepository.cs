using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.PromotionRepo;
using CafeManagement.Models.PromotionModel;
using CafeManagement.Models.Stock;
using Microsoft.EntityFrameworkCore;

namespace CafeManagement.Repositories.PromotionRepo
{
    public class PromotionRepository:BaseRepository<Promotion>,IPromotion
    {
        public PromotionRepository(CafeManagementDbContext _context) : base(_context) { }
        public override async Task<IEnumerable<Promotion>> GetAll()
        {
            return await _context.Promotions.Include(p => p.Schedules).ToListAsync();
        }
        public override async Task<Promotion> GetById(Guid id)
        {
            return await _context.Promotions
                .Include(p => p.Schedules)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
