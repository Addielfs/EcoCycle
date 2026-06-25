using EcoCycle.Application.DTOs;

namespace EcoCycle.Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenDto> RegisterAsync(RegisterDto registerDto);
        Task<TokenDto> LoginAsync(LoginDto loginDto);
    }
}
