namespace CatchMeUp.Core.Entities;

public class Member : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public bool Status { get; set; }
    public bool Available { get; set; }
    public bool OfficeStatus { get; set; }
    public MemberType MemberType { get; set; }


}


public enum MemberType
{
    Individual,
    Team
}