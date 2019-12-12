using Api.Pessoas.ConfiguracoesSSL;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Net;
using System.Security.Authentication;

namespace Api.Pessoas
{
	/// <summary>
	/// 
	/// </summary>
	public class Program
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		public static void Main(string[] args)
		{

			BuildWebHost(args).Run();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
			    .UseLibuv()
				.UseKestrel(options =>
				{
					options.Listen(IPAddress.Any, 9000);
					//options.Listen(IPAddress.Any, 9000, configOPT =>
					//{
					//	configOPT.Protocols = HttpProtocols.Http1AndHttp2;
					//	//var certificate = KestrelServerOptionsExtensions.LoadCertificate(options);
					//	//configOPT.UseHttps(new HttpsConnectionAdapterOptions
					//	//{
					//	//	ServerCertificate = certificate,
					//	//});
					//});


				})
				.Build();


	}
}
