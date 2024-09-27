using API_AIRFAST.Data;
using API_AIRFAST.Logic;
using API_AIRFAST.Models;

namespace API_AIRFAST.Services.LoginService;

public class LoginService : ILoginService
{
    private readonly AppDbContext _context;

    //private readonly LoginLogic _loginLogic;

    public LoginService(AppDbContext context)
    {
        _context = context;
        //_loginLogic = new LoginLogic();
    }

    public bool ValidarUsuario(string email, string contrasena)
    {
        // Verificar si existe un usuario con el correo y contraseña dados en la base de datos
        return _context.Usuarios.Any(u => u.Correo == email && u.Contrasena == contrasena);
    }

    public bool RegistrarUsuario(UsuarioModel nuevoUsuario)
    {
        // Verificar si el usuario existe
        if (_context.Usuarios.Any(u => u.Correo == nuevoUsuario.Correo))
        {
            return false; // El correo ya está en uso
        }

        // Verificar si el usuario ya esta en uso
        if (_context.Usuarios.Any(u => u.Usuario == nuevoUsuario.Usuario))
        {
            return false; // El usuario ya está en uso
        }

        // Verificar la edad del nuevo usuario (que no sea menor de 18 años)
        if (nuevoUsuario.FechaNacimiento > DateTime.Now.AddYears(-18))
        {
            return false; // El usuario es menor de 18 años
        }

        // Agregar el nuevo usuario a la base de datos
        _context.Usuarios.Add(nuevoUsuario);
        _context.SaveChanges();
        return true; // Registro exitoso
    }

}
