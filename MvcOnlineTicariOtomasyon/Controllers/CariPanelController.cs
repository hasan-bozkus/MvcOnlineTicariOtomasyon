﻿using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Carilers.FirstOrDefault(x => x.CariMail == mail);
            ViewBag.mail = mail;
            return View(degerler);
        }

        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.CariId).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.CariID == id).ToList();

            return View(degerler);
        }

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


    }
}