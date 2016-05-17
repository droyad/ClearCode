using System.Linq;

namespace ClearCode.Web.Plumbing.Query
{
    public class AllFilter<T> : Filter<T>
    {
        public override IQueryable<T> Execute(IQueryable<T> items)
        {
            return items;
        }
    }
}