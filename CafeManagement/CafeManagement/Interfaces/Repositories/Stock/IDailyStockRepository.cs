using CafeManagement.Models.Stock;
using System.Collections.Generic;

namespace CafeManagement.Interfaces.Repositories.Stock
{
    public interface IDailyStockRepository : IRepository<DailyStock>
    {
        Task<IEnumerable<DailyStock>> GetByDate(DateOnly date);
        Task<IEnumerable<DailyStock>> GetLastestStock();
    }
}
