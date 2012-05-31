using System.Collections;
using System.Collections.Generic;

namespace Bitdiff.Utils
{
    public class PaginatedList<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> _list;

        public PaginatedList(IEnumerable<T> list, PaginationHelper paginator)
        {
            _list = list;
            Paginator = paginator;
        }

        public PaginationHelper Paginator { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}