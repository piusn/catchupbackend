namespace CatchMeUp.Core.Entities;

public class Availability : BaseEntity
{
    public Availability()
    {
        MemberInterests = new List<MemberInterest>();
    }
    public int UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public virtual List<MemberInterest> MemberInterests { get; set; }
}