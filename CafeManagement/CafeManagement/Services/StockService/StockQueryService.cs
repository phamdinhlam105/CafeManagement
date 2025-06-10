using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.StockService
{
    public class StockQueryService : IStockQueryService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockQueryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<DailyStock>> CurrentStock()
        {
            var dailyReport = await _unitOfWork.DailyReport.GetByDate(Ultilities.GetToday());
            var listDailyStock = new List<DailyStock>();
            foreach(var detail in dailyReport.StockReport.StockReportDetails)
            {
                listDailyStock.Add(new DailyStock
                {
                    StockRemaining = detail.QuantityOnHand,
                    StockImport = detail.QuantityImported,
                });
            }
            return listDailyStock;
        }

        public async Task<List<DailyStock>> GetStockAtAtime(DateOnly date)
        {
            var dailyReport = await _unitOfWork.DailyReport.GetByDate(date);
            var listDailyStock = new List<DailyStock>();
            foreach (var detail in dailyReport.StockReport.StockReportDetails)
            {
                listDailyStock.Add(new DailyStock
                {
                    StockRemaining = detail.QuantityOnHand,
                    StockImport = detail.QuantityImported,
                });
            }
            return listDailyStock;
        }

        public async Task<DailyStock> GetStockByIngredient(Guid ingredientId)
        {
            var dailyReport = await _unitOfWork.DailyReport.GetByDate(Ultilities.GetToday());
            foreach (var detail in dailyReport.StockReport.StockReportDetails)
            {
                if (detail.IngredientId == ingredientId)
                    return new DailyStock
                    {
                        StockRemaining = detail.QuantityOnHand,
                        StockImport = detail.QuantityImported
                    };
            }
            return null;
        }
    }
}
