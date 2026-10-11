using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaPacientesAPI.Context;
using SistemaPacientesAPI.DTOs;
using SistemaPacientesAPI.Models;

namespace SistemaPacientesAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly AppDbContext _context;

    public PacientesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<PacienteDto>>> Get()
    {
        var pacientes = await _context.Pacientes.AsNoTracking()
            .OrderBy(p => p.Id)
            .Select(p => new PacienteDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Cedula = p.Cedula,
                Edad = p.Edad,
                Telefono = p.Telefono
            }).ToListAsync();

        return Ok(pacientes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PacienteDto>> GetById(int id)
    {
        var paciente = await _context.Pacientes.AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new PacienteDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Cedula = p.Cedula,
                Edad = p.Edad,
                Telefono = p.Telefono
            }).FirstOrDefaultAsync();

        if (paciente == null)
            return NotFound("Paciente no encontrado.");

        return Ok(paciente);
    }

    [HttpPost]
    public async Task<ActionResult<PacienteDto>> Post(PacienteCrearDto dto)
    {
        if (await _context.Pacientes.AnyAsync(p => p.Cedula == dto.Cedula))
            return Conflict("Ya existe un paciente con esa cédula.");

        var paciente = new Paciente
        {
            Nombre = dto.Nombre,
            Cedula = dto.Cedula,
            Edad = dto.Edad,
            Telefono = dto.Telefono
        };

        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();

        var respuesta = new PacienteDto
        {
            Id = paciente.Id,
            Nombre = paciente.Nombre,
            Cedula = paciente.Cedula,
            Edad = paciente.Edad,
            Telefono = paciente.Telefono
        };

        return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, respuesta);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, PacienteCrearDto dto)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null)
            return NotFound("Paciente no encontrado.");

        if (await _context.Pacientes.AnyAsync(p => p.Cedula == dto.Cedula && p.Id != id))
            return Conflict("Ya existe otro paciente con esa cédula.");

        paciente.Nombre = dto.Nombre;
        paciente.Cedula = dto.Cedula;
        paciente.Edad = dto.Edad;
        paciente.Telefono = dto.Telefono;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null)
            return NotFound("Paciente no encontrado.");

        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
