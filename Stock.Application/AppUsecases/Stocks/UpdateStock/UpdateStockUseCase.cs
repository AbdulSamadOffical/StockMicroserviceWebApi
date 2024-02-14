using Stock.Domain.Dtos;
using Stock.Domain.Interfaces;


namespace Stock.Application.AppUsecases.Stocks.UpdateStock
{
    public class UpdateStockUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateStockUseCase(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public void UpdateStock (StockDto stock, int id)
        {
            _unitOfWork.StockProductRepository.PutStock (stock, id);
            _unitOfWork.Complete();
        }
    }
}
