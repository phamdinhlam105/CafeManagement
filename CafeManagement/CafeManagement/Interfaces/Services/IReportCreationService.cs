using CafeManagement.Models;

namespace CafeManagement.Interfaces.Services
{
    public interface IReportCreationService
    {
        void CreateDailyReport(DateOnly date);
        void CreateMonthlyReport(int month, int year);
        void CreateQuarterlyReport(int quarter, int year);
    }
}
