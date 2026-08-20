using AutoMapper;
using CRUD_WEBAPI.DTOs;
using CRUD_WEBAPI.Models;
using CRUD_WEBAPI.Repositories;

namespace CRUD_WEBAPI.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentService> _logger;   // <-- injected, same pattern as always

    private readonly IConfiguration _configuration;

    public StudentService(IStudentRepository repository, IMapper mapper, ILogger<StudentService> logger, IConfiguration configuration)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _configuration = configuration;
    }

    public async Task<List<StudentDto>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();
        return _mapper.Map<List<StudentDto>>(students);
    }

    public async Task<StudentDto?> GetByIdAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        return student == null ? null : _mapper.Map<StudentDto>(student);
    }

    public async Task<StudentDto> CreateAsync(CreateStudentDto dto)
    {
        _logger.LogInformation("Creating a new student with name: {Name}", dto.Name);
        var student = _mapper.Map<Student>(dto);
        await _repository.AddAsync(student);
        await _repository.SaveChangesAsync();
        bool shouldSendEmail = _configuration.GetValue<bool>("AppSettings:EnableWelcomeEmails");

        if (shouldSendEmail)
        {
            _logger.LogInformation("Sending welcome email to student: {Name}", student.Name);
        }

        _logger.LogInformation("Student created with ID: {Id}", student.Id);
        return _mapper.Map<StudentDto>(student);
    }

    public async Task<bool> UpdateAsync(int id, UpdateStudentDto dto)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null) return false;

        student.Name = dto.Name;
        student.Age = dto.Age;
        student.Email = dto.Email;

        await _repository.UpdateAsync(student);
        return await _repository.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null) return false;

        await _repository.DeleteAsync(student);
        return await _repository.SaveChangesAsync();
    }
}