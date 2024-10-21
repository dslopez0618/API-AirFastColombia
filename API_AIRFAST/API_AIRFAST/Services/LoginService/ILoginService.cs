using API_AIRFAST.Models;

namespace API_AIRFAST.Services.LoginService;

public interface ILoginService
{
    (bool, int?) ValidarUsuario(string email, string contrasena);
    (bool, string) RegistrarUsuario(UsuarioModel nuevoUsuario);

    bool EditarUsuario(string id, UsuarioModel usuario);
}
