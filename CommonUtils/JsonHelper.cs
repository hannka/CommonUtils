using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Common.Utils
{
    /// <summary>
    /// JsonHelper
    /// </summary>
    public static class JsonHelper
    {
        private static JsonSerializerSettings _jsonSettings;

        static JsonHelper()
        {
            IsoDateTimeConverter datetimeConverter = new IsoDateTimeConverter();
            datetimeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            _jsonSettings = new JsonSerializerSettings();
            _jsonSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
            _jsonSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            _jsonSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            _jsonSettings.Converters.Add(datetimeConverter);
        }

        /// <summary>
        /// 将指定的对象序列化成 JSON 数据。
        /// </summary>
        /// <param name="obj">要序列化的对象。</param>
        /// <returns></returns>
        public static string ToJson<T>(T source)
        {
            try
            {
                if (null == source)
                    return null;

                if (source is NameValueCollection)
                {
                    NameValueCollection nvc = source as NameValueCollection;
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    foreach (string key in nvc.Keys)
                    {
                        dict.Add(key, nvc[key]);
                    }
                    return JsonConvert.SerializeObject(dict, Formatting.None, _jsonSettings);
                }

                return JsonConvert.SerializeObject(source, Formatting.None, _jsonSettings);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 将指定的 JSON 数据反序列化成指定对象。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="json">JSON 数据。</param>
        /// <returns></returns>
        public static T FromJson<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, _jsonSettings);
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
