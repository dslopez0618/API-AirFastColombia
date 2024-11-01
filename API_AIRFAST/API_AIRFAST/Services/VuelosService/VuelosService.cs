using API_AIRFAST.Data;
using API_AIRFAST.Models;

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
}
