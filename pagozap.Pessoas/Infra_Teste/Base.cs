using Infra.Pessoas.IoC;
using SimpleInjector;

namespace Infra_Teste
{
    public class Base
    {
        public Container Container { get { return _container; } }
        private Container _container { get; set; }
		public Base()
		{
			_container = IoCGeral.Start();
		}
    }
}
