using API_AIRFAST.Data;
using API_AIRFAST.Models;
using Microsoft.EntityFrameworkCore;

namespace API_AIRFAST.Services.UsuarioService
{
    public interface IUsuarioService
    {
        Task<bool> RecargarSaldo(long usuarioId, decimal monto);
        Task<decimal> ObtenerSaldo(long usuarioId);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        // Recargar saldo
        public async Task<bool> RecargarSaldo(long usuarioId, decimal monto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId && u.IdTipo == 3); // Verificar que sea cliente
            if (usuario == null)
            {
                return false; // Usuario no encontrado o no es cliente
            }

            usuario.SaldoDisponible += monto; // Añadir el monto al saldo disponible
            _context.Update(usuario); // Actualizar el usuario
            await _context.SaveChangesAsync(); // Guardar los cambios en la base de datos
            return true;
        }

        // Consultar saldo
        public async Task<decimal> ObtenerSaldo(long usuarioId)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == usuarioId && u.IdTipo == 3); // Verificar que sea cliente
            if (usuario == null)
            {
                return 0; // Si el usuario no existe o no es cliente, devolver 0
            }

            return usuario.SaldoDisponible; // Retornar el saldo disponible
        }
    }
}
