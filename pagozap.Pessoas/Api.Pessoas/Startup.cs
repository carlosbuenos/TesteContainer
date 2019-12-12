using Api.Pessoas.ConfiguracoesSSL;
using CrossCutting.LogErros;
using CrossCutting.Pessoas;
using Infra.Pessoas.HubManagerData;
using Infra.Pessoas.IoC;
using Infra.Pessoas.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using RestSharp;
using SimpleInjector;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;

namespace Api.Pessoas
{
	/// <summary>
	/// 
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// 
		/// </summary>
		public static Container container;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{

			Configuration = configuration;
			

		}
		/// <summary>
		/// 
		/// </summary>
		public IConfiguration Configuration { get; }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="services"></param>
		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().AddJsonOptions(
				//options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
				options => options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
			);
			/*IntegrateSimpleInjector(services);*/
			services.AddResponseCompression(options =>
			{
				options.Providers.Add<GzipCompressionProvider>();
				
			});

			
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1",
					new Info
					{
						Title = "Api.Pessoas",
						Version = "v1",
						Description = "API REST criada com o ASP.NET Core",
						Contact = new Contact
						{
							Name = "Declink",
							Url = ""
						}
						
					});
				string caminhoAplicacao =
				 PlatformServices.Default.Application.ApplicationBasePath;
				string nomeAplicacao =
					PlatformServices.Default.Application.ApplicationName;
				string caminhoXmlDoc =
					Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

				c.IncludeXmlComments(caminhoXmlDoc);
				
			});
			
			
			IoCGeral.StartInjection(services);
			
			if (IoCGeral.Container == null)
			{
				IoCGeral.Start(); 
			}
			var parameters = IoCGeral.Container.GetInstance<IParameters>();
			parameters.SetParameters();
			//var listener = IoCGeral.Container.GetInstance<IStartListeners>();
			//listener.startListenersBase();


		}


		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="app"></param>
		/// <param name="env"></param>
		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			
			app.UseMvc();
			app.UseResponseCompression();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json",
					"Api.Pessoas");
				

			});
		}

	
		/// <summary>
		/// 
		/// </summary>
		//public class BrotliCompressionProvider : ICompressionProvider
		//{
		//	///// <summary>
		//	///// 
		//	///// </summary>
		//	//public string EncodingName => "br";
		//	///// <summary>
		//	///// 
		//	///// </summary>
		//	//public bool SupportsFlush => true;
		//	///// <summary>
		//	///// 
		//	///// </summary>
		//	///// <param name="outputStream"></param>
		//	///// <returns></returns>
		//	//public Stream CreateStream(Stream outputStream) => new BrotliStream(outputStream, CompressionLevel.Optimal, true);
		//}
	}


}
