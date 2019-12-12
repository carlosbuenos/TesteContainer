using System;

namespace Infra.Pessoas.FactoryPagination
{
    public abstract class PagedResultBase
    {
        public int Currentpage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage { get { return (Currentpage - 1) * PageSize + 1; } }
        public int LastRowOnPage { get { return Math.Min(Currentpage * PageSize, RowCount); } }
    }
}
