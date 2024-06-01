using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        Context context = new Context();


        // GET: Yapilacak
        public ActionResult Index()
        {
            var toplamCari = context.Carilers.Count().ToString();
            var urunler = context.Uruns.Count().ToString();
            var kategoriler = context.Kategoris.Count().ToString();
            var carilerSehir = (from x in context.Carilers select x.CariSehir).Distinct().Count().ToString();

            ViewBag.toplamCari = toplamCari;
            ViewBag.urunler = urunler;
            ViewBag.kategoriler = kategoriler;
            ViewBag.carilerSehir = carilerSehir;


            var yapilacaklar = context.Yapilacaks.ToList();
            return View(yapilacaklar);
        }
    }
}