using CrossCutting.LogErros;
using CrossCutting.Pessoas;
using Domain.Pessoas.Entities;
using Domain.Pessoas.Interfaces.Repository;
using Infra.Pessoas.DataAccess.FactoryTypeConn;
using Infra.Pessoas.DataAccess.MySql;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Pessoas.Repository
{
	public class HubMysql : Repository<Pessoa>, IPessoasMySqlRepository
	{
		public MySqlContext _MySqlCTX;
		public HubMysql()
		{
			_MySqlCTX = new MySqlContext(new ConectaMySql().StrConnect());
		}
		public List<Pessoa> GetByListID(List<string> Ids)
		{
			var pessoa = (from p in _MySqlCTX.Pessoas.AsQueryable<Pessoa>()
						  where Ids.Contains(p.PessoaID)
						  select p
						  ).AsQueryable();
			return pessoa.ToList();
		}
		public async Task<List<Pessoa>> GetAll()
		{
			try
			{
				var pessoa = _MySqlCTX.Pessoas
				.Include(x => x.PessoaFisica)
				.Include(x => x.PessoaJuridica)
				.Include(x => x.PessoaTelefone)
				.Include(x => x.PessoaEndereco)
				.Include(x => x.PessoaEmail).OrderBy(x => x.PessoaID)
				.AsQueryable();

				return await pessoa.ToListAsync();
			}
			finally
			{


			}

		}

		public async Task<List<Pessoa>> GetAll(int page, int pageSize)
		{
			try
			{
				var pessoa = _MySqlCTX.Pessoas
				.Include(x => x.PessoaFisica)
				.Include(x => x.PessoaJuridica)
				.Include(x => x.PessoaTelefone)
				.Include(x => x.PessoaEndereco)
				.Include(x => x.PessoaEmail).OrderBy(x => x.PessoaID)
				.AsQueryable();
				var skip = (page - 1) * pageSize;
				return await pessoa.Skip(skip).Take(pageSize).ToListAsync();
			}
			finally
			{

				_MySqlCTX.Dispose();
			}

		}

		public async Task<Pessoa> GetByCNPJ(string CNPJ)
		{
			try
			{


				var pessoa =await  _MySqlCTX.Pessoas
					.Include(x => x.PessoaFisica)
					.Include(x => x.PessoaJuridica)
					.Include(x => x.PessoaTelefone)
					.Include(x => x.PessoaEndereco)
					.Include(x => x.PessoaEmail)
					.Where(x => x.PessoaJuridica.CNPJ.Equals(CNPJ)).FirstOrDefaultAsync();

				if (pessoa != null)
				{
					return pessoa;
				}
				return null;


			}
			finally
			{

				_MySqlCTX.Dispose();
			}
		}

		public async Task<Pessoa> GetByCPF(string CPF)
		{
			try
			{
				var pessoa = await _MySqlCTX.Pessoas
					.Include(x => x.PessoaFisica)
					.Include(x => x.PessoaJuridica)
					.Include(x => x.PessoaTelefone)
					.Include(x => x.PessoaEndereco)
					.Include(x => x.PessoaEmail).Where(x => x.PessoaFisica.CPF.Equals(CPF)).FirstOrDefaultAsync();

				if (pessoa != null)
				{
					return pessoa;
				}
				return null;

			}
			finally
			{

				_MySqlCTX.Dispose();
			}
		}

		public async Task<Pessoa> GetByID(string ID)
		{
			try
			{


				var pessoa = await _MySqlCTX.Pessoas
				   .Include(x => x.PessoaFisica)
				   .Include(x => x.PessoaJuridica)
				   .Include(x => x.PessoaTelefone)
				   .Include(x => x.PessoaEndereco)
				   .Include(x => x.PessoaEmail).Where(x => x.PessoaID.Equals(ID)).FirstOrDefaultAsync();
				if (pessoa != null)
				{
					return pessoa;
				}
				return null;
			}
			finally
			{

				_MySqlCTX.Dispose();
			}
		}

		public void Save(Domain.Pessoas.Entities.Pessoa _obj)
		{
			try
			{
				_MySqlCTX.Set<Pessoa>().Add(_obj);
				_MySqlCTX.SaveChanges();
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
						NomeClasse = "HubMySQL",
						NomeMetodo = "Save",
						ObjetoFalha = JsonConvert.SerializeObject(_obj),
						TipoMicroServico = ParametersLogErro.tipoMicroServico
					};
					request.AddJsonBody(JsonConvert.SerializeObject(logErro));
					client.Execute(request);
				}
				throw ex;
			}
			finally
			{

				_MySqlCTX.Dispose();
			}
		}

		public void Update(Domain.Pessoas.Entities.Pessoa _obj)
		{
			try
			{
				if (_obj != null)
				{
					_MySqlCTX.Update(_obj);
					_MySqlCTX.SaveChanges();
				}
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
						NomeClasse = "HubMySQL",
						NomeMetodo = "Update",
						ObjetoFalha = JsonConvert.SerializeObject(_obj),
						TipoMicroServico = ParametersLogErro.tipoMicroServico
					};
					request.AddJsonBody(JsonConvert.SerializeObject(logErro));
					client.Execute(request);
				}
				throw ex;

			}
			finally
			{

				_MySqlCTX.Dispose();
			}


		}

		public void Delete(string ID)
		{
			try
			{
				if (ID != string.Empty)
				{
					var pessoadel = new Domain.Pessoas.Entities.Pessoa { PessoaID = ID };
					_MySqlCTX.Entry<Pessoa>(pessoadel).State = EntityState.Deleted;
					_MySqlCTX.SaveChanges();
				}
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
						NomeClasse = "HubMySQL",
						NomeMetodo = "Delete",
						ObjetoFalha = $"ID:{ID}",
						TipoMicroServico = ParametersLogErro.tipoMicroServico
					};
					request.AddJsonBody(JsonConvert.SerializeObject(logErro));
					client.Execute(request);
				}
				throw ex;
			}
			finally
			{

				_MySqlCTX.Dispose();
			}

		}

		

		~HubMysql()
		{

			_MySqlCTX.Dispose();
		}
	}
}
