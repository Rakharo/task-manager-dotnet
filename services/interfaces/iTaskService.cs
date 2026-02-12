using task_manager_dotnet.Models;
using task_manager_dotnet.DTOs;


namespace task_manager_dotnet.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskResponseDto>> GetAllAsync();
    Task<TaskResponseDto?> GetByIdAsync(int id);
    Task<TaskResponseDto> CreateAsync(CreateTaskDto dto, int userId);
    Task<TaskResponseDto> UpdateAsync(int id, UpdateTaskDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> CompleteAsync(int id);
}
