using CrossCutting.LogErros;
using CrossCutting.Pessoas;
using Infra.Pessoas.Repository;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Pessoas.HubManagerData
{
    public class StartListeners: IStartListeners
	{
		public void startListenersBase() {
			ParametersLogErro.tipoMicroServico = TipoMicroServico.Pessoas;


			try
			{
				new ListenQueueMongo().StartSerevrListenQueue(Parameters.hostRabbit, Parameters.hostUser, Parameters.hostPassword, Parameters.ListenMongo, Parameters.ListenMongo);
			}
			catch (Exception ex)
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel();
				logErro.EnviarEmail = false;
				logErro.ExceptionMessage = ex.Message;
				logErro.NomeClasse = "Program";
				logErro.NomeMetodo = "Main";
				logErro.ObjetoFalha = Parameters.ListenMongo;
				logErro.TipoMicroServico = ParametersLogErro.tipoMicroServico;
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				client.Execute(request);
			}
			try
			{
				
				new ListenQueueMySql().StartSerevrListenQueue(Parameters.hostRabbit, Parameters.hostUser, Parameters.hostPassword, Parameters.ListenMySQL, Parameters.ListenMySQL);
			}
			catch (Exception ex)
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel();
				logErro.EnviarEmail = false;
				logErro.ExceptionMessage = ex.Message;
				logErro.NomeClasse = "Program";
				logErro.NomeMetodo = "Main";
				logErro.ObjetoFalha = Parameters.ListenMySQL;
				logErro.TipoMicroServico = ParametersLogErro.tipoMicroServico;
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				client.Execute(request);
			}
		}
    }
}
