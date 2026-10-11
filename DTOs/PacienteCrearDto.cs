using System.ComponentModel.DataAnnotations;

namespace SistemaPacientesAPI.DTOs;

public class PacienteCrearDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La cédula es obligatoria")]
    [StringLength(20)]
    public string Cedula { get; set; } = string.Empty;

    [Range(0, 120, ErrorMessage = "La edad debe estar entre 0 y 120")]
    public int Edad { get; set; }

    [StringLength(20)]
    public string Telefono { get; set; } = string.Empty;
}
