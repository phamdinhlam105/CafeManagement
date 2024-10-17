using CafeManagement.Models;

namespace CafeManagement.Repositories.Interfaces
{
    public interface IOrderDetailRepository:IRepository<OrderDetail>
    {
        IEnumerable<OrderDetail> GetDetailByOrderId(Guid OrderId);
    }
}
