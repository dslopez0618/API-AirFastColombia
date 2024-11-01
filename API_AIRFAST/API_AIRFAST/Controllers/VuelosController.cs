using API_AIRFAST.Models;
using API_AIRFAST.Services.VuelosService;
using Microsoft.AspNetCore.Mvc;

namespace API_AIRFAST.Controllers;

[ApiController]
[Route("api/vuelos")]
public class VuelosController : ControllerBase
{
    private readonly IVuelosService _vuelosService;

    public VuelosController(IVuelosService vuelosService)
    {   
        _vuelosService = vuelosService;
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
}
