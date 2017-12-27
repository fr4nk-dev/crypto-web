using System.IO;
using System.Security.Cryptography;

namespace CryptoUtil
{
    public class CriptoSimetrico
    {

        /// <summary>
        /// Encrypt a byte array using AES 128
        /// </summary>
        /// <param name="key">128 bit key</param>
        /// <param name="toDecrypt">byte array that need to be encrypted</param>
        /// <returns>Encrypted array</returns>
        public static byte[] EncryptByteArray(byte[] key, byte[] toDecrypt)
        {
            var iv = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (AesManaged cryptor = new AesManaged())
                {
                    cryptor.Mode = CipherMode.CBC;
                    cryptor.Padding = PaddingMode.PKCS7;
                    cryptor.KeySize = 128;
                    cryptor.BlockSize = 128;


                    using (CryptoStream cs = new CryptoStream(ms, cryptor.CreateEncryptor(key, iv), CryptoStreamMode.Write))
                    {
                        cs.Write(toDecrypt, 0, toDecrypt.Length);
                    }
                    byte[] encryptedContent = ms.ToArray();

                    byte[] result = new byte[iv.Length + encryptedContent.Length];

                    System.Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                    System.Buffer.BlockCopy(encryptedContent, 0, result, iv.Length, encryptedContent.Length);

                    return result;
                }
            }
        }

        /// <summary>
        /// Decrypt a byte array using AES 128
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key">key in bytes</param>
        /// <returns>decrypted bytes</returns>
        public static byte[] DecryptByteArray(byte[] toDecrypt, byte[] key)
        {
            byte[] encryptedContent = new byte[toDecrypt.Length - 16];
            var iv = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            System.Buffer.BlockCopy(toDecrypt, 0, iv, 0, iv.Length);
            System.Buffer.BlockCopy(toDecrypt, iv.Length, encryptedContent, 0, encryptedContent.Length);

            using (MemoryStream ms = new MemoryStream())
            {
                using (AesManaged cryptor = new AesManaged())
                {
                    cryptor.Mode = CipherMode.CBC;
                    cryptor.Padding = PaddingMode.PKCS7;
                    cryptor.KeySize = 128;
                    cryptor.BlockSize = 128;

                    using (CryptoStream cs = new CryptoStream(ms, cryptor.CreateDecryptor(key, iv), CryptoStreamMode.Write))
                    {
                        cs.Write(encryptedContent, 0, encryptedContent.Length);

                    }
                    return ms.ToArray();
                }
            }
        }

    }
}