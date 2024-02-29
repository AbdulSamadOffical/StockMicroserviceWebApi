using Microsoft.AspNetCore.Mvc;
using Stock.Application.AppUsecases.Stocks.CreateStocks;
using Stock.Application.AppUsecases.Stocks.DeleteStock;
using Stock.Application.AppUsecases.Stocks.GetStocks;
using Stock.Application.AppUsecases.Stocks.UpdateStock;
using Stock.Domain.DomainEntities;
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
        public IActionResult GetById(string id)
        {
            try
            {
                var stockDomain = _getStocksUseCase.GetStockById(id);
                if (stockDomain == null)
                {
                    throw new NotFoundException("No Stock Found.");
                }


                return Ok(new StockResponseDto<StockDomain>() { Message = null, Data = stockDomain });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw new NotFoundException(ex.Message);
            }


        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                
                var allStocks = _getStocksUseCase.GetAllStocks();
                return Ok(new StockResponseDtoList<StockDomain>() { Message = null, Data = allStocks });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw new NotFoundException(ex.Message);
            }

           
        }

        [HttpGet("symbol/{symbol}")]
        public IActionResult GetStockBySymbol(string symbol) 
        {
            try
            {
                var stockBySymbol = _getStocksUseCase.GetStockBySymbol(symbol);
                if (stockBySymbol == null)
                {
                    throw new NotFoundException("No Stock Found By this Symbol.");
                }
                return Ok(new StockResponseDto<StockDomain>() { Message = null, Data = stockBySymbol });

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw new NotFoundException(ex.Message);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] StockRequestDto stock)
        {
            try 
            {
                await _createStockUseCase.CreateStock(stock);

                return Ok(new StockResponseDto<StockRequestDto>() { Message = "Resource created successfully", Data = stock });


            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw new NotFoundException(ex.Message);
            }


        }

        [HttpPut("{id}")]
        public IActionResult UpdateStock([FromBody] StockRequestDto stock, string id)
        {
            try 
            {
                _updateStockUseCase.UpdateStock(stock, id);
                return Ok(new StockResponseDto<StockRequestDto>() { Message = "Resource updated successfully", Data = stock });
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw new NotFoundException(ex.Message);
            }
          

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteStock(string id) 
        {
            try
            {
                _deleteStockUseCase.DeleteStock(id);
                return Ok(new StockResponseDto<StockRequestDto>() { Message = "Resource deleted successfully", Data = null });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
                throw new NotFoundException(ex.Message);
            }

        }
    }
}
