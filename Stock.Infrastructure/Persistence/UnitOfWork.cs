using Stock.Domain.Interfaces;
using Stock.Domain.RepositoryContracts;


namespace Stock.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

   
        public UnitOfWork(ApplicationContext context, IStockProductRepository productRepository, IUserRepository userRepository) 
        {
            _context = context;
            StockProductRepository = productRepository;
            UserRepository = userRepository;
        }

        public IStockProductRepository StockProductRepository { get; }
        public IUserRepository UserRepository { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

    }
}
