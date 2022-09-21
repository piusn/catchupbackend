namespace CatchMeUp.Core.Entities;

public class TeamEvent : BaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int TeamId { get; set; }
    public virtual Team Team { get; set; }
}