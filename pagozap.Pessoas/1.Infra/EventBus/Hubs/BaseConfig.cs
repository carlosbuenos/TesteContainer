using Infra.Pessoas.IoC;
using SimpleInjector;

namespace Infra.Pessoas.EventBus.Hubs
{
    public class BaseConfig
    {
        public Container Container { get; set; }
        public BaseConfig()
        {
            Container = IoCGeral.Start();
        }
    }
}
