using CafeManagement.Interfaces.Observer;
using CafeManagement.Interfaces.Services.StockService;
using CafeManagement.Models.Stock;
using CafeManagement.UnitOfWork;

namespace CafeManagement.Services.StockService
{
    public class StockAdjustmentService:IStockAdjustmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStockFIFOService _stockFIFOService;

        private readonly ISubject<StockAdjustment> _newAdjustmentEvent;
        public StockAdjustmentService(IUnitOfWork unitOfWork, 
            IStockFIFOService stockFIFOService,
             ISubject<StockAdjustment> newAdjustmentEvent,
             IEventRegister<StockAdjustment> newAdjustmentRegister
            )
        {
            _unitOfWork = unitOfWork;
            _stockFIFOService = stockFIFOService;
            _newAdjustmentEvent = newAdjustmentEvent;
            newAdjustmentRegister.Register(_newAdjustmentEvent);
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
            var newId = new Guid();
            adjustment.Id = newId;
            adjustment.AdjustmentDate = DateTime.UtcNow;
            foreach(var detail in adjustment.AdjustmentDetails)
            {
                detail.Id = new Guid();
                detail.StockAdjustmentId = newId;
                detail.AdjustValue = await _stockFIFOService.GetIngredientValueFIFO(detail.IngredientId, detail.QuantityAdjusted);
            }
            await _unitOfWork.StockAdjustment.Add(adjustment);
            await _newAdjustmentEvent.Notify(adjustment);
        }
    }
}
