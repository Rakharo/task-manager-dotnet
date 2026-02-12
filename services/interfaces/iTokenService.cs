using task_manager_dotnet.Models;

namespace task_manager_dotnet.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}
