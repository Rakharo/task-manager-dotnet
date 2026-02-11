using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace task_manager_dotnet.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public required string Name { get; set; } = string.Empty;
    public required string Login { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public required string Email { get; set; }
    public string? Phone { get; set; }
    public bool Status { get; set; } = true; //Usuario ativo ou inativo
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}