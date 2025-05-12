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
            var todayStock = await _unitOfWork.DailyStock.GetByDate(Ultilities.GetToday());
            if (todayStock != null)
            {
                return todayStock;
            }
            DailyStock yesterdayStock = await _unitOfWork.DailyStock.GetByDate(Ultilities.GetYesterday());

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
                    else
                        startStock = 0;
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
            var currentStock = await _unitOfWork.DailyStock.GetByDate(today);

            if (currentStock == null)
                currentStock = await NewDailyStock();

            return currentStock;
        }

        public async Task StockUpdate(Guid ingredientId, float amountRemain)
        {
            var dailyStock = await StockRemain();
            DailyStockDetail stockDetail = dailyStock.DailyStockDetails
                .FirstOrDefault(d => d.IngredientId == ingredientId);

            if (stockDetail == null)
            {
                throw new Exception("No ingredient on today");
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
            return (await _unitOfWork.DailyStock.GetByDate(date)).DailyStockDetails;
        }
    }
}
