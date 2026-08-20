// Services/ITokenService.cs
using CRUD_WEBAPI.Models;
namespace CRUD_WEBAPI.Services;
public interface ITokenService
{
    string GenerateToken(User user);
}