using System;
using System.Collections.Generic;
using System.IO;
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
            if(Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                personel.PersonelGorsel = "/Image/" + dosyaAdi + uzanti;
            }
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
            if (Request.Files.Count > 0)
            {
                string dosyaAdi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaAdi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                personel.PersonelGorsel = "/Image/" + dosyaAdi + uzanti;
            }
            var values = c.Personels.Find(personel.PersonelID);
            values.PersonelAd = personel.PersonelAd;
            values.PersonelSoyad = personel.PersonelSoyad;
            values.PersonelGorsel = personel.PersonelGorsel;
            values.DepartmanID = personel.DepartmanID;
            c.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult PersonelListe()
        {
            var values = c.Personels.ToList();
            return View(values);
        }

    }
}