using Domain.Pessoas.Entities;
using Domain.Pessoas.Interfaces.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Infra_Teste
{
    [TestClass]
    public class AlterarPessoas : Base
    {
        IPessoasRepository _pessoas;
        public AlterarPessoas()
        {
            _pessoas = Container.GetInstance<IPessoasRepository>();
        }

        [TestMethod]
        public void AlterandoUmaPessoaFisica()
        {
            var chave = "5a815d42-d664-45a9-ab04-93eb2a8d37e3";
            var foi = true; ;
            try
            {
                _pessoas.Update(new Pessoa
                {
                    PessoaID = chave.ToString(),

                    Nome = "Carlos",
                    PessoaFisica = new PessoaFisica
                    {
                        PessoaID = chave.ToString().ToString(),

                        CPF = "10535422709",
                        DataNascimento = Convert.ToDateTime("01/06/1985"),
                        EstadoCivil = "Casado",
                        Sexo = "M"
                    }

                });
            }
            catch
            {
                foi = false;
            }

            Assert.IsTrue(foi);

        }

        [TestMethod]
        public void AlterandoUmaPessoaFisicaAdicionandoTelefones()
        {
            var chave = "e255ff2b-7b69-4061-86a3-304eea63d5a4";
            List<PessoaTelefone> telefones = new List<PessoaTelefone> {

                new PessoaTelefone
                {
                    DDD="21",
                    Numero="24116162",
                    PessoaID = chave.ToString(),

                },
                 new PessoaTelefone
                {
                    DDD="21",
                    Numero="24116162",
                    PessoaID = chave.ToString(),

                }
            };

            var foi = true ;
            try
            {
                _pessoas.Update(new Pessoa
                {
                    PessoaID = chave.ToString(),

                    Nome = "Carlos",
                    PessoaFisica = new PessoaFisica
                    {
                        PessoaID = chave.ToString().ToString(),

                        CPF = "10535422709",
                        DataNascimento = Convert.ToDateTime("01/06/1985"),
                        EstadoCivil = "Casado",
                        Sexo = "M"
                    },
                    PessoaTelefone = telefones

                });
            }
            catch
            {
                foi = false;
            }

            Assert.IsTrue(foi);

        }

        [TestMethod]
        public void AlterandoUmaPessoaFisicaAdicionandoTelefonesAdicionandoEmail()
        {
            var chave = "5a815d42-d664-45a9-ab04-93eb2a8d37e3";
            List<PessoaEmail> emails = new List<PessoaEmail>
            {
                new PessoaEmail
                {
                    Email="ccarlosdeclink@base.com.br",
                    PessoaID = chave.ToString(),
                    FlagPrincipal= true,

                },

                new PessoaEmail
                {
                    Email="ccarlosdeclink@jogo.com.br",
                    PessoaID = chave.ToString(),
                    FlagPrincipal= true,

                }
            };
            List<PessoaTelefone> telefones = new List<PessoaTelefone> {

                new PessoaTelefone
                {
                    DDD="21",
                    Numero="24116162",
                    PessoaID = chave.ToString(),

                },
                 new PessoaTelefone
                {
                    DDD="21",
                    Numero="24116162",
                    PessoaID = chave.ToString(),

                }
            };

            var foi = true;

            try
            {
                _pessoas.Update(new Pessoa
                {
                    PessoaID = chave.ToString(),

                    Nome = "Carlos",
                    PessoaFisica = new PessoaFisica
                    {
                        PessoaID = chave.ToString().ToString(),

                        CPF = "10535422709",
                        DataNascimento = Convert.ToDateTime("01/06/1985"),
                        EstadoCivil = "Casado",
                        Sexo = "M"
                    },
                    PessoaTelefone = telefones,
                    PessoaEmail = emails

                });
            }
            catch
            {
                foi = false;
            }

            Assert.IsTrue(foi);

        }
    }
}
