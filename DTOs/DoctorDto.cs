namespace SistemaPacientesAPI.DTOs;

public class DoctorDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Especialidad { get; set; } = string.Empty;
    public string Telefono { get; set; } = string.Empty;
}
