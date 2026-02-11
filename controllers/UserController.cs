using Microsoft.AspNetCore.Mvc;
using task_manager_dotnet.DTOs;
using task_manager_dotnet.Services.Interfaces;

namespace task_manager_dotnet.Controllers;

[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUserDto dto)
    {
        try
        {
            var createdUser = await _userService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdUser.Id },
                createdUser
            );
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateUserDto dto)  
    {
        try
        {
            var updatedUser = await _userService.UpdateAsync(id, dto);
            return Ok(updatedUser);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _userService.DeleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpPatch("{id}/disable")]
    public async Task<IActionResult> Disable(int id)
    {
        var success = await _userService.DisableAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}