﻿using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        Context c = new Context();

        // GET: UrunDetay
        public ActionResult Index()
        {
            Class1 cs = new Class1();
            cs.Uruns = c.Uruns.Where(x => x.UrunID == 1).ToList();
            cs.Detays = c.Detays.Where(y => y.DetayID == 1).ToList();
           // var values = c.Uruns.Where(x => x.UrunID == 1).ToList();
            return View(cs);
        }
    }
}