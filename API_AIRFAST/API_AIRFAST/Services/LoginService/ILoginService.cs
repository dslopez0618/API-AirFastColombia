using API_AIRFAST.Models;

namespace API_AIRFAST.Services.LoginService;

public interface ILoginService
{
    (bool, int?, long?) ValidarUsuario(string email, string contrasena);
    (bool, string) RegistrarUsuario(UsuarioModel nuevoUsuario);

    bool EditarUsuario(string id, UsuarioModel usuario);

    bool DesactivarUsuario(string id);
}
