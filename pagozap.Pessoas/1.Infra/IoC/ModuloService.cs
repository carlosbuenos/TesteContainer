using Domain.Pessoas.Interfaces.ServiceImplementation;
using Domain.Pessoas.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace Infra.Pessoas.IoC
{
    public  class ModuloService
    {
        /*public static void Start(Container container)
        {
            container.Register(typeof(IService<>), typeof(Service<>));
            container.Register<IPessoaService, PessoaService>();
        }*/

		public static void startInjectionServices(IServiceCollection services)
		{
			services.AddTransient(typeof(IService<>), typeof(Service<>));
			services.AddTransient<IPessoaService, PessoaService>();
		}
	}
}
