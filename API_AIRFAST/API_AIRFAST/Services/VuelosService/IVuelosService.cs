using API_AIRFAST.Models;

namespace API_AIRFAST.Services.VuelosService;

public interface IVuelosService
{
    /// <summary>
    /// AGREGA UN NUEVO VUELO AL SISTEMA.
    /// </summary>
    /// <param name="nuevoVuelo">OBJETO VUELO QUE CONTIENE LOS DETALLES A AGREGAR.</param>
    /// <returns>DEVUELVE EL OBJETO VUELO CREADO.</returns>
    Task<VuelosModel> CrearVuelo(VuelosModel nuevoVuelo);

    Task<bool> EditarVueloAsync(VuelosModel nuevoVuelo);

    Task<IEnumerable<VuelosModel>> ObtenerTodosLosVuelosAsync();

    Task<IEnumerable<VuelosModel>> ObtenerVuelosPorUsuarioConFiltro(int usuarioId, string campo, string valor);
}
