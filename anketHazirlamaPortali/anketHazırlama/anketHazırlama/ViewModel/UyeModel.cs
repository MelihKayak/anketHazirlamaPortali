using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace anketHazırlama.ViewModel
{
    public class UyeModel
    {
        public int uyeId { get; set; }
        public string kullaniciAdi { get; set; }
        public string email { get; set; }
        public string sifre { get; set; }
        public string adSoyad { get; set; }
        public int uyeAdmin { get; set; }
    }
}