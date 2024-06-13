using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.mail = mail;
            var mailId = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariId).FirstOrDefault();
            ViewBag.mailId = mailId;
            var toplamSatis = c.SatisHarekets.Where(x => x.CariID == mailId).Count();
            ViewBag.toplamSatis = toplamSatis;
            var toplamTutar = c.SatisHarekets.Where(x => x.CariID == mailId).Sum(y => y.ToplamTutar);
            ViewBag.toplamTutar = toplamTutar;
            var toplamUrunSayisi = c.SatisHarekets.Where(x => x.CariID == mailId).Sum(y => y.Adet);
            ViewBag.toplamUrunSayisi = toplamUrunSayisi;
            var adSoyad = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adSoyad = adSoyad;
            return View(degerler);
        }

        [Authorize]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariId).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.CariID == id).ToList();

            return View(degerler);
        }

        [Authorize]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(x => x.MesajID).ToList();
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelenSayisi = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gidenSayisi = gidenSayisi;
            return View(mesajlar);
        }

        [Authorize]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x => x.MesajID).ToList();
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelenSayisi = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gidenSayisi = gidenSayisi;
            return View(mesajlar);
        }

        [Authorize]
        public ActionResult MesajDetay(int id)
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();
            var mesajlar = c.Mesajlars.Where(x => x.Gonderici == mail).ToList();
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelenSayisi = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gidenSayisi = gidenSayisi;
            return View(degerler);
        }

        [Authorize]
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gelenSayisi = c.Mesajlars.Count(x => x.Alici == mail).ToString();
            ViewBag.gelenSayisi = gelenSayisi;
            var gidenSayisi = c.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.gidenSayisi = gidenSayisi;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar mesajlar)
        {
            var mail = (string)Session["CariMail"];
            mesajlar.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            mesajlar.Gonderici = mail;
            c.Mesajlars.Add(mesajlar);
            c.SaveChanges();
            return RedirectToAction("GidenMesajlar");
        }

        [Authorize]
        public ActionResult KargoTakip(string query)
        {
            var k = from x in c.KargoDetays select x;
            k = k.Where(y => y.TakipKodu.Contains(query));
            return View(k.ToList());
        }

        [Authorize]
        public ActionResult CariKargoTakip(string id)
        {
            var degerler = c.kargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        [HttpGet]
        public PartialViewResult SettingPartial()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariId).FirstOrDefault();
            var cariBul = c.Carilers.Find(id);
            return PartialView("SettingPartial", cariBul);
        }

        [HttpPost]
        public ActionResult SettingPartial(Cariler cariler)
        {
            var cari = c.Carilers.Find(cariler.CariId);
            cari.CariAd = cariler.CariAd;
            cari.CariSoyad = cariler.CariSoyad;
            cari.CariSifre = cariler.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index", "CariPanel");
        }

        public PartialViewResult DuyurularPartial()
        {
            var veriler = c.Mesajlars.Where(x => x.Gonderici == "admin").ToList();
            return PartialView(veriler);
        }
    }
}