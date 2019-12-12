using App.Pessoas.Interface;
using Domain.Pessoas.Entities;
using Domain.Pessoas.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Pessoas.AppImplementation
{
	public class PessoasApp : App<Pessoa>, IPessoasApp
    {
        IPessoaService _serv;
        public PessoasApp(IPessoaService serv)
        {
            _serv = serv;
        }

        public async Task<List<Pessoa>> GetAll()
        {
            return await _serv.GetAll();
        }

        public async Task<List<Pessoa>> GetAll(int Page, int PageSize)
        {
            return await _serv.GetAll(Page, PageSize);
        }

        public async Task<Pessoa> GetByCNPJ(string CNPJ)
        {
            return await _serv.GetByCNPJ(CNPJ);
        }

        public async Task<Pessoa> GetByCPF(string CPF)
        {
            return await _serv.GetByCPF(CPF);
        }

        public async Task<Pessoa> GetByID(string ID)
        {
            return await _serv.GetByID(ID);
        }

        public async Task<string> Save(Domain.Pessoas.Entities.Pessoa _obj)
        {
          
            return await _serv.Save(_obj);
        }

        public async Task Update(Domain.Pessoas.Entities.Pessoa _obj)
        {
            await _serv.Update(_obj);
        }

        public void Delete(string ID)
        {
            _serv.Delete(ID);
        }

		public List<Pessoa> GetByListID(List<string> Ids)
		{
			return _serv.GetByListID(Ids);
		}
	}
}
