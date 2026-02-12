using task_manager_dotnet.Models;

namespace task_manager_dotnet.Repositories.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAllAsync(int userId);
    Task<TaskItem?> GetByIdAndUserIdAsync(int id, int userId);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task<TaskItem> UpdateAsync(TaskItem task);
    Task RemoveAsync(TaskItem task);
}