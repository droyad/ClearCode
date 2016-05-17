namespace ClearCode.Web.Plumbing.Query
{
    public static class QueryExtensions
    {
        public static IProjection<TIn, TOut> Pipe<TIn, TMiddle, TOut>(this IProjection<TIn, TMiddle> a, IProjection<TMiddle, TOut> b)
        {
            return new Filter<TIn, TMiddle, TOut>(a, b);
        }

        public static PipeScalarProjection<TIn, TMiddle, TOut> Pipe<TIn, TMiddle, TOut>(this IProjection<TIn, TMiddle> a, IScalarProjection<TMiddle, TOut> b)
        {
            return new PipeScalarProjection<TIn, TMiddle, TOut>(a, b);
        }
    }
}