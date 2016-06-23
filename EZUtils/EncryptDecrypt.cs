using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace EZUtils
{
    public class EncryptDecrypt
    {
        public EncryptDecrypt()
        {
        }

        /// <summary>
        /// Encrypt a plain text string and return the
        /// encrypted string.
        /// </summary>
        /// <param name="Message">Plain text string to encrypt</param>
        /// <param name="Passphrase">password</param>
        /// <returns>Encrypted string</returns>
        public /*static*/ string EncryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            ICryptoTransform Encryptor = null;
            byte[] DataToEncrypt = null;
            byte[] TDESKey = null;

            Trace.Enter("EncryptDecrypt.EncryptString");

            try
            {
                // Step 1. We hash the passphrase using MD5
                // We use the MD5 hash generator as the result is a 128 bit byte array
                // which is a valid length for the TripleDES encoder we use below
                TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

                // Step 3. Setup the encoder
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                // Step 4. Convert the input string to a byte[]
                DataToEncrypt = UTF8.GetBytes(Message);

                // Step 5. Attempt to encrypt the string
                try
                {
                    Encryptor = TDESAlgorithm.CreateEncryptor();
                    Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
                }
                finally
                {
                    // Clear the TripleDes and Hashprovider services of any sensitive information
                    TDESAlgorithm.Clear();
                    HashProvider.Clear();
                }

                // Step 6. Return the encrypted string as a base64 encoded string
                return Convert.ToBase64String(Results);
            }

            catch (Exception ex)
            {
                EZException ezEx =
                    new EZException("EncryptString failed", ex);
                throw ezEx;
            }

            finally
            {
                Trace.Exit("EncryptDecrypt.EncryptString");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Passphrase"></param>
        /// <returns></returns>
        public /*static*/ string DecryptString(string Message, string Passphrase)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            byte[] DataToDecrypt = null;
            ICryptoTransform Decryptor = null;

            Trace.Enter("EncryptDecrypt.DecryptString");

            try
            {
                // Step 1. We hash the passphrase using MD5
                // We use the MD5 hash generator as the result is a 128 bit byte array
                // which is a valid length for the TripleDES encoder we use below
                byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

                // Step 3. Setup the decoder
                TDESAlgorithm.Key = TDESKey;
                TDESAlgorithm.Mode = CipherMode.ECB;
                TDESAlgorithm.Padding = PaddingMode.PKCS7;

                // Step 4. Convert the input string to a byte[]
                DataToDecrypt = Convert.FromBase64String(Message);

                // Step 5. Attempt to decrypt the string
                try
                {
                    Decryptor = TDESAlgorithm.CreateDecryptor();
                    Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
                }
                finally
                {
                    // Clear the TripleDes and Hashprovider services of any sensitive information
                    TDESAlgorithm.Clear();
                    HashProvider.Clear();
                }

                // Step 6. Return the decrypted string in UTF8 format
                return UTF8.GetString(Results);
            }

            catch (Exception ex)
            {
                EZException ezEx =
                    new EZException("DecryptString failed", ex);
                throw ezEx;
            }

            finally
            {
                Trace.Exit("EncryptDecrypt.DecryptString");
            }
        }

    }
}
