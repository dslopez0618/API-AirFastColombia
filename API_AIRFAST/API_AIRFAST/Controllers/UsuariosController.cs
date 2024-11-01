using API_AIRFAST.Models;
using API_AIRFAST.Services.LoginService;
using Microsoft.AspNetCore.Mvc;

namespace API_AIRFAST.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuariosController : ControllerBase
{
    private readonly ILoginService _loginService;

    public UsuariosController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpGet]
    [Route("obtener-todos")]
    public async Task<ActionResult<IEnumerable<UsuarioModel>>> ObtenerUsuariosActivos()
    {
        Console.WriteLine("Entra a ObtenerUsuariosActivos()");
        var usuarios = await _loginService.ObtenerUsuariosActivos();
        return Ok(usuarios);
    }
}
