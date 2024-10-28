using API_AIRFAST.Services.LoginService;
using Microsoft.AspNetCore.Mvc;

namespace API_AIRFAST.Controllers;

[ApiController]
[Route("api/recuperar-contrasena")]
public class RecuperarContrasenaController : ControllerBase
{
    private readonly ILoginService _loginService;

    public RecuperarContrasenaController(ILoginService loginService)
    {
        _loginService = loginService;
    }


}
