﻿using CafeManagement.Interfaces.Repositories;
using CafeManagement.Interfaces.Repositories.Report;
using CafeManagement.Interfaces.Repositories.Stock;

namespace CafeManagement.UnitOfWork
{
    public interface IUnitOfWork:IDisposable
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
        IUserRepository User { get; }
        IOrderRepository Order { get; }
        IOnlineOrderRepository OnlineOrder { get; }
        IOrderDetailRepository OrderDetail { get; }
        ICustomerRepository Customer { get; }
        IIngredientRepository Ingredient { get; }
        IDailyStockRepository DailyStock { get; }
        IDailyStockDetailRepository DailyStockDetail { get; }
        IStockEntryRepository StockEntry { get; }
        IStockEntryDetailRepository StockEntryDetail { get; }
        IDailyReportRepository DailyReportRepository { get; }
        IMonthlyReportRepository MonthlyReportRepository { get; }
        IQuarterlyReportRepository QuarterlyReportRepository { get; }
        IYearlyReportRepository YearlyReportRepository {  get; }
        void Save();
    }
}
