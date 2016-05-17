using System.Linq;

namespace ClearCode.Web.Plumbing.Query
{
    public class Filter<TIn, TMiddle, TOut> : IProjection<TIn, TOut>
    {
        private readonly IProjection<TIn, TMiddle> _a;
        private readonly IProjection<TMiddle, TOut> _b;

        public Filter(IProjection<TIn, TMiddle> a, IProjection<TMiddle, TOut> b)
        {
            _a = a;
            _b = b;
        }

        public IQueryable<TOut> Execute(IQueryable<TIn> items)
        {
            return _b.Execute(_a.Execute(items));
        }
    }
}