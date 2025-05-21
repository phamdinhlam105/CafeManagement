using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Stock
{
    public class StockEntryService : IStockEntryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubject<StockEntry> _stockImportEvent;
        private readonly IObserverFactory<StockEntry> _stockObserverFactory;
        public StockEntryService(IUnitOfWork unitOfWork,
            ISubject<StockEntry> stockImportEvent,
            IObserverFactory<StockEntry> stockObserverFactory)
        {
            _unitOfWork = unitOfWork;
            _stockImportEvent = stockImportEvent;
            _stockObserverFactory = stockObserverFactory;
            _stockImportEvent.Attach(_stockObserverFactory.Create("report"));
            _stockImportEvent.Attach(_stockObserverFactory.Create("stock"));
        }

        public async Task StockImport(StockEntry entry)
        {
            await _stockImportEvent.Notify(entry);
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
