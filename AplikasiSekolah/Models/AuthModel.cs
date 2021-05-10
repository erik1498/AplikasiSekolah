using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AplikasiSekolah.Models
{
    public class AuthModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage="Username Harus Diisi.")]
        public string username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Harus Diisi.")]
        [DataType(DataType.Password)]
        public string password { get; set; }

    }
}