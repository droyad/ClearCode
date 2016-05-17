using System.Linq;

namespace ClearCode.Web.Plumbing.Query
{
    public interface IScalarProjection<in T, out TProjection>
    {
        TProjection Execute(IQueryable<T> items);
    }
}