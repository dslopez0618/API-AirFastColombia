using API_AIRFAST.Models;
using API_AIRFAST.Services.LoginService;
using Microsoft.AspNetCore.Mvc;

namespace API_AIRFAST.Controllers;

[ApiController]
[Route("api/registro")]
public class RegistroController : ControllerBase
{
    private readonly ILoginService _loginService;

    public RegistroController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost]
    public IActionResult RegistrarUsuario([FromBody] UsuarioModel nuevoUsuario)
    {

        try
        {
            /*
            if (_loginService.RegistrarUsuario(nuevoUsuario))
            {
                return Ok(new { mensaje = "Registro exitoso" });
            }
            else
            {
                return BadRequest(new { mensaje = "Error al registrar usuario. Verifique si el correo o el usuario ya existen o si es menor de edad." });
            }
            */
            (bool resBool, string mensaje) =  _loginService.RegistrarUsuario(nuevoUsuario);

            if (resBool)
            {
                //return Ok(new { mensaje = mensaje });
                return Ok(new { mensaje });
            }
            else
            {
                //return BadRequest(new { mensaje = "Error al registrar usuario. Verifique si el correo o el usuario ya existen o si es menor de edad." });
                return BadRequest(new { mensaje });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { mensaje = "Ocurrió un error en el servidor." });
        }
    }
}
