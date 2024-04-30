using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        Context c = new Context();

        // GET: Cari
        public ActionResult Index()
        {
            var degerler = c.Carilers.Where(x=> x.Durum == true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariEkle(Cariler cariler)
        {
            cariler.Durum = true;
            c.Carilers.Add(cariler);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariSil(int id)
        {
            var cari = c.Carilers.Find(id);
            cari.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariGetir(int id)
        {
            var cari = c.Carilers.Find(id);
            return View("CariGetir", cari);
        }

        public ActionResult CariGuncelle(Cariler cariler)
        {
            if(!ModelState.IsValid)
            {
                return View("CariGetir");
            }
            var cari = c.Carilers.Find(cariler.CariId);
            cari.CariAd = cariler.CariAd;
            cari.CariSoyad = cariler.CariSoyad;
            cari.CariSehir = cariler.CariSehir;
            cari.CariMail = cariler.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.CariID == id).ToList();
            var cari = c.Carilers.Where(x => x.CariId == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cari;
            return View(degerler);
        }
    }
}