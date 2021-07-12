using AccountApp.Infrastructure.Dto;

namespace AccountApp.Infrastructure.Services
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(string email);
    }
}