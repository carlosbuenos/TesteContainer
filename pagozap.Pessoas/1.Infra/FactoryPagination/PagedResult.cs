using System.Collections.Generic;

namespace Infra.Pessoas.FactoryPagination
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }

       
    }
}
