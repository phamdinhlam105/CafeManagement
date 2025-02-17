﻿namespace CafeManagement.Interfaces.Services
{
    public interface IReportUpdateService
    {
        void UpdateDailyReport(DateOnly date, DailyReportUpdateDto updateData);
        void UpdateMonthlyReport(int month, int year, MonthlyReportUpdateDto updateData);
        void UpdateQuarterlyReport(int quarter, int year, QuarterlyReportUpdateDto updateData);
    }
}
