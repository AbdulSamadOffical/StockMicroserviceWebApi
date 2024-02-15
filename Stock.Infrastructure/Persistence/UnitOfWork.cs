using Stock.Domain.Interfaces;
using Stock.Domain.RepositoryContracts;


namespace Stock.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

   
        public UnitOfWork(ApplicationContext context, IStockProductRepository productRepository) 
        {
            _context = context;
            StockProductRepository = productRepository;
        }

        public IStockProductRepository StockProductRepository { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

    }
}
