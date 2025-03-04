using CafeManagement.Models.Order;

namespace CafeManagement.Interfaces.Services
{
    public interface IExportBillService
    {
        byte[] GenerateInvoicePdf(Order order);
    }
}
