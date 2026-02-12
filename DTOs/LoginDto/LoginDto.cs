using System.ComponentModel.DataAnnotations;

namespace task_manager_dotnet.DTOs;

public class LoginDto
{
    [Required]
    public string Login { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
