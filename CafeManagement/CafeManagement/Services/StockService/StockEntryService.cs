using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.StockService
{
    public class StockEntryService : IStockEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubject<StockEntry> _stockImportEvent;
        public StockEntryService(IUnitOfWork unitOfWork,
            ISubject<StockEntry> stockImportEvent,
            IEventRegister<StockEntry> stockImportRegister)
        {
            _unitOfWork = unitOfWork;
            _stockImportEvent = stockImportEvent;
            stockImportRegister.Register(_stockImportEvent);
        }

        public async Task AddNewEntry(StockEntry entry)
        {
            var newId = new Guid();
            entry.Id = newId;
            entry.EntryDate = DateTime.UtcNow;
            foreach(var detail in entry.StockEntryDetails)
            {
                detail.Id = new Guid();
                detail.StockEntryId = newId;
            }
            await _unitOfWork.StockEntry.Add(entry);
            await _stockImportEvent.Notify(entry);
        }

        public async Task<List<StockEntry>> GetAll()
        {
            return (await _unitOfWork.StockEntry.GetAll()).ToList();
        }
        public async Task<List<StockEntry>> GetByDate(DateOnly date)
        {
            return (await _unitOfWork.StockEntry.GetByDate(date)).ToList();
        }

    }
}
