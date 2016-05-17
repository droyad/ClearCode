using System.Linq;

namespace ClearCode.Web.Plumbing.Query
{
    public abstract class PipeProjection<T, TProjection> : ScalarProjection<T, IQueryable<TProjection>> 
    {

    }
}