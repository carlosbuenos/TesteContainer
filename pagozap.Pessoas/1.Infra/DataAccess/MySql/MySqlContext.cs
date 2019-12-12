using Domain.Pessoas.Entities;
using Infra.Pessoas.DataAccess.FactoryTypeConn;
using Microsoft.EntityFrameworkCore;

namespace Infra.Pessoas.DataAccess.MySql
{

	public class MySqlContext : DbContext
	{
		private string _strConnection { get; set; }
		public MySqlContext(string strConn)
		{
			_strConnection = strConn;
			Conectar();

		}
		public MySqlContext()
		{
			_strConnection = new ConectaMySql().StrConnect();
			Conectar();
		}
		private void Conectar()
		{
			Database.OpenConnection();
		}
		public DbSet<Pessoa> Pessoas { get; set; }
		public DbSet<PessoaFisica> PessoaFisica { get; set; }
		public DbSet<PessoaJuridica> PessoaJuridica { get; set; }
		public DbSet<PessoaTelefone> PessoaTelefone { get; set; }
		public DbSet<PessoaEndereco> PessoaEndereco { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseMySql(_strConnection);
			}
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
	.Entity<PessoaTelefone>().HasKey(k => new { k.PessoaID, k.DDD, k.Numero });
		}

		public override void Dispose()
		{
			Database.CloseConnection();
		}

		~MySqlContext()
		{
			Dispose();
		}
	}
}
