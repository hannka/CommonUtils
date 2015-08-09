using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common.Utils
{
    /// <summary>
    /// Converter
    /// </summary>
    public static class ConvertHelper
    {
        /// <summary>
        /// convert to int64
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static long ToInt64<T>(T source)
        {
            long result;
            if ((source != null) && long.TryParse(string.Format("{0:f0}", source), out result))
            {
                return result;
            }
            return 0;
        }

        /// <summary>
        /// convert to nullabelint64
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static long? ToNullabelInt64<T>(T source)
        {
            long result;
            if ((source != null) && long.TryParse(source.ToString(), out result))
            {
                return new long?(result);
            }
            return null;
        }

        // Methods
        public static bool ToBool<T>(T source)
        {
            bool flag;
            if (source == null)
            {
                return false;
            }
            string str = source.ToString().Trim().ToUpper();
            if (str.Equals("T"))
            {
                return true;
            }
            if (str.Equals("F"))
            {
                return false;
            }
            if (!bool.TryParse(str, out flag))
            {
                flag = false;
            }
            return flag;
        }
        public static byte ToByte<T>(T source)
        {
            byte num;
            if ((source != null) && byte.TryParse(source.ToString(), out num))
            {
                return num;
            }
            return 0;
        }

        public static DateTime ToDateTime<T>(T source)
        {
            DateTime time;
            if ((source != null) && DateTime.TryParse(source.ToString(), out time))
            {
                return time;
            }
            return new DateTime();
        }

        public static decimal ToDecimal<T>(T source)
        {
            decimal? nullable = ToNullableDecimal<T>(source);
            if (nullable.HasValue)
            {
                return nullable.Value;
            }
            return 0M;
        }

        public static string ToENCultureInfoDateTime(DateTime date)
        {
            DateTimeFormatInfo dateTimeFormat = CultureInfo.CreateSpecificCulture("en").DateTimeFormat;
            return date.ToString("MMM-dd-yyyy", dateTimeFormat);
        }

        public static TResult ToEntity<TSource, TResult>(TSource source) where TResult : new()
        {
            if (source == null)
            {
                return default(TResult);
            }
            TResult local = default(TResult);
            Type type = typeof(TSource);
            PropertyInfo[] properties = typeof(TResult).GetProperties(BindingFlags.SetProperty | BindingFlags.Public | BindingFlags.Instance);
            if ((properties != null) && (properties.Length > 0))
            {
                local = (default(TResult) == null) ? Activator.CreateInstance<TResult>() : default(TResult);
                foreach (PropertyInfo info in properties)
                {
                    string name = info.Name;
                    PropertyInfo property = type.GetProperty(name);
                    if (property != null)
                    {
                        info.SetValue(local, property.GetValue(source, null), null);
                    }
                }
            }
            return local;
        }

        public static V ToEnum<T, V>(T source) where V : struct
        {
            V result = default(V);
            if (source != null)
            {
                Enum.TryParse<V>(source.ToString(), true, out result);
            }
            return result;
        }

        public static float ToFloat<T>(T source)
        {
            float num;
            if ((source != null) && float.TryParse(source.ToString(), out num))
            {
                return num;
            }
            return 0f;
        }

        public static string ToFormatedDateTimeString(DateTime dt, string cultureInforName = "zh-cn", string format = "yyyy-MM-dd")
        {
            if (string.IsNullOrEmpty(cultureInforName))
            {
                cultureInforName = "zh-cn";
            }
            if (string.IsNullOrEmpty(format))
            {
                format = "yyyy-MM-dd";
            }
            CultureInfo provider = new CultureInfo(cultureInforName);
            return dt.ToString(format, provider);
        }

        public static int ToInt32<T>(T source)
        {
            int num;
            if ((source != null) && int.TryParse(string.Format("{0:f0}", source), out num))
            {
                return num;
            }
            return 0;
        }

        public static List<TResult> ToList<TSource, TResult>(List<TSource> list) where TResult : new()
        {
            if (list == null)
            {
                return null;
            }
            if (list.Count == 0)
            {
                return null;
            }
            return list.ConvertAll<TResult>(new Converter<TSource, TResult>( ConvertHelper.ToEntity<TSource, TResult>));
        }

        public static int? ToNullabelInt32<T>(T source)
        {
            int num;
            if ((source != null) && int.TryParse(source.ToString(), out num))
            {
                return new int?(num);
            }
            return null;
        }

        public static DateTime? ToNullableDateTime<T>(T source)
        {
            DateTime time;
            if ((source != null) && DateTime.TryParse(source.ToString(), out time))
            {
                return new DateTime?(time);
            }
            return null;
        }

        public static decimal? ToNullableDecimal<T>(T source)
        {
            decimal num;
            if ((source != null) && decimal.TryParse(source.ToString(), out num))
            {
                return new decimal?(num);
            }
            return null;
        }

        public static int ToRoundInt32<T>(T source)
        {
            int num;
            if (source == null)
            {
                return 0;
            }
            if (int.TryParse(source.ToString(), out num))
            {
                return num;
            }
            return ToInt32<decimal>(Math.Floor((decimal)(ToDecimal<T>(source) + 0.5M)));
        }

        public static string ToString<T>(T source)
        {
            if (source != null)
            {
                return source.ToString();
            }
            return string.Empty;
        }

        public static string ToTOrF(bool source)
        {
            string str = "F";
            if (source)
            {
                str = "T";
            }
            return str;
        }

        public static string ToTrimString<T>(T source)
        {
            if (source == null)
            {
                return string.Empty;
            }
            string str = source.ToString();
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Trim();
            }
            return str;
        }

        public static int ToWeekInt32(DayOfWeek week)
        {
            int num = (int)week;
            if (num == 0)
            {
                num = 7;
            }
            return num;
        }

        public static int ToWeekInt32<T>(T source)
        {
            DateTime? nullable = ToNullableDateTime<T>(source);
            int dayOfWeek = 0;
            if (nullable.HasValue)
            {
                dayOfWeek = (int)nullable.Value.DayOfWeek;
                if (dayOfWeek == 0)
                {
                    dayOfWeek = 7;
                }
            }
            return dayOfWeek;
        }

        public static string ToZHCultureInfoDateTime(DateTime date)
        {
            DateTimeFormatInfo dateTimeFormat = CultureInfo.CreateSpecificCulture("zh-cn").DateTimeFormat;
            return date.ToString("yyyy-MM-dd", dateTimeFormat);
        }
    }
}
