using Stock.Domain.Dtos;
using Stock.Domain.Interfaces;
using Stock.Domain.Interfaces.MessageBroker;


namespace Stock.Application.AppUsecases.Stocks.CreateStocks
{
    public class CreateStockUsecase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _busControl;
        public CreateStockUsecase(IUnitOfWork unitOfWork, IBus busControl) 
        {
            _unitOfWork = unitOfWork;
            _busControl = busControl;
        }

        public async Task CreateStock(StockDto stock)
        {
            _unitOfWork.StockProductRepository.CreateStock(stock);
            _unitOfWork.Complete();
            await _busControl.SendAsync<StockDto>("stock-topic", stock); // Will remove the magic strings
        }
    }
}
