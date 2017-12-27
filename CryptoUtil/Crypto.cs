using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;

namespace CryptoUtil
{
    public class Crypto
    {
        public string Encrypt(string plainText)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            var textReaderPublicKey = new StringReader(Certificados.RsaPublicKey());
            PemReader pr = new PemReader(textReaderPublicKey);

            RsaKeyParameters keys = (RsaKeyParameters)pr.ReadObject();

            // Pure mathematical RSA implementation
            // RsaEngine eng = new RsaEngine();

            // PKCS1 v1.5 paddings
            Pkcs1Encoding eng = new Pkcs1Encoding(new RsaEngine());

            // PKCS1 OAEP paddings
            //OaepEncoding eng = new OaepEncoding(new RsaEngine());
            eng.Init(true, keys);

            int length = plainTextBytes.Length;
            int blockSize = eng.GetInputBlockSize();
            List<byte> cipherTextBytes = new List<byte>();
            for (int chunkPosition = 0;
                chunkPosition < length;
                chunkPosition += blockSize)
            {
                int chunkSize = Math.Min(blockSize, length - chunkPosition);
                cipherTextBytes.AddRange(eng.ProcessBlock(
                    plainTextBytes, chunkPosition, chunkSize
                ));
            }
            return Convert.ToBase64String(cipherTextBytes.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            var textReaderPrivateKey = new StringReader(Certificados.RsaPrivateKey());
            PemReader pr = new PemReader(textReaderPrivateKey);

            AsymmetricCipherKeyPair keys = (AsymmetricCipherKeyPair)pr.ReadObject();

            // Pure mathematical RSA implementation
            // RsaEngine eng = new RsaEngine();

            // PKCS1 v1.5 paddings
             Pkcs1Encoding eng = new Pkcs1Encoding(new RsaEngine());

            // PKCS1 OAEP paddings
            //OaepEncoding eng = new OaepEncoding(new RsaEngine());
            eng.Init(false, keys.Private);

            int length = cipherTextBytes.Length;
            int blockSize = eng.GetInputBlockSize();
            List<byte> plainTextBytes = new List<byte>();
            for (int chunkPosition = 0;
                chunkPosition < length;
                chunkPosition += blockSize)
            {
                int chunkSize = Math.Min(blockSize, length - chunkPosition);
                plainTextBytes.AddRange(eng.ProcessBlock(
                    cipherTextBytes, chunkPosition, chunkSize
                ));
            }
            return Encoding.UTF8.GetString(plainTextBytes.ToArray());
        }
    }
}
