using System.Linq;

namespace ClearCode.Web.Plumbing.Query
{
    public class PipeScalarProjection<TIn, TMiddle, TOut> : IScalarProjection<TIn, TOut>
    {
        private readonly IProjection<TIn, TMiddle> _a;
        private readonly IScalarProjection<TMiddle, TOut> _b;

        public PipeScalarProjection(IProjection<TIn, TMiddle> a, IScalarProjection<TMiddle, TOut> b)
        {
            _a = a;
            _b = b;
        }

        public TOut Execute(IQueryable<TIn> items)
        {
            return _b.Execute(_a.Execute(items));
        }
    }
}