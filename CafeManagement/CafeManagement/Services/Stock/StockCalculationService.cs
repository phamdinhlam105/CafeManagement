using CafeManagement.Helpers;
using CafeManagement.Interfaces.Services.Stock;
using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.Stock
{
    public class StockCalculationService : IStockCalculationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public StockCalculationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<DailyStock>> CurrentStock()
        {
            var dailyReport = new List<DailyStock>();
            foreach(var ingredient in (await _unitOfWork.Ingredient.GetAll()))
            {
                var entryDetails = await _unitOfWork.StockEntryDetail.GetAvailableIngredient(ingredient.Id);
                if (entryDetails.Count==0) continue;
                var remainAmount = entryDetails.Sum(sed=>sed.RemainQuantity);
                dailyReport.Add(new DailyStock
                {
                    Id = new Guid(),
                    StockRemaining = remainAmount,
                    StockImport = entryDetails.Where(sed => DateOnly.FromDateTime(sed.StockEntry.EntryDate) == Ultilities.GetToday()).Sum(sed => sed.ImportQuantity),
                });
            }
            return dailyReport;
        }

        public async Task<List<DailyStock>> GetStockAtAtime(DateOnly date)
        {
            throw new NotImplementedException();
        }

        public async Task<DailyStock> GetStockByIngredient(Guid ingredientId)
        {
            var dailyReport = new List<DailyStock>();
            var entryDetails = await _unitOfWork.StockEntryDetail.GetAvailableIngredient(ingredientId);
            if (entryDetails.Count == 0)
                return null;
            return new DailyStock
            {
                Id = new Guid(),
                StockRemaining = entryDetails.Sum(sed => sed.RemainQuantity),
                StockImport = entryDetails.Where(sed => DateOnly.FromDateTime(sed.StockEntry.EntryDate) == Ultilities.GetToday()).Sum(sed => sed.ImportQuantity),
            };
        }
    }
}
