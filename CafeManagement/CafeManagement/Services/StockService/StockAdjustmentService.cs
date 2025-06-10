using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.StockService
{
    public class StockAdjustmentService:IStockAdjustmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockFIFOService _stockFIFOService;
        public StockAdjustmentService(IUnitOfWork unitOfWork, IStockFIFOService stockFIFOService)
        {
            _unitOfWork = unitOfWork;
            _stockFIFOService = stockFIFOService;
        }

        public async Task<List<StockAdjustment>> GetAdjustmentsByDate(DateOnly date)
        {
            return await _unitOfWork.StockAdjustment.GetByDate(date);
        }

        public async Task<List<StockAdjustment>> GetAdjustmentsByRange(DateOnly start, DateOnly end)
        {
            return await _unitOfWork.StockAdjustment.GetByDateRange(start, end);
        }

        public async Task NewAdjustment(StockAdjustment adjustment)
        {
            foreach(var detail in adjustment.AdjustmentDetails)
            {
                detail.AdjustValue = await _stockFIFOService.GetIngredientValueFIFO(detail.IngredientId, detail.QuantityAdjusted);
            }
            await _unitOfWork.StockAdjustment.Add(adjustment);
        }
    }
}
