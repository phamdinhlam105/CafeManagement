namespace CafeManagement.Interfaces.Services.Report
{
    public interface IOrderCompleteUpdateService
    {
        Task UpdateOrderReportByOrder(Guid orderId);
        Task UpdateStockReportByOrder(Guid orderId);
    }
}
