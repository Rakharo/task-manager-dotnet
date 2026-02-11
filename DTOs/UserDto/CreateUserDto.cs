using System.ComponentModel.DataAnnotations;

namespace task_manager_dotnet.DTOs;

public class CreateUserDto
{
    
    [Required]
    [MaxLength(150)]
    public required string Name { get; set; } = null!;
    public required string Password { get; set; } = null!;
    public required string Login { get; set; } = null!;
    public required string Email { get; set; } = null!;
    public string? Phone { get; set; } = null!;

}
