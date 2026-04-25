using ControlDeAsientos.Data;
using ControlDeAsientos.Models;
using System.Linq;

namespace ControlDeAsientos.Services;

public class StudentService
{
    public void Create(Student student)
    {
        using var context = new AppDbContext();
        context.Students.Add(student);
        context.SaveChanges();
    }

    public int GetIdByEnrollment(string enrollment)
    {
        using var context = new AppDbContext();
        var student = context.Students.FirstOrDefault(s => s.Enrollment == enrollment);
        return student?.Id ?? 0;
    }
}
