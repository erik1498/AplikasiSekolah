using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplikasiSekolah.Models;
using AplikasiSekolah.DBModel;

namespace AplikasiSekolah.Controllers
{
    public class HomeController : Controller
    {
        DBSekolahEntities DBSekolah = new DBSekolahEntities();
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(AuthModel authModel)
        {
            if (ModelState.IsValid)
            {
                var user = DBSekolah.Auths.Where(A => A.username == authModel.username && A.password == authModel.password).FirstOrDefault();
                if (user == null)
                {
                    ModelState.AddModelError("Error", "Username Atau Password Salah !");
                }
                else
                {
                    Session["username"] = user.username;
                    Session["status"] = user.status;
                    Session.Add("Id", user.id);
                    if (user.status == "Admin")
                    {
                        var data = DBSekolah.Admins.Where(D => D.id_auth == user.id).FirstOrDefault();
                        Session["Nama"] = data.Nama;
                    }
                    else if (user.status == "Guru")
                    {
                        var data = DBSekolah.Gurus.Where(D => D.id_auth == user.id).FirstOrDefault();
                        Session["Nama"] = data.Nama;
                    }
                    else if(user.status == "Siswa")
                    {
                        var data = DBSekolah.Siswas.Where(D => D.id_auth == user.id).FirstOrDefault();
                        Session["Nama"] = data.Nama;
                    }
                    return RedirectToAction("Index", "Dashboard"+user.status);
                }
            }
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}