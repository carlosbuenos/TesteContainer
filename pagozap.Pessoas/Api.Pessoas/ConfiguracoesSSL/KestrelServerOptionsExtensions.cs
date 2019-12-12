using CrossCutting.LogErros;
using CrossCutting.Pessoas;
using Infra.Pessoas.IoC;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Api.Pessoas.ConfiguracoesSSL
{
	/// <summary>
	/// 
	/// </summary>
	public static class KestrelServerOptionsExtensions
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="options"></param>
		/*public static void ConfigureEndpoints(this KestrelServerOptions options)
		{
			var configuration = options.ApplicationServices.GetRequiredService<IConfiguration>();
			var environment = options.ApplicationServices.GetRequiredService<IHostingEnvironment>();

			var endpoints = configuration.GetSection("HttpServer:Endpoints")
				.GetChildren()
				.ToDictionary(section => section.Key, section =>
				{
					var endpoint = new EndpointConfiguration();
					section.Bind(endpoint);
					return endpoint;
				});

			foreach (var endpoint in endpoints)
			{
				var config = endpoint.Value;
				var port = config.Port ?? (config.Scheme == "https" ? (int)config.Port : 9000);

				options.Listen(IPAddress.Any, port,
					listenOptions =>
					{
						if (config.Scheme == "https")
						{
							var certificate = LoadCertificate(config, environment);
							listenOptions.UseHttps(certificate);
						}
					});

			}
		}*/

		public static X509Certificate2 LoadCertificate(this KestrelServerOptions options)
		{
			var parameters = IoCGeral.Container.GetInstance<IParameters>();
			parameters.SetParameters();

			var configuration = options.ApplicationServices.GetRequiredService<IConfiguration>();
			var environment = options.ApplicationServices.GetRequiredService<IHostingEnvironment>();
			EndpointConfiguration config = new EndpointConfiguration();
			var endpoints = configuration.GetSection("HttpServer:Endpoints")
				.GetChildren()
				.ToDictionary(section => section.Key, section =>
				{
					var endpoint = new EndpointConfiguration();
					section.Bind(endpoint);
					return endpoint;
				});
			foreach (var endpoint in endpoints)
			{
				config = JsonConvert.DeserializeObject<EndpointConfiguration>(JsonConvert.SerializeObject(endpoint.Value));
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				string existe = "";
				if (!string.IsNullOrEmpty(Parameters.FilePathCertificateSSl) && File.Exists(Parameters.FilePathCertificateSSl))
				{
					existe = "Existe-Sim";
				}
				else
				{
					existe = "Não-Existe";
				}
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = false,
					ExceptionMessage = "Acessando pasta no servidor pelo container",
					NomeClasse = "PessoasCossCustting",
					NomeMetodo = "CFG",
					ObjetoFalha = existe + "--" + JsonConvert.SerializeObject(endpoint.Value),
					TipoMicroServico = ParametersLogErro.tipoMicroServico
				};
				logErro.CodigoErro = ParametersLogErro.GerarCodigoErro(logErro.TipoMicroServico);
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				client.Execute(request);
			}

			var port = config.Port ?? (config.Scheme == "https" ? (int)config.Port : 9000);

			if (!string.IsNullOrEmpty(config.StoreName) && !string.IsNullOrEmpty(config.StoreLocation))
			{
				using (var store = new X509Store(config.StoreName, Enum.Parse<StoreLocation>(config.StoreLocation)))
				{
					store.Open(OpenFlags.ReadOnly);
					var certificate = store.Certificates.Find(
						X509FindType.FindBySubjectName,
						config.Host,
						validOnly: !environment.IsDevelopment());

					if (certificate.Count == 0)
					{
						throw new InvalidOperationException($"Certificate not found for {config.Host}.");
					}

					return certificate[0];
				}
			}

			if (!string.IsNullOrEmpty(Parameters.FilePathCertificateSSl))
			{
				var client = new RestClient(Parameters.routeLog);
				var request = new RestRequest("SaveLogErro", Method.POST);
				LogErroViewModel logErro = new LogErroViewModel
				{
					EnviarEmail = false,
					ExceptionMessage = "Acessando pasta no servidor pelo container",
					NomeClasse = "PessoasCossCustting",
					NomeMetodo = "CFG",
					ObjetoFalha = "Entrou--" + Parameters.hostPassword + "---" + Parameters.FilePathCertificateSSl,
					TipoMicroServico = ParametersLogErro.tipoMicroServico
				};
				logErro.CodigoErro = ParametersLogErro.GerarCodigoErro(logErro.TipoMicroServico);
				request.AddJsonBody(JsonConvert.SerializeObject(logErro));
				client.Execute(request);
				return new X509Certificate2(Parameters.FilePathCertificateSSl, Parameters.hostPassword);
			}

			throw new InvalidOperationException("No valid certificate configuration found for the current endpoint.");

		}
	}
}
