using Microsoft.AspNetCore.Http;
using Trade.Domain.Interfaces;

namespace Trade.Infrastructure.Context
{
    public class CurrentUser : ICurrentContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string LoggedInUser { get; set; }



        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Console.WriteLine(_httpContextAccessor);
            if (_httpContextAccessor.HttpContext != null)
            {
                LoggedInUser = _httpContextAccessor.HttpContext.Request.Headers["user_id"];
            }

        }

    }
}
