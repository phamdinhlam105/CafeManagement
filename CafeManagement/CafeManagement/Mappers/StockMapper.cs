using CafeManagement.Dtos.Request;
using CafeManagement.Dtos.Respone;
using CafeManagement.Dtos.Respone.StockRes;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Mappers.BaseMapper;
using CafeManagement.Models.Report;
using CafeManagement.Models.Stock;

namespace CafeManagement.Mappers
{
    public class StockMapper : IStockMapper
    {
        private readonly IStockDetailMapper _stockDetailMapper;
        public StockMapper(IStockDetailMapper stockDetailMapper)
        {
            _stockDetailMapper = stockDetailMapper;
        }

        public StockResponse MapToResponse(StockReport stock)
        {
            return new StockResponse
            {
                Id = stock.Id,
                ReportDate = stock.DailyReport.ReportDate,
                Details = stock.StockReportDetails.Select(srd => _stockDetailMapper.MapToResponse(srd)).ToList()
            };
        }


    }
}
