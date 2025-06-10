

using CafeManagement.Models.OrderModel;

namespace CafeManagement.Interfaces.Services.OrderService
{
    public interface IOrderDetailService : IService<OrderDetail>
    {

        Task<IEnumerable<OrderDetail>> GetDetailsByOrder(Guid orderId);
        Task<IEnumerable<OrderDetail>> GetByDate(DateOnly date);
    }
}
