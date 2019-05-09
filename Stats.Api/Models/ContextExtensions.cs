using System;

namespace Stats.Api.Models
{
    public static class ContextExtensions
    {
        public static void Seed(this StatsDbContext context)
        {
            if(context.Find<Competition>(Common.Enums.Competition.Aba) == null)
            {
                context.Add(new Competition {
                    Id = Common.Enums.Competition.Aba,
                    Country = Common.Enums.Country.Yugoslavia,
                    Name = "ABA League",
                    Timestamp = DateTime.Now
                });

                context.SaveChanges();
            }
        }
    }
}
