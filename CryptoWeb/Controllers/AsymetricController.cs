using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CryptoWeb.Controllers
{
    public class AsymetricController : ApiController
    {
        [HttpPost]
        [Route("~/api/asymetric/decrypt")]
        public string Decrypt([FromBody] dynamic toDecrypt)
        {
            var param = toDecrypt.value.ToString();
            var crypto = new CryptoUtil.Crypto();
            var result = crypto.Decrypt(param);
            return result;
        }

        [HttpPost]
        [Route("~/api/asymetric/encrypt")]
        public string Encrypt([FromBody] dynamic toEncrypt)
        {
            var param = toEncrypt.value.ToString();
         
            var crypto = new CryptoUtil.Crypto();
            var result = crypto.Encrypt(param);
            return result;
        }

    }
}