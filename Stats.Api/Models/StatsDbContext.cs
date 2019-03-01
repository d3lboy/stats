using Microsoft.EntityFrameworkCore;
using Stats.Common.Dto;

namespace Stats.Api.Models
{
    public sealed class StatsDbContext : DbContext
    {
        public StatsDbContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<BoxScore> BoxScores { get; set; }
        public DbSet<RoundDto> RoundDto { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }
}
