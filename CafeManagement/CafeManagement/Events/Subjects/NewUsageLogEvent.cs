using CafeManagement.Interfaces.Observer;
using CafeManagement.Models.Stock;
using CafeManagement.Observers.Subjects;

namespace CafeManagement.Events.Subjects
{
    public class NewUsageLogEvent: BaseSubjectEvent<StockUsageLog>,ISubject<StockUsageLog>
    {
    }
}
