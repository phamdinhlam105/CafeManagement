using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Services
{
    public interface IOrderDetailService:IService<OrderDetail>
    {

        ICollection<OrderDetail> GetDetailsByOrder(Guid orderId);
    }
}
