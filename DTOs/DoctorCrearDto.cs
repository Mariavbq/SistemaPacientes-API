using System.ComponentModel.DataAnnotations;

namespace SistemaPacientesAPI.DTOs;

public class DoctorCrearDto
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La especialidad es obligatoria")]
    [StringLength(100)]
    public string Especialidad { get; set; } = string.Empty;

    [StringLength(20)]
    public string Telefono { get; set; } = string.Empty;
}
