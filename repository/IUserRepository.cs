// Repositories/IUserRepository.cs
using CRUD_WEBAPI.Models;

namespace CRUD_WEBAPI.Repositories;

public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task<bool> SaveChangesAsync();
}