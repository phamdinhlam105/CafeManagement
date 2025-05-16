using CafeManagement.Interfaces.Repositories;
using CafeManagement.Interfaces.Repositories.PromotionRepo;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Interfaces.Repositories.Stock;

namespace CafeManagement.UnitOfWork
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IOrderRepository Order { get; }
        IOrderDetailRepository OrderDetail { get; }
        ICustomerRepository Customer { get; }
        IIngredientRepository Ingredient { get; }
        IDailyStockRepository DailyStock { get; }
        IDailyStockDetailRepository DailyStockDetail { get; }
        IStockEntryRepository StockEntry { get; }
        IStockEntryDetailRepository StockEntryDetail { get; }
        IOrderReportRepository OrderReport { get; }
        IStockReportRepository StockReport { get; }
        IProductReportRepository ProductReport {  get; }
        IDailyReportRepository DailyReport { get; }
        IMonthlyReportRepository MonthlyReport { get; }
        IQuarterlyReportRepository QuarterlyReport { get; }
        IYearlyReportRepository YearlyReport {  get; }
        IPromotion Promotion { get; }
        IPromotionSchedule PromotionSchedule {  get; }
        IProfileRepository Profile { get; }
        Task Save();
    }
}
