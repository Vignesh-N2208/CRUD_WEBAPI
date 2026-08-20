// DTOs/RegisterDto.cs
using System.ComponentModel.DataAnnotations;

namespace CRUD_WEBAPI.DTOs;

public class RegisterDto
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    // Optional: let the caller request a role, but see AuthService for why
    // we don't just trust this blindly.
    public string? Role { get; set; }
}