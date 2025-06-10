using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Report;
using CafeManagement.Models.Report;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Report
{
    public class ReportCreationService:IReportCreationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReportCreationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<DailyReport> GetTodayReport()
        {
            var todayReport = await _unitOfWork.DailyReport.GetByDate(Ultilities.GetToday());
            var listIngredient = await _unitOfWork.Ingredient.GetAll();
            if (todayReport == null)
            {
                todayReport = new DailyReport
                {
                    Id = Guid.NewGuid(),
                    ReportDate = Ultilities.GetToday(),
                    StockReport = new StockReport
                    {
                        Id = Guid.NewGuid(),
                        StockReportDetails = new List<StockReportDetail>()
                    }
                };

                foreach (var ingredient in listIngredient)
                {
                    var availableEntries = await _unitOfWork.StockEntryDetail.GetAvailableIngredient(ingredient.Id);

                    if (availableEntries != null && availableEntries.Count > 0)
                    {
                        float quantityOnHand = availableEntries.Sum(e => e.RemainQuantity);

                        decimal totalValueRemain = availableEntries.Sum(e => e.TotalValue);

                        var detail = new StockReportDetail
                        {
                            Id = Guid.NewGuid(),
                            IngredientId = ingredient.Id,
                            IngredientName = ingredient.Name,
                            QuantityOnHand = quantityOnHand,
                            QuantityImported = 0,      
                            QuantityExported = 0,      
                            AdjustmentQuantity = 0,    
                            TotalValueRemain = totalValueRemain
                        };

                        todayReport.StockReport.StockReportDetails.Add(detail);
                    }
                }
                await _unitOfWork.DailyReport.Add(todayReport);
            }
            return todayReport;
        }
    }
}
