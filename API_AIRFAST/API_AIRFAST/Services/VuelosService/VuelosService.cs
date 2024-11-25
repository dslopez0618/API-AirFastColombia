using API_AIRFAST.Data;
using API_AIRFAST.Models;
using Microsoft.EntityFrameworkCore;

namespace API_AIRFAST.Services.VuelosService;

public class VuelosService : IVuelosService
{
    private readonly AppDbContext _context;

    public VuelosService(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// AGREGA UN NUEVO VUELO AL SISTEMA.
    /// </summary>
    /// <param name="nuevoVuelo">OBJETO VUELO QUE CONTIENE LOS DETALLES A AGREGAR.</param>
    /// <returns>DEVUELVE EL OBJETO VUELO CREADO.</returns>
    public async Task<VuelosModel> CrearVuelo(VuelosModel nuevoVuelo)
    {
        _context.Vuelos.Add(nuevoVuelo); // AGREGA EL VUELO A LA BASE DE DATOS
        await _context.SaveChangesAsync(); // GUARDA LOS CAMBIOS
        return nuevoVuelo; // DEVUELVE EL VUELO CREADO
    }

    public async Task<bool> EditarVueloAsync(VuelosModel vuelo)
    {
        var vueloExistente = await _context.Vuelos.FindAsync(vuelo.Id);

        if (vueloExistente == null)
        {
            return false; // Vuelo no encontrado
        }

        // Actualizamos las propiedades necesarias
        vueloExistente.FechaVuelo = vuelo.FechaVuelo;
        vueloExistente.HoraVuelo = vuelo.HoraVuelo;
        vueloExistente.Origen = vuelo.Origen;
        vueloExistente.Destino = vuelo.Destino;
        vueloExistente.TiempoDeVuelo = vuelo.TiempoDeVuelo;
        vueloExistente.IdTipoVuelo = vuelo.IdTipoVuelo;
        vueloExistente.FechaLlegada = vuelo.FechaLlegada;
        vueloExistente.HoraLlegada = vuelo.HoraLlegada;
        vueloExistente.CostoPorPersona = vuelo.CostoPorPersona;

        // Campos de auditoría
        vueloExistente.ModificadoPor = "usuario_actual"; // Reemplaza con el valor adecuado
        vueloExistente.FechaModificacion = DateTime.Now;

        _context.Vuelos.Update(vueloExistente); // No es estrictamente necesario si se usa EF Core tracking
        await _context.SaveChangesAsync(); // Guardamos los cambios en la base de datos
        return true;
    }


    public async Task<IEnumerable<VuelosModel>> ObtenerTodosLosVuelosAsync()
    {
        // SE REALIZA UNA CONSULTA A LA BASE DE DATOS PARA OBTENER TODOS LOS VUELOS
        return await _context.Vuelos.ToListAsync();
    }


    /// <summary>
    /// OBTIENE VUELOS POR USUARIO CON OPCIÓN DE FILTROS.
    /// </summary>
    /// <param name="usuarioId">ID DEL USUARIO QUE CREÓ LOS VUELOS.</param>
    /// <param name="campo">CAMPO A FILTRAR ("origen", "destino", "fecha", "id", "estado").</param>
    /// <param name="valor">VALOR A BUSCAR SEGÚN EL CAMPO INDICADO.</param>
    /// <returns>LISTA DE VUELOS FILTRADOS.</returns>
    public async Task<IEnumerable<VuelosModel>> ObtenerVuelosPorUsuarioConFiltro(int usuarioId, string campo = null, string valor = null)
    {
        var query = _context.Vuelos.AsQueryable();

        // Filtro por usuario
        query = query.Where(v => v.CreadoPor == usuarioId.ToString());

        // filtros dinámicos segun el campo
        if (!string.IsNullOrEmpty(campo) && !string.IsNullOrEmpty(valor))
        {
            switch (campo.ToLower())
            {
                case "origen":
                    query = query.Where(v => v.Origen == valor);
                    break;
                case "destino":
                    query = query.Where(v => v.Destino == valor);
                    break;
                case "fecha":
                    if (DateTime.TryParse(valor, out var fecha))
                    {
                        query = query.Where(v => v.FechaVuelo.Date == fecha.Date);
                    }
                    break;
                case "id":
                    if (int.TryParse(valor, out var id))
                    {
                        query = query.Where(v => v.Id == id);
                    }
                    break;
                case "estado":
                    if (int.TryParse(valor, out var estado))
                    {
                        query = query.Where(v => v.Estado == estado);
                    }
                    break;
                default:
                    throw new ArgumentException("Campo de filtro no válido");
            }
        }

        return await query.ToListAsync();
    }


}