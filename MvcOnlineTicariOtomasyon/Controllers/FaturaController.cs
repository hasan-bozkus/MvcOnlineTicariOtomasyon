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
            return RedirectToAction("FaturaDetay", "Fatura");
        }

        public ActionResult Dinamik()
        {
            Class4 cs = new Class4();
            cs.Fatura = c.Faturalars.ToList();
            cs.FaturaKalem = c.FaturaKalems.ToList();
            return View(cs);
        }

        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo, DateTime Tarih, string VergiDairesi, string Saat, string TeslimAlan, string TeslimEden, string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar faturalar = new Faturalar();
            faturalar.FaturaSeriNo = FaturaSeriNo;
            faturalar.FaturaSıraNo = FaturaSıraNo;
            faturalar.Tarih = Tarih;
            faturalar.VergiDairesi = VergiDairesi;
            faturalar.Saat = Saat;
            faturalar.TeslimEden = TeslimEden;
            faturalar.TeslimAlan = TeslimAlan;
            faturalar.Toplam = decimal.Parse(Toplam);
            c.Faturalars.Add(faturalar);
            foreach(var x in kalemler)
            {
                FaturaKalem faturaKalem = new FaturaKalem();
                faturaKalem.Aciklama = x.Aciklama;
                faturaKalem.BirimFiyat = x.BirimFiyat;
                faturaKalem.FaturaID = x.FaturaKalemID;
                faturaKalem.Miktar = x.Miktar;
                faturaKalem.Tutar = x.Tutar;
                c.FaturaKalems.Add(faturaKalem);
            }
            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }
    }
}