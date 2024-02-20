using Stock.Domain.Dtos;
using Stock.Domain.Entities;


namespace Stock.Domain.RepositoryContracts
{
    public interface IUserRepository
    {
        public void CreateUser(UserDto userdto);
    }
}
