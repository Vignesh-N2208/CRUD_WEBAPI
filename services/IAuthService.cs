// Services/IAuthService.cs
using CRUD_WEBAPI.DTOs;

namespace CRUD_WEBAPI.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
}