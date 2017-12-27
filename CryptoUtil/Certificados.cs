using System;

namespace CryptoUtil
{
    public static class Certificados
    {
        public static string RsaPrivateKey()
        {
            return CertificadosRsa.PrivateKey;
        }

        public static string RsaPublicKey()
        {
            return CertificadosRsa.PublicKey;
        }

        
    }
}
