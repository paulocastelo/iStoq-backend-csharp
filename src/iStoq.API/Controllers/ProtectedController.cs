using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iStoq.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProtectedController : ControllerBase
{
    [HttpGet("secret")]
    [Authorize]
    public IActionResult GetSecret()
    {
        var user = User.Identity?.Name ?? "Usuário desconhecido";
        return Ok(new { message = $"Olá, {user}! Você acessou um endpoint protegido." });
    }
}
