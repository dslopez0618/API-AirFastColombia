using API_AIRFAST.Models;
using API_AIRFAST.Services.LoginService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_AIRFAST.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;
    private readonly IConfiguration _configuration;

    public LoginController(ILoginService loginService, IConfiguration configuration)
    {
        _loginService = loginService;
        _configuration = configuration;
    }

    [HttpPost]
    public IActionResult IniciarSesion([FromBody] UsuarioModel usuario)
    {
        Console.WriteLine("se consume desde el front");
        try
        {
            var (exito, idTipo, idUsuario) = _loginService.ValidarUsuario(usuario.Correo, usuario.Contrasena);

            //if (_loginService.ValidarUsuario(usuario.Correo, usuario.Contrasena))
            if(exito)
            {
                var token = GenerateJwtToken(usuario.Correo);

                //return Ok(new { mensaje = "Inicio de sesión exitoso" });
                return Ok(new { exito = true, mensaje = "Inicio de sesión exitoso", userType = idTipo, userId = idUsuario, token});

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

    private string GenerateJwtToken(string email)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
