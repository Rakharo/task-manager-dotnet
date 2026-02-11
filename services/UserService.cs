
using task_manager_dotnet.Models;
using task_manager_dotnet.Repositories.Interfaces;
using task_manager_dotnet.Services.Interfaces;
using task_manager_dotnet.DTOs;


namespace task_manager_dotnet.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;


    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User> CreateAsync(CreateUserDto dto)
    {
        var existingLogin = await _userRepository.GetByLoginAsync(dto.Login);
        if(existingLogin != null)
        {
            throw new ArgumentException("Login já está em uso.");
        }
        
        var existingEmail = await _userRepository.GetByEmailAsync(dto.Email);
        if(existingEmail != null)
        {
            throw new ArgumentException("Email já está em uso.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var user = new User
        {
            Name = dto.Name,
            Login = dto.Login,
            Email = dto.Email,
            Phone = dto.Phone,
            PasswordHash = passwordHash,
            Status = true,
            CreatedAt = DateTime.UtcNow
        };

        return await _userRepository.CreateAsync(user);
    }

    public async Task<User> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new ArgumentException("Usuário não encontrado.");
        }
        var existingLogin = await _userRepository.GetByLoginAsync(dto.Login);
        if(existingLogin != null)
        {
            throw new ArgumentException("Login já está em uso.");
        }
        
        var existingEmail = await _userRepository.GetByEmailAsync(dto.Email);
        if(existingEmail != null)
        {
            throw new ArgumentException("Email já está em uso.");
        }


        user.Name = dto.Name;
        user.Login = dto.Login;
        user.Email = dto.Email;
        user.Phone = dto.Phone;
        user.UpdatedAt = DateTime.UtcNow;

        return await _userRepository.UpdateAsync(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new ArgumentException("Usuário não encontrado.");
        }
        else
        {
            await _userRepository.RemoveAsync(user);
            return true;
        }
    }

    public async Task<bool> DisableAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return false;
        }

        user.Status = false;
        user.UpdatedAt = DateTime.UtcNow;
        await _userRepository.UpdateAsync(user);

        return true;
    }
}
