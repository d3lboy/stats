using Microsoft.EntityFrameworkCore;

namespace Stats.Api.Models
{
    public class StatsDbContext : DbContext
    {
        public StatsDbContext(DbContextOptions<StatsDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseNpgsql();
        //}

        public DbSet<Player> Players { get; set; }
    }
}
