using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace EZUtils
{
    public class Cryptography
    {
        private string mKey;

        public Cryptography()
        {
            mKey = "`~0s#v6^";
        }

        public Cryptography(String Key)
        {
            mKey = Key;
        }
       
        public string Decrypt(string CipherText)
        {
            try
            {
                TripleDESCryptoServiceProvider DES =
                    new TripleDESCryptoServiceProvider();

                MD5CryptoServiceProvider hashMD5 =
                    new MD5CryptoServiceProvider();

                DES.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(mKey));
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESDecrypt = DES.CreateDecryptor();

                byte[] Buffer = Convert.FromBase64String(CipherText);
                return ASCIIEncoding.ASCII.GetString(DESDecrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch
            {
                return "";
            }
        }

        public string Encrypt(string PlainText)
        {
            try
            {
                TripleDESCryptoServiceProvider DES = new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();

                DES.Key = hashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(mKey));
                DES.Mode = CipherMode.ECB;

                ICryptoTransform DESEncrypt = DES.CreateEncryptor();
                byte[] Buffer = ASCIIEncoding.ASCII.GetBytes(PlainText);

                return Convert.ToBase64String(DESEncrypt.TransformFinalBlock(Buffer, 0, Buffer.Length));
            }
            catch
            {
                return "";
            }
        }

    }
}
