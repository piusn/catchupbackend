namespace CatchMeUp.Core.Entities;

public class Following : BaseEntity
{
    public int MemberId { get; set; }
    public int UserId { get; set; }
    public DateTime FollowedOn { get; set; }

}