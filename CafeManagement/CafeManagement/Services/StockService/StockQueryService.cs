using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Report;
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

        public async Task<StockReport> GetStockAtAtime(DateOnly date)
        {
            var dailyReport = await _unitOfWork.DailyReport.GetByDate(date);
            return dailyReport.StockReport;
        }

        public async Task<StockReportDetail> GetStockByIngredient(Guid ingredientId)
        {
            var ingredient = await _unitOfWork.Ingredient.GetById(ingredientId) ?? throw new Exception("Id not found");
            var dailyReport = await _unitOfWork.DailyReport.GetByDate(Ultilities.GetToday());
            var reportDetail = dailyReport.StockReport.StockReportDetails
                .FirstOrDefault(srd => srd.IngredientId == ingredientId);
            return reportDetail;
        }
    }
}
