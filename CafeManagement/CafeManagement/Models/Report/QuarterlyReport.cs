﻿namespace CafeManagement.Models.Report
{
    public class QuarterlyReport : ReportBase
    {
        public int Quarter { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public IEnumerable<BestDays> BestDaysInQuarter { get; set; }
        public IEnumerable<MonthlyReport> MonthlyReports { get; set; }

        public QuarterlyReport()
        {
            BestDaysInQuarter = new List<BestDays>();
            MonthlyReports = new List<MonthlyReport>();
        }
    }
}
