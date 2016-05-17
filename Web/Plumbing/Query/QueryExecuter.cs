using System.Collections.Generic;
using System.Linq;
using Autofac;
using ClearCode.Web.Domain;

namespace ClearCode.Web.Plumbing.Query
{
    public interface IQueryExecuter
    {
        TProjection Execute<T, TProjection>(IScalarProjection<T, TProjection> projection);
        IReadOnlyList<TProjection> Execute<T, TProjection>(IProjection<T, TProjection> projection);
    }

    [InstancePerDependency]
    public class QueryExecuter : IQueryExecuter
    {
        private readonly IDataContext _dataContext;

        public QueryExecuter(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public TProjection Execute<T, TProjection>(IScalarProjection<T, TProjection> projection)
        {
            return projection.Execute(_dataContext.Table<T>());
        }

        public IReadOnlyList<TProjection> Execute<T, TProjection>(IProjection<T, TProjection> projection)
        {
            return projection.Execute(_dataContext.Table<T>()).ToArray();
        }
    }
}