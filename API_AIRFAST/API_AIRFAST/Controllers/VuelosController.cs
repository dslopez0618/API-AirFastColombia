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

    [HttpPut("editar-vuelo")]
    public async Task<IActionResult> EditarVuelo(int id, [FromBody] VuelosModel vuelo)
    {
        if (id != vuelo.Id)
        {
            return BadRequest("El ID del vuelo no coincide con el ID proporcionado.");
        }

        try
        {
            // Llamamos al servicio para editar el vuelo
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
