using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplikasiSekolah.Models
{
    public class MapelModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string Nama { get; set; }
    }
}