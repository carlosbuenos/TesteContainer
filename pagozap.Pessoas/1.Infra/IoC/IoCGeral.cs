using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace Infra.Pessoas.IoC
{

	public class IoCGeral
	{
		public static Container Container { get; set; }
        public static Container Start()
        {
                Container = new Container();
                Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
                /*ModuloApp.Start(Container);
                ModuloService.Start(Container);*/
                ModuloRepository.Start(Container);
                //Container.Verify();

            return Container;
        }

		public static void StartInjection(IServiceCollection services)
		{
			ModuloApp.startInjectionApp(services);
			ModuloService.startInjectionServices(services);
			ModuloRepository.startInjectionRepository(services);
		}

	}
}
