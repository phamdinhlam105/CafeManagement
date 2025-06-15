using CafeManagement.Dtos.Request.StockReq;
using CafeManagement.Dtos.Respone.StockRes;
using CafeManagement.Dtos.Response.StockRes;
using CafeManagement.Helpers;
using CafeManagement.Interfaces.Facade.StockFacade;
using CafeManagement.Interfaces.Mappers;
using CafeManagement.Interfaces.Services.StockService;

namespace CafeManagement.Facades
{
    public class StockFacade:IStockQueryUseCase,IStockUpdateUseCase
    {
        private readonly IStockQueryService _stockQueryService;
        private readonly IStockAdjustmentService _stockAdjustmentService;
        private readonly IStockEntryService _stockEntryService;
        private readonly IStockEntryMapper _stockEntryMapper;
        private readonly IStockMapper _stockMapper;
        private readonly IStockAdjustmentMapper _stockAdjustmentMapper;
        private readonly IStockDetailMapper _stockDetailMapper;

        public StockFacade(IStockQueryService stockQueryService,
            IStockAdjustmentService stockAdjustmentService,
            IStockEntryService stockEntryService,
            IStockEntryMapper stockEntryMapper,
            IStockMapper stockMapper,
            IStockAdjustmentMapper stockAdjustmentMapper,
            IStockDetailMapper stockDetailMapper
           )
        {
            _stockQueryService = stockQueryService;
            _stockAdjustmentService = stockAdjustmentService;
            _stockEntryService = stockEntryService;
            _stockEntryMapper = stockEntryMapper;
            _stockMapper = stockMapper;
            _stockAdjustmentMapper = stockAdjustmentMapper;
            _stockDetailMapper = stockDetailMapper;
        }

        public async Task<List<StockAdjustmentResponse>> GetAdjustmentsByDate(DateOnly? date)
        {
            var targetDate = date ?? Ultilities.GetToday();

            return (await _stockAdjustmentService.GetAdjustmentsByDate(targetDate)).Select(sa=>_stockAdjustmentMapper.MapToResponse(sa)).ToList();
        }

        public async Task<List<StockEntryResponse>> GetEntriesByDate(DateOnly? date)
        {
            var targetDate = date ?? Ultilities.GetToday();
            return (await _stockEntryService.GetByDate(targetDate)).Select(se=> _stockEntryMapper.MapToResponse(se)).ToList();
        }

        public async Task<StockDetailResponse> GetStockByIngredientId(Guid ingredientId)
        {
            return _stockDetailMapper.MapToResponse(await _stockQueryService.GetStockByIngredient(ingredientId));
        }

        public async Task<StockResponse> GetStockDetailByDate(DateOnly? date)
        {
            var targetDate = date ?? Ultilities.GetToday();
            return _stockMapper.MapToResponse(await _stockQueryService.GetStockAtAtime(targetDate));
        }

        public async Task NewAdjustment(StockAdjustmentRequest adjustment)
        {
            await _stockAdjustmentService.NewAdjustment(_stockAdjustmentMapper.MapToEntity(adjustment));
        }


        public async Task StockImport(StockEntryRequest entry)
        {
            await _stockEntryService.AddNewEntry(_stockEntryMapper.MapToEntity(entry));
        }
    }
}
