using Microsoft.EntityFrameworkCore;
using SistemaPacientesAPI.Models;

namespace SistemaPacientesAPI.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Doctor> Doctores => Set<Doctor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paciente>().Property(p => p.Nombre).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Paciente>().Property(p => p.Cedula).HasMaxLength(20).IsRequired();
        modelBuilder.Entity<Paciente>().Property(p => p.Telefono).HasMaxLength(20);
        modelBuilder.Entity<Paciente>().HasIndex(p => p.Cedula).IsUnique();

        modelBuilder.Entity<Doctor>().Property(d => d.Nombre).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Doctor>().Property(d => d.Especialidad).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Doctor>().Property(d => d.Telefono).HasMaxLength(20);
    }
}
