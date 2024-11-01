using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_AIRFAST.Models;

[Table("vuelos", Schema = "app")]
public class VuelosModel
{
    /// <summary>
    /// ID DEL VUELO AUTOGENERADO.
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// FECHA DE SALIDA DEL VUELO.
    /// </summary>
    [Column("fecha_vuelo")]
    public DateTime FechaVuelo { get; set; }

    /// <summary>
    /// HORA DE SALIDA DEL VUELO.
    /// </summary>
    [Column("hora_vuelo")]
    public TimeSpan HoraVuelo { get; set; }

    /// <summary>
    /// CIUDAD DE ORIGEN DEL VUELO.
    /// </summary>
    [Column("origen")]
    public string Origen { get; set; }

    /// <summary>
    /// CIUDAD DE DESTINO DEL VUELO.
    /// </summary>
    [Column("destino")]
    public string Destino { get; set; }

    /// <summary>
    /// TIEMPO ESTIMADO DE DURACION DEL VUELO.
    /// </summary>
    [Column("tiempo_de_vuelo")]
    public TimeSpan TiempoDeVuelo { get; set; }

    /// <summary>
    /// ID DEL TIPO DE VUELO (NACIONAL O INTERNACIONAL).
    /// </summary>
    [Column("id_tipo_vuelo")]
    public int IdTipoVuelo { get; set; }


    /// <summary>
    /// FECHA DE LLEGADA DEL VUELO.
    /// </summary>
    [Column("fecha_llegada")]
    public DateTime FechaLlegada { get; set; }

    /// <summary>
    /// HORA DE LLEGADA DEL VUELO.
    /// </summary>
    [Column("hora_llegada")]
    public TimeSpan HoraLlegada { get; set; }

    /// <summary>
    /// COSTO DEL VUELO POR PERSONA.
    /// </summary>
    [Column("costo_por_persona")]
    public decimal CostoPorPersona { get; set; }

}

