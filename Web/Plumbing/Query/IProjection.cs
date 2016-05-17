using System.Collections.Generic;
using System.Linq;

namespace ClearCode.Web.Plumbing.Query
{
    public interface IProjection<in T, out TProjection>
    {
        IQueryable<TProjection> Execute(IQueryable<T> items);
    }
}