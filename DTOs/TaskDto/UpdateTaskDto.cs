
namespace task_manager_dotnet.DTOs;

public class UpdateTaskDto

{
    public string Title { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public bool Status { get; set; } = false;
}
