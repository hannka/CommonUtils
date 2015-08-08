using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Utils
{
    /// <summary>
    /// Converter
    /// </summary>
    public static class Converter
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


    }
}
