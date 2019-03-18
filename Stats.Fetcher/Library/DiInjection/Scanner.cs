using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Stats.Common.Interfaces;

namespace Stats.Fetcher.Library.DiInjection
{
    public class Scanner
    {
        public static void Scan(IServiceCollection collection)
        {
            AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes())
                .Where(x => typeof(IRegistry).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .ToList()
                .ForEach(x =>
                {
                    ((IRegistry)Activator.CreateInstance(x.UnderlyingSystemType)).Initialize(collection);
                });
        }
    }
}
