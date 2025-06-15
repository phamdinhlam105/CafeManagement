us
using CafeManagement.Dtos.Request.StockReq;
using CafeManagement.Dtos.Respone.StockRes;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.Stock;

namespace CafeManagement.Mappers
{
    public class StockEntryMapper : IStockEntryMapper
    {
        private readonly IStockEntryDetailMapper _stockEntryDetailMapper;
        public StockEntryMapper(IStockEntryDetailMapper stockEntryDetailMapper)
        {
            _stockEntryDetailMapper = stockEntryDetailMapper;
        }

        public StockEntry MapToEntity(StockEntryRequest request)
        {
            return new StockEntry
            {
                TotalValue = request.TotalValue,
                StockEntryDetails = request.Details.Select(d => _stockEntryDetailMapper.MapToEntity(d)).ToList()
            };
        }

        public StockEntryResponse MapToResponse(StockEntry entry)
        {
            return new StockEntryResponse
            {
                Id = entry.Id,
                EntryDate = entry.EntryDate,
                TotalValue = entry.TotalValue,
                Details = entry.StockEntryDetails.Select(sed => _stockEntryDetailMapper.MapToResponse(sed)).ToList()
            };
        }
    }
}
