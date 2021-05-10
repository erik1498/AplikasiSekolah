using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplikasiSekolah.Models;
using AplikasiSekolah.DBModel;
using System.Data.Entity;

namespace AplikasiSekolah.Controllers
{
    public class DashboardGuruController : Controller
    {
        DBSekolahEntities DBSekolah = new DBSekolahEntities();
        public bool CekSession()
        {
            if (Session.Count > 1)
            {
                if (Session["status"].ToString() == "Guru")
                {
                    return true;
                }
            }
            return false;
        }
        // GET: DashboardGuru
        public ActionResult Index()
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            int id = (int)Session["id"];
            var guru = DBSekolah.Gurus.Where(x => x.id_auth == id).FirstOrDefault();
            var hasil = (from x in DBSekolah.Jadwals
                         join y in DBSekolah.Mapels on x.Mata_Pelajaran equals y.id
                         where x.Guru == guru.id
                         select new JadwalModel
                         {
                             Hari=x.Hari,
                             Jam_Mulai=x.Jam_Mulai,
                             Jam_Selesai=x.Jam_Selesai,
                             Nama_Mata_Pelajaran=y.Nama
                         }).ToList();
            return View(hasil);
        }
    }
}