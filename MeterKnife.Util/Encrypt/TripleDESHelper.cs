using System;
using System.Security.Cryptography;
using System.Text;

namespace MeterKnife.Util.Encrypt
{
    public class TripleDESHelper
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="toEncrypt">要加密的字符串，即明文</param>
        /// <param name="key">公共密钥</param>
        /// <param name="useHashing">是否使用MD5生成机密秘钥</param>
        /// <returns>加密后的字符串，即密文</returns>
        public static string Encrypt(string toEncrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = Encoding.UTF8.GetBytes(key);

            var tdes = new TripleDESCryptoServiceProvider
                           {
                               Key = keyArray, 
                               Mode = CipherMode.ECB, 
                               Padding = PaddingMode.PKCS7
                           };

            var cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="toDecrypt">要解密的字符串，即密文</param>
        /// <param name="key">公共密钥</param>
        /// <param name="useHashing">是否使用MD5生成机密密钥</param>
        /// <returns>解密后的字符串，即明文</returns>
        public static string Decrypt(string toDecrypt, string key, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                var hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = Encoding.UTF8.GetBytes(key);

            var tdes = new TripleDESCryptoServiceProvider
                           {
                               Key = keyArray,
                               Mode = CipherMode.ECB,
                               Padding = PaddingMode.PKCS7
                           };

            var cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
