using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace anketHazırlama.ViewModel
{
    public class AnketModel
    {
        public int anketId { get; set; }
        public string anketBaslik { get; set; }
        public string anketSoruları { get; set; }
        public System.DateTime anketTarih { get; set; }
        public int anketKategoriId { get; set; }
        public int anketUyeId { get; set; }
        public int anketOkunma { get; set; }
    }
}