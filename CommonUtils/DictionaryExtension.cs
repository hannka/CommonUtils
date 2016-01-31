using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utils
{
    /// <summary>
    /// Dictionary扩展方法，不支持多线程
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// 将键和值添加到字典中：如果不存在，则添加；存在，则不添加，非线程安全
        /// </summary>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value, bool isReplace = false)
        {
            TValue result;
            if (false.Equals(dict.TryGetValue(key, out result)))
            {
                dict.Add(key, value);
            }
            else if (isReplace)
            {
                dict[key] = value;
            }
            return dict;
        }

        /// <summary>
        /// 获取与指定的键相关联的值，如果没有则返回输入的默认值，非线程安全
        /// </summary>
        public static TValue TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            TValue result;
            if (false.Equals(dict.TryGetValue(key, out result)))
            {              
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 向字典中批量添加键值对，非线程安全
        /// </summary>
        /// <param name="isReplace">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> TryAddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool isReplace = false)
        {
            if (values == null)
            {            
                throw new Exception("values is empty");
            }
            foreach (var item in values)
            {
                dict.TryAdd<TKey, TValue>(item.Key, item.Value, isReplace);
            }
            return dict;
        } 
    }
}
