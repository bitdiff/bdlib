using System;
using System.Linq.Expressions;

namespace Bitdiff.Utils.Caching
{
    public class CacheHelper : ICacheHelper
    {
        private readonly ICacheProvider _cacheProvider;

        public CacheHelper(ICacheProvider cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public T Fetch<T>(Expression<Func<T>> source, string cacheKey, TimeSpan validFor)
        {
            object retrieve = _cacheProvider.Retrieve(cacheKey);
            if (retrieve == null)
            {
                retrieve = source.Compile().Invoke();
                if (retrieve != null)
                {
                    _cacheProvider.Store(cacheKey, retrieve, validFor);
                }
            }

            return (T)retrieve;
        }
    }
}