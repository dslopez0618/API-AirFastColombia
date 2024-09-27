using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_AIRFAST.Models;

[Table("usuario", Schema = "app")]
public class UsuarioModel
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("correo")]
    [Required]
    [MaxLength(255)]
    public string Correo { get; set; }

    [Column("contrasena")]
    [Required]
    [MaxLength(255)]
    public string Contrasena { get; set; }

    [Column("usuario")]
    [MaxLength(255)]
    public string? Usuario { get; set; }

    [Column("id_tipo")]
    public int? IdTipo { get; set; }

    [Column("notis")]
    public bool? Notis { get; set; }

    [Column("estado")]
    public bool? Estado { get; set; }

    [Column("documento")]
    [MaxLength(255)]
    public string? Documento { get; set; }

    [Column("nombre")]
    [MaxLength(255)]
    public string? Nombre { get; set; }

    [Column("apellido")]
    [MaxLength(255)]
    public string? Apellido { get; set; }

    [Column("fecha_nacimiento")]
    public DateTime? FechaNacimiento { get; set; }

    [Column("lugar_nacimiento")]
    [MaxLength(255)]
    public string? LugarNacimiento { get; set; }

    [Column("direccion")]
    [MaxLength(255)]
    public string? Direccion { get; set; }

    [Column("genero")]
    public short? Genero { get; set; }
}
