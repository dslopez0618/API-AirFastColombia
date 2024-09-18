namespace API_AIRFAST.Services.LoginService;

public interface ILoginService
{
    bool ValidarUsuario(string email, string contrasena);
}
