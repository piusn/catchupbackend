namespace CatchMeUp.Core.Entities;

public class UserAvailability : BaseEntity
{
    public UserAvailability()
    {
        UserInterests = new List<UserInterest>();
    }
    public int UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public virtual List<UserInterest> UserInterests { get; set; }
}