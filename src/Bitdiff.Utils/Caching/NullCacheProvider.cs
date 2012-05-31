using System;

namespace Bitdiff.Utils.Caching
{
    public class NullCacheProvider : ICacheProvider
    {
        public void Store(string key, object value, TimeSpan validFor) { }
        public void Store(string key, object value, params string[] fileDependencies) { }
        public void Remove(string key) { }

        public object Retrieve(string key)
        {
            return null;
        }
        
        public bool Contains(string key)
        {
            return false;
        }
    }
}