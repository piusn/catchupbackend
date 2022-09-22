using CatchMeUp.Core.Entities;

namespace CatchMeUp.API.Dto
{
    public class MemberDto
    {
        public bool Status { get; set; }
        public bool Available { get; set; }
        public bool OfficeStatus { get; set; }
        public int  TeamId { get; set; }
    }
}
