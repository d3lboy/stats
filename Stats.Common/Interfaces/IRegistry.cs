using Microsoft.Extensions.DependencyInjection;

namespace Stats.Common.Interfaces
{
    public interface IRegistry
    {
        void Initialize(IServiceCollection collection);
    }
}
