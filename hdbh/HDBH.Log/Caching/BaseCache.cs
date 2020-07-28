using HDBH.Log.Enums;
using System;


namespace HDBG.Log.Caching
{
    public abstract class BaseCache
    {
        public abstract T Get<T>(string strKey);
        public abstract void Set<T>(CacheModeEnum cacheMode, string strKey, T obj, TimeSpan expired);
        public abstract void RemoveCached(string key);
        public abstract void RemoveAllCached();
        public abstract bool CacheNull(string key);
    }
}
