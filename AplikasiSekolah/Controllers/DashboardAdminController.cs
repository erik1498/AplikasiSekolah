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
    public class DashboardAdminController : Controller
    {
        DBSekolahEntities DBSekolah = new DBSekolahEntities();
        // GET: Dashboard

        public bool CekSession()
        {
            if (Session.Count > 1)
            {
                if (Session["status"].ToString() == "Admin")
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
            ViewBag.JumlahSiswa = DBSekolah.Siswas.Count();
            ViewBag.JumlahGuru = DBSekolah.Gurus.Count();
            ViewBag.JumlahMapel = DBSekolah.Mapels.Count();
            var hasil = (from a in DBSekolah.Jadwals
                         join b in DBSekolah.Mapels on a.Mata_Pelajaran equals b.id
                         join c in DBSekolah.Gurus on a.Guru equals c.id
                         select new JadwalModel {
                             id = a.id,
                             Hari= a.Hari,
                             Jam_Mulai = a.Jam_Mulai,
                             Jam_Selesai = a.Jam_Selesai,
                             Nama_guru = c.Nama,
                             Nama_Mata_Pelajaran = b.Nama
                         });
            return View(hasil);
        }
        public ActionResult ListSiswa()
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            var hasil = DBSekolah.Siswas.ToList();
            return View(hasil);
        }

        public ActionResult ListGuru()
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            var hasil = DBSekolah.Gurus.ToList();
            return View(hasil);
        }

        public ActionResult ListMapel()
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            var hasil = DBSekolah.Mapels.ToList();
            return View(hasil);
        }

        public ActionResult TambahSiswa()
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            return View();
        }

        public ActionResult TambahGuru()
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            return View();
        }

        public ActionResult TambahMapel()
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            return View();
        }

        public ActionResult TambahJadwal()
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            ViewBag.Mapel = DBSekolah.Mapels.ToList();
            ViewBag.Guru = DBSekolah.Gurus.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult TambahSiswa(SiswaModel SiswaModel)
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            if (ModelState.IsValid)
            {
                Auth Auth = new DBModel.Auth();
                Auth.username = SiswaModel.username;
                Auth.password = SiswaModel.password;
                Auth.status = "Siswa";

                DBSekolah.Auths.Add(Auth);
                DBSekolah.SaveChanges();

                
                Siswa Siswa = new DBModel.Siswa();
                Siswa.id_auth = Auth.id;
                Siswa.Nama = SiswaModel.Nama;

                DBSekolah.Siswas.Add(Siswa);
                DBSekolah.SaveChanges();

                return RedirectToAction("ListSiswa");
            }
            return View();
        }

        [HttpPost]
        public ActionResult TambahGuru(GuruModel GuruModel)
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            if (ModelState.IsValid)
            {
                Auth Auth = new DBModel.Auth();
                Auth.username = GuruModel.username;
                Auth.password = GuruModel.password;
                Auth.status = "Guru";

                DBSekolah.Auths.Add(Auth);
                DBSekolah.SaveChanges();

                Guru Guru = new DBModel.Guru();
                Guru.id_auth = Auth.id;
                Guru.Nama = GuruModel.Nama;

                DBSekolah.Gurus.Add(Guru);
                DBSekolah.SaveChanges();

                return RedirectToAction("ListGuru");
            }
            return View();
        }

        [HttpPost]
        public ActionResult TambahMapel(MapelModel mapelModel)
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            if (ModelState.IsValid)
            {
                Mapel Mapel = new DBModel.Mapel();
                Mapel.Nama = mapelModel.Nama;

                DBSekolah.Mapels.Add(Mapel);
                DBSekolah.SaveChanges();

                return RedirectToAction("ListMapel");
            }
            return View();
        }

        [HttpPost]
        public ActionResult TambahJadwal(JadwalModel jadwalModel)
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            if (ModelState.IsValid)
            {
                Jadwal jadwal = new Jadwal() { 
                    Hari = jadwalModel.Hari,
                    Jam_Mulai = jadwalModel.Jam_Mulai,
                    Jam_Selesai = jadwalModel.Jam_Selesai,
                    Mata_Pelajaran = jadwalModel.Mata_Pelajaran,
                    Guru = jadwalModel.Guru
                };

                DBSekolah.Jadwals.Add(jadwal);
                DBSekolah.SaveChanges();

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult UbahSiswa(int id)
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            var hasil = (from a in DBSekolah.Auths
                         join s in DBSekolah.Siswas on a.id equals s.id_auth where s.id == id
                         select new SiswaModel
                         {
                             id =  a.id,
                             id_auth = a.id,
                             Nama = s.Nama,
                             username=a.username,
                             password=a.password
                         }).FirstOrDefault();
            return View(hasil);
        }

        public ActionResult UbahGuru(int id)
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            var hasil = (from a in DBSekolah.Auths
                         join s in DBSekolah.Gurus on a.id equals s.id_auth
                         where s.id == id
                         select new GuruModel
                         {
                             id = a.id,
                             id_auth = a.id,
                             Nama = s.Nama,
                             username = a.username,
                             password = a.password
                         }).FirstOrDefault();
            return View(hasil);
        }

        public ActionResult UbahMapel(int id)
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            var hasil = DBSekolah.Mapels.Where(m => m.id == id).FirstOrDefault();
            MapelModel mapel = new MapelModel()
            {
                id = hasil.id,
                Nama = hasil.Nama
            };
            return View(mapel);
        }

        public ActionResult UbahJadwal(int id)
        {
            if (!CekSession())
            {
                return RedirectToAction("Error", "Home");
            }
            var hasil = DBSekolah.Jadwals.Where(m => m.id == id).FirstOrDefault();
            JadwalModel jadwal = new JadwalModel()
            {
                id=hasil.id,
                Hari =hasil.Hari,
                Jam_Mulai=hasil.Jam_Mulai,
                Jam_Selesai=hasil.Jam_Selesai,
                Mata_Pelajaran=hasil.Mata_Pelajaran,
                Guru = hasil.Guru
            };
            ViewBag.Mapel = DBSekolah.Mapels.ToList();
            ViewBag.Guru = DBSekolah.Gurus.ToList();
            return View(jadwal);
        }

        [HttpPost]
        public ActionResult UbahSiswa(int id, SiswaModel siswaModel)
        {
            if (ModelState.IsValid)
            {
                Auth auth = new Auth()
                {
                    id = siswaModel.id_auth,
                    username = siswaModel.username,
                    password = siswaModel.password,
                    status = "Siswa"
                };

                Siswa siswa = new Siswa()
                {
                    id = id,
                    id_auth = siswaModel.id_auth,
                    Nama = siswaModel.Nama
                };

                DBSekolah.Entry(auth).State = EntityState.Modified;
                DBSekolah.Entry(siswa).State = EntityState.Modified;
                DBSekolah.SaveChanges();
                return RedirectToAction("ListSiswa", "DashboardAdmin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UbahGuru(int id, GuruModel guruModel)
        {
            if (ModelState.IsValid)
            {
                Auth auth = new Auth()
                {
                    id = guruModel.id_auth,
                    username = guruModel.username,
                    password = guruModel.password,
                    status = "Guru"
                };

                Guru guru = new Guru()
                {
                    id = id,
                    id_auth = guruModel.id_auth,
                    Nama = guruModel.Nama
                };

                DBSekolah.Entry(auth).State = EntityState.Modified;
                DBSekolah.Entry(guru).State = EntityState.Modified;
                DBSekolah.SaveChanges();
                return RedirectToAction("ListGuru");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UbahMapel(int id, MapelModel mapelModel)
        {
            if (ModelState.IsValid)
            {
                Mapel mapel = new Mapel()
                {
                    id = mapelModel.id,
                    Nama = mapelModel.Nama
                };
                DBSekolah.Entry(mapel).State = EntityState.Modified;
                DBSekolah.SaveChanges();
                return RedirectToAction("ListMapel");
            }
            return View();
        }

        [HttpPost]
        public ActionResult UbahJadwal(int id, JadwalModel jadwalModel)
        {
            if (ModelState.IsValid)
            {
                Jadwal jadwal = new Jadwal()
                {
                    id=id,
                    Hari=jadwalModel.Hari,
                    Jam_Mulai=jadwalModel.Jam_Mulai,
                    Jam_Selesai=jadwalModel.Jam_Selesai,
                    Mata_Pelajaran=jadwalModel.Mata_Pelajaran,
                    Guru=jadwalModel.Guru
                };
                DBSekolah.Entry(jadwal).State = EntityState.Modified;
                DBSekolah.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult HapusSiswa(int id)
        {
            Siswa siswa = DBSekolah.Siswas.Where(x => x.id == id).FirstOrDefault();
            Auth auth = DBSekolah.Auths.Where(a => a.id == siswa.id_auth).FirstOrDefault();
            DBSekolah.Siswas.Remove(siswa);
            DBSekolah.Auths.Remove(auth);
            DBSekolah.SaveChanges();
            return RedirectToAction("ListSiswa", "DashboardAdmin");
        }

        public ActionResult HapusGuru(int id)
        {
            Guru guru = DBSekolah.Gurus.Where(x => x.id == id).FirstOrDefault();
            Auth auth = DBSekolah.Auths.Where(a => a.id == guru.id_auth).FirstOrDefault();
            DBSekolah.Gurus.Remove(guru);
            DBSekolah.Auths.Remove(auth);
            DBSekolah.SaveChanges();
            return RedirectToAction("ListGuru", "DashboardAdmin");
        }
        public ActionResult HapusMapel(int id)
        {
            Mapel mapel = DBSekolah.Mapels.Where(x => x.id == id).FirstOrDefault();
            DBSekolah.Mapels.Remove(mapel);
            DBSekolah.SaveChanges();
            return RedirectToAction("ListMapel", "DashboardAdmin");
        }
        public ActionResult HapusJadwal(int id)
        {
            Jadwal jadwal = DBSekolah.Jadwals.Where(x => x.id == id).FirstOrDefault();
            DBSekolah.Jadwals.Remove(jadwal);
            DBSekolah.SaveChanges();
            return RedirectToAction("Index", "DashboardAdmin");
        }
    }
}