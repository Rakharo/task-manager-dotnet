using Microsoft.AspNetCore.Mvc;
using task_manager_dotnet.DTOs;
using task_manager_dotnet.Services.Interfaces;

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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskService.GetAllAsync();
        return Ok(tasks);
    }

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

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTaskDto dto)
    {
        var createdTask = await _taskService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdTask.Id },
            createdTask
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateTaskDto dto)
    {
        try
        {
            var updatedTask = await _taskService.UpdateAsync(id, dto);
            return Ok(updatedTask);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _taskService.DeleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        var success = await _taskService.CompleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}