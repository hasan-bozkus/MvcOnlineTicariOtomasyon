using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        Context c = new Context();

        // GET: Departman
        public ActionResult Index()
        {
            var degerler = c.Departmans.Where(x=> x.Durum == true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DepartmanEkle(Departman departman)
        {
            departman.Durum = true;

            c.Departmans.Add(departman);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanSil(int id)
        {
            var dep = c.Departmans.Find(id);
            dep.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult DepartmanGetir(int id)
        {
            var dep = c.Departmans.Find(id);
            return View("DepartmanGetir", dep);
        }

        public ActionResult DepartmanGuncelle(Departman departman)
        {
            var dep = c.Departmans.Find(departman.DepartmanID);
            dep.DepartmanAd = departman.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DepartmanDetay(int id)
        {
            var degerler = c.Personels.Where(x => x.DepartmanID == id).ToList();
            var departman = c.Departmans.Where(x => x.DepartmanID == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.departman = departman;
            return View(degerler);
        }

        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.PersonelID == id).ToList();
            var departmanPersonel = c.Personels.Where(x => x.PersonelID == id).Select(y => y.PersonelAd + " " + y.PersonelSoyad).FirstOrDefault();
            ViewBag.departmanPersonel = departmanPersonel;
            return View(degerler); 
        }
    }
}