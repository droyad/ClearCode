using System.Linq;

namespace ClearCode.Web.Plumbing.Query
{
    public abstract class ScalarProjection<T, TProjection>
    {
        public abstract TProjection Execute(IQueryable<T> items);
    }
}