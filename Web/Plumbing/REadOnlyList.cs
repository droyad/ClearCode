using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ClearCode.Web.Plumbing
{
    public class ReadOnlyList<T> : IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T> _items;

        public ReadOnlyList(IEnumerable<T> items)
        {
            _items = items.ToArray();
        }
        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _items.Count;

        public T this[int index] => _items[index];
    }
}