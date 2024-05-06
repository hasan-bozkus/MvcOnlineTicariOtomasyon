using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        Context c = new Context();

        // GET: Satis
        public ActionResult Index()
        {
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> urun = (from uruns in c.Uruns.ToList()
                                         select new SelectListItem
                                         {
                                             Text = uruns.UrunAd,
                                             Value = uruns.UrunID.ToString()
                                         }).ToList();

            List<SelectListItem> cari = (from caris in c.Carilers.ToList()
                                         select new SelectListItem
                                         {
                                             Text = caris.CariAd + " " + caris.CariSoyad,
                                             Value = caris.CariId.ToString()
                                         }).ToList();

            List<SelectListItem> personel = (from personels in c.Personels.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = personels.PersonelAd + " " + personels.PersonelSoyad,
                                                 Value = personels.PersonelID.ToString()
                                             }).ToList();
            ViewBag.urun = urun;
            ViewBag.cari = cari;
            ViewBag.personel = personel;
            return View();
        }

        [HttpPost]
        public ActionResult YeniSatis(SatisHareket satisHareket)
        {
            satisHareket.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(satisHareket);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SatisGetir(int id)
        {
            var deger = c.SatisHarekets.Find(id);

            List<SelectListItem> urun = (from uruns in c.Uruns.ToList()
                                         select new SelectListItem
                                         {
                                             Text = uruns.UrunAd,
                                             Value = uruns.UrunID.ToString()
                                         }).ToList();

            List<SelectListItem> cari = (from caris in c.Carilers.ToList()
                                         select new SelectListItem
                                         {
                                             Text = caris.CariAd + " " + caris.CariSoyad,
                                             Value = caris.CariId.ToString()
                                         }).ToList();

            List<SelectListItem> personel = (from personels in c.Personels.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = personels.PersonelAd + " " + personels.PersonelSoyad,
                                                 Value = personels.PersonelID.ToString()
                                             }).ToList();
            ViewBag.urun = urun;
            ViewBag.cari = cari;
            ViewBag.personel = personel;

            return View("SatisGetir", deger);
        }

        [HttpPost]
        public ActionResult SatisGuncelle(SatisHareket satisHareket)
        {
            var values = c.SatisHarekets.Find(satisHareket.SatisID);

            values.CariID = satisHareket.CariID;
            values.UrunID = satisHareket.UrunID;
            values.PersonelID = satisHareket.PersonelID;
            values.Adet = satisHareket.Adet;
            values.Fiyat = satisHareket.Fiyat;
            values.ToplamTutar = satisHareket.ToplamTutar;
            values.Tarih = DateTime.Parse(values.Tarih.ToShortDateString());

            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SatisDetay(int id)
        {
            var values = c.SatisHarekets.Where(x=>x.SatisID == id).ToList();
            return View(values);
        }
    }
}