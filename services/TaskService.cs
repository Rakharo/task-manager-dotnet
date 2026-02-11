
using task_manager_dotnet.Models;
using task_manager_dotnet.Repositories.Interfaces;
using task_manager_dotnet.Services.Interfaces;
using task_manager_dotnet.DTOs;


namespace task_manager_dotnet.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IUserRepository _userRepository;


    public TaskService(ITaskRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }


    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<TaskItem> CreateAsync(CreateTaskDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);
        if(user == null)
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

        return await _repository.CreateAsync(task);
    }

    public async Task<TaskItem> UpdateAsync(int id, UpdateTaskDto dto)
    {
        var task = await _repository.GetByIdAsync(id);
        if (task == null)
            throw new ArgumentException("Tarefa não encontrada.");

        task.Title = dto.Title;
        task.Description = dto.Description;
        task.UpdatedAt = DateTime.UtcNow;

        return await _repository.UpdateAsync(task);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await GetByIdAsync(id);
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
        var task = await GetByIdAsync(id);
        if (task == null)
        {
            return false;
        }

        task.Status = true;
        await _repository.UpdateAsync(task);

        return true;
    }
}
