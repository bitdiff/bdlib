using System;
using System.Linq.Expressions;

namespace Bitdiff.Utils.Caching
{
    public interface ICacheHelper
    {
        T Fetch<T>(Expression<Func<T>> source, string cacheKey, TimeSpan validFor);
    }
}