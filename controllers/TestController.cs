using Microsoft.AspNetCore.Mvc;

namespace MinhaPrimeiraApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Controller funcionando 🚀");
    }
}
