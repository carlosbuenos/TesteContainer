
using Domain.Pessoas.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Pessoas.Interface
{
    public interface IPessoasApp : IApp<Pessoa>
    {
		Task<List<Pessoa>> GetAll();
		Task<List<Pessoa>> GetAll(int Page, int Pagesize);
		List<Pessoa> GetByListID(List<string> Ids);
		Task<Pessoa> GetByID(string ID);
		Task<Pessoa> GetByCPF(string CPF);
		Task<Pessoa> GetByCNPJ(string CNPJ);
		Task<string> Save(Pessoa _obj);
		Task Update(Pessoa _obj);
		void Delete(string ID);
	}
}
