using CRUD_WEBAPI.Models;

namespace CRUD_WEBAPI.Repositories;

public interface IStudentRepository
{
    Task<List<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(Student student);
    Task<bool> SaveChangesAsync();
}