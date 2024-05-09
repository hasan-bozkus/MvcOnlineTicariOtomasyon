using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura

        Context c = new Context();

        public ActionResult Index()
        {
            var values = c.Faturalars.ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult FaturaEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FaturaEkle(Faturalar faturalar)
        {
            faturalar.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            faturalar.Saat = DateTime.Now.ToString("HH") + ":" + DateTime.UtcNow.Minute;
            c.Faturalars.Add(faturalar);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaGetir(int id)
        {
            var values = c.Faturalars.Find(id);
            return View(values);
        }

        [HttpPost]
        public ActionResult FaturaGuncelle(Faturalar faturalar)
        {
            var values = c.Faturalars.Find(faturalar.FaturaID);
            values.FaturaSeriNo = faturalar.FaturaSeriNo;
            values.FaturaSıraNo = faturalar.FaturaSıraNo;
            values.VergiDairesi = faturalar.VergiDairesi;
            values.TeslimAlan = faturalar.TeslimAlan;
            values.TeslimEden = faturalar.TeslimEden;
            values.Saat = values.Saat.ToString();
            values.Tarih = DateTime.Parse(values.Tarih.ToShortDateString());
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaDetay(int id)
        {
            var degerler = c.FaturaKalems.Where(x => x.FaturaID == id).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniKalem()
        {
            return View();
        }

        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem faturaKalem)
        {
            c.FaturaKalems.Add(faturaKalem);
            c.SaveChanges();
            return View();
        }
    }
}