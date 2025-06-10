namespace CafeManagement.Interfaces.Services.Report
{
    public interface IOrderReportUpdateService
    {
        Task UpdateOrderReportByOrder(Guid orderId);
    }
}
