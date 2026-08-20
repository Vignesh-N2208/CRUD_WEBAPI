using System.ComponentModel.DataAnnotations;

namespace CRUD_WEBAPI.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 120)]
    public int Age { get; set; }

    [EmailAddress]
    public string? Email { get; set; }
}