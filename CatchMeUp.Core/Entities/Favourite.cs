
namespace CatchMeUp.Core.Entities
{
    public class Favourite : BaseEntity
    {
        public int UserId { get; set; }
        public int MemberId { get; set; }
        public DateTime FavoritedOn { get; set; }
        public Nullable<DateTime> UnFavoritedOn { get; set; }
    }
}

