using Autofac;
using ClearCode.Data;

namespace ClearCode.Web.Plumbing.Query
{
    public interface IQueryExecuter
    {
        TProjection Execute<T, TProjection>(ScalarProjection<T, TProjection> projection);
    }

    [InstancePerDependency]
    public class QueryExecuter : IQueryExecuter
    {
        private readonly IDataContext _dataContext;

        public QueryExecuter(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public TProjection Execute<T, TProjection>(ScalarProjection<T, TProjection> projection)
        {
            return projection.Execute(_dataContext.Table<T>());
        }
    }
}