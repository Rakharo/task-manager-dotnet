namespace task_manager_dotnet.DTOs;
public class UserResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Phone { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<TaskResponseDto> Tasks { get; set; } = new();
}
