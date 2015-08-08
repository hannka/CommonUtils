using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Common.Utils
{
    /// <summary>
    /// String 扩展方法
    /// </summary>
    public static class StringExtension
    {
        private const char NO_BREAK_SPACE = '\u00A0';

        private const char SPACE = ' ';

        private const string JAPANESE = @"[\u3040-\u309F\u30A0-\u30FF]"; // 平假名 + 片假名

        private const string CJK = @"[\u4e00-\u9fa5]"; // 中文

        /// <summary>
        /// 判断输入是英文还是中文
        /// </summary>
        /// <param name="input">传入字符串</param>
        /// <returns>返回（０：传人参数为空，１：中文，２：英文）</returns>
        public static int IsEnglishOrChinese(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return 0;
            }
            int QueryType = 0;
            byte BytesInput;
            //得到ASC码，用来判断中文或拼音．
            BytesInput = ((byte[])System.Text.Encoding.ASCII.GetBytes(input.Substring(0, 1)))[0];
            if ((BytesInput >= 65 && BytesInput <= 90) || (BytesInput >= 97 && BytesInput <= 122))
            {
                QueryType = 2;
            }
            else
            {
                QueryType = 1;
            }
            return QueryType;
        }

        /// <summary>
        /// 简体转繁体
        /// </summary>
        /// <param name="input">传入字符串</param>
        /// <returns>input参数翻译成繁体</returns>
        public static string CHS2CHT(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                return ChineseConverter.Convert(HttpUtility.HtmlDecode(input), ChineseConversionDirection.SimplifiedToTraditional);
            }
            return string.Empty;
        }

        /// <summary>
        /// 转换空格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToNormalSpace(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input) && input.Contains(NO_BREAK_SPACE))
            {
                return input.Replace(NO_BREAK_SPACE, SPACE);
            }

            return input;
        }

        /// <summary>
        /// 是否包含中文
        /// </summary>
        /// <param name="strWord">验证字符</param>
        /// <returns></returns>
        public static bool IsChinese(this string input)
        {
            if (!string.IsNullOrWhiteSpace(input))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(input, JAPANESE))
                {
                    return false;
                }

                return System.Text.RegularExpressions.Regex.IsMatch(input, CJK);
            }

            return false;
        }

        /// <summary>
        /// 转换大写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Upper(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return input.ToUpper();
            }
            return input;
        }

        /// <summary>
        /// 转换小写
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Lower(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return input.ToLower();
            }
            return input;
        }
    }
}
