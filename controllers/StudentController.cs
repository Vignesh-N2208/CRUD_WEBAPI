using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CRUD_WEBAPI.DTOs;
using CRUD_WEBAPI.Services;

namespace CRUD_WEBAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize("Admin")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _studentService.GetAllAsync();
        return Ok(students);
    }

    [HttpGet("{id}")]
     // <-- This endpoint now requires authentication
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);
        if (student == null) return NotFound();
        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentDto dto)
    {
        var created = await _studentService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateStudentDto dto)
    {
        bool success = await _studentService.UpdateAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await _studentService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}