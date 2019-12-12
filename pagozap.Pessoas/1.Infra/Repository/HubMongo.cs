using CrossCutting.LogErros;
using CrossCutting.Pessoas;
using Domain.Pessoas.Entities;
using Domain.Pessoas.Interfaces.Repository;
using Infra.Pessoas.DataAccess.FactoryTypeConn;
using Infra.Pessoas.DataAccess.Mongo;
using MongoDB.Driver;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
namespace Infra.Pessoas.Repository
{
    public class HubMongo : Repository<Pessoa>, IPessoasMongoRepository
    {
        public MongoContext _MongoCTX;
        public HubMongo()
        {
            _MongoCTX = new MongoContext(new ConectaMongoDB().StrConnect(), "Pagozap_Pessoas");
			
		}

		public List<Pessoa> GetByListID(List<string> Ids)
		{
			
			var pessoa =  (from p in _MongoCTX.Pessoas.AsQueryable<Pessoa>()
						  where Ids.Contains(p.PessoaID)
						  select p
						  ).AsQueryable();
			return pessoa.ToList();
		}

		public async Task<List<Pessoa>> GetAll()
        {
            var pessoa = await _MongoCTX.Pessoas.Find(x => x.PessoaID != null).SortBy(x => x.PessoaID).ToListAsync();
            return pessoa;
        }

        public async Task<List<Pessoa>> GetAll(int Page, int PageSize)
        {
			
			await _MongoCTX.Pessoas.Find(x => x.PessoaID != null).Skip((Page - 1) * PageSize).Limit(PageSize).ToListAsync();
            var pessoa = await _MongoCTX.Pessoas.Find(x => x.PessoaID != null).SortBy(x => x.PessoaID).Skip((Page - 1) * PageSize).Limit(PageSize).ToListAsync();
            return pessoa;
        }

        public async Task<Pessoa> GetByCNPJ(string CNPJ)
        {
            try
            {
                var pessoa = await _MongoCTX.Pessoas.Find(x => x.PessoaJuridica.CNPJ == CNPJ).FirstOrDefaultAsync();
                return pessoa;
            }
            catch
            {
                return null;
            }

        }

        public async Task<Pessoa> GetByCPF(string CPF)
        {
            try
            {
                var pessoa = await _MongoCTX.Pessoas.Find(x => x.PessoaFisica.CPF == CPF).FirstOrDefaultAsync();
                return pessoa;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Pessoa> GetByID(string ID)
        {
            var pessoa = await _MongoCTX.Pessoas.Find(x => x.PessoaID == ID).FirstOrDefaultAsync();
            return pessoa;
        }

        public void Save(Domain.Pessoas.Entities.Pessoa _obj)
        {
            try
            {
                _MongoCTX.Pessoas.InsertOne(_obj, null);
            }
            catch (Exception ex)
            {
                if (Parameters.ultimoException == null || Parameters.ultimoException.Message != ex.Message)
                {
                    Parameters.ultimoException = ex;
					var client = new RestClient(Parameters.routeLog);
					var request = new RestRequest("SaveLogErro", Method.POST);
					LogErroViewModel logErro = new LogErroViewModel
                    {
                        EnviarEmail = false,
                        ExceptionMessage = ex.Message,
                        NomeClasse = "HubMongo",
                        NomeMetodo = "Save",
                        ObjetoFalha = JsonConvert.SerializeObject(_obj),
                        TipoMicroServico = ParametersLogErro.tipoMicroServico
                    };
                    request.AddJsonBody(JsonConvert.SerializeObject(logErro));
                    client.Execute(request);
                }
                throw ex;
            }

        }

        public void Update(Domain.Pessoas.Entities.Pessoa _obj)
        {
            try
            {
                var modificationsUpdate = Builders<Pessoa>.Update
                .Set(a => a.Nome, _obj.Nome)
                .Set(a => a.PessoaEmail, _obj.PessoaEmail)
                .Set(a => a.PessoaEndereco, _obj.PessoaEndereco)
                .Set(a => a.PessoaFisica, _obj.PessoaFisica)
                .Set(a => a.PessoaJuridica, _obj.PessoaJuridica)
                .Set(a => a.PessoaTelefone, _obj.PessoaTelefone);

                _MongoCTX.Pessoas.UpdateOne(x => x.PessoaID.Equals(_obj.PessoaID), modificationsUpdate);
            }
            catch (Exception ex)
            {
                if (Parameters.ultimoException == null || Parameters.ultimoException.Message != ex.Message)
                {
                    Parameters.ultimoException = ex;
					var client = new RestClient(Parameters.routeLog);
					var request = new RestRequest("SaveLogErro", Method.POST);
					LogErroViewModel logErro = new LogErroViewModel
                    {
                        EnviarEmail = false,
                        ExceptionMessage = ex.Message,
                        NomeClasse = "HubMongo",
                        NomeMetodo = "Update",
                        ObjetoFalha = JsonConvert.SerializeObject(_obj),
                        TipoMicroServico = ParametersLogErro.tipoMicroServico
                    };
                    request.AddJsonBody(JsonConvert.SerializeObject(logErro));
                    client.Execute(request);
                }
                throw ex;
            }

        }

        public void Delete(string ID)
        {
            try
            {
                _MongoCTX.Pessoas.DeleteOne(x => x.PessoaID == ID);
            }
            catch (Exception ex)
            {
                if (Parameters.ultimoException == null || Parameters.ultimoException.Message != ex.Message)
                {
                    Parameters.ultimoException = ex;
					var client = new RestClient(Parameters.routeLog);
					var request = new RestRequest("SaveLogErro", Method.POST);
					LogErroViewModel logErro = new LogErroViewModel
                    {
                        EnviarEmail = false,
                        ExceptionMessage = ex.Message,
                        NomeClasse = "HubMongo",
                        NomeMetodo = "Delete",
                        ObjetoFalha = $"ID:{ID}",
                        TipoMicroServico = ParametersLogErro.tipoMicroServico
                    };
                    request.AddJsonBody(JsonConvert.SerializeObject(logErro));
                    client.Execute(request);
                }
                throw ex;
            }

        }

		
	}
}
