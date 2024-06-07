using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        Context c = new Context();

        // GET: Urun
        public ActionResult Index(string query,int page = 1)
        {
            var urunler = from x in c.Uruns select x;
            if(!string.IsNullOrEmpty(query))
            {
                urunler = urunler.Where(y => y.UrunAd.Contains(query));
            }
            return View(urunler.ToList().ToPagedList(page, 10));
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> kategori = (from kategoris in c.Kategoris.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = kategoris.KategoriAd,
                                                 Value = kategoris.KategoriID.ToString()
                                             }).ToList();

            ViewBag.kategori = kategori;
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Urun urun)
        {
            urun.Durum = true;

            c.Uruns.Add(urun);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        public enum Durumlar
        {
            Aktif = 1,
            Pasif = 0
        }

        [HttpGet]
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> kategori = (from kategoris in c.Kategoris.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = kategoris.KategoriAd,
                                                 Value = kategoris.KategoriID.ToString()
                                             }).ToList();

            List<SelectListItem> list = (from x in Getdurum()
                                         select new SelectListItem
                                         {
                                             Text = x,
                                             Value = x
                                         }).ToList();

            ViewBag.StatusList = list;
                
            ViewBag.kategori = kategori;
            var urungetir = c.Uruns.Find(id);
            return View("UrunGetir", urungetir);
        }

        public List<string> Getdurum()
        {
            String[] CitiesArray = new String[] { "false", "true" };
            return new List<string>(CitiesArray);
        }

        [HttpPost]
        public ActionResult UrunGuncelle(Urun urun)
        {
            var values = c.Uruns.Find(urun.UrunID);

            values.Durum = urun.Durum;
            values.AlisFiyat = urun.AlisFiyat;
            values.SatisFiyat = urun.SatisFiyat;
            values.KategoriID = urun.KategoriID;
            values.Marka = urun.Marka;
            values.Stok = urun.Stok;
            values.UrunAd = urun.UrunAd;
            values.UrunGorsel = urun.UrunGorsel;


            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunListesi()
        {
            var values = c.Uruns.ToList();
            return View(values);
        }
    }
}