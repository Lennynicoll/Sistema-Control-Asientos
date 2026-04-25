using ControlDeAsientos.Data;
using ControlDeAsientos.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ControlDeAsientos.Services;

public class SeatService
{
    public List<Seat> GetAll()
    {
        using var context = new AppDbContext();
        return context.Seats.ToList();
    }

    public Seat? GetByPosition(string row, int number)
    {
        using var context = new AppDbContext();
        return context.Seats.FirstOrDefault(s => s.Row == row && s.Number == number);
    }

    public void Assign(int studentId, int seatId)
    {
        using var context = new AppDbContext();
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var assignment = new Assignment
            {
                StudentId = studentId,
                SeatId = seatId,
                AssignmentDate = DateTime.Now
            };
            context.Assignments.Add(assignment);

            var seat = context.Seats.Find(seatId);
            if (seat != null) seat.Status = "Ocupado";

            context.SaveChanges();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public dynamic? GetAssignmentInfo(string enrollment)
    {
        using var context = new AppDbContext();
        var data = (from e in context.Students
                    join asig in context.Assignments on e.Id equals asig.StudentId
                    join a in context.Seats on asig.SeatId equals a.Id
                    where e.Enrollment == enrollment
                    select new { e.Name, a.Row, a.Number }).FirstOrDefault();
        return data;
    }

    public void UpdateAssignment(int studentId, int oldSeatId, int newSeatId)
    {
        using var context = new AppDbContext();
        using var transaction = context.Database.BeginTransaction();
        try
        {
            var assignment = context.Assignments.FirstOrDefault(a => a.StudentId == studentId);
            if (assignment != null) assignment.SeatId = newSeatId;

            var oldSeat = context.Seats.Find(oldSeatId);
            if (oldSeat != null) oldSeat.Status = "Disponible";

            var newSeat = context.Seats.Find(newSeatId);
            if (newSeat != null) newSeat.Status = "Ocupado";

            context.SaveChanges();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public int GetCurrentSeatId(int studentId)
    {
        using var context = new AppDbContext();
        var assignment = context.Assignments.FirstOrDefault(a => a.StudentId == studentId);
        return assignment?.SeatId ?? 0;
    }

    public void InitializeLayout(int rowCount, int colCount)
    {
        using var context = new AppDbContext();
        using var transaction = context.Database.BeginTransaction();
        try
        {
            context.Assignments.ExecuteDelete();
            context.Seats.ExecuteDelete();

            for (int i = 0; i < rowCount; i++)
            {
                string rowLabel = ((char)('A' + i)).ToString();
                for (int j = 1; j <= colCount; j++)
                {
                    context.Seats.Add(new Seat { Row = rowLabel, Number = j, Status = "Disponible" });
                }
            }

            context.SaveChanges();
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }
}
