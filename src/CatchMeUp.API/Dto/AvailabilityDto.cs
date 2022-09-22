namespace CatchMeUp.API.Dto
{
    public class AvailabilityDto
    {
        public int UserId { get; set; }

        public string AvailabilityStatus { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int InterestId { get; set; }
    }
}
