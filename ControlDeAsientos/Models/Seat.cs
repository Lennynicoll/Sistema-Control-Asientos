namespace ControlDeAsientos.Models;

public class Seat : Entity
{
    public string Row { get; set; } = string.Empty;
    public int Number { get; set; }
    public string Status { get; set; } = "Disponible";

    public Seat() : base() { }

    public Seat(int id, string row, int number, string status) : base(id)
    {
        Row = row;
        Number = number;
        Status = status;
    }
}
