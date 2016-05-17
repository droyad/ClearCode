using System.Linq;

namespace ClearCode.Web.Plumbing.Query
{
    public class PipeScalarProjection<TIn, TMiddle, TOut> : ScalarProjection<TIn, TOut>
    {
        private readonly PipeProjection<TIn, TMiddle> _a;
        private readonly ScalarProjection<TMiddle, TOut> _b;

        public PipeScalarProjection(PipeProjection<TIn, TMiddle> a, ScalarProjection<TMiddle, TOut> b)
        {
            _a = a;
            _b = b;
        }

        public override TOut Execute(IQueryable<TIn> items)
        {
            return _b.Execute(_a.Execute(items));
        }
    }
}