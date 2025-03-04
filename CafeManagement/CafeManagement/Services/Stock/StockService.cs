using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Stock
{
    public class StockService : IStockService
    {
        private IUnitOfWork _unitOfWork;
        public StockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DailyStock> NewDailyStock()
        {
            var listStock = await _unitOfWork.DailyStock.GetAll();
            DailyStock todayStock =listStock.FirstOrDefault(ds => ds.createDate == Ultilities.GetToday());

            if (todayStock != null)
            {
                return todayStock;
            }
            DailyStock yesterdayStock = listStock.FirstOrDefault(ds => ds.createDate == Ultilities.GetYesterday());

            todayStock = new DailyStock
            {
                Id = Guid.NewGuid(),
                createDate = Ultilities.GetToday(),
                DailyStockDetails = new List<DailyStockDetail>()
            };

            var ingredients = await _unitOfWork.Ingredient.GetAll();

            foreach (var ingredient in ingredients)
            {
                float startStock = 0;
                if (yesterdayStock != null)
                {
                    var lastDetail = yesterdayStock.DailyStockDetails
                        .FirstOrDefault(d => d.Ingredient.Id == ingredient.Id);

                    if (lastDetail != null)
                    {
                        startStock = lastDetail.StockRemaining;
                    }
                }

                var stockDetail = new DailyStockDetail
                {
                    Id = Guid.NewGuid(),
                    Ingredient = ingredient,
                    StockAtStartOfDay = startStock,
                    StockImport = 0,
                    StockRemaining = startStock
                };

                todayStock.DailyStockDetails.Add(stockDetail);
            }

            await _unitOfWork.DailyStock.Add(todayStock);
            return todayStock;
        }

        public async Task<DailyStock> StockRemain()
        {
            DateOnly today = Ultilities.GetToday();
            var listStock = await _unitOfWork.DailyStock.GetAll();
            DailyStock currentStock = listStock.FirstOrDefault(ds => ds.createDate == today);

            if (currentStock == null)
                currentStock = await NewDailyStock();

            return currentStock;
        }

        public async Task StockUpdate(Guid stockId, Ingredient ingredient, float amountRemain)
        {
            var listStock = await _unitOfWork.DailyStock.GetAll();
            DailyStock dailyStock = listStock.FirstOrDefault(ds => ds.Id == stockId);

            if (dailyStock == null)
            {
                throw new Exception("DailyStock không tồn tại.");
            }

            DailyStockDetail stockDetail = dailyStock.DailyStockDetails
                .FirstOrDefault(d => d.Ingredient.Id == ingredient.Id);

            if (stockDetail == null)
            {
                throw new Exception($"Không tìm thấy nguyên liệu {ingredient.Id} trong kho hôm nay.");
            }

            stockDetail.StockRemaining = amountRemain;

            await _unitOfWork.DailyStockDetail.Update(stockDetail);
        }

        public async Task<IEnumerable<DailyStock>> GetAllDailyStocks()
        {
            return await _unitOfWork.DailyStock.GetAll();
        }
        public async Task<IEnumerable<DailyStockDetail>> GetDetailByDate(DateOnly date)
        {
            var stock = (await _unitOfWork.DailyStock.GetAll()).FirstOrDefault(d => d.createDate == date);
            if (stock == null)
            {
                return null;
            }
            return stock.DailyStockDetails.ToList();
        }
    }
}
