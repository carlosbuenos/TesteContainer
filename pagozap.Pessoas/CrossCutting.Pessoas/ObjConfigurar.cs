using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.Pessoas
{
	public class ObjConfigurar
	{
		public string hostBase { get; set; }
		public string hostRabbit { get; set; }
		public string hostUser { get; set; }
		public string hostPassword { get; set; }
		public string hostMongo { get; set; }
		public string hostMySQL { get; set; }
		public string routeLog { get; set; }
		public string FilePathCertificateSSl { get; set; }
	}
}
