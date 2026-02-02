using Microsoft.EntityFrameworkCore;
using task_manager_dotnet.Data;
using task_manager_dotnet.Models;
using task_manager_dotnet.Repositories.Interfaces;

namespace task_manager_dotnet.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks.ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return task;

    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {

        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task RemoveAsync(TaskItem task)
    {
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
    }
}