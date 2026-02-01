using task_manager_dotnet.Models;

namespace task_manager_dotnet.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task<TaskItem> UpdateAsync(TaskItem task);
    Task<bool> DeleteAsync(int id);
    Task<bool> CompleteAsync(int id);
}
