using DeclinkLogErros;
using DeclinkLogErrosPackage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infra_Teste
{
	[TestClass]
	public class TesteLog
	{
		[TestMethod]
		public void TesteSave()
		{
			try
			{
				LogErroViewModel logErro = new LogErroViewModel();
				logErro.EnviarEmail = false;
				logErro.ExceptionMessage = "Teste carlos e Lud";
				logErro.NomeClasse = "PessoasController";
				logErro.NomeMetodo = "GetAll";
				logErro.ObjetoFalha = "Não há objeto/parametros";
				logErro.TipoMicroServico = TipoMicroServico.Pessoas;
				logErro.CodigoErro = ManagerError.GerarCodigoErro(TipoMicroServico.Pessoas);
				LogErroController cont = new LogErroController();
				cont.Salvar(logErro);
				Assert.IsTrue(true);
			}
			catch (System.Exception ex)
			{

				Assert.IsTrue(false);
			}

		}
	}
}
