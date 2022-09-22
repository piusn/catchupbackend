namespace CatchMeUp.Core.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public bool Status { get; set; }
    public bool Available { get; set; }
    public bool OfficeStatus { get; set; }
    public int TeamId { get; set; }
    public string  UserId { get; set; }
    public string UserName { get; set; }    
    protected virtual Team Team { get; set; }
}