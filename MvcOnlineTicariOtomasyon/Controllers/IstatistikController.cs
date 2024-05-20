using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        Context c = new Context();

        // GET: Istatistik
        public ActionResult Index()
        {
            //kısım 1
            var toplamCari = c.Carilers.Count().ToString();
            ViewBag.toplamCari = toplamCari;

            var urunSayisi = c.Uruns.Count().ToString();
            ViewBag.urunSayisi = urunSayisi;

            var personelSayisi = c.Personels.Count().ToString();
            ViewBag.personelSayisi = personelSayisi;

            var kategoriSayisi = c.Kategoris.Count().ToString();
            ViewBag.kategoriSayisi = kategoriSayisi;

            //kısım 2            
            var toplamStok = c.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.toplamStok = toplamStok;

            var markaSayisi = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.markaSayisi = markaSayisi;

            var kritikSeviye = c.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.kritikSeviye = kritikSeviye;

            var maxFiyatliUrun = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            ViewBag.maxFiyatliUrun = maxFiyatliUrun;

            //kısım 3
            var minFiyatliUrun = (from x in c.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
            ViewBag.minFiyatliUrun = minFiyatliUrun;

            var maxMarka = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.maxMarka = maxMarka;

            var buzdolabiSayisi = c.Uruns.Count(x => x.UrunAd == "Buzdolabı").ToString();
            ViewBag.buzdolabiSayisi = buzdolabiSayisi;

            var laptopSayisi = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.laptopSayisi = laptopSayisi;

            //kısım 4
            var enCokSatan = c.Uruns.Where(u => u.UrunID == (c.SatisHarekets.GroupBy(x => x.UrunID).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.enCokSatan = enCokSatan;

            var kasadakiTutar = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.kasadakiTutar = kasadakiTutar;

            var bugunkuSatislar = c.SatisHarekets.Count(x => x.Tarih == DateTime.Today).ToString();
            ViewBag.bugunkuSatislar = bugunkuSatislar;

            var bugunkuKasa = c.SatisHarekets.Where(x => x.Tarih == DateTime.Today).Sum(y => y.ToplamTutar).ToString();
            ViewBag.bugunkuKasa = bugunkuKasa;


            return View();
        }

        public ActionResult KolayTablolar()
        {
            var query = from x in c.Carilers
                        group x by x.CariSehir into g
                        select new SinifGroup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return View(query.ToList());
        }

        public PartialViewResult Partial1()
        {
            var query = from x in c.Personels
                        group x by x.Departman.DepartmanAd into g
                        select new SinifGroup2
                        {
                            Departman = g.Key,
                            Sayi = g.Count()
                        };
            return PartialView(query);
        }

        public PartialViewResult Partial2()
        {
            var query = c.Carilers.ToList();
            return PartialView(query);
        }

        public PartialViewResult Partial3()
        {
            var query = c.Uruns.ToList();
            return PartialView(query);
        }

        public PartialViewResult Partial4()
        {
            var query = from x in c.Uruns
                        group x by x.Marka into g
                        select new SinifGroup3
                        {
                            Marka = g.Key,
                            Sayi = g.Count()
                        };
            return PartialView(query);
        }
    }
}