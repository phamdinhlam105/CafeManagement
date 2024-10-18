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
        void Save();
    }
}
