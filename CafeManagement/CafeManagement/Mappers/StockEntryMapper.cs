using CafeManagement.Dtos.Request.Stock;
using CafeManagement.Dtos.Respone.Stock;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Models.Stock;

namespace CafeManagement.Mappers
{
    public class StockEntryMapper : IStockEntryMapper
    {
        public StockEntry MaptoEntity(StockEntryRequest request)
        {
            var newStockId = Guid.NewGuid();
            return new StockEntry
            {
                Id = newStockId,
                EntryDate = request.EntryDate,
                TotalValue = request.TotalValue,
                StockEntryDetails = request.Details.Select(d => new StockEntryDetail
                {
                    Id = Guid.NewGuid(),
                    IngredientId = d.IngredientId,
                    StockEntryId = newStockId,
                    Quantity = d.Quantity,
                    Price = d.Price
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
                Details = entry.StockEntryDetails.Select(sed => new StockEntryDetailResponse
                {
                    Id = sed.Id,
                    Quantity = sed.Quantity,
                    Price = sed.Price,
                    Ingredient = sed.Ingredient,
                    IngredientId = sed.IngredientId,
                    StockEntryId = sed.StockEntryId
                }).ToList()
            };
        }
    }
}
