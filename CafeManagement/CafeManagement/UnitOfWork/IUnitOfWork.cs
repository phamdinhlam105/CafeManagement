using CafeManagement.Interfaces.Repositories;
using CafeManagement.Interfaces.Repositories.ProductRepo;
using CafeManagement.Interfaces.Repositories.PromotionRepo;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Interfaces.Repositories.Stock;

namespace CafeManagement.UnitOfWork
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        #region Product
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IRecipeDetailRepository RecipeDetail { get; }
        IRecipeRepository Recipe { get; }
        #endregion
        #region Order
        IOrderRepository Order { get; }
        IOrderDetailRepository OrderDetail { get; }
        #endregion
        ICustomerRepository Customer { get; }
        #region Stock
        IIngredientRepository Ingredient { get; }
        IDailyStockRepository DailyStock { get; }
        IStockEntryRepository StockEntry { get; }
        IStockEntryDetailRepository StockEntryDetail { get; }
        IStockUsageLogRepository StockUsageLog { get; }
        IStockUsageDetailRepository StockUsageDetail { get; }
        IStockAdjustmentRepository StockAdjustment { get; }
        IAdjustmentDetailRepository AdjustmentDetail { get; }
        #endregion
        #region Report
        IOrderReportRepository OrderReport { get; }
        IStockReportRepository StockReport { get; }
        IProductReportRepository ProductReport {  get; }
        IDailyReportRepository DailyReport { get; }
        IMonthlyReportRepository MonthlyReport { get; }
        IQuarterlyReportRepository QuarterlyReport { get; }
        IYearlyReportRepository YearlyReport {  get; }
        #endregion
        #region Promotion
        IPromotion Promotion { get; }
        IPromotionSchedule PromotionSchedule {  get; }
        #endregion
        IProfileRepository Profile { get; }
        Task Save();
    }
}
