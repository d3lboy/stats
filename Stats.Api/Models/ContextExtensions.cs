using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stats.Api.Models
{
    public static class ContextExtensions
    {
        public static void Seed(this StatsDbContext context)
        {
            if (context.Players.Any())
            {

            }
        }
    }
}
