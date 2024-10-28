using API_AIRFAST.Services.LoginService;
using Microsoft.AspNetCore.Mvc;

namespace API_AIRFAST.Controllers;

[ApiController]
[Route("api/desactivar-usuario")]
public class DesactivarUsuarioController : ControllerBase
{
    private readonly ILoginService _loginService;

    public DesactivarUsuarioController (ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPatch]
    //[Route("desactiar")]
    public IActionResult DesactivarUsuario(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest(new { mensaje = "El ID del usuario no puede estar vacío." });
        }

        try
        {
            var actualizado = _loginService.DesactivarUsuario(id);
            if (actualizado)
            {
                return Ok(new { mensaje = "Usuario actualizado correctamente"});
            }
            else
            {
                return NotFound(new { mensaje = "No se encontró el usuario para desactivar." });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { mensaje = "Ocurrió un error en el servidor." });
        }
    }
}
