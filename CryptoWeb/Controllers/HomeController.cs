using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CryptoWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Asimetrico()
        { 
            ViewBag.PublicKey = CryptoUtil.Certificados.RsaPublicKey();
            ViewBag.PrivateKey = CryptoUtil.Certificados.RsaPrivateKey();
            return View();
        }



        public ActionResult Simetrico()
        {
            return View();
        }
    }
}