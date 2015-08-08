using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Common.Utils
{
    /// <summary>
    /// 密码类
    /// </summary>
  public static class Cryptography
    {
        /// <summary>
        /// sha1 加密字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string SHA1Encrypt(string sourceString)
        {
            if (string.IsNullOrEmpty(sourceString))
                return string.Empty;

            byte[] bytRes = Encoding.UTF8.GetBytes(sourceString);
            HashAlgorithm iSHA = new SHA1CryptoServiceProvider();
            byte[] bytHash = iSHA.ComputeHash(bytRes);
            iSHA.Clear();
            StringBuilder sbEnText = new StringBuilder();
            foreach (byte byt in bytHash)
            {
                sbEnText.AppendFormat("{0:x2}", byt);
            }
            return sbEnText.ToString();
        }

        /// <summary>
        /// MD5 加密字符串
        /// </summary>
        /// <param name="sourceString"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string sourceString)
        {
            if (string.IsNullOrEmpty(sourceString))
                return string.Empty;

            byte[] bytRes = Encoding.UTF8.GetBytes(sourceString);
            HashAlgorithm md5 = new MD5CryptoServiceProvider();
            byte[] bytHash = md5.ComputeHash(bytRes);
            md5.Clear();
            StringBuilder sbEnText = new StringBuilder();
            foreach (byte byt in bytHash)
            {
                sbEnText.AppendFormat("{0:x2}", byt);
            }
            return sbEnText.ToString();
        }
    }
}
