using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API_AIRFAST.Models;

public class TipoDeVueloModel
{
    /// <summary>
    /// ID DEL TIPO DE VUELO.
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// DESCRIPCION DEL TIPO DE VUELO (NACIONAL O INTERNACIONAL).
    /// </summary>
    [Column("descripcion")]
    public string Descripcion { get; set; }
}
