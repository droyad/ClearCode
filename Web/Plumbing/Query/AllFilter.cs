using System.Linq;

namespace ClearCode.Web.Plumbing.Query
{
    public class AllFilter<T> : IFilter<T>
    {
        public IQueryable<T> Execute(IQueryable<T> items)
        {
            return items;
        }
    }
}