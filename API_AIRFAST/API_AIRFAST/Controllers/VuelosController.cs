using API_AIRFAST.Data;
using API_AIRFAST.Models;
using API_AIRFAST.Services.VuelosService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_AIRFAST.Controllers;

[ApiController]
[Route("api/vuelos")]
public class VuelosController : ControllerBase
{
    private readonly IVuelosService _vuelosService;
    private readonly AppDbContext _dbContext;

    public VuelosController(IVuelosService vuelosService, AppDbContext dbContext)
    {   
        _vuelosService = vuelosService;
        _dbContext = dbContext;
    }

    /// <summary>
    /// CREA UN NUEVO VUELO.
    /// </summary>
    /// <param name="vuelo">OBJETO VUELO CON LOS DETALLES DEL NUEVO VUELO.</param>
    /// <returns>DEVUELVE EL OBJETO DEL VUELO CREADO.</returns>
    [HttpPost("crear-vuelo")]
    public async Task<IActionResult> CrearVuelo([FromBody] VuelosModel vuelo)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var vueloCreado = await _vuelosService.CrearVuelo(vuelo);

        return CreatedAtAction(nameof(CrearVuelo), new { id = vueloCreado.Id }, vueloCreado);

    }


    [HttpPut("editar-vuelo/{id}")]
    public async Task<IActionResult> EditarVuelo(int id, [FromBody] VuelosModel vuelo)
    {
        if (id != vuelo.Id)
        {
            return BadRequest("El ID del vuelo no coincide con el ID proporcionado.");
        }

        try
        {
            var resultado = await _vuelosService.EditarVueloAsync(vuelo);

            if (resultado)
            {
                return Ok(new { mensaje = "Vuelo actualizado exitosamente." });
            }
            else
            {
                return NotFound(new { mensaje = "Vuelo no encontrado." });
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, new { mensaje = "Ocurrió un error al actualizar el vuelo." });
        }
    }



    [HttpGet("proximo-id")]
    public async Task<ActionResult<int>> ObtenerProximoId()
    {
        // Consultar el último ID en la base de datos
        int ultimoId = await _dbContext.Vuelos.MaxAsync(v => v.Id);
        int proximoId = ultimoId + 1;
        return Ok(proximoId);
    }

    [HttpGet("{id}")]
    public IActionResult ObtenerVueloPorId(int id)
    {
        try
        {
            var vuelo = _dbContext.Vuelos.Find(id);
            if (vuelo == null)
            {
                return NotFound(new { mensaje = $"Vuelo con ID {id} no encontrado" });
            }
            return Ok(vuelo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener el vuelo con ID {id}: {ex.Message}");
            return StatusCode(500, new { mensaje = "Error al obtener el vuelo", error = ex.Message });
        }
    }


    


    /// <summary>
    /// OBTIENE LOS VUELOS CREADOS POR UN USUARIO, CON OPCIÓN DE FILTROS.
    /// </summary>
    /// <param name="usuarioId">ID DEL USUARIO QUE CREÓ LOS VUELOS.</param>
    /// <param name="campo">CAMPO A FILTRAR ("origen", "destino", "fecha", "id", "estado").</param>
    /// <param name="valor">VALOR A BUSCAR SEGÚN EL CAMPO INDICADO.</param>
    /// <returns>LISTA DE VUELOS SEGÚN LOS FILTROS APLICADOS.</returns>
    [HttpGet("obtener-vuelos")]
    public async Task<ActionResult<IEnumerable<VuelosModel>>> Obtenervuelos(
        [FromQuery] int usuarioId,
        [FromQuery] string campo = null,
        [FromQuery] string valor = null)
    {
        try
        {
            var vuelos = await _vuelosService.ObtenerVuelosPorUsuarioConFiltro(usuarioId, campo, valor);
            Console.WriteLine("llegó al 95");
            if (!vuelos.Any())
            {
                return NotFound(new { mensaje = "No se encontraron vuelos con los criterios especificados." });
            }

            return Ok(vuelos);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
        
    }

    // VuelosController.cs
    [HttpGet("ciudades-disponibles")]
    public ActionResult<List<string>> ObtenerCiudadesDisponibles([FromQuery] string tipoVuelo)
    {
        List<string> ciudades;

        if (tipoVuelo.ToLower() == "nacional")
        {
            ciudades = new List<string> { "Bogotá", "Medellín", "Cali", "Barranquilla", "Cartagena", "Pereira" }; // Ejemplo de capitales
        }
        else if (tipoVuelo.ToLower() == "internacional")
        {
            ciudades = new List<string> { "Pereira", "Bogotá", "Medellín", "Cali", "Cartagena", "Madrid", "Londres", "New York", "Buenos Aires", "Miami" };
        }
        else
        {
            return BadRequest("Tipo de vuelo inválido");
        }

        return Ok(ciudades);
    }
}
