﻿
using CafeManagement.Models.Stock;
using CafeManagement.Models.OrderModel;

namespace CafeManagement.Interfaces.Services.StockService
{
    public interface IStockUsageService
    {
        Task AddStockUsageLogByOrder(Order order);
        Task<List<StockUsageLog>> GetUsageLogByOrder(Guid orderId);
        Task<List<StockUsageLog>> GetUsageLogByDate( DateOnly date);
    }
}
