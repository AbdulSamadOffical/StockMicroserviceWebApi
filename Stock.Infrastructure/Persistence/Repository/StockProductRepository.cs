using Stock.Domain.Entities;
using Stock.Domain.RepositoryContracts;
using AutoMapper;
using Stock.Domain.DomainEntities;
using Stock.Domain.Dtos;
using Stock.Domain.Exceptions;

namespace Stock.Infrastructure.Persistence.Repository
{
    public class StockProductRepository : IStockProductRepository
    {
        private readonly IMapper _mapper;
        protected readonly ApplicationContext _context;
        private readonly IGenericRepository<StockProduct> _stockProduct;
        public StockProductRepository(IMapper mapper, ApplicationContext context, IGenericRepository<StockProduct> stockProduct)
        {
            _context = context;
            _stockProduct = stockProduct;
            _mapper = mapper;
        }

        public StockDomain GetStockById(int id)
        {
            var stockProduct = _stockProduct.GetById(id);
            var stock = _mapper.Map<StockDomain>(stockProduct);
            return stock;
        }

        public IEnumerable<StockDomain> GetAllStocks()
        {
            var stockProducts = _stockProduct.GetAll();
            var stock = _mapper.Map<IEnumerable<StockDomain>>(stockProducts);
            return stock;
        }

        public StockDomain GetStockBySymbol(string symbol)
        {
            var stockProductsBySymbol = _context.StockProduct.FirstOrDefault(s => s.Symbol == symbol);
            var stock = _mapper.Map<StockDomain>(stockProductsBySymbol);
            return stock;
        }

        public void CreateStock(StockDto stock)
        {
            var stockProduct = _mapper.Map<StockProduct>(stock);
            _stockProduct.Add(stockProduct);
        }

        public void PutStock(StockDto stock, int id) 
        {
            var stockProduct = _stockProduct.GetById(id);

            if(stockProduct == null)
            {
                throw new NotFoundException("Stock product doesn't exist's.");
            }

            stockProduct.Symbol = stock.Symbol;
            stockProduct.Price = stock.Price;
            stockProduct.CompanyName = stock.CompanyName;
        }

        public void DeleteStockById(int id) 
        {
            var stockProduct = _stockProduct.GetById(id);

            if (stockProduct == null)
            {
                throw new NotFoundException("Stock product doesn't exist's.");
            }
            _stockProduct.Remove(stockProduct);
        }
    }
}
