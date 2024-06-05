using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult LoginRegister()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult LoginRegister(Cariler cariler)
        {
            c.Carilers.Add(cariler);
            c.SaveChanges();
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult LoginCariLogin()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult LoginCariLogin(Cariler cariler)
        {
            var bilgiler = c.Carilers.FirstOrDefault(x=> x.CariMail == cariler.CariMail && x.CariSifre == cariler.CariSifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.CariMail, false);
                Session["CariMail"] = bilgiler.CariMail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public PartialViewResult LoginPersonelLogin()
        {
            return PartialView();
        }

        //[HttpPost]
        //public ActionResult LoginPersonelLogin()
        //{
        //    return PartialView();
        //}
    }
}