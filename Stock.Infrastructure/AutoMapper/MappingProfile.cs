using AutoMapper;
using Stock.Domain.Entities;
using Stock.Domain.DomainEntities;
using Stock.Domain.Dtos;


namespace Stock.Infrastructure.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile() 
        {

            CreateMap<StockProduct, StockDomain>();
            CreateMap<StockRequestDto, StockProduct>();
            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
            CreateMap<StockProduct, StockDto>();
        }

    }
}
