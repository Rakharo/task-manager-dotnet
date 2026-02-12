using task_manager_dotnet.DTOs;


namespace task_manager_dotnet.Services.Interfaces;

public interface ITaskService
{
    Task<List<TaskResponseDto>> GetAllAsync(int userId);
    Task<TaskResponseDto?> GetByIdAsync(int id, int userId);
    Task<TaskResponseDto> CreateAsync(CreateTaskDto dto, int userId);
    Task<TaskResponseDto> UpdateAsync(int id, UpdateTaskDto dto, int userId);
    Task<bool> DeleteAsync(int id, int userId);
    Task<bool> CompleteAsync(int id, int userId);
}
