using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Pessoas.ConfiguracoesSSL
{
	public class EndpointConfiguration
	{
		public string Host { get; set; }
		public int? Port { get; set; }
		public string Scheme { get; set; }
		public string StoreName { get; set; }
		public string StoreLocation { get; set; }
		public string FilePath { get; set; }
		public string Password { get; set; }
	}
}
