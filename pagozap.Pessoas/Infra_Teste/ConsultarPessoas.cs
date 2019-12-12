using Domain.Pessoas.Entities;
using Domain.Pessoas.Interfaces.Repository;
using Infra.Pessoas.DataAccess.FactoryTypeConn;
using Infra.Pessoas.DataAccess.Mongo;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Infra_Teste
{
    [TestClass]
    public class ConsultarPessoas : Base
    {
        IPessoasRepository _pessoas;
        public ConsultarPessoas()
        {
            _pessoas = Container.GetInstance<IPessoasRepository>();
        }

        [TestMethod]
        public void BuscaTodos()
        {
            //var t = 1;

            //var r = ((TipoPessoa)t).ToString();

            //var chave = "5a815d42-d664-45a9-ab04-93eb2a8d37e3";
            //Assert.IsNull(t != 1);

        }
        [TestMethod]
        public void BuscaUmaPessoaFisicaInexistente()
        {
            //var chave = "bd5f290c-30e1-4db4-9538-6b7a9ed857cd";
            Assert.IsNull(_pessoas.GetAll());

        }

        [TestMethod]
        public void BuscaUmaPessoaFisicaExistentePorID()
        {
            var chave = "5a815d42-d664-45a9-ab04-93eb2a8d37e3";
            Assert.IsNotNull(_pessoas.GetByID(chave));

        }
        [TestMethod]
        public void BuscaUmaPessoaFisicaExistentePorCPF()
        {
           var cpf = "10535422709";
            Assert.IsNotNull(_pessoas.GetByCPF(cpf));

        }

        [TestMethod]
        public void BuscaUmaPessoaJuridicaExistentePorID()
        {
            var chave = "ea3c46da-cd83-4483-ab52-6cce96912ee8";
            Assert.IsNotNull(_pessoas.GetByID(chave));

        }

        [TestMethod]
        public void BuscaUmaPessoaJuridicaExistentePorCNPJ()
        {
            var cnpj = "10339422708159";
            Assert.IsNotNull(_pessoas.GetByCNPJ(cnpj));

        }

        [TestMethod]
        public void ContadorPessoas()
        {
            var listaPessoas = _pessoas.GetAll(1,10);
            int qtdPessoaFisica = 0;
            int qtdPessoaJuridica = 0;
            
            foreach (var pessoa in listaPessoas)
            {
                if(pessoa.TipoDePessoa == TipoPessoa.PessoaFisica)
                {
                    qtdPessoaFisica++;
                }
                else if(pessoa.TipoDePessoa == TipoPessoa.PessoaJuridica)
                {
                    qtdPessoaJuridica++;
                }
            }

             Assert.IsTrue(qtdPessoaFisica + qtdPessoaJuridica > 0);

        }

        [TestMethod]
        public void TesteClasse()
        {


            var _obj =  _pessoas.GetAll(1, 3)[0];
            _obj.PessoaID = "ooolllaaa111222333";
            _obj.Nome = "TesteDeTrocaClasse";
            _obj.PessoaFisica.PessoaID = _obj.PessoaID;
            _obj.PessoaTelefone = null;

            var _MongoCTX = new MongoContext(new ConectaMongoDB().StrConnect(), "Pagozap_Pessoas");
            _MongoCTX.Pessoas.InsertOne(_obj, null);

            var retorno = _pessoas.GetByID(_obj.PessoaID);
        }


    }
}
