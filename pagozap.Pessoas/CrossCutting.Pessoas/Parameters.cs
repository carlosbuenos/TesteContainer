using CrossCutting.LogErros;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CrossCutting.Pessoas
{
	public class Parameters : IParameters
	{
		public static Exception ultimoException;


		public static string host = "";
		public static string hostRabbit = "";
		public static string hostUser = "";
		public static string hostPassword = "";
		public static string hostMongo = "";
		public static string StrConnMongo = "";
		public static string hostMySQL = "";
		public static string strConnMySql = "";
		public static string ListenMongo = "ListenMongoPessoa";
		public static string ListenMySQL = "ListenMySqlPessoa";
		public static string routeLog = "";
		public static string FilePathCertificateSSl = "";
		public Parameters()
		{
			ParametersLogErro.tipoMicroServico = TipoMicroServico.Pessoas;
		}
		public async Task SetParameters()
		{

			string full = @"/var/tmp";
			//string full = @"C:\PROJETOS\_BitBucket_Pagozap_Microservices\MicroServices\MSPessoas\pagozap.Pessoas\Api.Pessoas\var\tmp\";
			string Existe = "";
			string leituraArquivo = "";
			ObjConfigurar obj = new ObjConfigurar();
			try
			{

				if (File.Exists(@Path.Combine(full, "configurar.txt")))
				{
					Existe = "Sim esxite";
					//string JSON = "{";
					//JSON += "hostBase: 'pagozap.mooo.com',";
					//JSON += "hostRabbit: 'pagozap.mooo.com',";
					//JSON += "hostUser: 'pagozap',";
					//JSON += "hostPassword: 'cGFnb3phcDIwMTg=',";
					//JSON += "hostMongo: 'pagozap.chickenkiller.com',";
					//JSON += "hostMySQL: 'pagozap.cwjukmdwlgvt.sa-east-1.rds.amazonaws.com',";
					//JSON += "routeLog: 'http://pagozap.mooo.com:9006/api/LogErros/',";
					//JSON += "}";
					leituraArquivo += File.ReadAllText(@Path.Combine(full, "configurar.txt"));

					obj = JsonConvert.DeserializeObject<ObjConfigurar>(leituraArquivo);
					if (string.IsNullOrEmpty(host) || obj.hostBase != host)
					{
						host = obj.hostBase;
					}
					if (string.IsNullOrEmpty(hostUser) || obj.hostUser != hostUser)
					{
						hostUser = obj.hostUser;
					}
					if (string.IsNullOrEmpty(hostPassword) || obj.hostPassword != hostPassword)
					{
						hostPassword = Descriptografar(obj.hostPassword);
					}
					if (string.IsNullOrEmpty(hostMongo) || obj.hostMongo != hostMongo)
					{
						hostMongo = obj.hostMongo;
					}
					if (string.IsNullOrEmpty(hostMySQL) || obj.hostMySQL != hostMySQL)
					{
						hostMySQL = obj.hostMySQL;
					}
					if (string.IsNullOrEmpty(hostRabbit) || obj.hostRabbit != hostRabbit)
					{
						hostRabbit = obj.hostRabbit;
					}
					if (string.IsNullOrEmpty(routeLog) || obj.routeLog != routeLog)
					{
						routeLog = obj.routeLog;
					}
					if (string.IsNullOrEmpty(FilePathCertificateSSl) || obj.FilePathCertificateSSl != FilePathCertificateSSl)
					{
						FilePathCertificateSSl = obj.FilePathCertificateSSl;
					}
					string mongo = $"mongodb://{hostUser}:{hostPassword}@{hostMongo}";
					if (string.IsNullOrEmpty(StrConnMongo) || mongo != StrConnMongo)
					{
						StrConnMongo = mongo;
					}
					string mysql = $"Server={hostMySQL};Database=Pessoas;Uid={hostUser};Pwd={hostPassword}";
					if (string.IsNullOrEmpty(strConnMySql) || mysql != strConnMySql)
					{
						strConnMySql = mysql;
					}


				}
				else
				{
					Existe = "Não Existe";
				}


				//var client = new RestClient(Parameters.routeLog);
				//var request = new RestRequest("SaveLogErro", Method.POST);
				//LogErroViewModel logErro = new LogErroViewModel
				//{
				//	EnviarEmail = false,
				//	ExceptionMessage = "Acessando pasta no servidor pelo container",
				//	NomeClasse = "PessoasCossCustting",
				//	NomeMetodo = "Construtor",
				//	ObjetoFalha = full + " ---" + Existe + " ---" + leituraArquivo + "---" + JsonConvert.SerializeObject(obj),
				//	TipoMicroServico = ParametersLogErro.tipoMicroServico
				//};
				//logErro.CodigoErro = ParametersLogErro.GerarCodigoErro(logErro.TipoMicroServico);
				//request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				//client.Execute(request);

			}
			catch (Exception)
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = false,
					ExceptionMessage = "Falha ao localizar arquivos dentro da pasta",
					NomeClasse = "PessoasCossCustting",
					NomeMetodo = "Construtor",
					ObjetoFalha = full + "--- " + Existe + " ---" + leituraArquivo + "--" + JsonConvert.SerializeObject(obj),
					TipoMicroServico = ParametersLogErro.tipoMicroServico
				};
				logErro.CodigoErro = ParametersLogErro.GerarCodigoErro(logErro.TipoMicroServico);
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				await client.ExecuteTaskAsync(request);
			}
		}

		public static string Criptografar(string valor)
		{
			string chaveCripto;
			Byte[] cript = System.Text.ASCIIEncoding.ASCII.GetBytes(valor);
			chaveCripto = Convert.ToBase64String(cript);
			return chaveCripto;


		}

		public static string Descriptografar(string valor)
		{
			string chaveCripto;
			Byte[] cript = Convert.FromBase64String(valor);
			chaveCripto = System.Text.ASCIIEncoding.ASCII.GetString(cript);
			return chaveCripto;

		}
	}
}
