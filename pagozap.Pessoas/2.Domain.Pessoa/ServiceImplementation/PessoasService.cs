
using Domain.Pessoas.Entities;
using Domain.Pessoas.Interfaces.Repository;
using Domain.Pessoas.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Pessoas.Interfaces.ServiceImplementation
{
    public class PessoaService : Service<Pessoa>, IPessoaService
    {
        IPessoasRepository _repo;
        public PessoaService(IPessoasRepository repo)
        {
            _repo = repo;
        }

        public async  Task<List<Entities.Pessoa>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task<List<Entities.Pessoa>> GetAll(int Page, int PageSize)
        {
            return await _repo.GetAll(Page, PageSize);
        }

        public async Task<Pessoa> GetByCNPJ(string CNPJ)
        {
            return await _repo.GetByCNPJ(CNPJ);
        }

        public async Task<Pessoa> GetByCPF(string CPF)
        {
            return await _repo.GetByCPF(CPF);
        }

        public async Task<Pessoa> GetByID(string ID)
        {
            return await _repo.GetByID(ID);
        }

        public async Task<string> Save(Entities.Pessoa _obj)
        {
            return await _repo.Save(_obj);
        }

        public async Task  Update(Entities.Pessoa _obj)
        {
            await _repo.Update(_obj);

        }

        public void Delete(string ID)
        {
            _repo.Delete(ID);
        }

		public List<Pessoa> GetByListID(List<string> Ids)
		{
			return _repo.GetByListID(Ids);
		}
	}
}
