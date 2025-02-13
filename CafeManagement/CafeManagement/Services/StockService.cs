using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services;
using CafeManagement.Models;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services
{
    public class StockService : IStockService
    {
        private IUnitOfWork _unitOfWork;
        public StockService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public DailyStock NewDailyStock()
        {
            DailyStock todayStock = _unitOfWork.DailyStock.GetAll()
       .FirstOrDefault(ds => ds.createDate == Ultilities.GetToday());

            if (todayStock != null)
            {
                return todayStock;
            }
            DailyStock yesterdayStock = _unitOfWork.DailyStock.GetAll()
                .FirstOrDefault(ds => ds.createDate == Ultilities.GetYesterday());

            todayStock = new DailyStock
            {
                Id = Guid.NewGuid(),
                createDate = Ultilities.GetToday(),
                DailyStockDetails = new List<DailyStockDetail>()
            };

            var ingredients = _unitOfWork.Ingredient.GetAll();

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

            _unitOfWork.DailyStock.Add(todayStock);
            return todayStock;
        }
       
        public DailyStock StockRemain()
        {
            DateOnly today = Ultilities.GetToday();

            DailyStock currentStock = _unitOfWork.DailyStock
                .GetAll()
                .FirstOrDefault(ds => ds.createDate == today);

            if (currentStock == null)
                currentStock = NewDailyStock();

            return currentStock;
        }

        public void StockUpdate(Guid stockId, Ingredient ingredient, float amountRemain)
        {
            DailyStock dailyStock = _unitOfWork.DailyStock
      .GetAll()
      .FirstOrDefault(ds => ds.Id == stockId);

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

            _unitOfWork.DailyStockDetail.Update(stockDetail);
        }

        public IEnumerable<DailyStock> GetAllDailyStocks() 
        {
            return _unitOfWork.DailyStock.GetAll();
        }
        public IEnumerable<DailyStockDetail> GetDetailByDate(DateOnly date)
        {
            var stock = _unitOfWork.DailyStock.GetAll().FirstOrDefault(d => d.createDate == date);
            if (stock == null)
            {
                return null;
            }
            return stock.DailyStockDetails.ToList();
        }
    }
}
