using Stock.Domain.Dtos;
using Stock.Domain.Entities;
using Stock.Domain.Interfaces;
using Stock.Domain.Interfaces.MessageBroker;
using AutoMapper;
using Stock.Domain.RepositoryContracts;
using Trade.Domain.Interfaces;

namespace Stock.Application.AppUsecases.Stocks.CreateStocks
{
    public class CreateStockUsecase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _busControl;
        private readonly IMapper _mapper;
        private readonly ICurrentContext _currentContext;
        private readonly IUserRepository _userRepository;
        public CreateStockUsecase(IUnitOfWork unitOfWork, IBus busControl, IMapper mapper,
            ICurrentContext currentContext, IUserRepository userRepository) 
        {
            _unitOfWork = unitOfWork;
            _busControl = busControl;
            _mapper = mapper;
            _currentContext = currentContext;
            _userRepository = userRepository;
        }

        public async Task CreateStock(StockRequestDto stock)
        {
            var stockProduct = _mapper.Map<StockProduct>(stock);
            var userId = _currentContext.LoggedInUser;            
            var user = _userRepository.GetUserById(userId);
            stockProduct.UserId = user.Id;
            _unitOfWork.StockProductRepository.CreateStock(stockProduct);
            _unitOfWork.Complete();
            var stockMessage = _mapper.Map<StockProduct, StockDto>(stockProduct);
            await _busControl.SendAsync<StockDto>("stock-trade", stockMessage); 
        }
    }
}
