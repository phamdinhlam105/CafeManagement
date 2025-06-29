﻿using CafeManagement.Events.Obsersvers.FinishOrderObs;
using CafeManagement.Interfaces.Factory;
using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Factories.Observers
{
    public class OrderObsFactory : IObserverFactory<Order>
    {
        private readonly IServiceProvider _serviceProvider;
        public OrderObsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IAppObserver<Order> Create(string type)
        {
            switch (type)
            {
                case "customer": return _serviceProvider.GetRequiredService<CustomerUpdater>();
                case "order-report": return _serviceProvider.GetRequiredService<OrderReportUpdater>();
                case "create-usagelog": return _serviceProvider.GetRequiredService<CreateUsageLogByOrder>();
                default:
                    throw new Exception("Invalid type");
            }

        }
    }
}
