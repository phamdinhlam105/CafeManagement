using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        IEnumerable<OrderDetail> GetDetailByOrderId(Guid OrderId);
    }
}
