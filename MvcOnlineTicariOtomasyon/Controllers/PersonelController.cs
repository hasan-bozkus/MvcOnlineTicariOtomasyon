using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        Context c = new Context();


        // GET: Personel
        public ActionResult Index()
        {
            var degerler = c.Personels.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> departman = (from departmans in c.Departmans.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = departmans.DepartmanAd,
                                                  Value = departmans.DepartmanID.ToString()
                                              }).ToList();
            ViewBag.departman = departman;
            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(Personel personel)
        {
            c.Personels.Add(personel);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            var personel = c.Personels.Find(id);


            List<SelectListItem> departman = (from departmans in c.Departmans.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = departmans.DepartmanAd,
                                                  Value = departmans.DepartmanID.ToString()
                                              }).ToList();
            ViewBag.departman = departman;
            return View("PersonelGetir", personel);
        }

        [HttpPost]
        public ActionResult PersonelGuncelle(Personel personel)
        {
            var values = c.Personels.Find(personel.PersonelID);
            values.PersonelAd = personel.PersonelAd;
            values.PersonelSoyad = personel.PersonelSoyad;
            values.PersonelGorsel = personel.PersonelGorsel;
            values.DepartmanID = personel.DepartmanID;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}