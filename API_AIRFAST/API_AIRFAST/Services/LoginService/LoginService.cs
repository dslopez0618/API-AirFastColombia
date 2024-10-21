using API_AIRFAST.Data;
using API_AIRFAST.Logic;
using API_AIRFAST.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;

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

    public (bool, int?) ValidarUsuario(string email, string contrasena)
    {
        // Verificar si existe un usuario con el correo y contraseña dados en la base de datos
        //return _context.Usuarios.Any(u => u.Correo == email && u.Contrasena == contrasena);
        var usuario = _context.Usuarios.SingleOrDefault(u => u.Correo == email && u.Contrasena == contrasena);

        if (usuario != null)
        {
            return (true, usuario.IdTipo); // credenciales correctas
        }

        return (false, null); // credenciales incorrectas
    }

    public (bool, string) RegistrarUsuario(UsuarioModel nuevoUsuario)
    {
        // Verificar si el usuario existe
        if (_context.Usuarios.Any(u => u.Correo == nuevoUsuario.Correo))
        {
            return (false, "Correo ya existe"); // El correo ya está en uso
        }

        // Verificar si el usuario ya esta en uso
        if (_context.Usuarios.Any(u => u.Usuario == nuevoUsuario.Usuario))
        {
            return (false, "Usuario ya existe"); // El usuario ya está en uso
        }

        // Verificar la edad del nuevo usuario (que no sea menor de 18 años)
        if (nuevoUsuario.FechaNacimiento > DateTime.Now.AddYears(-18))
        {
            return (false, "Debes ser mayor de 18 años para registrarte."); // El usuario es menor de 18 años
        }
        //nuevoUsuario.Id = 100;

        // Agregar el nuevo usuario a la base de datos
        _context.Usuarios.Add(nuevoUsuario);
        _context.SaveChanges();
        return (true, "exito"); // Registro exitoso
    }

    public bool EditarUsuario(string id, UsuarioModel usuario)
    {

        if (string.IsNullOrEmpty(id) || usuario == null)
        {
            return false;
        }

        if (!long.TryParse(id, out long userId))
        {
            return false; // Retornamos falso si el ID no es un número válido
        }

        // Buscamos el usuario en la base de datos
        UsuarioModel usuarioEditado = _context.Usuarios.FirstOrDefault(u => u.Id == userId);

        //UsuarioModel usuarioEditado = _context.Usuarios.FirstOrDefault(u => u.Id == long.Parse(id));

        if (usuarioEditado == null)
        {
            return false; // Si no se encuentra el usuario, retornamos falso
        }


        /*usuarioEditado.Correo = usuario.Correo;
        usuarioEditado.Usuario = usuario.Usuario;
        usuarioEditado.Contrasena = usuario.Contrasena;
        usuarioEditado.Nombre = usuario.Nombre;
        usuarioEditado.Apellido = usuario.Apellido;
        usuarioEditado.Documento = usuario.Documento;
        usuarioEditado.LugarNacimiento = usuario.Documento;
        usuarioEditado.FechaNacimiento = usuario.FechaNacimiento;
        usuarioEditado.Genero = usuario.Genero;*/

        // Actualizamos solo los campos relevantes que hayan sido proporcionados
        usuarioEditado.Correo = usuario.Correo ?? usuarioEditado.Correo;
        usuarioEditado.Usuario = usuario.Usuario ?? usuarioEditado.Usuario;
        usuarioEditado.Contrasena = usuario.Contrasena ?? usuarioEditado.Contrasena;
        usuarioEditado.Nombre = usuario.Nombre ?? usuarioEditado.Nombre;
        usuarioEditado.Apellido = usuario.Apellido ?? usuarioEditado.Apellido;
        usuarioEditado.Documento = usuario.Documento ?? usuarioEditado.Documento;
        usuarioEditado.LugarNacimiento = usuario.LugarNacimiento ?? usuarioEditado.LugarNacimiento;
        usuarioEditado.FechaNacimiento = usuario.FechaNacimiento != DateTime.MinValue
            ? usuario.FechaNacimiento
            : usuarioEditado.FechaNacimiento;
        usuarioEditado.Genero = usuario.Genero != 0 ? usuario.Genero : usuarioEditado.Genero;


        _context.Update(usuarioEditado);
        _context.SaveChanges();
        return true;

    }
}
