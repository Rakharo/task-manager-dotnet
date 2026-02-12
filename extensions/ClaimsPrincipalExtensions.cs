using System.Security.Claims;

namespace task_manager_dotnet.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim == null)
        {
            throw new UnauthorizedAccessException("Usuário não autenticado.");
        }

        return int.Parse(userIdClaim.Value);
    }
}
