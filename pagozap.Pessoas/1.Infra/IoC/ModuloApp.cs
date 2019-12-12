using App.Pessoas.AppImplementation;
using App.Pessoas.Interface;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

namespace Infra.Pessoas.IoC
{
    public class ModuloApp
    {

        /*public static void  Start(Container container)
        {
            container.Register(typeof(IApp<>), typeof(App<>));
            container.Register<IPessoasApp, PessoasApp>();
            
        }*/

		public static void startInjectionApp(IServiceCollection services)
		{
			services.AddTransient(typeof(IApp<>), typeof(App<>));
			services.AddTransient<IPessoasApp, PessoasApp>();
		}
	}
}
