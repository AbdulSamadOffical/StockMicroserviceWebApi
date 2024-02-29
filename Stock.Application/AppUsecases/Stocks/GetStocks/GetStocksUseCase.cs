using Stock.Domain.DomainEntities;
using Stock.Domain.Interfaces;



namespace Stock.Application.AppUsecases.Stocks.GetStocks
{
    public class GetStocksUseCase
    {

        private readonly IUnitOfWork _unitOfWork;

        public GetStocksUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public StockDomain GetStockById(string id)
        {
            return _unitOfWork.StockProductRepository.GetStockById(id);
        }
        public IEnumerable<StockDomain> GetAllStocks()
        {
            return _unitOfWork.StockProductRepository.GetAllStocks();
        }
        public StockDomain GetStockBySymbol (string symbol) 
        {
            return _unitOfWork.StockProductRepository.GetStockBySymbol(symbol);        
        }
    }
}
