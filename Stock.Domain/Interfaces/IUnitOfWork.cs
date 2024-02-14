using Stock.Domain.RepositoryContracts;

namespace Stock.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        int Complete ();
        IStockProductRepository StockProductRepository { get; }
    }
}
