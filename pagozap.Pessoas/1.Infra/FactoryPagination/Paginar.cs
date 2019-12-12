using Domain.Pessoas.Entities;
using System;
using System.Linq;

namespace Infra.Pessoas.FactoryPagination
{
    public static class Paginar
    {
        public static PagedResult<Pessoa> GetPaged(this IQueryable<Domain.Pessoas.Entities.Pessoa> query,int page, int PageSize)
        {

            var result = new PagedResult<Pessoa>
            {
                Currentpage = page,
                PageSize = PageSize,
                RowCount = query.Count()
            };
            var pageCount = (double)result.RowCount / PageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (page - 1) * PageSize;
            result.Results = query.Skip(skip).Take(PageSize).ToList();
            return result;
        }
    }
}
