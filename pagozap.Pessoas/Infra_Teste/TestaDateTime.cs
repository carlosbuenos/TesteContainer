using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra_Teste
{
	[TestClass]
    public class TestaDateTime
    {
		[TestMethod]
		public void UltimoDiaDoAno() {
			var ultimo = new DateTime().ToString("31/12/2019 23:59:59");
			var dataUltimo = DateTime.Now.AddYears(1);
			var	dataUltimo2 = dataUltimo.AddDays(((DateTime.Now.Day)*(-1))).ToString("dd/MM/yyyy 23:59:59");
			Assert.AreEqual(ultimo, dataUltimo);
		}
		[TestMethod]
		public void PrimeiroDiaAno() {
			var primeiro = new DateTime().ToString("01/01/2019 00:00:00");
			var DtIni = DateTime.Now.AddDays(((DateTime.Now.Day -1) * (-1))).ToString("dd/MM/yyyy 00:00:00");
			Assert.AreEqual(primeiro, DtIni);
		}

		[TestMethod]
		public void GetGuid()
		{
			var chave = Guid.NewGuid().ToString();
			
			Assert.AreNotEqual(chave,string.Empty);
		}
	}
}
