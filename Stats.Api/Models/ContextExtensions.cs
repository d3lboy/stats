using System;
using System.Linq;

namespace Stats.Api.Models
{
    public static class ContextExtensions
    {
        public static void Seed(this StatsDbContext context)
        {
            Guid aba = Guid.Parse("b3ad01b7-66a6-41ed-90b0-1f3bd96a4cb2");

            if(context.Find<Competition>(Common.Enums.Competition.Aba) == null)
            {
                context.Add<Competition>(new Competition {
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
