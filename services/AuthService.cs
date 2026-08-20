// Services/AuthService.cs
using CRUD_WEBAPI.DTOs;
using CRUD_WEBAPI.Models;
using CRUD_WEBAPI.Repositories;

namespace CRUD_WEBAPI.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUserRepository userRepository,
        ITokenService tokenService,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
    {
        var existing = await _userRepository.GetByUsernameAsync(dto.Username);
        if (existing != null)
        {
            _logger.LogWarning("Registration attempted with existing username: {Username}", dto.Username);
            return null;   // caller (controller) will turn this into a 409/400
        }

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            // Never trust a client-supplied "Admin" role on public signup.
            // Only allow the default role here; promoting someone to Admin
            // should be a separate, protected action.
            Role = "Student"
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        _logger.LogInformation("New user registered: {Username}", user.Username);

        var token = _tokenService.GenerateToken(user);
        return new AuthResponseDto { Token = token, Username = user.Username, Role = user.Role };
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
        {
            _logger.LogWarning("Failed login attempt for username: {Username}", dto.Username);
            return null;
        }

        _logger.LogInformation("User logged in: {Username}", user.Username);

        var token = _tokenService.GenerateToken(user);
        return new AuthResponseDto { Token = token, Username = user.Username, Role = user.Role };
    }
}