using CafeManagement.Dtos.Request.Stock;
using CafeManagement.Dtos.Respone.Stock;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Mappers.BaseMapper;
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
            var newStockId = Guid.NewGuid();
            return new StockEntry
            {
                Id = newStockId,
                EntryDate = request.EntryDate,
                TotalValue = request.TotalValue,
                StockEntryDetails = request.Details.Select(d =>
                {
                    var detail = _stockEntryDetailMapper.MapToEntity(d);
                    detail.StockEntryId = newStockId;
                    return detail;
                }).ToList()
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
