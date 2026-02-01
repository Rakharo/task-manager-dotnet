using task_manager_dotnet.Models;

namespace task_manager_dotnet.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task<bool> CompleteAsync(int id);
}
