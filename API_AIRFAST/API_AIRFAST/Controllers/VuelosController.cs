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
}
