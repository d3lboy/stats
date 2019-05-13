using Microsoft.Extensions.DependencyInjection;
using Stats.Common.Interfaces;

namespace Stats.Fetcher.Jobs.ABA
{
    public class AbaDi:IRegistry
    {
        public void Initialize(IServiceCollection collection)
        {
            collection.AddTransient<Rounds, Rounds>();
            collection.AddTransient<Round, Round>();
            collection.AddTransient<Game, Game>();
            collection.AddTransient<Team, Team>();
        }
    }
}
