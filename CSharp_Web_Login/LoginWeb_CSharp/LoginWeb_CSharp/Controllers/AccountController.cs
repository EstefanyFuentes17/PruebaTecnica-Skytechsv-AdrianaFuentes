using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoginWeb_CSharp.Models;
using LoginWeb_CSharp.Data;

namespace LoginWeb_CSharp.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Login(string username, string password)
        {
            try
            {
                UsuarioDAL _usuario = new UsuarioDAL();

                Usuario user = _usuario.ValidarLogin(username, password);

                if (user != null)
                {
                    Session["UserID"] = user.Id;
                    Session["UserName"] = user.NombreCompleto;

                    return RedirectToAction("Index", "Home"); 
                }
                else
                {
                    ViewBag.Error = "Credenciales inválidas.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Account");

        }
    }
}