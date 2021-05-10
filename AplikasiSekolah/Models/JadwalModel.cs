using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AplikasiSekolah.Models
{
    public class JadwalModel
    {
        [Required]
        public int id { get; set; }
        
        [Required(ErrorMessage="Hari harus diisi !")]
        [MinLength(4)]
        public string Hari { get; set; }

        [Required(ErrorMessage="Jam Mulai harus diisi !")]
        [MinLength(5)]
        public string Jam_Mulai { get; set; }

        [Required(ErrorMessage = "Jam Mulai harus diisi !")]
        [MinLength(5)]
        public string Jam_Selesai { get; set; }

        [Required(ErrorMessage = "Mata Pelajaran harus diisi !")]
        public int Mata_Pelajaran { get; set; }

        public string Nama_Mata_Pelajaran { get; set; }
        public string Nama_guru { get; set; }

        [Required(ErrorMessage = "Guru harus diisi !")]
        public int Guru { get; set; }

    }
}