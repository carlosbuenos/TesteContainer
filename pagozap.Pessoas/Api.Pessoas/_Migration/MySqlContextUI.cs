using Domain.Pessoas.Entities;
using Infra.Pessoas.DataAccess.FactoryTypeConn;
using Microsoft.EntityFrameworkCore;

namespace Api.Pessoas._Migration
{
    /// <summary>
    /// 
    /// </summary>
	public class MySqlContextUI : DbContext
	{
        /// <summary>
        /// 
        /// </summary>
		public MySqlContextUI()
		{
			//base.Database.Migrate();
			//base.Database.EnsureCreated();
		}
		private void Conectar()
		{
			Database.OpenConnection();
		}
        /// <summary>
        /// 
        /// </summary>
		public DbSet<Pessoa> Pessoas { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{


			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseMySql(new ConectaMySql().StrConnect());

			}
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder
	.Entity<PessoaTelefone>().HasKey(k => new { k.PessoaID, k.DDD, k.Numero });

		}
	}
}
