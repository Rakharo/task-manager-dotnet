namespace task_manager_dotnet.DTOs;

public class TaskResponseDto
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
