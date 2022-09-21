namespace CatchMeUp.API.Dto
{
    public class AvailabilityDto
    {
        public int UserId { get; set; }

        public string AvailabilityStatus { get; set; }
        public DateTime When { get; set; }
        public int Activity { get; set; }
    }
}
