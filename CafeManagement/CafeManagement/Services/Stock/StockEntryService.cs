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
            DailyStock dailyStock = await _stockService.StockRemain();

            foreach (var entryDetail in entry.StockEntryDetails)
            {
                var stockDetail = dailyStock.DailyStockDetails
                    .FirstOrDefault(d => d.IngredientId == entryDetail.IngredientId);
                if (stockDetail == null)
                {
                    var newStockDetail = new DailyStockDetail
                    {
                        Id = Guid.NewGuid(),
                        DailyStockId = dailyStock.Id,
                        StockAtStartOfDay = 0,
                        StockImport = entryDetail.Quantity,
                        StockRemaining = entryDetail.Quantity,
                        IngredientId = entryDetail.IngredientId,
                    };
                    await _unitOfWork.DailyStockDetail.Add(newStockDetail);
                }
                else
                {
                    stockDetail.StockImport += entryDetail.Quantity;
                    stockDetail.StockRemaining += entryDetail.Quantity;
                    await _unitOfWork.DailyStockDetail.Update(stockDetail);
                }

                await _unitOfWork.DailyStockDetail.Update(stockDetail);
            }
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
