using task_manager_dotnet.Models;
using task_manager_dotnet.DTOs;


namespace task_manager_dotnet.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(CreateUserDto dto);
    Task<User> UpdateAsync(int id, UpdateUserDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DisableAsync(int id);
}
