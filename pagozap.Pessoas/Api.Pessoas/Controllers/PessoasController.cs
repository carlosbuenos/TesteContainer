using App.Pessoas.Interface;
using CrossCutting.LogErros;
using CrossCutting.Pessoas;
using Domain.Pessoas.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Pessoas.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	[Produces("application/json")]
	[Route("api/Pessoas")]
	public class PessoasController : BaseController
	{
		IPessoasApp _app;
		/// <summary>
		/// 
		/// </summary>
		public PessoasController(IPessoasApp app)
		{
			_app = app;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>

		[HttpGet]
		[Route("GetAll")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var pessoas = await _app.GetAll();
				if (pessoas != null && pessoas.Count() > 0)
				{
					Response.StatusCode = 200;
					return StatusCode(200, pessoas);
				}
				else
				{
					Response.StatusCode = 500;
					return Json(ListError.RequiredObjectIsNull);
				}
			}
			catch (Exception ex)
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = false,
					ExceptionMessage = ex.Message,
					NomeClasse = "PessoasController",
					NomeMetodo = "GetAll",
					ObjetoFalha = "Não há objeto/parametros",
					TipoMicroServico = ParametersLogErro.tipoMicroServico
				};
				logErro.CodigoErro = ParametersLogErro.GerarCodigoErro(logErro.TipoMicroServico);
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				await client.ExecuteTaskAsync(request);

				throw new Exception(logErro.CodigoErro);
			}

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="Page"></param>
		/// <param name="PageSize"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetAll/{Page}/{PageSize}")]
		public async Task<IActionResult> GetAll(int Page, int PageSize)
		{
			try
			{
				var pessoas = await _app.GetAll(Page, PageSize);
				if (pessoas != null && pessoas.Count() > 0)
				{
					Response.StatusCode = 200;
					return StatusCode(200, pessoas);
				}
				else
				{
					Response.StatusCode = 500;
					return Json(ListError.RequiredObjectIsNull);
				}
			}
			catch (Exception ex)
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = false,
					ExceptionMessage = ex.Message,
					NomeClasse = "PessoasController",
					NomeMetodo = "GetAll",
					ObjetoFalha = $"Page:{Page} PageSize:{PageSize}",
					TipoMicroServico = ParametersLogErro.tipoMicroServico
				};
				logErro.CodigoErro = ParametersLogErro.GerarCodigoErro(logErro.TipoMicroServico);
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				await client.ExecuteTaskAsync(request);

				throw new Exception(logErro.CodigoErro);
			}

		}

		/// <summary>
		/// Preencha um objeto Pessoa.Pessoajuridica e preenhca o campo CNPJ
		/// Com o Valor desejado e envie para a fila Pessoas
		/// preencha também IDTransctionQueue e QueueNameRequest(sÃO PROPRIEDADES NA CLASSE PESSOAS)
		/// com estas propriedades preenchidas o serviço pessoa entregará o resultado na fila 
		/// do serviço que requisitou.
		/// </summary>
		/// <returns>HttpResponseMessage</returns>
		[HttpGet]
		[Route("GetByCNPJ/{CNPJ}")]
		public async Task<IActionResult> GetByCNPJ(string CNPJ)
		{
			try
			{
				var pessoa = await _app.GetByCNPJ(CNPJ);
				if (pessoa != null)
				{
					Response.StatusCode = 200;
					return StatusCode(200, pessoa);
				}
				else
				{
					Response.StatusCode = 500;
					return Json(ListError.RequiredObjectIsNull);
				}
			}
			catch (Exception ex)
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = false,
					ExceptionMessage = ex.Message,
					NomeClasse = "PessoasController",
					NomeMetodo = "GetByCNPJ",
					ObjetoFalha = $"CNPJ:{CNPJ}",
					TipoMicroServico = ParametersLogErro.tipoMicroServico
				};
				logErro.CodigoErro = ParametersLogErro.GerarCodigoErro(logErro.TipoMicroServico);
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				await client.ExecuteTaskAsync(request);

				throw new Exception(logErro.CodigoErro);
			}
		}

		/// <summary>
		/// Preencha um objeto Pessoa.PessoaFisica e preenhca o campo CPF
		/// Com o Valor desejado e envie para a fila Pessoas
		/// preencha também IDTransctionQueue e QueueNameRequest(sÃO PROPRIEDADES NA CLASSE PESSOAS)
		/// com estas propriedades preenchidas o serviço pessoa entregará o resultado na fila 
		/// do serviço que requisitou.
		/// </summary>
		///<returns>HttpResponseMessage</returns>
		[HttpGet]
		[Route("GetByCPF/{CPF}")]
		public async Task<IActionResult> GetByCPF(string CPF)
		{
			try
			{
				var pessoa = await _app.GetByCPF(CPF);
				if (pessoa != null)
				{
					Response.StatusCode = 200;
					return StatusCode(200, pessoa);
				}
				else
				{
					Response.StatusCode = 500;
					return Json(ListError.RequiredObjectIsNull);
				}
			}
			catch (Exception ex)
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = false,
					ExceptionMessage = ex.Message,
					NomeClasse = "PessoasController",
					NomeMetodo = "GetByCPF",
					ObjetoFalha = $"CNPJ:{CPF}",
					TipoMicroServico = ParametersLogErro.tipoMicroServico
				};
				logErro.CodigoErro = ParametersLogErro.GerarCodigoErro(logErro.TipoMicroServico);
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				await client.ExecuteTaskAsync(request);

				throw new Exception(logErro.CodigoErro);
			}

		}

		/// <summary>
		/// Preencha um objeto Pessoa e preenhca o campo PessoaID
		/// Com o Valor desejado e envie para a fila Pessoas
		/// preencha também IDTransctionQueue e QueueNameRequest(sÃO PROPRIEDADES NA CLASSE PESSOAS)
		/// com estas propriedades preenchidas o serviço pessoa entregará o resultado na fila 
		/// do serviço que requisitou.
		/// </summary>
		/// <returns>HttpResponseMessage</returns>
		[HttpGet]
		[Route("GetByID/{ID}")]
		public async Task<JsonResult> GetByID(string ID)
		{
			try
			{
				var pessoa = await _app.GetByID(ID);
				if (pessoa != null)
				{
					Response.StatusCode = 200;
					return Json(pessoa);
				}
				else
				{
					Response.StatusCode = 500;
					return Json(ListError.RequiredObjectIsNull);
				}
			}
			catch (Exception ex)
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = false,
					ExceptionMessage = ex.Message,
					NomeClasse = "PessoasController",
					NomeMetodo = "GetByID",
					ObjetoFalha = $"ID:{ID}",
					TipoMicroServico = ParametersLogErro.tipoMicroServico
				};
				logErro.CodigoErro = ParametersLogErro.GerarCodigoErro(logErro.TipoMicroServico);
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				client.Execute(request);
				Response.StatusCode = 500;

				return Json(logErro.CodigoErro);
			}

		}
		/// <summary>
		/// METODO SERVE PARA ACELERAR O RETORNO DE PESSOA PARA PAGADOR FACILITANDO EM UMA REQUISIÇÃO RETORNAR N PESSOAS PAGADORAS DE UM DETERMINADO CLIENTE
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		[HttpGet]
		[Route("GetByListID")]
		public async Task<JsonResult> GetByListID(List<string> ID)
		{
			try
			{
				List<Pessoa> pessoa = new List<Pessoa>();
				pessoa = _app.GetByListID(ID);
				if (pessoa == null)
				{
					foreach (var item in ID)
					{
						var pess = await _app.GetByID(item);
						pessoa.Add(pess);
					}
				}
				
				if (pessoa != null)
				{
					Response.StatusCode = 200;
					return Json(pessoa);
				}
				else
				{
					Response.StatusCode = 500;
					return Json(ListError.RequiredObjectIsNull);
				}
			}
			catch (Exception ex)
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = false,
					ExceptionMessage = ex.Message,
					NomeClasse = "PessoasController",
					NomeMetodo = "GetByID",
					ObjetoFalha = $"ID:{ID}",
					TipoMicroServico = ParametersLogErro.tipoMicroServico
				};
				logErro.CodigoErro = ParametersLogErro.GerarCodigoErro(logErro.TipoMicroServico);
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				await client.ExecuteTaskAsync(request);
				Response.StatusCode = 500;

				return Json(logErro.CodigoErro);
			}

		}

		/// <summary>
		/// Preencha um objeto Pessoa de preferencia completo
		/// Com os Valores desejados e envie para a fila Pessoas
		/// preencha também IDTransctionQueue e QueueNameRequest(sÃO PROPRIEDADES NA CLASSE PESSOAS)
		/// com estas propriedades preenchidas o serviço pessoa entregará o resultado na fila 
		/// do serviço que requisitou.
		/// </summary>
		/// <returns>HttpResponseMessage</returns>
		[HttpPost]
		[Route("SaveOrUpdate")]
		public async Task<JsonResult> SaveOrUpdate([FromBody]Pessoa pessoa)
		{
			try
			{
				if (pessoa != null)
				{
					//var pessoa = JsonConvert.DeserializeObject<Pessoa>(pessoaString);

					if (string.IsNullOrEmpty(pessoa.PessoaID))
					{
						pessoa.TipoEvento = "I";
						pessoa.NomeEvento = "SAVE";

						var resposta = await _app.Save(pessoa);
						Response.StatusCode = 200;
						return Json(resposta);
					}
					else
					{
						pessoa.TipoEvento = "U";
						pessoa.NomeEvento = "UPDATE";
						await _app.Update(pessoa);

						Response.StatusCode = 200;
						return Json(pessoa.PessoaID);
					}
				}
				else
				{
					throw new Exception(ListError.RequiredObjectIsNull);
				}
			}
			catch (Exception ex)
			{
				string msgErro = ParametersLogErro.GerarCodigoErro(ParametersLogErro.tipoMicroServico);
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = true,
					ExceptionMessage = ex.Message,
					NomeClasse = "PessoasRepository",
					NomeMetodo = "Save ListenMongoPessoa",
					ObjetoFalha = JsonConvert.SerializeObject(pessoa),
					TipoMicroServico = ParametersLogErro.tipoMicroServico,
					CodigoErro = msgErro
				};
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				client.Execute(request);
				Response.StatusCode = 500;
				return Json(msgErro);
			}
		}

		//[HttpPost]
		//[Route("Save")]
		//public JsonResult SavePessoa([FromBody]Domain.Pessoas.Entities.Pessoas pessoa)
		//{
		//    try
		//    {
		//        if (pessoa != null)
		//        {
		//            return Json(_app.Save(pessoa));
		//        }
		//        else
		//        {
		//            throw new Exception("Objeto à qual se realizaria tratamento está null");
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//}

		//[HttpPost]
		//[Route("Update")]
		//public JsonResult Update([FromBody]Domain.Pessoas.Entities.Pessoas pessoa)
		//{
		//    try
		//    {
		//        if (pessoa != null)
		//        {
		//            _app.Update(pessoa);
		//            var resposta = new MensageriaResponse();
		//            resposta.Objeto = null;
		//            resposta.Mensageria = Mensageria.Success;
		//            return Json(resposta);
		//        }
		//        else
		//        {
		//            throw new Exception(ListError.RequiredObjectIsNull);
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//}

		/// <summary>
		/// Preencha um objeto Pessoa e preenhca o campo PessoaID.
		/// preencha também IDTransctionQueue e QueueNameRequest(sÃO PROPRIEDADES NA CLASSE PESSOAS)
		/// com estas propriedades preenchidas o serviço pessoa entregará o resultado na fila 
		/// do serviço que requisitou.
		/// </summary>
		/// <returns>HttpResponseMessage</returns>
		[HttpPost]
		[Route("Delete/{pessoaID}")]
		public JsonResult Delete(string pessoaID)
		{
			try
			{
				if (!string.IsNullOrEmpty(pessoaID))
				{
					_app.Delete(pessoaID);
					Response.StatusCode = 200;
					return Json(true);
				}
				else
				{
					throw new Exception(ListError.RequiredObjectIsNull);
				}
			}
			catch (Exception ex)
			{
				string msgErro = ParametersLogErro.GerarCodigoErro(ParametersLogErro.tipoMicroServico);
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = true,
					ExceptionMessage = ex.Message,
					NomeClasse = "PessoasRepository",
					NomeMetodo = "Save ListenMongoPessoa",
					ObjetoFalha = JsonConvert.SerializeObject(new { pessoaID = pessoaID }),
					TipoMicroServico = ParametersLogErro.tipoMicroServico,
					CodigoErro = msgErro
				};
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				client.Execute(request);
				Response.StatusCode = 500;
				return Json(msgErro);
			}

		}


	}
}