using System.Linq;

namespace ClearCode.Web.Plumbing.Query
{
    public class PipeFilter<TIn, TMiddle, TOut> : PipeProjection<TIn, TOut>
    {
        private readonly PipeProjection<TIn, TMiddle> _a;
        private readonly PipeProjection<TMiddle, TOut> _b;

        public PipeFilter(PipeProjection<TIn, TMiddle> a, PipeProjection<TMiddle, TOut> b)
        {
            _a = a;
            _b = b;
        }

        public override IQueryable<TOut> Execute(IQueryable<TIn> items)
        {
            return _b.Execute(_a.Execute(items));
        }
    }
}