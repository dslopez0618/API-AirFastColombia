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

    [HttpPost]
    public IActionResult IniciarSesion([FromBody] UsuarioModel usuario)
    {
        Console.WriteLine("se consume desde el front");
        try
        {
            var (exito, idTipo) = _loginService.ValidarUsuario(usuario.Correo, usuario.Contrasena);

            //if (_loginService.ValidarUsuario(usuario.Correo, usuario.Contrasena))
            if(exito)
            {
                //return Ok(new { mensaje = "Inicio de sesión exitoso" });
                return Ok(new { exito = true, mensaje = "Inicio de sesión exitoso", userType = idTipo});

            }
            else
            {
                //return Unauthorized(new { mensaje = "Credenciales incorrectas" });
                return Ok(new { exito = false, mensaje = "Credenciales incorrectas"});
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { exito = false, mensaje = "Ocurrió un error en el servidor." });
        }
        /*{
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { mensaje = "Ocurrió un error en el servidor." });
        }*/

    }
}
