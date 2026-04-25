namespace ControlDeAsientos.Models;

public class Student : Entity
{
    public string Enrollment { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Major { get; set; } = string.Empty;

    public Student() : base() { }

    public Student(int id, string enrollment, string name, string major) : base(id)
    {
        Enrollment = enrollment;
        Name = name;
        Major = major;
    }
}
