using task_manager_dotnet.Models;
using task_manager_dotnet.DTOs;


namespace task_manager_dotnet.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(CreateTaskDto dto);
    Task<TaskItem> UpdateAsync(int id, UpdateTaskDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> CompleteAsync(int id);
}
