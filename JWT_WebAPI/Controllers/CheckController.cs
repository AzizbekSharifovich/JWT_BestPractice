using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWT_WebAPI.Controllers;

[Authorize]
[Route("api/[check]")]
[ApiController]
public class CheckController : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet("adminText")]
    public string AdminText()
    {
        return "This is only Admin";
    }
}
