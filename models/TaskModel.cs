using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace task_manager_dotnet.Models;

public class TaskItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    public required string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}