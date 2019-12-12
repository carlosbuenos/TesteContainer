using Domain.Pessoas.Interfaces.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infra_Teste
{
    [TestClass]
    public class ExcluirPessoas: Base
    {
        IPessoasRepository _pessoas;
        public ExcluirPessoas()
        {
            _pessoas = Container.GetInstance<IPessoasRepository>();
        }

        [TestMethod]
        public void RemovendoUmaPessoaFisica()
        {
            var chave = "bd5f290c-30e1-4db4-9538-6b7a9ed857cd";
            _pessoas.Delete(chave);
            Assert.IsTrue(true);

        }

		
    }
}
