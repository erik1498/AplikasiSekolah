using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AplikasiSekolah.DBModel;
using AplikasiSekolah.Models;


namespace AplikasiSekolah.Controllers
{
    public class DashboardSiswaController : Controller
    {
        // GET: DashboardSiswa
        DBSekolahEntities DBSekolah = new DBSekolahEntities();

        public bool CekSession()
        {
            if (Session.Count > 1)
            {
                if (Session["status"].ToString() == "Siswa")
                {
                    return true;
                }
            }
            return false;
        }
        public ActionResult Index()
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            } 
            var hasil = (from x in DBSekolah.Jadwals
                        join y in DBSekolah.Mapels on x.Mata_Pelajaran equals y.id
                        select new JadwalModel
                        {
                            Hari = x.Hari,
                            Jam_Mulai = x.Jam_Mulai,
                            Jam_Selesai = x.Jam_Selesai,
                            Nama_Mata_Pelajaran = y.Nama
                        }).ToList();
            return View(hasil);
        }
    }
}