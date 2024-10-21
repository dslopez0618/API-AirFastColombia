using API_AIRFAST.Data;
using API_AIRFAST.Models;
using API_AIRFAST.Services.LoginService;
using Microsoft.AspNetCore.Mvc;

namespace API_AIRFAST.Controllers;

[ApiController]
[Route("api/editar-perfil")]
public class EditarPerfiController : Controller
{
    private readonly ILoginService _loginService;

    public EditarPerfiController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPatch]
    public IActionResult EditarUsuario(string id, [FromBody] UsuarioModel usuario)
    {
        if (string.IsNullOrEmpty(id) || usuario == null)
        {
            return BadRequest(new { mensaje = "El ID o los datos del usuario no pueden estar vacíos." });
        }

        try
        {
            /*
            if(_loginService.EditarUsuario(id, usuario))
            {
                return Ok(new { mensaje = "Usuario actualizado correctamente", usuario });
                //return View(usuario);
            }*/

            var actualizado = _loginService.EditarUsuario(id, usuario);
            if (actualizado)
            {
                return Ok(new { mensaje = "Usuario actualizado correctamente", usuario });
            }
            else
            {
                return NotFound(new { mensaje = "No se encontró el usuario para actualizar." });
            }

        }
        /*catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { mensaje = "Ocurrió un error en el servidor." });
        }
        return View(usuario);*/
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { mensaje = "Ocurrió un error en el servidor." });
        }
    }
}
