using Domain.Pessoas.Entities;
using Domain.Pessoas.Interfaces.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Infra_Teste
{
    [TestClass]
    public class IncluirPessoas
    {
        IPessoasRepository _pessoas;
        public IncluirPessoas()
        {

            //QueueParameters.Host = Parameters.hostIPMongo;
            //QueueParameters.userName = "declink";
            //QueueParameters.password = "123";
            //QueueParameters.QueuelName = Parameters.ListenMongo;
            //new ListenQueueMongo().StartSerevrListenQueue(QueueParameters.Host, QueueParameters.userName, QueueParameters.password, QueueParameters.QueuelName, QueueParameters.QueuelName);
            ////QueueParameters.QueuelName = Parameters.ListenMySQL;
            ////ListenQueueMySql.StartSerevrListenQueue(QueueParameters.Host, QueueParameters.userName, QueueParameters.password, QueueParameters.QueuelName, QueueParameters.QueuelName);
            //IoCGeral.Start();
            //_pessoas = IoCGeral.Container.GetInstance<IPessoasRepository>();
        }
        [TestMethod]
        public void IncluindoUmaPessoaFisica()
        {
            var chave = Guid.NewGuid();
            Assert.IsNotNull(_pessoas.Save(new Pessoa
            {
                PessoaID = chave.ToString().ToString(),

                Nome = "Carlos",
                PessoaFisica = new PessoaFisica
                {
                    PessoaID = chave.ToString().ToString(),

                    CPF = "12345698728",
                    DataNascimento = Convert.ToDateTime("01/06/1985"),
                    EstadoCivil = "Solteiro",
                    Sexo = "M"
                },
                NomeEvento = "Save",
                TipoEvento = "I"

            }));

        }

        [TestMethod]
        public void IncluindoUmaPessoaFisicaComMaisDeUmTelefone()
        {
            var chave = Guid.NewGuid();
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
            Assert.IsNotNull(_pessoas.Save(new Pessoa
            {
                PessoaID = chave.ToString().ToString(),

                Nome = "Carlos",
                PessoaFisica = new PessoaFisica
                {
                    PessoaID = chave.ToString().ToString(),

                    CPF = "10022280998",
                    DataNascimento = Convert.ToDateTime("01/06/1985"),
                    EstadoCivil = "Solteiro",
                    Sexo = "M"
                },
                PessoaTelefone = telefones


            }));

        }

        [TestMethod]
        public void IncluindoUmaPessoaFisicaComMaisDeUmTelefoneEMaisDeUmEmail()
        {
            var chave = Guid.NewGuid();
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
            Assert.IsNotNull(_pessoas.Save(new Pessoa
            {
                PessoaID = chave.ToString().ToString(),

                Nome = "Carlos",
                PessoaFisica = new PessoaFisica
                {
                    PessoaID = chave.ToString().ToString(),

                    CPF = "10022280798",
                    DataNascimento = Convert.ToDateTime("01/06/1985"),
                    EstadoCivil = "Solteiro",
                    Sexo = "M"
                },
                PessoaTelefone = telefones,
                PessoaEmail = emails


            }));

        }

        /*  [TestMethod]
          public void IncluindoMultiplasPessoasFisicas()
          {
              var chave = Guid.NewGuid();
              List<Pessoas> lista = new List<Pessoas>
              {
                  new Pessoas
                  {
                      PessoaID = chave.ToString().ToString(),

                      Nome = "julio",
                      PessoaFisica = new PessoaFisica
                      {
                          PessoaID = chave.ToString(),

                          CPF = "10537422798",
                          DataNascimento = Convert.ToDateTime("01/06/1985"),
                          EstadoCivil = "Solteiro",
                          Sexo = "M"
                      }

                  }
              };
              var chave1 = Guid.NewGuid();
              lista.Add(new Pessoas
              {
                  PessoaID = chave1.ToString(),

                  Nome = "Ivan",
                  PessoaFisica = new PessoaFisica
                  {
                      PessoaID = chave1.ToString(),

                      CPF = "10730822708",
                      DataNascimento = Convert.ToDateTime("01/06/1985"),
                      EstadoCivil = "Solteiro",
                      Sexo = "M"
                  }

              });

              Assert.IsTrue(_pessoas.Save(lista));

          }*/

        [TestMethod]
        public void IncluindoUmaPessoaFisicaCompleta()
        {
            var chave = Guid.NewGuid();
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

            List<PessoaEndereco> enderecos = new List<PessoaEndereco>
            {
                new PessoaEndereco
                {
                    PessoaID = chave.ToString(),
                    CEP="23082-120",
                    Logradouro="Rua leonidas moreira",
                    Bairro="Campo Grande",
                    Municipio="Rio de Janeiro",
                    UF ="RJ",
                    Numero="24116162",
                    Complemento="Quadra 3 Lote 17",
                    FlagPrincipal=true
                },
                 new PessoaEndereco
                {
                    PessoaID = chave.ToString(),
                    CEP="23070-420",
                    Logradouro="Rua vergel",
                    Bairro="Campo Grande",
                    Municipio="Rio de Janeiro",
                    UF ="RJ",
                    Numero="24116162",
                    Complemento="Quadra 34 Lote 6",
                    FlagPrincipal=false
                }
            };

            Assert.IsNotNull(_pessoas.Save(new Pessoa
            {
                PessoaID = chave.ToString().ToString(),

                Nome = "Carlos",
                PessoaFisica = new PessoaFisica
                {
                    PessoaID = chave.ToString().ToString(),

                    CPF = "10022280792",
                    DataNascimento = Convert.ToDateTime("01/06/1985"),
                    EstadoCivil = "Solteiro",
                    Sexo = "M"
                },
                PessoaTelefone = telefones,
                PessoaEmail = emails,
                PessoaEndereco = enderecos

            }));

        }

        [TestMethod]
        public void IncluindoUmaPessoaJuridica()
        {
            var chave = Guid.NewGuid();
            Assert.IsNotNull(_pessoas.Save(new Pessoa
            {
                PessoaID = chave.ToString(),

                Nome = "Carlos",
                PessoaJuridica = new PessoaJuridica
                {
                    PessoaID = chave.ToString(),
                    CNPJ = "10330422708159",
                    NomeFantasia = "Ivan empres",
                }

            }));

        }

        [TestMethod]
        public void IncluindoUmaPessoaJuridicaComMaisDeUmTelefone()
        {
            var chave = Guid.NewGuid();
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
            Assert.IsNotNull(_pessoas.Save(new Pessoa
            {
                PessoaID = chave.ToString(),

                Nome = "Carlos",
                PessoaJuridica = new PessoaJuridica
                {
                    PessoaID = chave.ToString(),
                    CNPJ = "10339422708159",
                    NomeFantasia = "Ivan empres",
                },
                PessoaTelefone = telefones


            }));

        }

        [TestMethod]
        public void IncluindoUmaPessoaJuridicaComMaisDeUmTelefoneEMaisDeUmEmail()
        {
            var chave = Guid.NewGuid();
            List<PessoaEmail> emails = new List<PessoaEmail>
            {
                new PessoaEmail
                {
                    Email="ivan@base.com.br",
                    PessoaID = chave.ToString(),
                    FlagPrincipal= true,

                },

                new PessoaEmail
                {
                    Email="ivan@jogo.com.br",
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
            Assert.IsNotNull(_pessoas.Save(new Pessoa
            {
                PessoaID = chave.ToString(),

                Nome = "Carlos",
                PessoaJuridica = new PessoaJuridica
                {
                    PessoaID = chave.ToString(),
                    CNPJ = "10339422703159",
                    NomeFantasia = "Ivan empres",
                },
                PessoaTelefone = telefones,
                PessoaEmail = emails


            }));

        }

        [TestMethod]
        public void IncluindoUmaPessoaJuridicaCompleta()
        {
            var chave = Guid.NewGuid();
            List<PessoaEmail> emails = new List<PessoaEmail>
            {
                new PessoaEmail
                {
                    Email="ivan@base.com.br",
                    PessoaID = chave.ToString(),
                    FlagPrincipal= true,

                },

                new PessoaEmail
                {
                    Email="ivan@jogo.com.br",
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
            List<PessoaEndereco> enderecos = new List<PessoaEndereco>
            {
                new PessoaEndereco
                {
                    PessoaID = chave.ToString(),
                    CEP="23082-120",
                    Logradouro="Rua leonidas moreira",
                    Bairro="Campo Grande",
                    Municipio="Rio de Janeiro",
                    UF ="RJ",
                    Numero="24116162",
                    Complemento="Quadra 3 Lote 17",
                    FlagPrincipal=true
                },
                 new PessoaEndereco
                {
                    PessoaID = chave.ToString(),
                    CEP="23070-420",
                    Logradouro="Rua vergel",
                    Bairro="Campo Grande",
                    Municipio="Rio de Janeiro",
                    UF ="RJ",
                    Numero="24116162",
                    Complemento="Quadra 34 Lote 6",
                    FlagPrincipal=false
                }
            };
            Assert.IsNotNull(_pessoas.Save(new Pessoa
            {
                PessoaID = chave.ToString(),

                Nome = "Carlos",
                PessoaJuridica = new PessoaJuridica
                {
                    PessoaID = chave.ToString(),
                    CNPJ = "10339422703151",
                    NomeFantasia = "Ivan empres",
                },
                PessoaTelefone = telefones,
                PessoaEmail = emails,
                PessoaEndereco = enderecos


            }));

        }

		[TestMethod]
		public void GerarIdMail()
		{
			var chave = Guid.NewGuid();
			List<PessoaEmail> emails = new List<PessoaEmail>
			{
				new PessoaEmail
				{
					Email="ivan@base.com.br",
					PessoaID = chave.ToString(),
					FlagPrincipal= true,

				},

				new PessoaEmail
				{
					Email="ivan@jogo.com.br",
					PessoaID = chave.ToString(),
					FlagPrincipal= true,

				}
			};
			foreach (var email in emails)
			{
				email.GenerateID();
			}
			Assert.IsNotNull(emails[0].PessoaEmailID);

		}

		[TestMethod]
		public void GerarIdEndereco()
		{
			var chave = Guid.NewGuid();
			List<PessoaEndereco> enderecos = new List<PessoaEndereco>
			{
				new PessoaEndereco
				{
					PessoaID = chave.ToString(),
					CEP="23082-120",
					Logradouro="Rua leonidas moreira",
					Bairro="Campo Grande",
					Municipio="Rio de Janeiro",
					UF ="RJ",
					Numero="24116162",
					Complemento="Quadra 3 Lote 17",
					FlagPrincipal=true
				},
				 new PessoaEndereco
				{
					PessoaID = chave.ToString(),
					CEP="23070-420",
					Logradouro="Rua vergel",
					Bairro="Campo Grande",
					Municipio="Rio de Janeiro",
					UF ="RJ",
					Numero="24116162",
					Complemento="Quadra 34 Lote 6",
					FlagPrincipal=false
				}
			};
			foreach (var endereco in enderecos)
			{
				endereco.GenerateID();
			}
			Assert.IsNotNull(enderecos[0].PessoaEnderecoID);

		}

	}


}
