using CafeManagement.Dtos.Respone.ReportRes;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.Report;

namespace CafeManagement.Interfaces.Mappers
{
    public interface IReportMapper:IEntityToResponse<DailyReport,OneDayReportResponse>
    {

    }
}
