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
        using (var context = _context)
        {
            var vueloExistente = await context.Vuelos.FindAsync(vuelo.Id);

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

            await context.SaveChangesAsync(); // Guardamos los cambios en la base de datos
            return true;
        }
    }


    public async Task<IEnumerable<VuelosModel>> ObtenerTodosLosVuelosAsync()
    {
        // SE REALIZA UNA CONSULTA A LA BASE DE DATOS PARA OBTENER TODOS LOS VUELOS
        return await _context.Vuelos.ToListAsync();
    }
}