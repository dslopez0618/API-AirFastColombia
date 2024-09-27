using API_AIRFAST.Models;
using API_AIRFAST.Services.LoginService;
using Microsoft.AspNetCore.Mvc;

namespace API_AIRFAST.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("iniciar-sesion")]
    public IActionResult IniciarSesion([FromBody] UsuarioModel usuario)
    {
        Console.WriteLine("se consume desde el front");
        try
        {
            if (_loginService.ValidarUsuario(usuario.Email, usuario.Contrasena))
            {
                return Ok(new { mensaje = "Inicio de sesión exitoso" });
            }
            else
            {
                return Unauthorized(new { mensaje = "Credenciales incorrectas" });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { mensaje = "Ocurrió un error en el servidor." });
        }

    }
}
