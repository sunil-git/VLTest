using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chilkat;
using Serenata_Checkout.Log;
using System.Configuration;
using System.Web;
using System.Net;
namespace Serenata_Checkout.ChilkatComponent
{
    public class Encryption
    {
         #region Private Variables
        private Chilkat.Crypt2 crypt;
        #endregion

        #region Constructor Defination
        public Encryption()
        {
            crypt = new Chilkat.Crypt2();
        }
        #endregion

        /// <summary>
        /// It used to check the license of the chilkat component.
        /// </summary>
        /// <param name="str_UnLockCode"></param>
        /// <returns>bool</returns>
        public bool CheckLicense(string str_UnLockCode)
        {
            bool bool_Success = false;
            try
            {
                bool_Success = crypt.UnlockComponent(str_UnLockCode);
                if (!bool_Success)
                {
                    ErrorLog.Error(new Exception(crypt.LastErrorText));
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return bool_Success;
        }

        /// <summary>
        /// Get AES encrypted message.
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns>string</returns>
        public string GetAesEncryptionString(string strVal)
        {
            string encStr = string.Empty;
            try
            {
                if (CheckLicense(ConfigurationManager.AppSettings["CryptUnlockCode"]) == true)
                {
                    crypt.CryptAlgorithm = "aes";
                    crypt.CipherMode = "cbc";
                    crypt.Charset = "utf-8";
                    crypt.KeyLength = 256;
                    string hexKey = crypt.GenEncodedSecretKey(ConfigurationManager.AppSettings["EncryptionPassword"], "hex");
                    crypt.SetEncodedKey(hexKey, "hex");
                    //crypt.EncodingMode = "base64";
                    crypt.EncodingMode = "hex";
                    encStr = crypt.EncryptStringENC(strVal);
                }

            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return encStr;
        }

        /// <summary>
        /// Get AES encrypted message.
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns>string</returns>
        public string GetAesEncryptionString(string strVal, string strPwd)
        {
            string encStr = string.Empty;
            try
            {
                if (CheckLicense(ConfigurationManager.AppSettings["CryptUnlockCode"]) == true)
                {
                    crypt.CryptAlgorithm = "aes";
                    crypt.CipherMode = "cbc";
                    crypt.Charset = "utf-8";
                    crypt.KeyLength = 256;
                    string hexKey = crypt.GenEncodedSecretKey(strPwd, "hex");
                    crypt.SetEncodedKey(hexKey, "hex");
                    crypt.EncodingMode = "base64";
                    encStr = crypt.EncryptStringENC(strVal);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return encStr;
        }




        /// <summary>
        /// Get AES decrypted message.
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns>string</returns>
        public string GetAesDecryptionString(string strVal)
        {
            string deccStr = string.Empty;
            try
            {
                if (CheckLicense(ConfigurationManager.AppSettings["CryptUnlockCode"]) == true)
                {
                    crypt.CryptAlgorithm = "aes";
                    crypt.CipherMode = "cbc";
                    crypt.Charset = "utf-8";
                    crypt.KeyLength = 256;
                    string hexKey = crypt.GenEncodedSecretKey(ConfigurationManager.AppSettings["EncryptionPassword"], "hex");
                    crypt.SetEncodedKey(hexKey, "hex");
                    //crypt.EncodingMode = "base64";
                    crypt.EncodingMode = "hex";
                    deccStr = crypt.DecryptStringENC(strVal);
                }
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return deccStr;
        }

        /// <summary>
        /// Get Base64 encoded string.
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        public string GetBase64EncodeMsg(string strVal)
        {
            string strBase64 = string.Empty;
            try
            {
                crypt.CryptAlgorithm = "none";
                crypt.EncodingMode = "base64";

                strBase64 = crypt.EncryptStringENC(strVal);
            }
            catch (Exception ex)
            {
                ErrorLog.Error(ex);
            }
            return strBase64;
        }
    }
}
