using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utils
{
    public interface IQueryData
    {
        // Methods
        object QueryData();
    }

    public class LocalCacheManager : IDisposable, ICacheItemRefreshAction
    {
        private static readonly object AsyncLock;
        private Dictionary<string, long> cachedItemsInfo;
        private ConcurrentDictionary<string, CacheListData> cacheKeySettings;
        private static readonly List<CacheListData> cacheListData;
        private ICacheManager cacheManager;
        private static readonly string defaultCacheName;
        private bool disposed;
        private static Dictionary<string, LocalCacheManager> instances;
        private static volatile object instancesLock;
        private string name;
        private static readonly string namePrefix;
        private ConcurrentDictionary<string, ICacheItemRefreshAction> refreshActions;
        private int serialNumber;
        private static int serialNumberCount;
        private static readonly object serialNumberLock;

        // Methods
        static LocalCacheManager();
        protected LocalCacheManager(ICacheManager cacheManager);
        protected LocalCacheManager(string name, ICacheManager cacheManager);
        public void Add(string key, object value);
        public void Add(string key, object value, CacheItemPriority scavengingPriority, ICacheItemRefreshAction refreshAction, params ICacheItemExpiration[] expirations);
        protected virtual void Clear(bool disposing);
        public bool Contains(string key);
        public void Dispose();
        protected override void Finalize();
        private CacheListData FindMatchedCacheListData(string key);
        public void Flush();
        public object GetData(string key);
        public object GetData(string key, IQueryData query);
        public T GetData<T>(string key, IQueryData query);
        public T GetData<T>(string key, Func<T> getDataFunc = null);
        public T GetData<T>(string key, T ifNullReturn);
        public TResult GetData<TResult, VParam>(string key, Func<VParam, TResult> getDataFunc = null, VParam param = null);
        public static LocalCacheManager GetDefault();
        public static LocalCacheManager GetInstance(string instanceName);
        public void Refresh(string removedKey, object expiredValue, CacheItemRemovedReason removalReason);
        public void Remove(string key);
        protected void UpdateTotalSize(string key, object o, int factor);

        // Properties
        public CachedItemInfo CachedKeyItemInfo { get; }
        public Dictionary<string, CacheListData> CacheKeySettings { get; }
        public int Count { get; }
        public object this[string key] { get; }
        public string Name { get; }
        public int SerialNumber { get; }
        public long Size { get; }

        // Nested Types
        public class CachedItemInfo : IEnumerable<KeyValuePair<string, long>>, IEnumerable
        {
            // Fields
            private LocalCacheManager inMemCacheManager;

            // Methods
            public CachedItemInfo(LocalCacheManager inMemCacheManager);
            public IEnumerator<KeyValuePair<string, long>> GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator();
        }
    }
}
