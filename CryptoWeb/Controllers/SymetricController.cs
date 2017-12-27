using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using CryptoUtil;

namespace CryptoWeb.Controllers
{
    public class SymetricController : ApiController
    {
        [HttpPost]
        [Route("~/api/symetric/decrypt")]
        public string Decrypt([FromBody] dynamic toDecrypt)
        {
            //Proceso asimetrico
            var certPrivado = Certificados.RsaPrivateKey();
            var key = toDecrypt.key.ToString();
            var keyBytes = CriptoAsimetrico.RsaDecrypt2Byte(key, certPrivado);


            //Proceso simetrico
            var crypted = toDecrypt.value.ToString();             
            var toDecryptBytes = Convert.FromBase64String(crypted);
            
            var result = CriptoSimetrico.DecryptByteArray(toDecryptBytes, keyBytes);
            return Encoding.UTF8.GetString(result);
        }

        //[HttpPost]
        //[Route("~/api/symetric/encrypt")]
        //public string Encrypt([FromBody] dynamic toEncrypt)
        //{
        //    var param = toEncrypt.value.ToString();
        //    var toEncryptBytes =   Encoding.UTF8.GetBytes(toEncrypt);
   
        //    var result = CriptoSimetrico.EncryptByteArray(param);
        //    return result;
        //}
    }
}