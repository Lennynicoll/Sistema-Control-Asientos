namespace ControlDeAsientos.Models;

public class Assignment : Entity
{
    public int StudentId { get; set; }
    public int SeatId { get; set; }
    public DateTime AssignmentDate { get; set; }

    public Assignment() : base() { }

    public Assignment(int id, int studentId, int seatId, DateTime assignmentDate) : base(id)
    {
        StudentId = studentId;
        SeatId = seatId;
        AssignmentDate = assignmentDate;
    }
}
