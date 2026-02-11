
using task_manager_dotnet.Models;
using task_manager_dotnet.Repositories.Interfaces;
using task_manager_dotnet.Services.Interfaces;
using task_manager_dotnet.DTOs;


namespace task_manager_dotnet.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IUserRepository _userRepository;

    private static TaskResponseDto MapToResponse(TaskItem task)
    {
        return new TaskResponseDto
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            CreatedAt = task.CreatedAt,
            UserId = task.UserId
        };
    }


    public TaskService(ITaskRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }


    public async Task<List<TaskResponseDto>> GetAllAsync()
    {
        var tasks = await _repository.GetAllAsync();
        return tasks.Select(MapToResponse).ToList();
    }

    public async Task<TaskResponseDto?> GetByIdAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
        {
            return null;
        }

        return MapToResponse(task);
    }

    public async Task<TaskResponseDto> CreateAsync(CreateTaskDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            throw new ArgumentException("Usuário não encontrado.");
        }

        var task = new TaskItem
        {
            Title = dto.Title,
            Description = dto.Description,
            UserId = dto.UserId,
            Status = false,
            CreatedAt = DateTime.UtcNow
        };

        var createdTask = await _repository.CreateAsync(task);

        return MapToResponse(createdTask);
    }

    public async Task<TaskResponseDto> UpdateAsync(int id, UpdateTaskDto dto)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
            throw new ArgumentException("Tarefa não encontrada.");

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.UpdatedAt = DateTime.UtcNow;

        var updatedTask = await _repository.UpdateAsync(task);

        return MapToResponse(updatedTask);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
        {
            throw new ArgumentException("Tarefa não encontrada.");
        }
        else
        {
            await _repository.RemoveAsync(task);
            return true;
        }
    }

    public async Task<bool> CompleteAsync(int id)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
        {
            return false;
        }

        task.Status = true;
        await _repository.UpdateAsync(task);

        return true;
    }
}
