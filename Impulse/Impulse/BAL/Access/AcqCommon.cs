using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Impulse.DAL;

namespace Impulse.BAL.Access
{
    public class AcqCommon
    {

        public string encrypt(string encryptString,string CompCode)
        {
            try
            {
                string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,new byte[] {
                    0x49,0x76,0x61,0x6e,0x20,0x4d,0x65,0x64,0x76,0x65,0x64,0x65,0x76
                    });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(),CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        encryptString = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                //Error_Log(0,"AcqCommonClass","encrypt", ex.Message,0);
            }
            return encryptString;
        }

        public string Decrypt(string cipherText, string CompCode)
        {
            try
            {
                string EncryptionKey = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                cipherText = cipherText.Replace(" ","+");
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
                    0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
                    });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        cipherText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                //BAL.Access.AcqCommon ErrorLogObj = new BAL.Access.AcqCommon;
                //ErrorLogObj.Error_Log(Convert.ToInt64(CompCode),"AcqCommonClass","Decrypt",ex.Message, Convert.ToInt64(CompCode));
            }
            return cipherText;
        }

        public List<SubCategoryKeyWordsModel> GetSubCategoryKeyWordDropDownForSearchFilter()
        {
            List<SubCategoryKeyWordsModel> SubCategoryKeyWordsList = new List<SubCategoryKeyWordsModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                SubCategoryKeyWordsList = DbEngineObj.Database.SqlQuery<SubCategoryKeyWordsModel>("GetSubCategoryKeyWordDropDownForSearchFilter").ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCommon","GetSubCategoryKeyWordDropDownForSearchFilter",ex.Message.ToString(),0);
            }
            return SubCategoryKeyWordsList;
        }

        public List<CategoryModel> GetCategoryDropDownForSearchFilter()
        {
            List<CategoryModel> SubCategoryList = new List<CategoryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SubCategoryList = DbEngineObj.Database.SqlQuery<CategoryModel>("GetCategoryDropDownForSearchFilter").ToList();
                DbEngineObj.Database.CommandTimeout = 32767;
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCommon","UsersModel",ex.Message.ToString(),0);
            }
            return SubCategoryList;
         }

        public List<CountryModel> GetCountryList()
        {
            List<CountryModel> CountryList = new List<CountryModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {

                CountryList = DbEngineObj.Database.SqlQuery<CountryModel>("GetCountryList").ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCommon","UsersModel",ex.Message.ToString(), 0);
            }

            return CountryList;
        }

        public List<StatesModel> GetStateList(long? CountryId)
        {
            List<StatesModel> StateList = new List<StatesModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                            {
                                                new SqlParameter("@CountryId",CountryId)
                                            };

                StateList = DbEngineObj.Database.SqlQuery<StatesModel>("GetStatesList @CountryId",parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCommon","GetStateList", ex.Message.ToString(), 0);
            }
                return StateList;
        }

        public List<CityModel> GetCityList(long? CountryId,long? StateId)
        {
            List<CityModel> CityList = new List<CityModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                            {
                                                new SqlParameter("@CountryId",CountryId),
                                                new SqlParameter("@StateId",StateId),
                                            };

                CityList = DbEngineObj.Database.SqlQuery<CityModel>("GetCityList @CountryId,@StateId",parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0, "AcqCommon","GetCityList",ex.Message.ToString(),0);
            }
            return CityList;
        }

        public List<SizeModel> GetSizeListByCategoryId(long? CategoryId)
        {
            List<SizeModel> SizeList = new List<SizeModel>();
            Impulse.DbEngine.DbEngine DbEngineObj = new Impulse.DbEngine.DbEngine();
            try
            {
                SqlParameter[] parameters =
                                            {
                                                new SqlParameter("@CategoryId",CategoryId),
                                            };

                SizeList = DbEngineObj.Database.SqlQuery<SizeModel>("GetSizeListByCategoryId @CategoryId", parameters).ToList();
            }
            catch (Exception ex)
            {
                Impulse.BAL.Update.UpdCommon UpdCommon = new Impulse.BAL.Update.UpdCommon();
                UpdCommon.Error_Log(0,"AcqCommon","GetSizeListByCategoryId",ex.Message.ToString(),0);
            }
            return SizeList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {

            // Check arguments.  
            if (cipherText == null || cipherText.Length <= 0)
            {
                throw new ArgumentNullException("cipherText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }

            // Declare the string used to hold  
            // the decrypted text.  
            string plaintext = null;

            // Create an RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                //Settings  
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                try
                {
                    // Create the streams used for decryption.  
                    using (var msDecrypt = new MemoryStream(cipherText))
                    {
                        using(var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {

                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream  
                                // and place them in a string.  
                                plaintext = srDecrypt.ReadToEnd();

                            }

                        }
                    }
                }
                catch
                {
                    plaintext = "keyError";
                }
            }

            return plaintext;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <param name="iv"></param>
        /// <returns></returns>
        public byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.  

            if (plainText == null || plainText.Length <= 0)
            {
                throw new ArgumentNullException("plainText");
            }
            if (key == null || key.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            if (iv == null || iv.Length <= 0)
            {
                throw new ArgumentNullException("key");
            }
            byte[] encrypted;
            // Create a RijndaelManaged object  
            // with the specified key and IV.  
            using (var rijAlg = new RijndaelManaged())
            {
                rijAlg.Mode = CipherMode.CBC;
                rijAlg.Padding = PaddingMode.PKCS7;
                rijAlg.FeedbackSize = 128;

                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.  
                var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.  
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.  
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.  
            return encrypted;
        }

        public string DecryptStringAES(string cipherText)
        {
            var keybytes = Encoding.UTF8.GetBytes("8080808080808080");
            var iv = Encoding.UTF8.GetBytes("8080808080808080");
            var encrypted = Convert.FromBase64String(cipherText);
            var decriptedFromJavascript = DecryptStringFromBytes(encrypted,keybytes,iv);
            return string.Format(decriptedFromJavascript);
        }
    }
}