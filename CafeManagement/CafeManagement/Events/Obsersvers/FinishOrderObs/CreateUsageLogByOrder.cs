using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Events.Obsersvers.FinishOrderObs
{
    public class CreateUsageLogByOrder : IAppObserver<Order>
    {
        private readonly IStockUsageService _stockUsageService;
        public CreateUsageLogByOrder(IStockUsageService stockUsageService)
        {
            _stockUsageService = stockUsageService;
        }

        public async Task Update(Order data)
        {
            await _stockUsageService.AddStockUsageLogByOrder(data);
        }
    }
}
