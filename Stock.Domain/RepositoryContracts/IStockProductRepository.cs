using Stock.Domain.DomainEntities;
using Stock.Domain.Dtos;


namespace Stock.Domain.RepositoryContracts
{
    public interface IStockProductRepository
    {
        public StockDomain GetStockById(int id);
        public IEnumerable<StockDomain> GetAllStocks();
        public StockDomain GetStockBySymbol(string symbol);
        public void CreateStock(StockDto stock);

        public void PutStock(StockDto stock, int ids);
        public void DeleteStockById(int id);
    }
}
