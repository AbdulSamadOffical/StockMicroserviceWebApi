using Stock.Domain.DomainEntities;
using Stock.Domain.Dtos;
using Stock.Domain.Entities;


namespace Stock.Domain.RepositoryContracts
{
    public interface IStockProductRepository
    {
        public StockDomain GetStockById(string id);
        public IEnumerable<StockDomain> GetAllStocks();
        public StockDomain GetStockBySymbol(string symbol);
        public void CreateStock(StockProduct stock);

        public void PutStock(StockRequestDto stock, string id);
        public void DeleteStockById(string id);
    }
}
