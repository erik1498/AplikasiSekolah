using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using AplikasiSekolah.DBModel;

namespace AplikasiSekolah.Models
{
    public class SiswaModel
    {
        [Required]
        public int id { get; set; }

        [Required]
        public int id_auth { get; set; }

        [Required(AllowEmptyStrings=false,ErrorMessage="Nama Tidak Boleh Kosong !")]
        public string Nama { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username Tidak Boleh Kosong !")]
        [MinLength(6)]
        public string username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Tidak Boleh Kosong !")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string password { get; set; }
    }
}