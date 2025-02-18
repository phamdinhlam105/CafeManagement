using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Stock
{
    public class StockEntryService : IStockEntryService
    {
        private IStockService _stockService;
        private IUnitOfWork _unitOfWork;
        public StockEntryService(IUnitOfWork unitOfWork, IStockService stockService)
        {
            _unitOfWork = unitOfWork;
            _stockService = stockService;
        }

        public void StockImport(StockEntry entry)
        {
            if (entry == null || entry.StockEntryDetails == null || !entry.StockEntryDetails.Any())
            {
                throw new ArgumentException("Phiếu nhập không hợp lệ.");
            }
            _unitOfWork.StockEntry.Add(entry);
            DailyStock dailyStock = _stockService.StockRemain();
            foreach (var entryDetail in entry.StockEntryDetails)
            {
                Ingredient ingredient = _unitOfWork.Ingredient.GetById(entryDetail.IngredientId);
                var stockDetail = dailyStock.DailyStockDetails
                    .FirstOrDefault(d => d.Ingredient.Id == entryDetail.IngredientId);

                float amountRemain = (float)entryDetail.Quantity + stockDetail.StockRemaining;
                _stockService.StockUpdate(stockDetail.Id, ingredient, amountRemain);
            }
            _unitOfWork.DailyStock.Update(dailyStock);
        }

        public IEnumerable<StockEntry> GetAll()
        {
            return _unitOfWork.StockEntry.GetAll();
        }
        public IEnumerable<StockEntry> GetByDate(DateOnly date)
        {
            return _unitOfWork.StockEntry.GetAll()
            .Where(se => DateOnly.FromDateTime(se.EntryDate) == date);
        }

    }
}
