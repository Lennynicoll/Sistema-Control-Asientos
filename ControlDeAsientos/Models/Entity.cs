namespace ControlDeAsientos.Models;

public abstract class Entity
{
    public int Id { get; set; }

    protected Entity() { }

    protected Entity(int id)
    {
        Id = id;
    }
}
