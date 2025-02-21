using CafeManagement.Data;
using CafeManagement.Interfaces.Repositories.PromotionRepo;
using CafeManagement.Models.Promotion;

namespace CafeManagement.Repositories.PromotionRepo
{
    public class PromotionRepository:BaseRepository<Promotion>,IPromotion
    {
        public PromotionRepository(CafeManagementDbContext _context) : base(_context) { }
    }
}
