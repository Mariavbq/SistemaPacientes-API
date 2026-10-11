using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPacientesAPI.Context;
using SistemaPacientesAPI.DTOs;
using SistemaPacientesAPI.Models;

namespace SistemaPacientesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DoctoresController : ControllerBase
{
    private readonly AppDbContext _context;

    public DoctoresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<DoctorDto>>> Get()
    {
        var doctores = await _context.Doctores.AsNoTracking()
            .OrderBy(d => d.Id)
            .Select(d => new DoctorDto
            {
                Id = d.Id,
                Nombre = d.Nombre,
                Especialidad = d.Especialidad,
                Telefono = d.Telefono
            }).ToListAsync();

        return Ok(doctores);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DoctorDto>> GetById(int id)
    {
        var doctor = await _context.Doctores.AsNoTracking()
            .Where(d => d.Id == id)
            .Select(d => new DoctorDto
            {
                Id = d.Id,
                Nombre = d.Nombre,
                Especialidad = d.Especialidad,
                Telefono = d.Telefono
            }).FirstOrDefaultAsync();

        if (doctor == null)
            return NotFound("Doctor no encontrado.");

        return Ok(doctor);
    }

    [HttpPost]
    public async Task<ActionResult<DoctorDto>> Post(DoctorCrearDto dto)
    {
        var doctor = new Doctor
        {
            Nombre = dto.Nombre,
            Especialidad = dto.Especialidad,
            Telefono = dto.Telefono
        };

        _context.Doctores.Add(doctor);
        await _context.SaveChangesAsync();

        var respuesta = new DoctorDto
        {
            Id = doctor.Id,
            Nombre = doctor.Nombre,
            Especialidad = doctor.Especialidad,
            Telefono = doctor.Telefono
        };

        return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, respuesta);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, DoctorCrearDto dto)
    {
        var doctor = await _context.Doctores.FindAsync(id);
        if (doctor == null)
            return NotFound("Doctor no encontrado.");

        doctor.Nombre = dto.Nombre;
        doctor.Especialidad = dto.Especialidad;
        doctor.Telefono = dto.Telefono;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var doctor = await _context.Doctores.FindAsync(id);
        if (doctor == null)
            return NotFound("Doctor no encontrado.");

        _context.Doctores.Remove(doctor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
