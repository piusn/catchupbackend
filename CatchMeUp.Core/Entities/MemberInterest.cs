namespace CatchMeUp.Core.Entities;

public class MemberInterest : BaseEntity
{
    public int InterestId { get; set; }

    public int MemberId { get; set; }
    public virtual Interest Interest { get; set; }
    public virtual User Member { get; set; }
}