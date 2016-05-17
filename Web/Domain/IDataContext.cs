using System.Linq;

namespace ClearCode.Data
{
    public interface IDataContext
    {
        IQueryable<T> Table<T>();
    }
}