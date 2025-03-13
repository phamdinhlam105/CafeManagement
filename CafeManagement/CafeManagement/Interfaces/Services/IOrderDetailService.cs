using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Services
{
    public interface IOrderDetailService:IService<OrderDetail>
    {

        Task<IEnumerable<OrderDetail>> GetDetailsByOrder(Guid orderId);
        Task<IEnumerable<OrderDetail>> GetByDate(DateOnly date);
    }
}
