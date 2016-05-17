using System.Collections.Generic;
using System.Linq;
using ClearCode.Data.Entities;

namespace ClearCode.Data
{
    public interface IDataContext
    {
        IQueryable<T> Table<T>();
    }
}