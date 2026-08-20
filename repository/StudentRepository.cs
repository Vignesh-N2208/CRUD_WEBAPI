using Microsoft.EntityFrameworkCore;
using CRUD_WEBAPI.Data;
using CRUD_WEBAPI.Models;

namespace CRUD_WEBAPI.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllAsync()
    {
        return await _context.Students.AsNoTracking().ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students.FindAsync(id);
    }

    public async Task AddAsync(Student student)
    {
        await _context.Students.AddAsync(student);
    }

    public Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Student student)
    {
        _context.Students.Remove(student);
        return Task.CompletedTask;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}