using ControlDeAsientos.Models;
using Microsoft.EntityFrameworkCore;

namespace ControlDeAsientos.Data;

public class AppDbContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Assignment> Assignments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS01;Database=SistemaDeControlAsientosDB;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().ToTable("Estudiantes");
        modelBuilder.Entity<Student>().Property(s => s.Id).HasColumnName("EstudianteId");
        modelBuilder.Entity<Student>().Property(s => s.Enrollment).HasColumnName("Matricula");
        modelBuilder.Entity<Student>().Property(s => s.Name).HasColumnName("Nombre");
        modelBuilder.Entity<Student>().Property(s => s.Major).HasColumnName("Carrera");

        modelBuilder.Entity<Seat>().ToTable("Asientos");
        modelBuilder.Entity<Seat>().Property(s => s.Id).HasColumnName("AsientoId");
        modelBuilder.Entity<Seat>().Property(s => s.Row).HasColumnName("Fila");
        modelBuilder.Entity<Seat>().Property(s => s.Number).HasColumnName("Numero");
        modelBuilder.Entity<Seat>().Property(s => s.Status).HasColumnName("Estado");

        modelBuilder.Entity<Assignment>().ToTable("Asignaciones");
        modelBuilder.Entity<Assignment>().Property(a => a.Id).HasColumnName("AsignacionId");
        modelBuilder.Entity<Assignment>().Property(a => a.StudentId).HasColumnName("EstudianteId");
        modelBuilder.Entity<Assignment>().Property(a => a.SeatId).HasColumnName("AsientoId");
        modelBuilder.Entity<Assignment>().Property(a => a.AssignmentDate).HasColumnName("FechaAsignacion");
    }
}
