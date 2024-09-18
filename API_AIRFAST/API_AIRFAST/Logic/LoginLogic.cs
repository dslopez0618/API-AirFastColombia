using API_AIRFAST.Models;

namespace API_AIRFAST.Logic;

public class LoginLogic
{
    // Aquí puedes almacenar los usuarios temporalmente
    private List<UsuarioModel> usuarios = new List<UsuarioModel>
    {
        new UsuarioModel { Email = "usuario1@example.com", Contrasena = "password1" },
        new UsuarioModel { Email = "usuario2@example.com", Contrasena = "password2" }
    };

    // Lógica para validar el inicio de sesión
    public bool ValidarUsuario(string email, string contrasena)
    {
        return usuarios.Any(u => u.Email == email && u.Contrasena == contrasena);
    }
}
