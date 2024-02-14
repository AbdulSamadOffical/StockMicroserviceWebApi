using Stock.Domain.Interfaces;

namespace Stock.Application.AppUsecases.Stocks.DeleteStock
{
    public class DeleteStockUsecase
    {

        private readonly IUnitOfWork _unitOfWork;
        public DeleteStockUsecase(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public void DeleteStock(int id) 
        {
            _unitOfWork.StockProductRepository.DeleteStockById(id);
            _unitOfWork.Complete();
        }
    }
}
