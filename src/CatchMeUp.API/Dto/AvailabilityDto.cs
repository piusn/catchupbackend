namespace CatchMeUp.API.Dto
{
    public class AvailabilityDto
    {
        public DateTime StartTime { get; set; }
        public string  UserId { get; set; }
        public DateTime EndTime { get; set; }
        public int InterestId { get; set; }
    }
}
