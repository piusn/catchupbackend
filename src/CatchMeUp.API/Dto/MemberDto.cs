using CatchMeUp.Core.Entities;

namespace CatchMeUp.API.Dto
{
    public class MemberDto
    {
        public string Name { get; set; } = string.Empty;
        public bool Status { get; set; }
        public bool Available { get; set; }
        public bool OfficeStatus { get; set; }
        public MemberType MemberType { get; set; }
    }
}
