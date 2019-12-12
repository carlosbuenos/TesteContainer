
using Domain.Pessoas.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Pessoas.Interfaces.Repository
{
    public interface IPessoasRepository : IRepository<Entities.Pessoa>
    {
        Task<List<Entities.Pessoa>> GetAll();
        Task<List<Entities.Pessoa>> GetAll(int Page,int Pagesize);
		List<Entities.Pessoa> GetByListID(List<string> Ids);
		Task<Pessoa> GetByID(string ID);
        Task<Pessoa> GetByCPF(string CPF);
        Task<Pessoa> GetByCNPJ(string CNPJ);
        Task<string> Save(Entities.Pessoa _obj);
        Task Update(Entities.Pessoa _obj);
        void Delete(string ID);
      
        
    }
}
