using Core.DTOs;

namespace Core.Interfaces;

public interface IAuthService
{
    Task<string> LoginAsync(LoginDto dto);
    Task<UserDto> RegisterAsync(RegisterDto dto);
}