using anketHazırlama.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace anketHazırlama.Models
{
    public class ServisController : ApiController
    {
        DB01Entities1 db = new DB01Entities1();
        Sonuc1Model sonuc = new Sonuc1Model();

        #region Kategori
        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
        {
                kategoriId = x.kategoriId,
                kategoriAdi = x.kategoriAdi,
                
            }).ToList();

            return liste;
        }
        [HttpGet]
        [Route("api/kategoribyid/{kategoriId}")]
        public KategoriModel KategoriById(int kategoriId)
        { KategoriModel kayit = db.Kategori.Where(s => s.kategoriId == kategoriId).Select(x => new KategoriModel()
        {
            kategoriId = x.kategoriId,
            kategoriAdi = x.kategoriAdi,
            
        }).SingleOrDefault();

            return kayit;
        }
        [HttpPost]
        [Route("api/kategoriekle")]
        public Sonuc1Model KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.kategoriAdi == model.kategoriAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Adı Kayıtlıdır!";
                return sonuc;
            }
            Kategori yeni = new Kategori();
            yeni.kategoriAdi = model.kategoriAdi;
            db.Kategori.Add(yeni); db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/kategoriduzenle")]
        public Sonuc1Model KategoriDuzenle(KategoriModel model)
        { Kategori kayit = db.Kategori.Where(s => s.kategoriId == model.kategoriId).FirstOrDefault();
            if (kayit == null)
            { sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc; }
            kayit.kategoriAdi = model.kategoriAdi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/kategorisil/{kategoriId}")]
        public Sonuc1Model KategoriSil(int kategoriId)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == kategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            } if (db.Anket.Count(s => s.anketKategoriId == kategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Soru Kayıtlı Kategori Silinemez!";
                return sonuc;
            } db.Kategori.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";

            return sonuc;
        }
        #endregion
        #region Anket
        [HttpGet]
        [Route("api/anketliste")]
        public List<AnketModel> MakaleListe()
        {
            List<AnketModel> liste = db.Anket.Select(x => new AnketModel()
            {
                anketId = x.anketId,
                anketBaslik = x.anketBaslik,
                anketSoruları = x.anketSoruları, 
                anketKategoriId = x.anketKategoriId,
                anketUyeId = x.anketUyeId,
                anketOkunma = x.anketOkunma,
                anketTarih = x.anketTarih,
                
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/anketlistesoneklenenler/{s}")]
        public List<AnketModel> AnketListeSonEklenenler(int s)
        {
            List<AnketModel> liste = db.Anket.OrderByDescending(o => o.anketId).Take(
           s).Select(x => new AnketModel()
           {
               anketId = x.anketId,
               anketBaslik = x.anketBaslik,
               anketSoruları = x.anketSoruları,
               anketKategoriId = x.anketKategoriId,
               anketUyeId = x.anketUyeId,
               anketOkunma = x.anketOkunma,
               anketTarih = x.anketTarih,
           }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/anketlistebykategoriid/{kategoriId}")]
        public List<AnketModel> AnketListeByKatId(int kategoriId)
        {
            List<AnketModel> liste = db.Anket.Where(s => s.anketKategoriId == kategoriId).Select
           (x => new AnketModel()
           {
               anketId = x.anketId,
               anketBaslik = x.anketBaslik,
               anketSoruları = x.anketSoruları,
               anketKategoriId = x.anketKategoriId,
               anketUyeId = x.anketUyeId,
               anketOkunma = x.anketOkunma,
               anketTarih = x.anketTarih,
           }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/anketlistebyuyeid/{uyeId}")]
        public List<AnketModel> AnketListeByUyeId(int uyeId)
        {
            List<AnketModel> liste = db.Anket.Where(s => s.anketUyeId == uyeId).Select(x =>
           new AnketModel()
           {
               anketId = x.anketId,
               anketBaslik = x.anketBaslik,
               anketSoruları = x.anketSoruları,
               anketKategoriId = x.anketKategoriId,
               anketUyeId = x.anketUyeId,
               anketOkunma = x.anketOkunma,
               anketTarih = x.anketTarih,
           }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/anketbyid/{anketId}")]
        public AnketModel AnketById(int anketId)
        {
            AnketModel kayit = db.Anket.Where(s => s.anketId == anketId).Select(x =>
           new AnketModel()
           {
               anketId = x.anketId,
               anketBaslik = x.anketBaslik,
               anketSoruları = x.anketSoruları,
               anketKategoriId = x.anketKategoriId,
               anketUyeId = x.anketUyeId,
               anketOkunma = x.anketOkunma,
               anketTarih = x.anketTarih,
           }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/anketekle")]
        public Sonuc1Model AnketEkle(AnketModel model)
        {
            if (db.Anket.Count(s => s.anketBaslik == model.anketBaslik) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Anket Başlığı Kayıtlıdır!";
                return sonuc;
            }
            Anket yeni = new Anket();
            yeni.anketBaslik = model.anketBaslik;
            yeni.anketSoruları = model.anketSoruları;
            yeni.anketTarih = model.anketTarih;
            yeni.anketOkunma = model.anketOkunma;
            yeni.anketKategoriId = model.anketKategoriId;
            yeni.anketUyeId = model.anketUyeId;
            db.Anket.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Anket Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/anketduzenle")]
        public Sonuc1Model AnketDuzenle(AnketModel model)
        {
            Anket kayit = db.Anket.Where(s => s.anketId == model.anketId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.anketBaslik = model.anketBaslik;
            kayit.anketSoruları = model.anketSoruları;
            kayit.anketTarih = model.anketTarih;
            kayit.anketOkunma = model.anketOkunma;
            kayit.anketKategoriId = model.anketKategoriId;
            kayit.anketUyeId = model.anketUyeId;           
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Anket Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/anketsil/{anketId}")]
        public Sonuc1Model AnketSil(int anketId)
        {
            Anket kayit = db.Anket.Where(s => s.anketId == anketId).SingleOrDefault(
           );
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            if (db.Anket.Count(s => s.anketId == anketId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Soru/Cevap Kayıtlı Anket Silinemez!";
                return sonuc;
            }
            db.Anket.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Anket Silindi";
            return sonuc;
        }


        #endregion
        #region Üye
        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                adSoyad = x.adSoyad,
                email = x.email,
                kullaniciAdi = x.kullaniciAdi,               
                sifre = x.sifre,
                uyeAdmin = x.uyeAdmin
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(int uyeId)
        {
            UyeModel kayit = db.Uye.Where(s => s.uyeId == uyeId).Select(x => new UyeModel()
 {
                uyeId = x.uyeId,
                adSoyad = x.adSoyad,
                email = x.email,
                kullaniciAdi = x.kullaniciAdi,
                sifre = x.sifre,
                uyeAdmin = x.uyeAdmin
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/uyeekle")]
        public Sonuc1Model UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s => s.kullaniciAdi == model.kullaniciAdi || s.email == model.email) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Adı veya E-Posta Adresi Kayıtlıdır!";
                return sonuc;
            }
            Uye yeni = new Uye();
            yeni.adSoyad = model.adSoyad;
            yeni.email = model.email;
            yeni.kullaniciAdi = model.kullaniciAdi;
            yeni.sifre = model.sifre;
            yeni.uyeAdmin = model.uyeAdmin;
            db.Uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/uyeduzenle")]
        public Sonuc1Model UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == model.uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            kayit.adSoyad = model.adSoyad;
            kayit.email = model.email;
            kayit.kullaniciAdi = model.kullaniciAdi;
            kayit.sifre = model.sifre;
            kayit.uyeAdmin = model.uyeAdmin;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public Sonuc1Model UyeSil(int uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı";
                return sonuc;
            }
            if (db.Anket.Count(s => s.anketUyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Anket Kaydı Olan Üye Silinemez!";
                return sonuc;
            }
            if (db.Sonuc.Count(s => s.sonucUyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Soru/Cevap Kaydı Olan Üye Silinemez!";
                return sonuc;
            }
            db.Uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }
        #endregion
        #region Sonuc
        [HttpGet]
        [Route("api/sonucliste")]
        public List<SonucModel> SonucListe()
        {
            List<SonucModel> liste = db.Sonuc.Select(x => new SonucModel()
            {
                sonucId = x.sonucId,
                sonucIcerik = x.sonucIcerik,
                sonucAnketId = x.sonucAnketId,
                sonucUyeId = x.sonucUyeId,
                sonucTarih = x.sonucTarih,
                
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/sonuclistebyuyeid/{uyeId}")]
        public List<SonucModel> SonucListeByUyeId(int uyeId)
        {
            List<SonucModel> liste = db.Sonuc.Where(s => s.sonucId == uyeId).Select(x => new SonucModel()
            {
                sonucId = x.sonucId,
                sonucIcerik = x.sonucIcerik,
                sonucAnketId = x.sonucAnketId,
                sonucUyeId = x.sonucUyeId,
                sonucTarih = x.sonucTarih,
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/sonuclistebyanketid/{anketId}")]
        public List<SonucModel> SonucListeByanketId(int anketId)
        {
            List<SonucModel> liste = db.Sonuc.Where(s => s.sonucAnketId == anketId).Select(
           x => new SonucModel()
           {
               sonucId = x.sonucId,
               sonucIcerik = x.sonucIcerik,
               sonucAnketId = x.sonucAnketId,
               sonucUyeId = x.sonucUyeId,
               sonucTarih = x.sonucTarih,
           }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/sonuclistesoneklenenler/{s}")]
        public List<SonucModel> SonucListeSonEklenenler(int s)
        {
            List<SonucModel> liste = db.Sonuc.OrderByDescending(o => o.sonucAnketId).Take(s).Select(x => new SonucModel()
           {
                sonucId = x.sonucId,
                sonucIcerik = x.sonucIcerik,
                sonucAnketId = x.sonucAnketId,
                sonucUyeId = x.sonucUyeId,
                sonucTarih = x.sonucTarih,
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/sonucbyid/{sonucId}")]
        public SonucModel SonucById(int sonucId)
        {
            SonucModel kayit = db.Sonuc.Where(s => s.sonucId == sonucId).Select(x => new SonucModel()
            {
                sonucId = x.sonucId,
                sonucIcerik = x.sonucIcerik,
                sonucAnketId = x.sonucAnketId,
                sonucUyeId = x.sonucUyeId,
                sonucTarih = x.sonucTarih,

            }).SingleOrDefault();

            return kayit;
        }
        [HttpPost]
        [Route("api/sonucekle")]
        public Sonuc1Model SonucEkle(SonucModel model)
        {
            if (db.Sonuc.Count(s => s.sonucAnketId == model.sonucUyeId && s.sonucAnketId == model.sonucAnketId && s.sonucIcerik == model.sonucIcerik) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Aynı Kişi, Aynı Soruya Aynı Cevabı Veremez!";
                return sonuc;
            }
            Sonuc yeni = new Sonuc();
            yeni.sonucId = model.sonucId;
            yeni.sonucIcerik = model.sonucIcerik;
            yeni.sonucUyeId = model.sonucUyeId;
            yeni.sonucAnketId = model.sonucAnketId;
            yeni.sonucTarih = model.sonucTarih;
            db.Sonuc.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Cevap Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/sonucduzenle")]
        public Sonuc1Model SonucDuzenle(SonucModel model)
        {
            Sonuc kayit = db.Sonuc.Where(s => s.sonucId == model.sonucId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Cevap Bulunamadı!";
                return sonuc;
            }
            kayit.sonucId = model.sonucId;
            kayit.sonucIcerik = model.sonucIcerik;
            kayit.sonucUyeId = model.sonucUyeId;
            kayit.sonucAnketId = model.sonucAnketId;
            kayit.sonucTarih = model.sonucTarih;

            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Cevap Düzenlendi";

            return sonuc;
        }
        [HttpDelete]
        [Route("api/sonucsil/{sonucId}")]
        public Sonuc1Model SonucSil(int sonucId)
        {
            Sonuc kayit = db.Sonuc.Where(s => s.sonucId == sonucId).SingleOrDefault();

            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";

                return sonuc;
            }

            db.Sonuc.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Yorum Silindi";

            return sonuc;
        }


        #endregion




    }
}
