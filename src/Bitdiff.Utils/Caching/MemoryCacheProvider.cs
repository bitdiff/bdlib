using System;
using System.Runtime.Caching;

namespace Bitdiff.Utils.Caching
{
    public class MemoryCacheProvider : ICacheProvider
    {
        private readonly IClock _clock;
        private readonly MemoryCache _cache = MemoryCache.Default;

        public MemoryCacheProvider(IClock clock)
        {
            _clock = clock;
        }

        public object Retrieve(string key)
        {
            return _cache.Get(key);
        }

        public void Store(string key, object value)
        {
            _cache.Set(key, value, ObjectCache.InfiniteAbsoluteExpiration);
        }

        public void Store(string key, object value, DateTime expiresAt)
        {
            _cache.Set(key, value, expiresAt);
        }

        public void Store(string key, object value, TimeSpan validFor)
        {
            Store(key, value, _clock.GetNow().Add(validFor));
        }

        public void Store(string key, object value, params string[] fileDependencies)
        {
            var itemPolicy = new CacheItemPolicy();
            itemPolicy.ChangeMonitors.Add(new HostFileChangeMonitor(fileDependencies));
            _cache.Set(key, value, itemPolicy);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public bool Contains(string key)
        {
            return _cache.Get(key) != null;
        }
    }
}