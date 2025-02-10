using CafeManagement.Interfaces.Repositories;

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
        void Save();
    }
}
