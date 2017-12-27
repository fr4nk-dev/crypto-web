using System;
using System.Text;
using CryptoUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CryptoTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void DecryptAsymmeticRsa()
        {
             var privateKeyPem = Certificados.RsaPrivateKey();
            var toDecrypt = CertificadosTestRsa.KeyElement;

            var decryptedByte = CriptoAsimetrico.RsaDecrypt2Byte(toDecrypt, privateKeyPem);

        }


        [TestMethod]
        public void DecryptSymmetrical()
        {
             var privateKeyPem = Certificados.RsaPrivateKey();
            var toDecrypt = CertificadosTestRsa.KeyElement;

            var decryptedByte = CriptoAsimetrico.RsaDecrypt2Byte(toDecrypt, privateKeyPem);


            var archivoByte = Convert.FromBase64String(CertificadosTestRsa.FileElement);
          
            var decryptedSimmetrical = CriptoSimetrico.DecryptByteArray(archivoByte, decryptedByte);

            var decripted = Encoding.UTF8.GetString(decryptedSimmetrical);
        }
    }
}
