using Domain.Pessoas.Interfaces.Repository;
using Infra.Pessoas.HubManagerData;
using Infra.Pessoas.EventBus.Producer;
using Infra.Pessoas.Repository;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using CrossCutting.Pessoas;

namespace Infra.Pessoas.IoC
{
    public  class ModuloRepository
    {

        public static void Start(Container container)
        {
            container.Register(typeof(IRepository<>), typeof(Repository<>));
            container.Register<IPessoasRepository, PessoasRepository>();
            //container.Register<IPessoasMySqlRepository, HubMysql>();
            container.Register<IPessoasMongoRepository, HubMongo>();
            container.Register<IPublisherQueue, PublisherQueue>();
			container.Register<IStartListeners, StartListeners>();
			container.Register<IParameters, Parameters>();
		}

		public static void startInjectionRepository(IServiceCollection services) {
			services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
			services.AddTransient<IPessoasRepository, PessoasRepository>();
			//services.AddTransient<IPessoasMySqlRepository, HubMysql>();
			services.AddTransient<IPessoasMongoRepository, HubMongo>();
			services.AddTransient<IPublisherQueue, PublisherQueue>();
		}
    }

}
