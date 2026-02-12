using task_manager_dotnet.Models;
using task_manager_dotnet.DTOs;


namespace task_manager_dotnet.Services.Interfaces;

public interface IUserService
{
    Task<List<UserResponseDto>> GetAllAsync();
    Task<UserResponseDto?> GetByIdAsync(int id);
    Task<UserResponseDto> CreateAsync(CreateUserDto dto);
    Task<UserResponseDto> UpdateAsync(int id, UpdateUserDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> DisableAsync(int id);
    Task<bool> EnableAsync(int id);
    Task<bool> ValidatePasswordAsync(string login, string password);
    Task<User?> GetByLoginAsync(string login);

}
