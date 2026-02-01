using Microsoft.EntityFrameworkCore;
using task_manager_dotnet.Data;
using task_manager_dotnet.Models;
using task_manager_dotnet.Services.Interfaces;

namespace task_manager_dotnet.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
        {
            throw new ArgumentException("Título é obrigatório.");
        }

        task.CreatedAt = DateTime.UtcNow;

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {
        if(string.IsNullOrWhiteSpace(task.Title))
        {
            throw new ArgumentException("Título é obrigatório.");
        }

        task.UpdatedAt = DateTime.UtcNow;

        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var task = await GetByIdAsync(id);
        if(task == null)
        {
            throw new ArgumentException("Tarefa não encontrada.");
        } else
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

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
        await _context.SaveChangesAsync();

        return true;
    }
}
