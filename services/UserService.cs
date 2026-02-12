
using task_manager_dotnet.Models;
using task_manager_dotnet.Repositories.Interfaces;
using task_manager_dotnet.Services.Interfaces;
using task_manager_dotnet.DTOs;


namespace task_manager_dotnet.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    private static UserResponseDto MapToResponse(User user)
    {
        return new UserResponseDto
        {
            Id = user.Id,
            Name = user.Name,
            Login = user.Login,
            Email = user.Email,
            Phone = user.Phone,
            Status = user.Status,
            CreatedAt = user.CreatedAt,
            Tasks = user.Tasks.Select(t => new TaskResponseDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                CreatedAt = t.CreatedAt
            }).ToList()
        };
    }


    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }


    public async Task<List<UserResponseDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(MapToResponse).ToList();
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return null;
        }

        return MapToResponse(user);
    }

    public async Task<UserResponseDto> CreateAsync(CreateUserDto dto)
    {
        var existingLogin = await _userRepository.GetByLoginAsync(dto.Login);
        if (existingLogin != null)
        {
            throw new ArgumentException("Login já está em uso.");
        }

        var existingEmail = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingEmail != null)
        {
            throw new ArgumentException("Email já está em uso.");
        }

        var passwordHash = _passwordHasher.Hash(dto.Password);

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

        var createdUser = await _userRepository.CreateAsync(user);

        return MapToResponse(createdUser);
    }

    public async Task<UserResponseDto> UpdateAsync(int id, UpdateUserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            throw new ArgumentException("Usuário não encontrado.");
        }
        var existingLogin = await _userRepository.GetByLoginAsync(dto.Login);
        if (existingLogin != null)
        {
            throw new ArgumentException("Login já está em uso.");
        }

        var existingEmail = await _userRepository.GetByEmailAsync(dto.Email);
        if (existingEmail != null)
        {
            throw new ArgumentException("Email já está em uso.");
        }


        user.Name = dto.Name;
        user.Login = dto.Login;
        user.Email = dto.Email;
        user.Phone = dto.Phone;
        user.UpdatedAt = DateTime.UtcNow;

        var updatedUser = await _userRepository.UpdateAsync(user);

        return MapToResponse(updatedUser);
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
    public async Task<User?> GetByLoginAsync(string login)
    {
        return (await _userRepository.GetAllAsync())
            .FirstOrDefault(u => u.Login == login);
    }

    public async Task<bool> ValidatePasswordAsync(string login, string password)
    {
        var user = (await _userRepository.GetAllAsync()).FirstOrDefault(u => u.Login == login);

        if (user == null || !user.Status)
        {
            return false;
        }

        return _passwordHasher.Verify(password, user.PasswordHash);
    }
}
