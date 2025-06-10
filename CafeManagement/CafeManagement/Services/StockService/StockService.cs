using CafeManagement.Helpers;
using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;

namespace CafeManagement.Services.StockService
{
    public class StockService : IStockService
    {
        private readonly IStockQueryService _stockCalculationService;
        private readonly IStockAdjustmentService _stockAdjustmentService;
        private readonly IStockEntryService _stockEntryService;
        private readonly ISubject<StockEntry> _stockImportEvent;
        private readonly ISubject<StockAdjustment> _newAdjustmentEvent;

        public StockService( IStockQueryService stockCalculationService,
            IStockAdjustmentService stockAdjustmentService,
            IStockEntryService stockEntryService,
            ISubject<StockEntry> stockImportEvent,
            ISubject<StockAdjustment> newAdjustmentEvent,
            IEventRegister<StockEntry> stockImportRegister,
            IEventRegister<StockAdjustment> newAdjustmentRegister)
        {
            _stockCalculationService = stockCalculationService;
            _stockAdjustmentService = stockAdjustmentService;
            _stockEntryService = stockEntryService;

            _stockImportEvent = stockImportEvent;
            _newAdjustmentEvent = newAdjustmentEvent;
            stockImportRegister.Register(_stockImportEvent);
            newAdjustmentRegister.Register(_newAdjustmentEvent);
        }

        public async Task<DailyStock> GetStockByIngredientId(Guid ingredientId)
        {
            return await _stockCalculationService.GetStockByIngredient(ingredientId);
        }

        public async Task<List<DailyStock>> GetStockDetailByDate(DateOnly date)
        {
            return await _stockCalculationService.GetStockAtAtime(date);
        }

        public async Task StockAdjustment(StockAdjustment adjustment)
        {
            await _stockAdjustmentService.NewAdjustment(adjustment);
            await _newAdjustmentEvent.Notify(adjustment);
        }

        public async Task StockImport(StockEntry entry)
        {
            await _stockEntryService.AddNewEntry(entry);
            await _stockImportEvent.Notify(entry);
        }
    }
}
