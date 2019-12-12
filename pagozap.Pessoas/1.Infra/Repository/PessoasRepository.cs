using CrossCutting.LogErros;
using CrossCutting.Pessoas;
using Domain.Pessoas.Entities;
using Domain.Pessoas.Interfaces.Repository;
using Infra.Pessoas.EventBus.Producer;
using Infra.Pessoas.HubManagerData;
using Infra.Pessoas.IoC;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infra.Pessoas.Repository
{
	public class PessoasRepository : Repository<Pessoa>, IPessoasRepository
	{
		IPublisherQueue _pub;
		
		//IPessoasMySqlRepository _pessoaMy;
		IPessoasMongoRepository _pessoaMongo;
		//IStartListeners _listenersRabbit;
		IParameters _parameters;
		public PessoasRepository()
		{
			IoCGeral.Container.GetInstance<IParameters>().SetParameters();
			
			// cacheClient = new StackExchangeRedisCacheClient(serializer, ConectaRedis.Connection.Configuration);
			//_pub = IoCGeral.Container.GetInstance<IPublisherQueue>();
			//try
			//{
				
			//	_pessoaMy = IoCGeral.Container.GetInstance<IPessoasMySqlRepository>();
			//}
			//catch (Exception ex)
			//{
			//	var client = new RestClient(Parameters.routeLog);
			//	var request = new RestRequest("SaveLogErro", Method.POST);
			//	LogErroViewModel logErro = new LogErroViewModel
			//	{
			//		EnviarEmail = true,
			//		ExceptionMessage = ex.Message,
			//		NomeClasse = "PessoasRepository",
			//		NomeMetodo = "Save ListenMongoPessoa",
			//		ObjetoFalha = "Conexao",
			//		TipoMicroServico = TipoMicroServico.Pessoas,
			//		CodigoErro = ex.Message
			//	};
			//	request.AddJsonBody(JsonConvert.SerializeObject(logErro));
			//	client.Execute(request);
			//}
			try
			{
				_pessoaMongo = IoCGeral.Container.GetInstance<IPessoasMongoRepository>();
			}
			catch (Exception ex)
			{

				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = true,
					ExceptionMessage = ex.Message,
					NomeClasse = "PessoasRepository",
					NomeMetodo = "Save ListenMongoPessoa",
					ObjetoFalha = "Conexao",
					TipoMicroServico = TipoMicroServico.Pessoas,
					CodigoErro = ex.Message
				};
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				client.Execute(request);
			}
			//IoCGeral.Container.GetInstance<IStartListeners>().startListenersBase();

		}

		public async Task<List<Pessoa>> GetAll()
		{
			var pessoas = await _pessoaMongo.GetAll();
			//if (pessoas == null)
			//{
			//	pessoas = await _pessoaMy.GetAll();
			//}
			return pessoas;
		}

		public List<Pessoa> GetByListID(List<string> Ids)
		{
			var pessoas =  _pessoaMongo.GetByListID(Ids);
			//if (pessoas == null)
			//{
			//	pessoas =  _pessoaMy.GetByListID(Ids);
			//}
			return pessoas;
		}

		public async Task<List<Pessoa>> GetAll(int Page, int Pagesize)
		{
			var pessoas = await _pessoaMongo.GetAll(Page, Pagesize);
			//if (pessoas == null)
			//{
			//	pessoas = await _pessoaMy.GetAll(Page, Pagesize);
			//}
			return pessoas;
		}

		public async Task<Pessoa> GetByCNPJ(string CNPJ)
		{
			var Rpessoas = await _pessoaMongo.GetByCNPJ(CNPJ);
			//if (Rpessoas == null)
			//{
			//	Rpessoas = await _pessoaMy.GetByCNPJ(CNPJ);
			//}
			return Rpessoas;
		}

		public async Task<Pessoa> GetByCPF(string CPF)
		{
			var Rpessoas = await _pessoaMongo.GetByCPF(CPF);
			//if (Rpessoas == null)
			//{
			//	Rpessoas = await _pessoaMy.GetByCPF(CPF);
			//}
			return Rpessoas;
		}

		public async Task<Pessoa> GetByID(string ID)
		{

			var Rpessoas = await _pessoaMongo.GetByID(ID);
			//if (Rpessoas == null)
			//{
			//	Rpessoas = await _pessoaMy.GetByID(ID);
			//}

			return Rpessoas;
		}

		public async Task<bool> PessoaUpdate(Domain.Pessoas.Entities.Pessoa _obj)
		{
			string pessoaID = "";

			if (_obj.TipoDePessoa == TipoPessoa.PessoaFisica)
			{
				var pessoa = await GetByCPF(_obj.PessoaFisica.CPF);
				if (pessoa != null)
				{
					pessoaID = pessoa.PessoaID;
					_obj.PessoaFisica.PessoaID = pessoaID;
				}
			}
			else if (_obj.TipoDePessoa == TipoPessoa.PessoaJuridica)
			{
				var pessoa = await GetByCNPJ(_obj.PessoaJuridica.CNPJ);
				if (pessoa != null)
				{
					pessoaID = pessoa.PessoaID;
					_obj.PessoaJuridica.PessoaID = pessoaID;
				}

			}

			if (!string.IsNullOrEmpty(pessoaID))
			{
				_obj.PessoaID = pessoaID;
				if (_obj.PessoaTelefone != null && _obj.PessoaTelefone.Count > 0)
				{
					foreach (var item in _obj.PessoaTelefone)
					{
						item.PessoaID = pessoaID;
					}
				}
				if (_obj.PessoaEndereco != null && _obj.PessoaEndereco.Count > 0)
				{
					foreach (var item in _obj.PessoaEndereco)
					{
						item.PessoaID = pessoaID;
					}
				}
				if (_obj.PessoaEmail != null && _obj.PessoaEmail.Count > 0)
				{
					foreach (var item in _obj.PessoaEmail)
					{
						item.PessoaID = pessoaID;
					}
				}
				_obj.TipoEvento = "U";
				_obj.NomeEvento = "Update";
				Update(_obj);
				return true;
			}
			return false;
		}

		public async Task<string> Save(Domain.Pessoas.Entities.Pessoa _obj)
		{
			int count = 0;
			string msgErro = ParametersLogErro.GerarCodigoErro(ParametersLogErro.tipoMicroServico);

			if (!await PessoaUpdate(_obj))
			{
				_obj.GerenciaID();
				foreach (var pessoaemail in _obj.PessoaEmail)
				{
					pessoaemail.GenerateID();
				}
				foreach (var pessoaEnd in _obj.PessoaEndereco)
				{
					pessoaEnd.GenerateID();
				}
				try
				{
					////salva na fila do Mongo
					//QueueParameters.Message = _obj;
					//QueueParameters.QueuelName = Parameters.ListenMongo;
					//QueueParameters.KeyChannel = Parameters.ListenMongo;
					//_pub.Send();
					_pessoaMongo.Save(_obj);
				}
				catch (Exception ex)
				{
					var client = new RestClient(Parameters.routeLog);
					var request = new RestRequest("SaveLogErro", Method.POST);
					LogErroViewModel logErro = new LogErroViewModel
					{
						EnviarEmail = true,
						ExceptionMessage = ex.Message,
						NomeClasse = "PessoasRepository",
						NomeMetodo = "Save ListenMongoPessoa",
						ObjetoFalha = JsonConvert.SerializeObject(_obj),
						TipoMicroServico = ParametersLogErro.tipoMicroServico,
						CodigoErro = msgErro
					};
					request.AddJsonBody(JsonConvert.SerializeObject(logErro));
					await client.ExecuteTaskAsync(request);
					count++;
				}
				//try
				//{
				//	//salva na fila do MySQL
				//	QueueParameters.QueuelName = Parameters.ListenMySQL;
				//	QueueParameters.KeyChannel = Parameters.ListenMySQL;
				//	_pub.Send();
				//}
				//catch (Exception ex)
				//{
				//	var client = new RestClient(Parameters.routeLog);
				//	var request = new RestRequest("SaveLogErro", Method.POST);
				//	LogErroViewModel logErro = new LogErroViewModel
				//	{
				//		EnviarEmail = true,
				//		ExceptionMessage = ex.Message,
				//		NomeClasse = "PessoasRepository",
				//		NomeMetodo = "Save ListenMySQLPessoa",
				//		ObjetoFalha = JsonConvert.SerializeObject(_obj),
				//		TipoMicroServico = ParametersLogErro.tipoMicroServico,
				//		CodigoErro = msgErro
				//	};
				//	request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				//	await client.ExecuteTaskAsync(request);
				//	count++;
				//}
			}

			if (count == 1)
			{
				throw new Exception(ListError.MicroServiceCrash(ParametersLogErro.tipoMicroServico));
			}
			return _obj.PessoaID;
		}

		public async Task Update(Domain.Pessoas.Entities.Pessoa _obj)
		{
			int count = 0;
			string msgErro = ParametersLogErro.GerarCodigoErro(ParametersLogErro.tipoMicroServico);

			try
			{
				//QueueParameters.Message = _obj;
				//QueueParameters.QueuelName = Parameters.ListenMongo;
				//QueueParameters.KeyChannel = Parameters.ListenMongo;
				//_pub.Send();
				_pessoaMongo.Update(_obj);
			}
			catch (Exception ex)
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = true,
					ExceptionMessage = ex.Message,
					NomeClasse = "PessoasRepository",
					NomeMetodo = "Update ListenMongoPessoa",
					ObjetoFalha = JsonConvert.SerializeObject(_obj),
					TipoMicroServico = ParametersLogErro.tipoMicroServico
				};
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				await client.ExecuteTaskAsync(request);
				count++;
			}

			//try
			//{
			//	QueueParameters.QueuelName = Parameters.ListenMySQL;
			//	QueueParameters.KeyChannel = Parameters.ListenMySQL;
			//	_pub.Send();
			//}
			//catch (Exception ex)
			//{
			//	var client = new RestClient(Parameters.routeLog);
			//	var request = new RestRequest("SaveLogErro", Method.POST);
			//	LogErroViewModel logErro = new LogErroViewModel
			//	{
			//		EnviarEmail = true,
			//		ExceptionMessage = ex.Message,
			//		NomeClasse = "PessoasRepository",
			//		NomeMetodo = "Update ListenMySQLPessoa",
			//		ObjetoFalha = JsonConvert.SerializeObject(_obj),
			//		TipoMicroServico = ParametersLogErro.tipoMicroServico
			//	};
			//	request.AddJsonBody(JsonConvert.SerializeObject(logErro));
			//	await client.ExecuteTaskAsync(request);
			//	count++;
			//}

			if (count == 1)
			{
				throw new Exception(ListError.MicroServiceCrash(ParametersLogErro.tipoMicroServico));
			}
		}

		public void Delete(string ID)
		{
			int count = 0;

			try
			{
				//QueueParameters.Message = new Domain.Pessoas.Entities.Pessoa { PessoaID = ID, NomeEvento = "Delete", TipoEvento = "D" };
				//QueueParameters.QueuelName = Parameters.ListenMongo;
				//QueueParameters.KeyChannel = Parameters.ListenMongo;
				//_pub.Send();
				_pessoaMongo.Delete(ID);
			}
			catch (Exception ex)
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = true,
					ExceptionMessage = ex.Message,
					NomeClasse = "PessoasRepository",
					NomeMetodo = "Delete ListenMongoPessoa",
					ObjetoFalha = $"ID:{ID}",
					TipoMicroServico = ParametersLogErro.tipoMicroServico
				};
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				client.Execute(request);
				count++;
			}
			//try
			//{
			//	QueueParameters.QueuelName = Parameters.ListenMySQL;
			//	QueueParameters.KeyChannel = Parameters.ListenMySQL;
			//	_pub.Send();
			//}
			//catch (Exception ex)
			//{
			//	var client = new RestClient(Parameters.routeLog);
			//	var request = new RestRequest("SaveLogErro", Method.POST);
			//	LogErroViewModel logErro = new LogErroViewModel
			//	{
			//		EnviarEmail = true,
			//		ExceptionMessage = ex.Message,
			//		NomeClasse = "PessoasRepository",
			//		NomeMetodo = "Delete ListenMySQLPessoa",
			//		ObjetoFalha = $"ID:{ID}",
			//		TipoMicroServico = ParametersLogErro.tipoMicroServico
			//	};
			//	request.AddJsonBody(JsonConvert.SerializeObject(logErro));
			//	client.Execute(request);
			//	count++;
			//}

			if (count == 1)
			{
				throw new Exception(ListError.MicroServiceCrash(ParametersLogErro.tipoMicroServico));
			}
		}

		
	}
}
