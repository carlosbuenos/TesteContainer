using CrossCutting.Pessoas;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infra_Teste
{
    [TestClass]
    public class ValidarCpfCnpj
    {
        [TestMethod]
        public void ValidaCPF() {
            Assert.IsTrue(ValidarCPF.IsCpf("10522430708"));
        }
        [TestMethod]
        public void ValidaCPFInvalido()
        {
            Assert.IsFalse(ValidarCPF.IsCpf("10522430808"));
        }
        [TestMethod]
        public void ValidaCNPJ()
        {
            Assert.IsTrue(ValidarCNPJ.IsCnpj("33742830000143"));
        }

        [TestMethod]
        public void ValidaCNPJInvalido()
        {
            Assert.IsFalse(ValidarCNPJ.IsCnpj("33742730000143"));
        }

    }
}
