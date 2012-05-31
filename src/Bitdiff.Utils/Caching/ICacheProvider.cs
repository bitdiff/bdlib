using System;

namespace Bitdiff.Utils.Caching
{
    public interface ICacheProvider
    {
        object Retrieve(string key);
        void Store(string key, object value, TimeSpan validFor);
        void Store(string key, object value, params string[] fileDependencies);
        void Remove(string key);
        bool Contains(string key);
    }
}