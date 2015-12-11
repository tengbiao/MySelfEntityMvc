using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace MySelfEntityMvc.UtilityTools.Caching
{
    /// <summary>
    /// MemoryCache
    /// </summary>
    public class MemoryCache
    {
        /// <summary>
        /// Cache
        /// </summary>
        public static ObjectCache Cache = System.Runtime.Caching.MemoryCache.Default;

        /// <summary>
        /// 从缓存中获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Object Get(String key)
        {
            return Cache.Get(key);
        }

        /// <summary>
        /// 将对象放入缓存，如果缓存中已有此项，则替换。a)永不过期，b)优先级为 Normal，c)没有缓存依赖项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        public static void Set(String key, Object val)
        {
            Cache.Set(key, val, null, null);
        }

        /// <summary>
        /// 将对象放入缓存，在参数 seconds 指定的秒数之后过期
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="seconds"></param>
        public static void Set(String key, Object val, int seconds)
        {
            //HttpRuntime.Cache.Insert(key, val, null, DateTime.UtcNow.AddSeconds((double) seconds),
            //    Cache.NoSlidingExpiration);
            Cache.Set(key, val, DateTime.UtcNow.AddSeconds((double) seconds));
        }

        /// <summary>
        /// 从缓存中移除某项
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(String key)
        {
            if (strUtil.HasText(key))
            {
                Cache.Remove(key);
                //HttpRuntime.Cache.Remove(key);
            }
        }
        
        /// <summary>
        /// 移除所有的缓存项
        /// </summary>
        public static void Remove()
        {
            Cache.ToList().ForEach(c => Remove(c.Key));
        }

    }
}
