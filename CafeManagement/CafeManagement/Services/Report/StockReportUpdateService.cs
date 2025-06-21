using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Report
{
    public class StockReportUpdateService : IStockReportUpdateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportCreationService _reportCreationService;
        public StockReportUpdateService(IUnitOfWork unitOfWork, IReportCreationService reportCreationService)
        {
            _unitOfWork = unitOfWork;
            _reportCreationService = reportCreationService;
        }
        public async Task UpdateStockReportByStockEntry(StockEntry entry)
        {
            var todayStockReport = (await _reportCreationService.GetTodayReport()).StockReport;
            todayStockReport.NumberOfImports++;
            todayStockReport.TotalExpenditure += entry.TotalValue;
            foreach (var entryDetail in entry.StockEntryDetails)
            {
                foreach (var stockreportDetail in todayStockReport.StockReportDetails)
                {
                    if (entryDetail.Ingredient.Id == stockreportDetail.IngredientId)
                    {
                        stockreportDetail.QuantityImported += entryDetail.ImportQuantity;
                        stockreportDetail.QuantityOnHand += entryDetail.ImportQuantity;
                        stockreportDetail.TotalValueRemain += entryDetail.TotalValue;
                    }
                }
            }
            await _unitOfWork.StockReport.Update(todayStockReport);
        }
        public async Task UpdateStockReportByAdjustment(StockAdjustment adjustment)
        {
            var todayStockReport = (await _reportCreationService.GetTodayReport()).StockReport;
            foreach(var adjustDetail in adjustment.AdjustmentDetails)
            {
                foreach(var stockDetail in todayStockReport.StockReportDetails)
                {
                    if (stockDetail.IngredientId == adjustDetail.IngredientId)
                    {
                        stockDetail.AdjustmentQuantity += adjustDetail.QuantityAdjusted;
                        stockDetail.TotalValueRemain -= adjustDetail.AdjustValue;
                    }
                }
            }
            await _unitOfWork.StockReport.Update(todayStockReport);
        }
        public async Task UpdateStockReportByUsage(Guid usageId)
        {
            var listUsageDetail = await _unitOfWork.StockUsageDetail.GetDetailListByUsageId(usageId);
            var todayStockReport = (await _reportCreationService.GetTodayReport()).StockReport;
            foreach (var usageDetail in listUsageDetail)
            {
                foreach (var stockDetail in todayStockReport.StockReportDetails)
                {
                    if (usageDetail.StockEntryDetail.IngredientId == stockDetail.IngredientId)
                    {
                        stockDetail.QuantityOnHand -= usageDetail.QuantityUsed;
                        stockDetail.QuantityExported += usageDetail.QuantityUsed;
                        stockDetail.TotalValueRemain -= usageDetail.TotalValue;
                    }
                }
            }
            await _unitOfWork.StockReport.Update(todayStockReport);
        }
    }
}
