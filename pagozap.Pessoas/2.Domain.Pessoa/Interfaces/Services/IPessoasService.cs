using Domain.Pessoas.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Pessoas.Interfaces.Services
{
    public interface IPessoaService : IService<Pessoa>
    {
        Task<List<Pessoa>> GetAll();
		Task<List<Pessoa>> GetAll(int Page, int Pagesize);
		List<Entities.Pessoa> GetByListID(List<string> Ids);
		Task<Pessoa> GetByID(string ID);
		Task<Pessoa> GetByCPF(string CPF);
		Task<Pessoa> GetByCNPJ(string CNPJ);
		Task<string> Save(Entities.Pessoa _obj);
		Task Update(Pessoa _obj);
        void Delete(string ID);
    }
}
