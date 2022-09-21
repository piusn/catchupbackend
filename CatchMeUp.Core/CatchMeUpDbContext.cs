using CatchMeUp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CatchMeUp.Core
{
    public class CatchMeUpDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public CatchMeUpDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CatchMeUpConnectionString"));
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Following> Followers { get; set; }
        public DbSet<Interest> Interests { get; set; }
        public DbSet<Favourite> Favourites { get; set; }
    }
}