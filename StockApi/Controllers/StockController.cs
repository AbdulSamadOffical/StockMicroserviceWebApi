using Microsoft.AspNetCore.Mvc;
using Stock.Application.AppUsecases.Stocks.CreateStocks;
using Stock.Application.AppUsecases.Stocks.DeleteStock;
using Stock.Application.AppUsecases.Stocks.GetStocks;
using Stock.Application.AppUsecases.Stocks.UpdateStock;
using Stock.Domain.Dtos;
using Stock.Domain.Exceptions;


namespace StockApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
       
        private readonly ILogger<StockController> _logger;
        private readonly GetStocksUseCase _getStocksUseCase;
        private readonly CreateStockUsecase _createStockUseCase;
        private readonly UpdateStockUseCase _updateStockUseCase;
        private readonly DeleteStockUsecase _deleteStockUseCase;

        public StockController(ILogger<StockController> logger, GetStocksUseCase getStocksUseCase, 
            CreateStockUsecase createStockUsecase, UpdateStockUseCase updateStockuseCase,
            DeleteStockUsecase deleteStockUseCase)
        {
            _logger = logger;
            _getStocksUseCase = getStocksUseCase;
            _createStockUseCase = createStockUsecase;
            _updateStockUseCase = updateStockuseCase;
            _deleteStockUseCase = deleteStockUseCase;
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var stockDomain = _getStocksUseCase.GetStockById(id);
            if (stockDomain == null)
            {
                 throw new NotFoundException("No Stock Found."); 
            }

            
            return Ok(stockDomain); 
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            
            var allStocks = _getStocksUseCase.GetAllStocks();
            return Ok(allStocks);
        }

        [HttpGet("symbol/{symbol}")]
        public IActionResult GetStockBySymbol(string symbol) 
        {
            var stockBySymbol = _getStocksUseCase.GetStockBySymbol(symbol);
            if (stockBySymbol == null)
            {
                throw new NotFoundException("No Stock Found By this Symbol.");
            }
            return Ok(stockBySymbol);
        }

        [HttpPost]
        public IActionResult CreateStock([FromBody] StockDto stock)
        {
            _createStockUseCase.CreateStock(stock);

            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateStock([FromBody] StockDto stock, int id)
        {
            _updateStockUseCase.UpdateStock(stock, id);

            return Ok();

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStock(int id) 
        {
            _deleteStockUseCase.DeleteStock(id);
            return Ok();

        }
    }
}
