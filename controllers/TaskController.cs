using Microsoft.AspNetCore.Mvc;
using task_manager_dotnet.DTOs;
using task_manager_dotnet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using task_manager_dotnet.Extensions;

namespace task_manager_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    private int UserId => User.GetUserId(); // Propriedade para obter o ID do usuário autenticado a partir dos claims

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }


    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskService.GetAllAsync(UserId);
        return Ok(tasks);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id, UserId);
        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post(CreateTaskDto dto)
    {
        var createdTask = await _taskService.CreateAsync(dto, UserId);
        return Ok(createdTask);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateTaskDto dto)
    {
        try
        {
            var updatedTask = await _taskService.UpdateAsync(id, dto, UserId);
            return Ok(updatedTask);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _taskService.DeleteAsync(id, UserId);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [Authorize]
    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var success = await _taskService.CompleteAsync(id, UserId);
        if (!success)
            return NotFound();

        return NoContent();
    }
}