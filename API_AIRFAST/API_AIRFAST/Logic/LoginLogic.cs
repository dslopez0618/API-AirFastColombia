using API_AIRFAST.Data;
using API_AIRFAST.Models;
using System.Linq;

namespace API_AIRFAST.Logic
{
    public class LoginLogic
    {
        private readonly AppDbContext _context;

        // Inyectar el contexto de la base de datos en el constructor
        public LoginLogic(AppDbContext context)
        {
            _context = context;
        }

        // Lógica para validar el inicio de sesión
        public bool ValidarUsuario(string email, string contrasena)
        {
            // Verificar si existe un usuario con el correo y contraseña dados en la base de datos
            return _context.Usuarios.Any(u => u.Correo == email && u.Contrasena == contrasena);
        }
    }
}
