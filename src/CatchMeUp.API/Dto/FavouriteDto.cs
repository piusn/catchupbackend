using System;

namespace CatchMeUp.API.Dto
{
    public class FavouriteDto
    {
        public int UserId { get; set; }
        public int MemberId { get; set; }
        public DateTime FavoritedOn { get; set; }
        public Nullable<DateTime> UnFavoritedOn { get; set; }
    }
}
