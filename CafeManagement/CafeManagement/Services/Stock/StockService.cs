using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Stock
{
    public class StockService : IStockService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private async Task<IEnumerable<DailyStock>> NewDailyStock()
        {
            var todayStock = (await _unitOfWork.DailyStock.GetByDate(Ultilities.GetToday())).ToList();
            if (todayStock.Any())
            {
                return todayStock;
            }
            IEnumerable<DailyStock> lastestDayStock = await _unitOfWork.DailyStock.GetLastestStock();
            todayStock = new List<DailyStock>();
            foreach(DailyStock stock in lastestDayStock)
            {
                var oneStock = new DailyStock
                {
                    Id = Guid.NewGuid(),
                    CreateDate = Ultilities.GetToday(),
                    Ingredient = stock.Ingredient,
                    StockAtStartOfDay = stock.StockRemaining,
                    StockImport = 0,
                    StockRemaining = stock.StockRemaining
                };
                todayStock.Add(oneStock);
                await _unitOfWork.DailyStock.Add(oneStock);
            }
            return todayStock;
        }

        public async Task<IEnumerable<DailyStock>> StockRemain()
        {
            DateOnly today = Ultilities.GetToday();
            var currentStock = await _unitOfWork.DailyStock.GetByDate(today) ?? await NewDailyStock();
            return currentStock;
        }

        public async Task StockUpdate(Guid ingredientId, float amountRemain)
        {
            var dailyStock = await StockRemain();
            DailyStock stockDetail = dailyStock
                .FirstOrDefault(d => d.IngredientId == ingredientId);

            if (stockDetail == null)
            {
                throw new Exception("No ingredient on today");
            }
            stockDetail.StockRemaining = amountRemain;

            await _unitOfWork.DailyStock.Update(stockDetail);
        }

        public async Task<IEnumerable<DailyStock>> GetAllDailyStocks()
        {
            return await _unitOfWork.DailyStock.GetAll();
        }
        public async Task<IEnumerable<DailyStock>> GetByDate(DateOnly date)
        {
            return await _unitOfWork.DailyStock.GetByDate(date);
        }
    }
}
