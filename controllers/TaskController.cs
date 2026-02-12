using Microsoft.AspNetCore.Mvc;
using task_manager_dotnet.DTOs;
using task_manager_dotnet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace task_manager_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {

        var task = await _taskService.GetByIdAsync(id);
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
        var userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );

        var createdTask = await _taskService.CreateAsync(dto, userId);
        return Ok(createdTask);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateTaskDto dto)
    {

        var userId = int.Parse(
            User.FindFirst(ClaimTypes.NameIdentifier)!.Value
        );

        try
        {
            var updatedTask = await _taskService.UpdateAsync(id, dto, userId);
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
        var success = await _taskService.DeleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [Authorize]
    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var success = await _taskService.CompleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}