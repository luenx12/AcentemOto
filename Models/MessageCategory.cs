namespace AcentemOto.Models
{
    public enum FilterCategory
    {
        SigortaTarihi,   // "Sigorta Tar" sütununa tarih aralığı filtresi
        Durum,           // "Durum" sütununa göre (KESİLDİ, SATILMIŞ vb.)
        Sirket,          // "Şirket" sütununa göre (Mapfre, Quick vb.)
        Turu,            // "Türü" sütununa göre (Trafik, Dask, Kasko vb.)
        TekNumara        // Sadece manuel girilen tek numaraya mesaj at
    }
}
