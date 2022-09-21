namespace CatchMeUp.Core.Entities;

public class Team : BaseEntity
{
    public Team()
    {
        Events = new List<TeamEvent>();
        Members = new List<User>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<TeamEvent> Events { get; set; }
    public virtual List<User> Members { get; set; }
}