using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Events.Registers
{
    public class OrderCompleteRegister:IEventRegister<Order>
    {
        private readonly IObserverFactory<Order> _factory;
        public OrderCompleteRegister(IObserverFactory<Order> factory)
        {
            _factory = factory;
        }
        public void Register(ISubject<Order> subject)
        {
            subject.Attach(_factory.Create("customer"));
            subject.Attach(_factory.Create("order-report"));
        }
    }
}
