using Stock.Domain.Dtos;
using Stock.Domain.Interfaces;


namespace Stock.Application.AppUsecases.Stocks.CreateStocks
{
    public class CreateStockUsecase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateStockUsecase(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateStock(StockDto stock)
        {
            _unitOfWork.StockProductRepository.CreateStock(stock);
            _unitOfWork.Complete();
        }
    }
}
