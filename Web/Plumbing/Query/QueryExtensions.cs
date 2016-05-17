namespace ClearCode.Web.Plumbing.Query
{
    public static class QueryExtensions
    {
        public static PipeProjection<TIn, TOut> Pipe<TIn, TMiddle, TOut>(this PipeProjection<TIn, TMiddle> a, PipeProjection<TMiddle, TOut> b)
        {
            return new PipeFilter<TIn, TMiddle, TOut>(a, b);
        }

        public static PipeScalarProjection<TIn, TMiddle, TOut> Pipe<TIn, TMiddle, TOut>(this PipeProjection<TIn, TMiddle> a, ScalarProjection<TMiddle, TOut> b)
        {
            return new PipeScalarProjection<TIn, TMiddle, TOut>(a, b);
        }
    }
}