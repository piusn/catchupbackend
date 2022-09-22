namespace CatchMeUp.API.Responses;

public sealed class UserAvailabilityResponse
{
    public int UserId { get; set; }
    public string Name { get; set; }
    public DateTime StartTime { get; set; }
    public string EndTime { get; set; }
    public List<UserInterestResponse> UserInterestResponses { get; set; }
}

public sealed class UserInterestResponse
{
    public int InterestId { get; set; }
    public string Name { get; set; }
}