using System.Linq;

namespace ClearCode.Web.Domain
{
    public interface IDataContext
    {
        IQueryable<T> Table<T>();
    }
}