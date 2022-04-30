using anketHazırlama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace anketHazırlama.ViewModel
{
    public class SonucModel
    {
        

        public int sonucId { get; set; }
        public string sonucIcerik { get; set; }
        public int sonucUyeId { get; set; }
        public int sonucAnketId { get; set; }
        public System.DateTime sonucTarih { get; set; }

        public virtual Anket Anket { get; set; }
        public virtual Uye Uye { get; set; }
    }
}