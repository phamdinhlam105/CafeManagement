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
        public void StockImport(StockEntry entry)
        {
            if (entry == null || entry.StockEntryDetails == null || !entry.StockEntryDetails.Any())
            {
                throw new ArgumentException("Phiếu nhập không hợp lệ.");
            }

            DailyStock dailyStock = StockRemain();

            foreach (var entryDetail in entry.StockEntryDetails)
            {
                var stockDetail = dailyStock.DailyStockDetails
                    .FirstOrDefault(d => d.Ingredient.Id == entryDetail.IngredientId);

                if (stockDetail == null)
                {
                    stockDetail = new DailyStockDetail
                    {
                        Id = Guid.NewGuid(),
                        Ingredient = _unitOfWork.Ingredient.GetById(entryDetail.IngredientId),
                        StockAtStartOfDay = 0,
                        StockImport = (float)entryDetail.Quantity,
                        StockRemaining = (float)entryDetail.Quantity
                    };

                    dailyStock.DailyStockDetails.Add(stockDetail);
                }
                else
                {
                    stockDetail.StockImport += (float)entryDetail.Quantity;
                    stockDetail.StockRemaining += (float)entryDetail.Quantity;
                }
            }
        }

        public DailyStock StockRemain()
        {
            DateTime today = DateTime.UtcNow.Date;

            DailyStock currentStock = _unitOfWork.DailyStock
                .GetAll()
                .FirstOrDefault(ds => ds.createDate.Date == today);

            if (currentStock == null)
            {
                DailyStock previousStock = _unitOfWork.DailyStock
                    .GetAll()
                    .OrderByDescending(ds => ds.createDate)
                    .FirstOrDefault(ds => ds.createDate < today);

                currentStock = new DailyStock
                {
                    Id = Guid.NewGuid(),
                    createDate = today,
                    DailyStockDetails = new List<DailyStockDetail>()
                };

                if (previousStock != null)
                {
                    foreach (var detail in previousStock.DailyStockDetails)
                    {
                        currentStock.DailyStockDetails.Add(new DailyStockDetail
                        {
                            Id = Guid.NewGuid(),
                            Ingredient = detail.Ingredient,
                            StockAtStartOfDay = detail.StockRemaining,
                            StockImport = 0,
                            StockRemaining = detail.StockRemaining
                        });
                    }
                }

                _unitOfWork.DailyStock.Add(currentStock);
            }

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

            _unitOfWork.DailyStock.Update(dailyStock);
        }
    }
}
