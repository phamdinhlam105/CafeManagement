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

        public async Task StockImport(StockEntry entry)
        {
            if (!entry.StockEntryDetails.Any())
            {
                throw new Exception("Không hợp lệ");
            }
            if (entry.Id == Guid.Empty)
                entry.Id = Guid.NewGuid();
            await _unitOfWork.StockEntry.Add(entry);
            DailyStock dailyStock = await _stockService.StockRemain();
            foreach (var entryDetail in entry.StockEntryDetails)
            {
                Ingredient ingredient = await _unitOfWork.Ingredient.GetById(entryDetail.IngredientId);
                var stockDetail = dailyStock.DailyStockDetails
                    .FirstOrDefault(d => d.Ingredient.Id == entryDetail.IngredientId);

                float amountRemain = (float)entryDetail.Quantity + stockDetail.StockRemaining;
                await _stockService.StockUpdate(stockDetail.Id, ingredient, amountRemain);
            }
            await _unitOfWork.DailyStock.Update(dailyStock);
        }

        public async Task<IEnumerable<StockEntry>> GetAll()
        {
            return await _unitOfWork.StockEntry.GetAll();
        }
        public async Task<IEnumerable<StockEntry>> GetByDate(DateOnly date)
        {
            return (await _unitOfWork.StockEntry.GetAll())
            .Where(se => DateOnly.FromDateTime(se.EntryDate) == date);
        }

    }
}
