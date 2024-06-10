using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        Context c = new Context();

        // GET: Kargo
        public ActionResult Index(string query)
        {
            var kargoDetaylar = from x in c.KargoDetays select x;
            if (!string.IsNullOrEmpty(query))
            {
                kargoDetaylar = kargoDetaylar.Where(y => y.TakipKodu.Contains(query));
            }          
            return View(kargoDetaylar.ToList());
        }

        [HttpGet]
        public ActionResult YeniKargo()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F", "G", "H", "K" };
            int k1, k2, k3;
            k1 = rnd.Next(0, karakterler.Length);
            k2 = rnd.Next(0, karakterler.Length);
            k3 = rnd.Next(0, karakterler.Length);
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000); //10 --> 3 1 2 1 2 1
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);
            string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            ViewBag.takipKod = kod;

            return View();
        }

        [HttpPost]
        public ActionResult YeniKargo(KargoDetay kargoDetay)
        {
            c.KargoDetays.Add(kargoDetay);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KargoDetay(string id)
        {
            //query = "393C78D87B";
            var degerler = c.kargoTakips.Where(x => x.TakipKodu == id).ToList();
           
            return View(degerler);
        }
    }
}