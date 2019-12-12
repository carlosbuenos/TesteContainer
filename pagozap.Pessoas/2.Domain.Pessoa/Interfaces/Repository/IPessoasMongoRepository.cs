using Domain.Pessoas.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Pessoas.Interfaces.Repository
{
	public interface IPessoasMongoRepository : IRepository<Entities.Pessoa>
	{
		Task<List<Entities.Pessoa>> GetAll();
		Task<List<Entities.Pessoa>> GetAll(int Page, int PageSize);
		List<Entities.Pessoa> GetByListID(List<string> Ids);
		Task<Pessoa> GetByID(string ID);
		Task<Pessoa> GetByCPF(string CNPJ);
		Task<Pessoa> GetByCNPJ(string CPF);
		void Save(Entities.Pessoa _obj);
		void Update(Entities.Pessoa _obj);
		void Delete(string ID);
	}
}
