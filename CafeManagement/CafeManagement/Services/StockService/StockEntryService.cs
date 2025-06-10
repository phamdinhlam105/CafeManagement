using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.StockService
{
    public class StockEntryService : IStockEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockEntryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddNewEntry(StockEntry entry)
        {
            await _unitOfWork.StockEntry.Add(entry);
        }

        public async Task<IEnumerable<StockEntry>> GetAll()
        {
            return await _unitOfWork.StockEntry.GetAll();
        }
        public async Task<IEnumerable<StockEntry>> GetByDate(DateOnly date)
        {
            return await _unitOfWork.StockEntry.GetByDate(date);
        }

    }
}
