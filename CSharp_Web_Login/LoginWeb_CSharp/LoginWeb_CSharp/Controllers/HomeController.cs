using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginWeb_CSharp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            Response.Cache.SetNoStore();

            ViewBag.NombreUsuario = Session["UserName"] ?? "Invitado";

            return View();
        }
    }
}