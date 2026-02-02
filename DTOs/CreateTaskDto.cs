using System.ComponentModel.DataAnnotations;

namespace task_manager_dotnet.DTOs;

public class CreateTaskDto
{
    
    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = null!;
    public string? Description { get; set; } = null!;
}
