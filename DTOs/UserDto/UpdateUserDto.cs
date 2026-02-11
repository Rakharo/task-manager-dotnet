using System.ComponentModel.DataAnnotations;

namespace task_manager_dotnet.DTOs;

public class UpdateUserDto
{
    [Required]
    public string Name { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
}
