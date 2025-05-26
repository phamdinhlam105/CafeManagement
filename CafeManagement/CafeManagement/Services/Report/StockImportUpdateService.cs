using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Report
{
    public class StockImportUpdateService : IStockImportUpdateService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockImportUpdateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task UpdateStockReportByStockEntry(StockEntry entry)
        {
            var todayReport = await _unitOfWork.DailyReport.GetByDate(Ultilities.GetToday());
            if (todayReport == null)
                todayReport = new DailyReport();
            todayReport.StockReport.NumberOfImports++;
            todayReport.StockReport.TotalExpenditure += entry.TotalValue;
            foreach (var entryDetail in entry.StockEntryDetails)
            {
                foreach (var stockreportDetail in todayReport.StockReport.StockReportDetails)
                {
                    if (entryDetail.Ingredient.Id == stockreportDetail.IngredientId)
                    {
                        stockreportDetail.QuantityImported += entryDetail.ImportQuantity;
                        stockreportDetail.QuantityOnHand += entryDetail.ImportQuantity;
                        stockreportDetail.TotalValueRemain += entryDetail.TotalValue;
                    }
                }
            }
            await _unitOfWork.StockReport.Update(todayReport.StockReport);
        }
        public async Task UpdateStockReportByManualChange(StockAdjustment adjustment)
        {
            var todayReport = await _unitOfWork.DailyReport.GetByDate(Ultilities.GetToday());
            if (todayReport == null)
                todayReport = new DailyReport();
            foreach(var adjustDetail in adjustment.AdjustmentDetails)
            {
                foreach(var stockDetail in todayReport.StockReport.StockReportDetails)
                {
                    if (stockDetail.IngredientId == adjustDetail.IngredientId)
                    {
                        stockDetail.AdjustmentQuantity += adjustDetail.QuantityAdjusted;
                    }
                }
            }
        }
    }
}
