using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Layout.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index2()
        {
            return View();
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }

        public ActionResult Iletisim()
        {
            return View();
        }

        public PartialViewResult UrunGetir()
        {
            List<string> urunListesi = new List<string>();
            urunListesi.Add("Kalem");
            urunListesi.Add("Silgi");
            urunListesi.Add("Defter");
            return PartialView("~/Views/Home/_partialUrunListesi.cshtml", urunListesi);
        }
    }
}