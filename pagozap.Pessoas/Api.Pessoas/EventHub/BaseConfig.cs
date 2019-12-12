using CrossCutting.Pessoas;
using Infra.Pessoas.IoC;
using SimpleInjector;

namespace Api.Pessoas.EventHub
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public Container Container { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public BaseConfig()
        {
            Container = IoCGeral.Start();
            QueueParameters.QueuelName = "Pessoas";
        }
    }
}
