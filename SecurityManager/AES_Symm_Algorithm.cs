using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityManager
{
    public class AES_Symm_Algorithm
    {
        /// <summary>
        /// Function that encrypts the plaintext from inFile and stores cipher text to outFile
        /// </summary>
        /// <param name="inFile"> filepath where plaintext is stored </param>
        /// <param name="outFile"> filepath where cipher text is expected to be stored </param>
        /// <param name="secretKey"> symmetric encryption key </param>
        public static byte[] EncryptFile(string inData, string secretKey)
        {

            byte[] forEncryption = Encoding.Unicode.GetBytes(inData);
            byte[] key = Encoding.ASCII.GetBytes(secretKey);
            byte[] encryptedData = null;


            AesCryptoServiceProvider aesCryptoProvider = new AesCryptoServiceProvider
            {

                KeySize = 128,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
                
            };

            
           
                ICryptoTransform aesEncryptTransform = aesCryptoProvider.CreateEncryptor(key,null);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptTransform, CryptoStreamMode.Write))
                    {  
                       cryptoStream.Write(forEncryption, 0, forEncryption.Length);         
                    }

                encryptedData = memoryStream.ToArray();
                }

                return encryptedData;
            
        
        }


        /// <summary>
        /// Function that decrypts the cipher text from inFile and stores as plaintext to outFile
        /// </summary>
        /// <param name="inFile"> filepath where cipher text is stored </param>
        /// <param name="outFile"> filepath where plain text is expected to be stored </param>
        /// <param name="secretKey"> symmetric encryption key </param>
        public static string DecryptFile(byte[] cipherText, string secretKey)
        {


            string plainText = string.Empty;
            byte[] decryptedData = null;
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            AesCryptoServiceProvider aesCryptoProvider = new AesCryptoServiceProvider
            {
                KeySize = 128,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

           
                ICryptoTransform aesDecryptTransform = aesCryptoProvider.CreateDecryptor(key,null);
                using (MemoryStream memoryStream = new MemoryStream(cipherText))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptTransform, CryptoStreamMode.Read))
                    {    
                        decryptedData = new byte[cipherText.Length];
                        cryptoStream.Read(decryptedData, 0, decryptedData.Length);
                    }
                }


            plainText = Encoding.Unicode.GetString(decryptedData);

            return plainText;
            
        }
    }
}
